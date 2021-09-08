﻿namespace simplify;
static class MainProgram {
    static void Main(string[] args) {
        // Load preferences [creates an immutable object (record)]
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
            // Create metadata object [creates an immutable object (record)]
            var file = new Metadata(fullPath);
            string rename = file.Name;

            // Order sensitive operations (first) [NOTE: all are call by reference]
            Simplify.AppendYearPre(ref rename, prefs);

            // Order insensitive operations [NOTE: all are call by reference]
            Simplify.RemoveSequence(ref rename, ".", prefs.RemoveDot);
            Simplify.RemoveSequence(ref rename, "-", prefs.RemoveDash);
            Simplify.RemoveSequence(ref rename, "_", prefs.RemoveUnderscore);

            Simplify.RemoveCurvedBracket(ref rename, prefs);
            Simplify.RemoveSquareBracket(ref rename, prefs);
            Simplify.RemoveSquareBracket(ref rename, prefs);
            Simplify.RemoveNonASCII(ref rename, prefs);


            // Order sensitive operations (last) [NOTE: all are call by reference]
            Simplify.AppendYearPost(ref rename, prefs);
            Simplify.ReduceWhitespace(ref rename);
            Simplify.ConvertToSentenceCase(ref rename, prefs);
            Simplify.OptimizeArticles(ref rename, prefs);
            Simplify.ConvertToCliFriendly(ref rename, prefs);
            Simplify.ConvertToLowercase(ref rename, prefs);


            // Full address of processed filename
            string simplifiedFileAddress = $"{file.Directory}/{rename}{file.Extension}";


            // Already simplified form
            if(file.Name == rename) {
                Print.NoChangeRequired(file);
                countUnchanged++;
            }
            // Rename conflict
            else if(File.Exists(simplifiedFileAddress)) {
                // Check for Windows specific case-insensitive directory
                if(String.Equals(file.Name, rename, StringComparison.OrdinalIgnoreCase)) {
                    // File . Move ( fileAddress -> temp -> simplifiedFileAddress );
                    // WARNING: Uncomment to make change permanent
                    Print.Success(file, rename);
                    countRenamed++;
                }
                // Actual conflict
                else {
                    Print.RenameConflict(file, rename);
                    countConflict++;
                }
            }
            // Can be renamed without any conflict
            else {
                Print.Success(file, rename);
                // File . Move ( fileAddress -> simplifiedFileAddress );
                // WARNING: Uncomment to make change permanent
                countRenamed++;
            }
        }


        // Print results
        Print.Results(countRenamed, countConflict, countUnchanged);
    }
}