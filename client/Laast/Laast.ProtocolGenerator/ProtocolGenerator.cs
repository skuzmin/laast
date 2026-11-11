
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Laast.ProtocolGenerator;

[Generator]
public class ProtocolGenerator : IIncrementalGenerator
{
    private static readonly Regex DefineRegex = new(
        @"^[ \t]*#define[ \t]+(?<name>[A-Za-z_][A-Za-z0-9_]*)[ \t]+""(?<value>[^""]*)""",
        RegexOptions.Multiline | RegexOptions.Compiled);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var protocolFiles = context.AdditionalTextsProvider
            .Where(file => file.Path.EndsWith(
                "protocol.h",
                StringComparison.OrdinalIgnoreCase))
            .Select((file, cancellationToken) =>
                file.GetText(cancellationToken)?.ToString() ?? string.Empty);

        context.RegisterSourceOutput(protocolFiles, (productionContext, source) =>
        {
            var commands = new List<(string Name, string Value)>();
            var mouseButtons = new List<(string Name, string Value)>();
            var keyboardKeys = new List<string>();

            foreach (Match match in DefineRegex.Matches(source))
            {
                var name = match.Groups["name"].Value;
                var value = match.Groups["value"].Value;

                if (name == "PROTOCOL_VERSION" ||
                    name.StartsWith("CMD_", StringComparison.Ordinal) ||
                    name.StartsWith("ARG_", StringComparison.Ordinal))
                {
                    commands.Add((name, value));
                }
                else if (name.StartsWith("BTN_", StringComparison.Ordinal))
                {
                    mouseButtons.Add((name, value));
                }
                else if (name.StartsWith("KEY_", StringComparison.Ordinal))
                {
                    keyboardKeys.Add(value);
                }
            }

            // Add digits 0-9.
            keyboardKeys.AddRange(
                Enumerable.Range(0, 10).Select(i => i.ToString()));

            // Add letters A-Z.
            keyboardKeys.AddRange(
                Enumerable.Range('A', 26).Select(c => ((char)c).ToString()));

            var result = new StringBuilder();

            result.AppendLine("namespace Laast.ProtocolGenerator.Generated;");
            result.AppendLine();
            result.AppendLine("public static class LaastProtocol");
            result.AppendLine("{");

            // Commands
            AppendConstants(result, "Commands", commands);

            // Mouse buttons
            AppendConstants(result, "MouseButtons", mouseButtons);

            // Keyboard keys
            result.AppendLine(
                "    public static readonly global::System.Collections.Generic.IReadOnlyList<string> KeyboardKeys =");
            result.AppendLine(
                "        global::System.Array.AsReadOnly<string>(new string[]");
            result.AppendLine("        {");

            foreach (var key in keyboardKeys)
            {
                result.AppendLine(
                    $"            {SymbolDisplay.FormatLiteral(key, quote: true)},");
            }

            result.AppendLine("        });");
            result.AppendLine("}");

            productionContext.AddSource(
                "LaastProtocol.g.cs",
                SourceText.From(result.ToString(), Encoding.UTF8));
        });
    }

    private static void AppendConstants(
        StringBuilder result,
        string className,
        IEnumerable<(string Name, string Value)> constants)
    {
        result.AppendLine($"    public static class {className}");
        result.AppendLine("    {");

        foreach (var (name, value) in constants)
        {
            result.AppendLine(
                $"        public const string {name} = {SymbolDisplay.FormatLiteral(value, quote: true)};");
        }

        result.AppendLine("    }");
        result.AppendLine();
    }
}
