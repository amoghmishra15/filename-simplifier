using System.Text.RegularExpressions;

/* --------- Input parameters --------- */
string filename = "Land.of_the.    Lustrous(2009)-[HEVC]";
string cliSeparator = "-";

/* --------- Flags --------- */
bool removeUnderscoreFlag = true;
bool removeDashFlag = true;
bool removeDotFlag = true;
bool removeCurvedBracketFlag = true;
bool removeSquareBracketFlag = true;
bool cliFriendlyFlag = false;

/* --------- Implementation --------- */
// Order insensitive operations
if(removeUnderscoreFlag)
    filename = RemoveUnderscore(filename);

if(removeDashFlag)
    filename = RemoveDash(filename);

if(removeDotFlag)
    filename = RemoveDot(filename);

if(removeCurvedBracketFlag)
    filename = RemoveCurvedBracket(filename);

if(removeSquareBracketFlag)
    filename = RemoveSquareBracket(filename);

// Order sensitive operations
filename = ReduceWhitespace(filename);

if(cliFriendlyFlag)
    filename = CliFriendlyConvert(filename, cliSeparator);

// Final output
Console.WriteLine(filename);

/* --------- Functions --------- */
// Remove underscore: `abc_def` -> `abc def`
static string RemoveUnderscore(string filename) {
    return filename.Replace("_", " ");
}

// Remove dash: `abc-def` -> `abc def`
static string RemoveDash(string filename) {
    return filename.Replace("-", " ");
}

// Remove dot: `abc-def` -> `abc def`
static string RemoveDot(string filename) {
    return filename.Replace(".", " ");
}

// Remove parentheses + text: `abc (def)` -> `abc  `
static string RemoveCurvedBracket(string filename) {
    return Regex.Replace(filename, @" ?\(.*?\)", " ");
}

// Remove square brackets + text: `abc [def]` -> `abc  `
static string RemoveSquareBracket(string filename) {
    return Regex.Replace(filename, @" ?\[.*?\]", " ");
}

// Remove 2+ and trailing whitespace: ` abc    def ` -> `abc def`
static string ReduceWhitespace(string filename) {
    filename = Regex.Replace(filename, @"\s+", " "); // 2+
    return filename.Trim(' '); // trailing
}

// CLI friendly conversion: `abc def` -> `abc-def`
static string CliFriendlyConvert(string filename, string cliSeparator) {
    return filename.Replace(" ", cliSeparator);
}