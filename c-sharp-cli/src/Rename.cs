namespace simplify;
static class Rename {
    private static void ApplySimplificationFunctions(ref string rename, JsonConfig prefs) {
        // Order sensitive operations (first) [NOTE: all are call by reference]
        Simplify.AppendYearPre(ref rename, prefs);

        // Order insensitive operations [NOTE: all are call by reference]
        Simplify.RemoveCurvedBracket(ref rename, prefs);
        Simplify.RemoveSquareBracket(ref rename, prefs);
        Simplify.RemoveBlacklistedWords(ref rename, prefs);
        Simplify.RemoveNonASCII(ref rename, prefs);


        // Order sensitive operations (last) [NOTE: all are call by reference]
        Simplify.AppendYearPost(ref rename, prefs);
        Simplify.SmartEpisodeDash(ref rename, prefs);
        Simplify.ReduceWhitespace(ref rename);
        Simplify.ConvertToSentenceCase(ref rename, prefs);
        Simplify.OptimizeArticles(ref rename, prefs);
        Simplify.ConvertToCliFriendly(ref rename, prefs);
        Simplify.ConvertToLowercase(ref rename, prefs);
    }

    public static void SimplifyFile(JsonConfig prefs, string fullPath, ref Counter counter) {
        // Create file metadata object [creates an immutable object (record)]
        var file = new FileMetadata(fullPath.Replace('\\', '/'));
        string rename = file.Name;
        ApplySimplificationFunctions(ref rename, prefs);

        // Full address of processed filename
        string simplifiedFileAddress = $"{file.Directory}/{rename}{file.Extension}";


        // Already simplified form
        if(file.Name == rename) {
            Print.NoFileChangeRequired(file);
            counter.Unchanged++;
        }

        // Rename conflict
        else if(File.Exists(simplifiedFileAddress)) {
            // Check for Windows specific case-insensitive directory
            if(string.Equals(file.Name, rename, StringComparison.OrdinalIgnoreCase)) {
                if(prefs.MakeChangesPermanent) {
                    File.Move(file.FullPath, $"{file.Directory}/TEMP_SIMPLIFY_RENAME");
                    File.Move($"{file.Directory}/TEMP_SIMPLIFY_RENAME", simplifiedFileAddress);
                }
                Print.FileSuccess(file, rename);
                counter.Renamed++;
            }

            // Actual conflict
            else {
                Print.FileRenameConflict(file, rename);
                counter.Conflict++;
            }
        }

        // Can be renamed without any conflict
        else {
            Print.FileSuccess(file, rename);
            if(prefs.MakeChangesPermanent) { File.Move(file.FullPath, simplifiedFileAddress); }
            counter.Renamed++;
        }
    }

    public static void SimplifyFolder(JsonConfig prefs, string fullPath, ref Counter counter) {
        // Create folder metadata object [creates an immutable object (record)]
        var folder = new FolderMetadata(fullPath.Replace('\\', '/'));
        string rename = folder.Name;
        ApplySimplificationFunctions(ref rename, prefs);

        // Full address of processed filename
        string simplifiedFolderAddress = $"{folder.ParentDirectory}/{rename}";

        // Already simplified form
        if(folder.Name == rename) {
            Print.NoFolderChangeRequired(folder);
            counter.Unchanged++;
        }

        // Rename conflict
        else if(File.Exists(simplifiedFolderAddress)) {
            // Check for Windows specific case-insensitive directory
            if(string.Equals(folder.Name, rename, StringComparison.OrdinalIgnoreCase)) {
                if(prefs.MakeChangesPermanent) {
                    Directory.Move(folder.FullPath, $"{folder.FullPath}_TEMP_SIMPLIFY_RENAME");
                    Directory.Move($"{folder.FullPath}_TEMP_SIMPLIFY_RENAME", simplifiedFolderAddress);
                }
                Print.FolderSuccess(folder, rename);
                counter.Renamed++;
            }

            // Actual conflict
            else {
                Print.FolderRenameConflict(folder, rename);
                counter.Conflict++;
            }
        }

        // Can be renamed without any conflict
        else {
            Print.FolderSuccess(folder, rename);
            if(prefs.MakeChangesPermanent) { Directory.Move(folder.FullPath, simplifiedFolderAddress); }
            counter.Renamed++;
        }
    }

}