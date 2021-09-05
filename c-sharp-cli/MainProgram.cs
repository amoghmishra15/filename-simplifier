namespace simplify;
static class MainProgram {
    static void Main(string[] args) {
        // Load preferences
        var prefs = Preferences.LoadConfig();

        // Counters
        int countRenamed = 0;
        int countConflict = 0;
        int countUnchanged = 0;

        // Populate files with required extensions
        string[] extensionList = Process.ConvertToExtensionList(prefs);
        IEnumerable<string> files = Scan.Files(prefs, extensionList);

        // Print selected files and get confirmation from user
        Print.Confirmation(files);

        // Apply rename functions
        foreach(var fullPath in files) {
            // Create metadata object
            var file = new Metadata(fullPath);
            string rename = file.Name;


            // Order insensitive operations [NOTE: all are call by reference]
            Simplify.RemoveSequence(ref rename, ".", prefs.RemoveDot);
            Simplify.RemoveSequence(ref rename, "-", prefs.RemoveDash);
            Simplify.RemoveSequence(ref rename, "_", prefs.RemoveUnderscore);

            Simplify.RemoveCurvedBracket(ref rename, prefs);
            Simplify.RemoveSquareBracket(ref rename, prefs);
            Simplify.RemoveSquareBracket(ref rename, prefs);
            Simplify.RemoveNonASCII(ref rename, prefs);


            // Order sensitive operations [NOTE: all are call by reference]
            Simplify.ReduceWhitespace(ref rename);
            Simplify.ConvertToSentenceCase(ref rename, prefs);
            Simplify.OptimizeArticles(ref rename, prefs);
            Simplify.ConvertToCliFriendly(ref rename, prefs);
            Simplify.ConvertToLowercase(ref rename, prefs);


            // Full address of processed filename
            string simplifiedFileAddress = $"{file.Directory}/{rename}{file.Extension}";


            // Already simplified form
            if(fullPath == simplifiedFileAddress) {
                Print.NoChangeRequired(file.Name, file.Directory);
                countUnchanged++;
            }
            // Rename conflict
            else if(File.Exists(simplifiedFileAddress)) {
                Print.RenameConflict(file.Name, rename, file.Extension, file.Directory);
                countConflict++;
            }
            // Can be renamed without any conflict
            else {
                Print.Success(file.Name, rename, file.Extension, file.Directory);
                // WARNING: File.Move(fileAddress, simplifiedFileAddress);
                // WARNING: Uncomment to make change permanent
                countRenamed++;
            }
        }


        // Print results
        Print.Results(countRenamed, countConflict, countUnchanged);
    }
}