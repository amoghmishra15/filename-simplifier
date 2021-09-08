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
    public static string ErrorText(string message) {
        return $"{message}".Pastel(red);
    }

    public static string WarningText(string message) {
        return $"{message}".Pastel(yellow);
    }

    public static string SuccessText(string message) {
        return $"{message}".Pastel(green);
    }

    public static string InfoText(string message) {
        return $"{message}".Pastel(blue);
    }

    public static string GrayedText(string message) {
        return $"{message}".Pastel(gray);
    }

    // Print selected files and get confirmation from user
    public static void Confirmation(IEnumerable<string> files, bool renameCliFlag) {
        InfoBlock();
        Console.WriteLine("Following files will be affected");

        foreach(string fileAddress in files) {
            string path = Path.GetFullPath(fileAddress);
            Console.WriteLine(InfoText(path));
            Console.WriteLine();
        }

        if(!renameCliFlag) {
            InfoBlock();
            Console.WriteLine($"You are currently in preview mode. Pass {"--rename".Pastel(blue)} to make changes permanent.");
        } else {
            WarningBlock();
            Console.WriteLine("\nYou are currently in rename mode.");
            Console.WriteLine(WarningText("Changes will be permanent. Make sure you have performed a dry run in preview mode."));
        }

        Console.Write($"\nContinue? ({"y".Pastel(green)}/{"N".Pastel(red)}): ");
        if(Console.Read() != 'y') { Environment.Exit(1); }
        Console.WriteLine();
    }

    // Print 'no change required' message
    public static void NoChangeRequired(Metadata file) {
        InfoBlock();
        Console.WriteLine($"[{GrayedText(file.Directory)}]");
        Console.WriteLine($"{InfoText(file.NameWithExtension)} is already in simplified form\n");
    }

    // Print 'rename conflict' message
    public static void RenameConflict(Metadata file, string rename) {
        WarningBlock();
        Console.WriteLine($"[{GrayedText(file.Directory)}]");
        Console.WriteLine(GrayedText(file.NameWithExtension));
        Console.WriteLine(WarningText($"{rename}{file.Extension}"));
        Console.WriteLine("File with same name already exists. Fix manually.\n");
    }

    // Print 'success' message
    public static void Success(Metadata file, string rename) {
        SuccessBlock();
        Console.WriteLine($"[{GrayedText(file.Directory)}]");
        Console.WriteLine(GrayedText(file.NameWithExtension));
        Console.WriteLine(SuccessText($"{rename}{file.Extension}\n"));
    }


    // Print results and stats
    public static void Results(int countRenamed, int countConflict, int countUnchanged) {
        Console.WriteLine("\n SUMMARY ".Pastel(white).PastelBg(gray));
        Console.WriteLine($"Renamed files: {SuccessText(countRenamed.ToString())}");
        Console.WriteLine($"Already simplified : {InfoText(countUnchanged.ToString())}");
        Console.WriteLine($"Rename conflicts: {WarningText(countConflict.ToString())}");
    }
}