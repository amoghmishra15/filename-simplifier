using System.Text.RegularExpressions;

namespace simplify {

    // Order insensitive operations
    partial class Simplify {
        // Replace sequence/character with whitespace
        public static string RemoveSequence(string filename, string sequence, bool isActive) {
            return isActive ?
                filename.Replace(sequence, " ") :
                filename;
        }

        // Remove parentheses + text: `abc (def)` -> `abc  `
        public static string RemoveCurvedBracket(string filename) {
            return Preferences.removeCurvedBracket ?
                Regex.Replace(filename, @" ?\(.*?\)", " ") :
                filename;
        }

        // Remove square brackets + text: `abc [def]` -> `abc  `
        public static string RemoveSquareBracket(string filename) {
            return Preferences.removeSquareBracket ?
                Regex.Replace(filename, @" ?\[.*?\]", " ") :
                filename;
        }

    }

    // Order sensitive functions
    partial class Simplify {
        // Remove 2+ and trailing whitespace: ` abc    def ` -> `abc def`
        public static string ReduceWhitespace(string filename) {
            filename = Regex.Replace(filename, @"\s+", " "); // 2+
            return filename.Trim(' '); // trailing
        }

        // Article formatting (a, an, the, etc.)
        public static string OptimizeArticles(string filename) {
            if(!Preferences.optimizeArticles) { return filename; }

            string[] splitFilename = filename.Split(' ');
            string[] articles = { "a", "an", "the", "of", "and", "in", "into", "onto", "from" };

            for(int i = 1; i < splitFilename.Length; i++) {
                foreach(string word in articles) {
                    if(splitFilename[i].ToLower() == word)
                        splitFilename[i] = word;
                }
            }
            return string.Join(' ', splitFilename);
        }

        // CLI friendly conversion: `abc def` -> `abc-def`
        public static string CliFriendlyConvert(string filename, string cliSeparator) {
            return Preferences.cliFriendly ?
                filename.Replace(" ", cliSeparator) :
                filename;
        }
    }
}
