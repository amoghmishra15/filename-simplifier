namespace simplify;
static class Print {
    // Print selected files and get confirmation from user
    public static void Confirmation(IEnumerable<string> files) {
        Console.WriteLine("Following files will be affected\n");

        foreach(var fileAddress in files) { Console.WriteLine(Path.GetFullPath(fileAddress)); }

        Console.Write("\nContinue? (y/N): ");
        if(Console.Read() != 'y') { System.Environment.Exit(1); }
        Console.WriteLine();
    }

    // Print 'no change required' message
    public static void NoChangeRequired(string oldName, string dir) {
        Console.WriteLine($"File '{oldName}' in '{dir}' is already in simplified form");
    }

    // Print 'rename conflict' message
    public static void RenameConflict(string oldName, string newName, string ext, string? dir) {
        Console.WriteLine("\n--- Rename Conflict ---");
        Console.WriteLine($"Attempting to rename '{oldName}' to '{newName}{ext}'");
        Console.WriteLine($"A file with the same name already exists in '{dir}'");
        Console.WriteLine("Delete/rename either file manually. No action has been taken.\n");
    }

    // Print 'success' message
    public static void Success(string oldName, string newName, string ext, string dir) {
        Console.WriteLine($"Renamed '{oldName}' to '{newName}{ext}' in '{dir}'");
    }


    // Print results and stats
    public static void Results(int countRenamed, int countConflict, int countUnchanged) {
        Console.WriteLine("\n\n--- Completed ---");
        Console.WriteLine($"Renamed files count: {countRenamed}");
        Console.WriteLine($"Already simplified files count: {countUnchanged}");
        Console.WriteLine($"Conflict files count: {countConflict} (skipped; manual change required; view log)");
    }
}
