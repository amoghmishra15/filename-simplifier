namespace simplify {
    class MainProgram {
        static void Main(string[] args) {
            // Counters
            int countRenamed = 0;
            int countConflict = 0;
            int countUnchanged = 0;

            // Populate files with required extensions
            string[] extensionList = Process.ConvertToExtensionList(Preferences.extensions);
            IEnumerable<string> files = Scan.Files(Preferences.libraryPath, extensionList);

            // Print selected files and get confirmation from user
            Print.Confirmation(files);

            // Apply rename functions
            foreach(var fullPath in files) {
                // Create metadata object
                var file = new Metadata(fullPath);
                string rename = file.Name;


                // Order insensitive operations [NOTE: all are call by reference]
                Simplify.RemoveSequence(ref rename, ".", Preferences.removeDot);
                Simplify.RemoveSequence(ref rename, "-", Preferences.removeDash);
                Simplify.RemoveSequence(ref rename, "_", Preferences.removeUnderscore);

                Simplify.RemoveCurvedBracket(ref rename);
                Simplify.RemoveSquareBracket(ref rename);
                Simplify.RemoveSquareBracket(ref rename);
                Simplify.ConvertToLowercase(ref rename);
                Simplify.RemoveNonASCII(ref rename);


                // Order sensitive operations [NOTE: all are call by reference]
                Simplify.ReduceWhitespace(ref rename);
                Simplify.OptimizeArticles(ref rename);
                Simplify.ConvertToCliFriendly(ref rename);


                // Full address of processed filename
                string simplifiedFileAddress = $"{file.Directory}\\{rename}{file.Extension}";


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