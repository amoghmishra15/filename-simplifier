namespace simplify {
    class Process {
        public static string[] ConvertToExtensionList(string extensions) {
            string[] extensionList = extensions.Split(',');
            for(int i = 0; i < extensionList.Length; i++) {
                Simplify.ReduceWhitespace(ref extensionList[i]);
                extensionList[i] = $"*.{extensionList[i]}";
            }

            return extensionList;
        }
    }
}
