namespace simplify;
static class Print {
    // Print selected files and get confirmation from user
    public static void Confirmation(IEnumerable<string> files) {
        Console.WriteLine("Following files will be affected\n");

        foreach(string fileAddress in files) { Console.WriteLine(Path.GetFullPath(fileAddress)); }

        Console.Write("\nContinue? (y/N): ");
        if(Console.Read() != 'y') { System.Environment.Exit(1); }
        Console.WriteLine();
    }

    // Print 'no change required' message
    public static void NoChangeRequired(Metadata file) {
        Console.WriteLine($"File '{file.Name}{file.Extension}' in '{file.Directory}' is already in simplified form");
    }

    // Print 'rename conflict' message
    public static void RenameConflict(Metadata file, string rename) {
        Console.WriteLine("\n--- Rename Conflict ---");
        Console.WriteLine($"Attempting to rename '{file.Name}{file.Extension}' to '{rename}{file.Extension}'");
        Console.WriteLine($"A file with the same name already exists in '{file.Directory}'");
        Console.WriteLine("Delete/rename either file manually. No action has been taken.\n");
    }

    // Print 'success' message
    public static void Success(Metadata file, string rename) {
        Console.WriteLine($"Renamed '{file.Name}{file.Extension}' to '{rename}{file.Extension}' in '{file.Directory}'");
    }


    // Print results and stats
    public static void Results(int countRenamed, int countConflict, int countUnchanged) {
        Console.WriteLine("\n\n--- Completed ---");
        Console.WriteLine($"Renamed files count: {countRenamed}");
        Console.WriteLine($"Already simplified files count: {countUnchanged}");
        Console.WriteLine($"Conflict files count: {countConflict} (skipped; manual change required; view log)");
    }
}
