/* --------- Input parameters --------- */
string filename = "Land.of_the.Lustrous-";

/* --------- Flags --------- */
bool removeUnderscoreFlag = true;
bool removeDashFlag = true;

/* --------- Implementation --------- */
if(removeUnderscoreFlag)
    filename = RemoveUnderscore(filename);

if(removeDashFlag)
    filename = RemoveDash(filename);

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