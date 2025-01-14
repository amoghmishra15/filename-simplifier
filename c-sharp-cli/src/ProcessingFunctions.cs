﻿namespace simplify;
static class Process {
    public static string[] ConvertToExtensionList(JsonConfig prefs) {
        string[] extensionList = prefs.Extensions.Split(',');
        for(int i = 0; i < extensionList.Length; i++) {
            Simplify.ReduceWhitespace(ref extensionList[i]);
            extensionList[i] = $"*.{extensionList[i]}";
        }

        return extensionList;
    }

    public static string FirstCharToUppercase(string word) {
        if(word == string.Empty) { return string.Empty; }
        return word.First().ToString().ToUpperInvariant() + word[1..];
    }
}