/* --------- Input parameters --------- */
string filename = "Land.of_the.Lustrous";

/* --------- Flags --------- */
bool removeUnderscoreFlag = true;

/* --------- Implementation --------- */
if(removeUnderscoreFlag)
    filename = RemoveUnderscore(filename);

// Final output
Console.WriteLine(filename);

/* --------- Functions --------- */
// Remove underscore: `abc_def` -> `abc def`
string RemoveUnderscore(string filename) {
    return filename.Replace("_", " ");
}