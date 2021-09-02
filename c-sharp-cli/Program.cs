using System.Text.RegularExpressions;


/* --------- Input parameters --------- */
string cliSeparator = "-";
string path = @"C:\Zeta\Testing\A";
string[] extensionFilter = { "*.mkv", "*.mp4" };


/* --------- Flags --------- */
bool removeUnderscoreFlag = true;
bool removeDashFlag = true;
bool removeDotFlag = true;
bool removeCurvedBracketFlag = true;
bool removeSquareBracketFlag = true;
bool cliFriendlyFlag = false;
bool getAllDirectoriesFlag = true;


/* --------- Counters --------- */
int countUnchanged = 0;
int countConflict = 0;
int countRenamed = 0;


/* --------- Implementation --------- */
// Populate IEnum<> of files with required extensions
var files = getAllDirectoriesFlag ?
    extensionFilter.SelectMany(f => Directory.GetFiles(path, f, SearchOption.AllDirectories)) :
    extensionFilter.SelectMany(f => Directory.GetFiles(path, f, SearchOption.TopDirectoryOnly));


// Print selected files and get confirmation
Console.WriteLine("Following files will be affected\n");
foreach(var fileAddress in files) Console.WriteLine(Path.GetFullPath(fileAddress));
Console.Write("\nContinue? (y/N): ");
if(Console.Read() != 'y') System.Environment.Exit(1);
Console.WriteLine();


// Apply rename functions
foreach(var fileAddress in files) {
    // Grab the metadata
    string filename = Path.GetFileNameWithoutExtension(fileAddress);
    string extension = Path.GetExtension(fileAddress);
    string? directory = Path.GetDirectoryName(fileAddress);
    string fullFilename = Path.GetFileName(fileAddress);


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


    // Full address of processed filename
    string simplifiedFileAddress = $"{Path.GetDirectoryName(fileAddress)}\\{filename}{Path.GetExtension(fileAddress)}";


    // Final rename
    if(fileAddress == simplifiedFileAddress) {          // No change required
        Console.WriteLine($"File `{filename}` in `{directory}` is already in simplified form");
        countUnchanged++;
    } else if(File.Exists(simplifiedFileAddress)) {     // Rename conflict (already exists)
        Console.WriteLine(
            "\n--- Rename Conflict ---\n" +
            $"Attempting to rename `{fullFilename}` to `{filename}{extension}`\n" +
            $"A file with the same name already exists in `{directory}`\n" +
            $"Delete/rename either file manually. No action has been taken.\n" +
            $"-----------------------\n"
            );
        countConflict++;
    } else {                                            // Can be renamed
        Console.WriteLine($"Renamed `{fullFilename}` to `{filename}{extension}` in `{directory}`");
        // File.Move(fileAddress, simplifiedFileAddress);
        // WARNING: Uncomment to make change permanent
        countRenamed++;
    }
}

// Print results
Console.WriteLine(
    "\n\n--- Completed ---\n" +
    $"Renamed files count: {countRenamed}\n" +
    $"Already simplified files count: {countUnchanged}\n" +
    $"Conflict files count: {countConflict} (skipped; manual change required; view log)"
    );


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
