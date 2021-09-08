namespace simplify;
class Rename {
    public static void ApplySimplificationMethods(Preferences.JsonConfig prefs, string fullPath, bool renameCliFlag, ref Counter counter) {
        // Create metadata object [creates an immutable object (record)]
        var file = new Metadata(fullPath);
        string rename = file.Name;

        // Order sensitive operations (first) [NOTE: all are call by reference]
        Simplify.AppendYearPre(ref rename, prefs);

        // Order insensitive operations [NOTE: all are call by reference]
        Simplify.RemoveSequence(ref rename, ".", prefs.RemoveDot);
        Simplify.RemoveSequence(ref rename, "-", prefs.RemoveDash);
        Simplify.RemoveSequence(ref rename, "_", prefs.RemoveUnderscore);

        Simplify.RemoveCurvedBracket(ref rename, prefs);
        Simplify.RemoveSquareBracket(ref rename, prefs);
        Simplify.RemoveSquareBracket(ref rename, prefs);
        Simplify.RemoveNonASCII(ref rename, prefs);


        // Order sensitive operations (last) [NOTE: all are call by reference]
        Simplify.AppendYearPost(ref rename, prefs);
        Simplify.ReduceWhitespace(ref rename);
        Simplify.ConvertToSentenceCase(ref rename, prefs);
        Simplify.OptimizeArticles(ref rename, prefs);
        Simplify.ConvertToCliFriendly(ref rename, prefs);
        Simplify.ConvertToLowercase(ref rename, prefs);


        // Full address of processed filename
        string simplifiedFileAddress = $"{file.Directory}/{rename}{file.Extension}";


        // Already simplified form
        if(file.Name == rename) {
            Print.NoChangeRequired(file);
            counter.Unchanged++;
        }
        // Rename conflict
        else if(File.Exists(simplifiedFileAddress)) {
            // Check for Windows specific case-insensitive directory
            if(string.Equals(file.Name, rename, StringComparison.OrdinalIgnoreCase)) {
                if(renameCliFlag) {
                    File.Move(file.FullPath, $"{file.Directory}/TEMP_SIMPLIFY_RENAME");
                    File.Move($"{file.Directory}/TEMP_SIMPLIFY_RENAME", simplifiedFileAddress);
                }
                Print.Success(file, rename);
                counter.Renamed++;
            }
            // Actual conflict
            else {
                Print.RenameConflict(file, rename);
                counter.Conflict++;
            }
        }
        // Can be renamed without any conflict
        else {
            Print.Success(file, rename);
            if(renameCliFlag) { File.Move(file.FullPath, simplifiedFileAddress); }
            counter.Renamed++;
        }
    }


}
