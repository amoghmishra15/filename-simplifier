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
                string newName = Path.GetFileNameWithoutExtension(fileAddress);
                string extension = Path.GetExtension(fileAddress);
                string directory = Path.GetDirectoryName(fileAddress) ?? "root\\";
                string oldNameWithExtension = Path.GetFileName(fileAddress);


                // Order insensitive operations
                newName = Simplify.RemoveUnderscore(newName, removeUnderscoreFlag);
                newName = Simplify.RemoveDash(newName, removeDashFlag);
                newName = Simplify.RemoveDot(newName, removeDotFlag);
                newName = Simplify.RemoveCurvedBracket(newName, removeCurvedBracketFlag);
                newName = Simplify.RemoveSquareBracket(newName, removeSquareBracketFlag);


                // Order sensitive operations
                newName = Simplify.ReduceWhitespace(newName);
                newName = Simplify.CliFriendlyConvert(newName, cliSeparator, cliFriendlyFlag);


                // Full address of processed filename
                string simplifiedFileAddress = $"{directory}\\{newName}{extension}";


                // Already simplified form
                if(fileAddress == simplifiedFileAddress) {
                    Print.NoChangeRequired(oldNameWithExtension, directory);
                    countUnchanged++;
                }
                // Rename conflict
                else if(File.Exists(simplifiedFileAddress)) {
                    Print.RenameConflict(oldNameWithExtension, newName, extension, directory);
                    countConflict++;
                }
                // Can be renamed without any conflict
                else {
                    Print.Success(oldNameWithExtension, newName, extension, directory);
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