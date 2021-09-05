namespace simplify;
static class Process {
    public static string[] ConvertToExtensionList(Preferences.JsonConfig prefs) {
        string[] extensionList = prefs.Extensions.Split(',');
        for(int i = 0; i < extensionList.Length; i++) {
            Simplify.ReduceWhitespace(ref extensionList[i]);
            extensionList[i] = $"*.{extensionList[i]}";
        }

        return extensionList;
    }
}
