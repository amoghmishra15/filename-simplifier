using System.Text.RegularExpressions;
namespace simplify;

// NOTE: all of the following finctions are 'call by reference' to increase performance
// Order insensitive operations
static partial class Simplify {
    // Replace sequence/character with whitespace
    public static void RemoveSequence(ref string filename, string sequence, bool isActive) {
        if(isActive) {
            filename = filename.Replace(sequence, " ");
        }
    }

    // Convert to Lower Case
    public static void ConvertToLowercase(ref string filename, Preferences.JsonConfig prefs) {
        if(prefs.ConvertToLowercase) {
            filename = filename.ToLowerInvariant();
        }
    }

    // Remove non-ASCII characters

    public static void RemoveNonASCII(ref string filename, Preferences.JsonConfig prefs) {
        if(prefs.RemoveNonAscii) {
            filename = Regex.Replace(filename, @"[^\u0000-\u007F]+", string.Empty);
        }
    }

    // Remove parentheses + text: `abc (def)` -> `abc  `
    public static void RemoveCurvedBracket(ref string filename, Preferences.JsonConfig prefs) {
        if(prefs.RemoveCurvedBracket) {
            filename = Regex.Replace(filename, @" ?\(.*?\)", " ");
        }
    }

    // Remove square brackets + text: `abc [def]` -> `abc  `
    public static void RemoveSquareBracket(ref string filename, Preferences.JsonConfig prefs) {
        if(prefs.RemoveSquareBracket) {
            filename = Regex.Replace(filename, @" ?\[.*?\]", " ");
        }
    }

}

// Order sensitive functions
static partial class Simplify {
    // Remove 2+ and trailing whitespace: ` abc    def ` -> `abc def`
    public static void ReduceWhitespace(ref string filename) {
        filename = Regex.Replace(filename, @"\s+", " ").Trim(' ');
    }

    // Smart Capitalization `abc aBc` -> `Abc  aBc` || Sentence Case `abc aBc` -> `Abc  ABc`
    public static void ConvertToSentenceCase(ref string filename, Preferences.JsonConfig prefs) {
        if(prefs.SmartCapitalization || prefs.SentenceCase) {
            string[] splitFilename = filename.Split(' ');

            for(int i = 0; i < splitFilename.Length; i++) {
                // Check to preserve words like 'USA', 'reZero'
                if(prefs.SmartCapitalization && Regex.IsMatch(splitFilename[i], "[A-Z]")) { continue; }
                splitFilename[i] = Process.FirstCharToUppercase(splitFilename[i]);
            }

            filename = string.Join(' ', splitFilename);
        }
    }


    // Article formatting (a, an, the, etc.)
    public static void OptimizeArticles(ref string filename, Preferences.JsonConfig prefs) {
        if(prefs.OptimizeArticles) {
            string[] splitFilename = filename.Split(' ');
            string[] articles = { "a", "an", "the", "of", "and", "in", "into", "onto", "from" };

            for(int i = 1; i < splitFilename.Length; i++) {
                foreach(string article in articles) {
                    if(splitFilename[i].ToLowerInvariant() == article) { splitFilename[i] = article; }
                }
            }

            filename = string.Join(' ', splitFilename);
        }
    }

    // CLI friendly conversion: `abc def` -> `abc-def`
    public static void ConvertToCliFriendly(ref string filename, Preferences.JsonConfig prefs) {
        if(prefs.IsCliFriendly) {
            filename = filename.Replace(" ", prefs.CliSeparator);
        }
    }
}
