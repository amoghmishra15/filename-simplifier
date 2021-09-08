using Pastel;

namespace simplify;
static class Print {
    // Colors
    const string red = "#F7525B";
    const string green = "#87E987";
    const string blue = "#7FACF7";
    const string yellow = "FFB900";
    const string gray = "68768A";
    const string white = "FFFFFF";
    const string black = "000000";

    // Reusable block identifiers
    public static void ErrorBlock() {
        Console.Write(" ERROR ".Pastel(black).PastelBg(red));
        Console.Write(" ");
    }
    public static void WarningBlock() {
        Console.Write(" WARNING ".Pastel(black).PastelBg(yellow));
        Console.Write(" ");
    }
    public static void SuccessBlock() {
        Console.Write(" SUCCESS ".Pastel(black).PastelBg(green));
        Console.Write(" ");
    }
    public static void InfoBlock() {
        Console.Write(" INFORMATION ".Pastel(black).PastelBg(blue));
        Console.Write(" ");
    }

    // Colorize strings
    public static string ErrorColor(string message) {
        return $"{message}".Pastel(red);
    }

    public static string WarningColor(string message) {
        return $"{message}".Pastel(yellow);
    }

    public static string SuccessColor(string message) {
        return $"{message}".Pastel(green);
    }

    public static string InfoColor(string message) {
        return $"{message}".Pastel(blue);
    }

    public static string StealthColor(string message) {
        return $"{message}".Pastel(gray);
    }

    // Print selected files and get confirmation from user
    public static void Confirmation(IEnumerable<string> files) {
        InfoBlock();
        Console.WriteLine("Following files will be affected");

        foreach(string fileAddress in files) {
            string path = Path.GetFullPath(fileAddress);
            Console.WriteLine(InfoColor(path));
        }

        Console.Write($"\nContinue? ({"y".Pastel(green)}/{"N".Pastel(red)}): ");
        if(Console.Read() != 'y') { Environment.Exit(1); }
        Console.WriteLine();
    }

    // Print 'no change required' message
    public static void NoChangeRequired(Metadata file) {
        InfoBlock();
        Console.WriteLine($"[{StealthColor(file.Directory)}]");
        Console.WriteLine($"{InfoColor(file.NameWithExtension)} is already in simplified form\n");
    }

    // Print 'rename conflict' message
    public static void RenameConflict(Metadata file, string rename) {
        WarningBlock();
        Console.WriteLine($"[{StealthColor(file.Directory)}]");
        Console.WriteLine(StealthColor(file.NameWithExtension));
        Console.WriteLine(WarningColor($"{rename}{file.Extension}"));
        Console.WriteLine("File with same name already exists. Fix manually.\n");
    }

    // Print 'success' message
    public static void Success(Metadata file, string rename) {
        SuccessBlock();
        Console.WriteLine($"[{StealthColor(file.Directory)}]");
        Console.WriteLine(StealthColor(file.NameWithExtension));
        Console.WriteLine(SuccessColor($"{rename}{file.Extension}\n"));
    }


    // Print results and stats
    public static void Results(int countRenamed, int countConflict, int countUnchanged) {
        Console.WriteLine("\n SUMMARY ".Pastel(white).PastelBg(gray));
        Console.WriteLine($"Renamed files: {SuccessColor(countRenamed.ToString())}");
        Console.WriteLine($"Already simplified : {InfoColor(countUnchanged.ToString())}");
        Console.WriteLine($"Rename conflicts: {WarningColor(countConflict.ToString())}");
    }
}