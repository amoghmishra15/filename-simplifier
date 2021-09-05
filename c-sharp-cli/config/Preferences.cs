
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
        public bool GetAllDirectories { get; set; } = true;
        public string Extensions { get; set; } = "ERR: null extension string in JSON";

        // Separators
        public bool RemoveDot { get; set; } = true;
        public bool RemoveDash { get; set; } = true;
        public bool RemoveUnderscore { get; set; } = true;

        // Metadata containers
        public bool RemoveCurvedBracket { get; set; } = true;
        public bool RemoveSquareBracket { get; set; } = true;

        // CLI friendly conversion settings
        public bool IsCliFriendly { get; set; } = true;
        public string CliSeparator { get; set; } = "-";

        // Optimizations
        public bool SentenceCase { get; set; } = false;
        public bool SmartCapitalization { get; set; } = true;
        public bool OptimizeArticles { get; set; } = true;
        public bool RemoveNonAscii { get; set; } = true;
        public bool ConvertToLowercase { get; set; } = false;
    }
}

