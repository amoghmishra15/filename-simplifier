namespace simplify {
    class Scan {
        // Crawl the directory to find files with required extension
        public static IEnumerable<string> Files(string path, string[] ext, bool AllDir) {
            IEnumerable<string> files;
            files = AllDir ?
                ext.SelectMany(f => Directory.GetFiles(path, f, SearchOption.AllDirectories)) :
                ext.SelectMany(f => Directory.GetFiles(path, f, SearchOption.TopDirectoryOnly));

            // No files found check
            if(!files.Any()) {      // equivalent to `files.isEmpty()`
                Console.WriteLine($"No file found with extension [{String.Join(", ", ext)}] in '{path}'\nExiting...");
                System.Environment.Exit(1);
            }

            return files;
        }
    }
}
