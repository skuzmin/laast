using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Laast.Services;

public class AppLogger : ILogger
{
    private static readonly Lock Lock = new();
    private static readonly AsyncLocal<Stack<object>> CurrentScopes = new();
    private LogLevel _minimalLogLevel = LogLevel.Trace;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string>? formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        string message;
        if (formatter != null)
        {
            message = formatter(state, exception);
        }
        else
        {
            message = state?.ToString() ?? string.Empty;
        }

        var logBuilder = new StringBuilder();
        logBuilder.Append($"[{logLevel}] ".PadRight(16));
        var scopes = CurrentScopes.Value;
        if (scopes?.Count > 0)
        {
            var scopePath = string.Join(" => ", scopes.Reverse());
            logBuilder.Append($"{scopePath} | ");
        }

        logBuilder.Append($"DateTime: {DateTime.Now:yyyy-MM-dd HH:mm:ss} | ");
        logBuilder.Append($"Message: {message} ");
        lock (Lock)
        {
            File.AppendAllText(Constants.LogFilePath, logBuilder + Environment.NewLine);
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _minimalLogLevel;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        CurrentScopes.Value ??= new Stack<object>();
        CurrentScopes.Value.Push(state);
        return new LogScope();
    }

    public void SetDebugLevel(string debugLevel)
    {
        _minimalLogLevel = debugLevel == Constants.DebugLevel.Detailed
            ? LogLevel.Trace
            : LogLevel.Warning;
    }

    private class LogScope : IDisposable
    {
        public void Dispose()
        {
            if (CurrentScopes.Value?.Count > 0)
            {
                CurrentScopes.Value.Pop();
            }
        }
    }
}