namespace simplify;
static class MainProgram {
    public static void Main(string[] args) {
        // Load preferences [creates an immutable object (record)]
        var prefs = Preferences.LoadConfig();

        // Process input arguments
        bool renameCliFlag = false;
        string libraryPath = prefs.LibraryPath;
        if(args.Any()) {
            libraryPath = args[0]; // if path is provided by argument, overrule config.json path
            Console.WriteLine($"\nLibrary path: {Print.InfoText(libraryPath)}");

            // Flags
            string cliFlags = string.Join(" ", args).ToLowerInvariant();
            if(cliFlags.Contains("--rename")) {
                renameCliFlag = true;
            }
        }

        // Init counters (unchanged, conflict, renamed)
        var counter = new Counter(0, 0, 0);

        // Populate files with required extensions
        var files = Scan.Files(libraryPath, prefs);

        // Print selected files and get confirmation from user
        Print.Confirmation(files, renameCliFlag);

        // Rename files
        foreach(var fullPath in files) {
            Rename.ApplySimplificationMethods(prefs, fullPath, renameCliFlag, ref counter);
        }

        // Print results
        Print.Results(counter.Renamed, counter.Conflict, counter.Unchanged);
    }
}