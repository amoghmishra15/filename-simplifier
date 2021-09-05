
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

    public class JsonConfig {
        // Crawl settings
        public string LibraryPath { get; set; } = "ERR: null libraryPath in JSON";
        public bool GetAllDirectories { get; set; }
        public string Extensions { get; set; } = "ERR: null extension string in JSON";

        // Separators
        public bool RemoveDot { get; set; }
        public bool RemoveDash { get; set; }
        public bool RemoveUnderscore { get; set; }

        // Metadata containers
        public bool RemoveCurvedBracket { get; set; }
        public bool RemoveSquareBracket { get; set; }

        // CLI friendly conversion settings
        public bool IsCliFriendly { get; set; }
        public string CliSeparator { get; set; } = "-";

        // Optimizations
        public bool SentenceCase { get; set; }
        public bool SmartCapitalization { get; set; }
        public bool OptimizeArticles { get; set; }
        public bool RemoveNonAscii { get; set; }
        public bool ConvertToLowercase { get; set; }
    }
}

