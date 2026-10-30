using System;
using System.IO;
using System.Text.Json;
using Laast.Models;
using Microsoft.Extensions.Logging;

namespace Laast.Services;

public class AppConfigService : IAppConfigService
{
    public AppConfiguration Configuration { get; private set; }
    private readonly ILogger _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public AppConfigService(ILogger logger)
    {
        _logger = logger;
        Configuration = new AppConfiguration();
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
    }

    public void InitConfig()
    {
        try
        {
            if (File.Exists(Constants.ConfigFilePath))
            {
                var json = File.ReadAllText(Constants.ConfigFilePath);
                Configuration = JsonSerializer.Deserialize<AppConfiguration>(json, _jsonOptions) ??
                                throw new InvalidOperationException("Config file is corrupted!");
            }
            else
            {
                SaveConfig();
            }
        }
        catch (Exception ex)
        {
            SaveConfig();
            _logger.LogError("Error during ConfigurationService Init: {Message}", ex.Message);
        }

        ValidateSettingsConfig();
        SetDebugLevel();
    }

    public void SaveConfig()
    {
        var json = JsonSerializer.Serialize(Configuration, _jsonOptions);
        File.WriteAllText(Constants.ConfigFilePath, json);
    }

    private void SetDebugLevel()
    {
    }

    private void ValidateSettingsConfig()
    {
    }
}