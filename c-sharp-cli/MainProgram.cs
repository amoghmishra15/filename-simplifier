namespace simplify {
    class MainProgram {
        static void Main(string[] args) {
            // Input parameters
            string cliSeparator = "-";
            string path = @"C:\Zeta\Testing\A";
            string[] extensionFilter = { "*.mp4", "*.mkv" };


            // Flags/Settings
            bool removeDotFlag = true;
            bool removeDashFlag = true;
            bool removeUnderscoreFlag = true;

            bool removeCurvedBracketFlag = true;
            bool removeSquareBracketFlag = true;
            
            bool cliFriendlyFlag = false;
            bool getAllDirectoriesFlag = true;


            // Counters
            int countRenamed = 0;
            int countConflict = 0;
            int countUnchanged = 0;


            // Populate files with required extensions
            IEnumerable<string> files = Scan.Files(path, extensionFilter, getAllDirectoriesFlag);

            // Print selected files and get confirmation from user
            Print.Confirmation(files);


            // Apply rename functions
            foreach(var fileAddress in files) {
                // Grab the metadata
                string filename = Path.GetFileNameWithoutExtension(fileAddress);
                string extension = Path.GetExtension(fileAddress);
                string? directory = Path.GetDirectoryName(fileAddress);
                string fullFilename = Path.GetFileName(fileAddress);


                // Order insensitive operations
                if(removeUnderscoreFlag)
                    filename = Simplify.RemoveUnderscore(filename);

                if(removeDashFlag)
                    filename = Simplify.RemoveDash(filename);

                if(removeDotFlag)
                    filename = Simplify.RemoveDot(filename);

                if(removeCurvedBracketFlag)
                    filename = Simplify.RemoveCurvedBracket(filename);

                if(removeSquareBracketFlag)
                    filename = Simplify.RemoveSquareBracket(filename);


                // Order sensitive operations
                filename = Simplify.ReduceWhitespace(filename);

                if(cliFriendlyFlag)
                    filename = Simplify.CliFriendlyConvert(filename, cliSeparator);


                // Full address of processed filename
                string simplifiedFileAddress = $"{Path.GetDirectoryName(fileAddress)}\\{filename}{Path.GetExtension(fileAddress)}";


                // Already simplified form
                if(fileAddress == simplifiedFileAddress) {
                    Print.NoChangeRequired(fullFilename, directory);
                    countUnchanged++;
                }
                // Rename conflict
                else if(File.Exists(simplifiedFileAddress)) {
                    Print.RenameConflict(fullFilename, filename, extension, directory);
                    countConflict++;
                }
                // Can be renamed without any conflict
                else {
                    Print.Success(fullFilename, filename, extension, directory);
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
        }
    }
}