namespace simplify {
    class MainProgram {
        static void Main(string[] args) {
            // Input parameters
            string cliSeparator = "-";
            string path = @"C:\Zeta\Testing\A";
            string[] extensionFilter = { "*.mp4", "*.mkv" };

            // Counters
            int countRenamed = 0;
            int countConflict = 0;
            int countUnchanged = 0;

            // Populate files with required extensions
            IEnumerable<string> files = Scan.Files(path, extensionFilter);

            // Print selected files and get confirmation from user
            Print.Confirmation(files);

            // Apply rename functions
            foreach(var fullPath in files) {
                // Create metadata object
                var file = new Metadata(fullPath);
                string simplify = file.Name;


                // Order insensitive operations
                simplify = Simplify.RemoveSequence(simplify, ".", Preferences.removeDot);
                simplify = Simplify.RemoveSequence(simplify, "-", Preferences.removeDash);
                simplify = Simplify.RemoveSequence(simplify, "_", Preferences.removeUnderscore);

                simplify = Simplify.RemoveCurvedBracket(simplify);
                simplify = Simplify.RemoveSquareBracket(simplify);


                // Order sensitive operations
                simplify = Simplify.ReduceWhitespace(simplify);
                simplify = Simplify.OptimizeArticles(simplify);
                simplify = Simplify.CliFriendlyConvert(simplify, cliSeparator);


                // Full address of processed filename
                string simplifiedFileAddress = $"{file.Directory}\\{simplify}{file.Extension}";


                // Already simplified form
                if(fullPath == simplifiedFileAddress) {
                    Print.NoChangeRequired(file.Name, file.Directory);
                    countUnchanged++;
                }
                // Rename conflict
                else if(File.Exists(simplifiedFileAddress)) {
                    Print.RenameConflict(file.Name, simplify, file.Extension, file.Directory);
                    countConflict++;
                }
                // Can be renamed without any conflict
                else {
                    Print.Success(file.Name, simplify, file.Extension, file.Directory);
                    // File.Move(fileAddress, simplifiedFileAddress);
                    // WARNING: Uncomment to make change permanent
                    countRenamed++;
                }
            }


            // Print results
            Print.Results(countRenamed, countConflict, countUnchanged);
        }
    }
}