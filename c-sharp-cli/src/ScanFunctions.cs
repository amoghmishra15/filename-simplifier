namespace simplify;
class Scan {
    // Crawl the directory to find files with required extension
    public static IEnumerable<string> Files(string path, string[] extension) {
        // Invalid path check
        if(!Directory.Exists(path)) {
            Console.WriteLine($"Directory '{path}' does not exist. Exiting...");
            System.Environment.Exit(1);
        }

        // Load files in the directory
        IEnumerable<string> files = Preferences.getAllDirectories ?
            extension.SelectMany(f => Directory.GetFiles(path, f, SearchOption.AllDirectories)) :
            extension.SelectMany(f => Directory.GetFiles(path, f, SearchOption.TopDirectoryOnly));

        // No files found check
        if(!files.Any()) {      // equivalent to `files.isEmpty()`
            Console.WriteLine($"No file found with extension [{string.Join(", ", extension)}] in '{path}'\nExiting...");
            System.Environment.Exit(1);
        }

        return files;
    }
}
