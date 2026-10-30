using System;
using System.IO;

namespace Laast;

public static class Constants
{
    public static readonly string AppDataPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LAAST");

    public static string ConfigFilePath => Path.Combine(AppDataPath, "laast.json");
    public static string LogFilePath => Path.Combine(AppDataPath, "laast.log");

    public static class DebugLevel
    {
        public const string Default = "Default";
        public const string Detailed = "Detailed";
    }
}