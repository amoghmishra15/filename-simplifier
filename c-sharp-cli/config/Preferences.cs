
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
        public string LibraryPath { get; set; }
        public bool GetAllDirectories { get; set; }
        public string Extensions { get; set; }
    }

    // User settings
    public static string libraryPath = @"C:\Zeta\Testing\A";
    public static bool getAllDirectories = true;
    public static string extensions = "mkv";

    // Common separators
    public static bool removeDot = true;
    public static bool removeDash = true;
    public static bool removeUnderscore = true;

    // Common metadata containers
    public static bool removeCurvedBracket = true;
    public static bool removeSquareBracket = true;

    // CLI friendly conversion controls
    public static bool isCliFriendly = false;
    public static string cliSeparator = "-";

    // Optimization preferences
    public static bool sentenceCase = false;
    public static bool smartCapitalization = true;
    public static bool optimizeArticles = true;
    public static bool removeNonAscii = true;
    public static bool convertToLowercase = false;
}

