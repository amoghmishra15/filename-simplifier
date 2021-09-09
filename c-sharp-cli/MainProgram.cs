namespace simplify;
static class MainProgram {
    public static void Main(string[] args) {
        // Load preferences [creates an immutable object (record)]
        var prefs = Preferences.LoadConfig();

        // Process input arguments
        bool makeChangesPermanent = false;
        bool includeFolders = false;

        string libraryPath = prefs.LibraryPath;
        if(args.Any()) {
            if(args[0].Replace('\\', '/').Contains('/')) {
                libraryPath = args[0]; // if path is provided by argument, overrule config.json path
            }
            Console.WriteLine($"\nLibrary path: {Print.InfoText(libraryPath)}");

            // Flags
            string cliFlags = string.Join(" ", args).ToLowerInvariant();

            if(cliFlags.Contains("--rename")) { makeChangesPermanent = true; }
            if(cliFlags.Contains("--includefolders")) { includeFolders = true; }
        }

        // Update runtime preferences
        prefs.MakeChangesPermanent = makeChangesPermanent;
        prefs.RenameFolders = includeFolders;

        // Init counters (unchanged, conflict, renamed)
        var counter = new Counter(0, 0, 0);

        // Rename folders
        if(prefs.RenameFolders) {
            var folders = Scan.Folders(libraryPath, prefs);
            if(folders.Any()) {
                if(Print.FolderConfirmation(folders, makeChangesPermanent)) {
                    for(int i = folders.Length - 1; i >= 0; i--) {
                        // WARNING: Reversed loop order becasuse innermost folder must be renamed first
                        // Simplifying the outermost folder first will break address of inner folders
                        string fullPath = folders[i];
                        Rename.SimplifyFolder(prefs, fullPath, ref counter);
                    }
                }
            }
            Console.WriteLine("\n\n");
        }

        // Rename files
        var files = Scan.Files(libraryPath, prefs);
        if(files.Any()) {
            if(Print.FilesConfirmation(files, makeChangesPermanent)) {
                foreach(var fullPath in files) {
                    Rename.SimplifyFile(prefs, fullPath, ref counter);
                }
            }
        }

        // Print results
        Print.Results(counter.Renamed, counter.Conflict, counter.Unchanged);
    }
}