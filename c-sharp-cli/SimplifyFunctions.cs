using System.Text.RegularExpressions;

namespace simplify {

    // Order insensitive operations
    partial class Simplify {
        // Remove underscore: `abc_def` -> `abc def`
        public static string RemoveUnderscore(string filename, bool isActive) {
            return isActive ? filename.Replace("_", " ") : filename;
        }

        // Remove dash: `abc-def` -> `abc def`
        public static string RemoveDash(string filename, bool isActive) {
            return isActive ? filename.Replace("-", " ") : filename;
        }

        // Remove dot: `abc-def` -> `abc def`
        public static string RemoveDot(string filename, bool isActive) {
            return isActive ? filename.Replace(".", " ") : filename;
        }

        // Remove parentheses + text: `abc (def)` -> `abc  `
        public static string RemoveCurvedBracket(string filename, bool isActive) {
            return isActive ? Regex.Replace(filename, @" ?\(.*?\)", " ") : filename;
        }

        // Remove square brackets + text: `abc [def]` -> `abc  `
        public static string RemoveSquareBracket(string filename, bool isActive) {
            return isActive ? Regex.Replace(filename, @" ?\[.*?\]", " ") : filename;
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
        public static string OptimizeArticles(string filename, bool isActive) {
            if(!isActive)
                return filename;

            String[] words = filename.Split(' ');

            if(words.Length == 1)
                return filename;

            String[] articles = { "a", "an", "the", "of", "and", "in", "into", "onto", "from" };

            for(int i = 1; i < words.Length; i++) {
                foreach(String word in articles) {
                    if(words[i].ToLower() == word)
                        words[i] = word;
                }
            }
            return string.Join(' ', words);
        }

        // CLI friendly conversion: `abc def` -> `abc-def`
        public static string CliFriendlyConvert(string filename, string cliSeparator, bool isActive) {
            return isActive ? filename.Replace(" ", cliSeparator) : filename;
        }
    }
}
