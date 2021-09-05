namespace simplify {
    class Preferences {
        // User settings
        public static string libraryPath = @"C:\Users\GR\Desktop\GR\Testing";
        public static bool getAllDirectories = true;
        public static string extensions = "mp4, mkv";

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
    }
}
