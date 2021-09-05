
using Newtonsoft.Json;

namespace simplify;
class Preferences {
    // Load preferences from 'config.yaml'
    public static JsonConfig LoadConfig() {
        // Get location of 'config.json'
        string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
        exeDirectory = exeDirectory.Remove(exeDirectory.Length - 1); // remove appended '\'
        string configPath = $"{exeDirectory}/config/config.json";

        // Not found check
        if(!File.Exists(configPath)) {
            Console.WriteLine("Config file not found, please re-install app.");
            Environment.Exit(1);
        }

        // Deserialize
        return JsonConvert.DeserializeObject<JsonConfig>(File.ReadAllText(configPath))!;
    }

    public record JsonConfig {
        // Crawl settings
        public string LibraryPath { get; init; } = "ERR: null libraryPath in JSON";
        public bool GetAllDirectories { get; init; }
        public string Extensions { get; init; } = "ERR: null extension string in JSON";

        // Separators
        public bool RemoveDot { get; init; }
        public bool RemoveDash { get; init; }
        public bool RemoveUnderscore { get; init; }

        // Metadata containers
        public bool RemoveCurvedBracket { get; init; }
        public bool RemoveSquareBracket { get; init; }

        // CLI friendly conversion settings
        public bool IsCliFriendly { get; init; }
        public string CliSeparator { get; init; } = "-";

        // Optimizations
        public bool SentenceCase { get; init; }
        public bool SmartCapitalization { get; init; }
        public bool OptimizeArticles { get; init; }
        public bool RemoveNonAscii { get; init; }
        public bool ConvertToLowercase { get; init; }
    }
}

