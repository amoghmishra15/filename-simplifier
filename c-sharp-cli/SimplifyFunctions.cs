using System.Text.RegularExpressions;

namespace simplify {
    class Simplify {
        // Remove underscore: `abc_def` -> `abc def`
        public static string RemoveUnderscore(string filename) {
            return filename.Replace("_", " ");
        }

        // Remove dash: `abc-def` -> `abc def`
        public static string RemoveDash(string filename) {
            return filename.Replace("-", " ");
        }

        // Remove dot: `abc-def` -> `abc def`
        public static string RemoveDot(string filename) {
            return filename.Replace(".", " ");
        }

        // Remove parentheses + text: `abc (def)` -> `abc  `
        public static string RemoveCurvedBracket(string filename) {
            return Regex.Replace(filename, @" ?\(.*?\)", " ");
        }

        // Remove square brackets + text: `abc [def]` -> `abc  `
        public static string RemoveSquareBracket(string filename) {
            return Regex.Replace(filename, @" ?\[.*?\]", " ");
        }

        // Remove 2+ and trailing whitespace: ` abc    def ` -> `abc def`
        public static string ReduceWhitespace(string filename) {
            filename = Regex.Replace(filename, @"\s+", " "); // 2+
            return filename.Trim(' '); // trailing
        }

        // CLI friendly conversion: `abc def` -> `abc-def`
        public static string CliFriendlyConvert(string filename, string cliSeparator) {
            return filename.Replace(" ", cliSeparator);
        }
    }
}
