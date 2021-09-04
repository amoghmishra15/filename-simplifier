using System.Text.RegularExpressions;

// NOTE: all of the following finctions are 'call by reference' to increase performance
namespace simplify {

    // Order insensitive operations
    partial class Simplify {
        // Replace sequence/character with whitespace
        public static void RemoveSequence(ref string filename, string sequence, bool isActive) {
            if(isActive) {
                filename = filename.Replace(sequence, " ");
            }
        }

        // Remove parentheses + text: `abc (def)` -> `abc  `
        public static void RemoveCurvedBracket(ref string filename) {
            if(Preferences.removeCurvedBracket) {
                filename = Regex.Replace(filename, @" ?\(.*?\)", " ");
            }
        }

        // Remove square brackets + text: `abc [def]` -> `abc  `
        public static void RemoveSquareBracket(ref string filename) {
            if(Preferences.removeSquareBracket) {
                filename = Regex.Replace(filename, @" ?\[.*?\]", " ");
            }
        }

    }

    // Order sensitive functions
    partial class Simplify {
        // Remove 2+ and trailing whitespace: ` abc    def ` -> `abc def`
        public static void ReduceWhitespace(ref string filename) {
            filename = Regex.Replace(filename, @"\s+", " ").Trim(' ');
        }

        // Article formatting (a, an, the, etc.)
        public static void OptimizeArticles(ref string filename) {
            if(Preferences.optimizeArticles) {
                string[] splitFilename = filename.Split(' ');
                string[] articles = { "a", "an", "the", "of", "and", "in", "into", "onto", "from" };

                for(int i = 1; i < splitFilename.Length; i++) {
                    foreach(string article in articles) {
                        if(splitFilename[i].ToLower() == article)
                            splitFilename[i] = article;
                    }
                }

                filename = string.Join(' ', splitFilename);
            }
        }

        // CLI friendly conversion: `abc def` -> `abc-def`
        public static void ConvertToCliFriendly(ref string filename) {
            if(Preferences.isCliFriendly) {
                filename = filename.Replace(" ", Preferences.cliSeparator);
            }
        }
    }
}
