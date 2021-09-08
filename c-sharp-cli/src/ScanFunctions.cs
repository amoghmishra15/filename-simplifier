namespace simplify;
static class Scan {
    // Crawl the directory to find files with required extension
    public static IEnumerable<string> Files(string path, Preferences.JsonConfig prefs) {
        // Invalid path check
        if(!Directory.Exists(path)) {
            Print.ErrorBlock();
            Console.WriteLine($"Directory {Print.ErrorText(path)} does not exist.\nExiting...");
            Environment.Exit(1);
        }

        // Load files in the directory
        string[] extensionList = Process.ConvertToExtensionList(prefs);
        IEnumerable<string> files = prefs.GetAllDirectories ?
            extensionList.SelectMany(f => Directory.GetFiles(path, f, SearchOption.AllDirectories)) :
            extensionList.SelectMany(f => Directory.GetFiles(path, f, SearchOption.TopDirectoryOnly));

        // No files found check
        if(!files.Any()) {      // equivalent to `files.isEmpty()`
            Print.InfoBlock();
            Console.WriteLine($"No file found with extension [{Print.InfoText(prefs.Extensions)}] in {Print.InfoText(path)}\nExiting...");
            Environment.Exit(1);
        }

        return files;
    }
}