namespace simplify {
    class Metadata {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Directory { get; set; }

        public Metadata(string fullPath) {
            FullPath = fullPath;

            // Extroplate
            Name = Path.GetFileNameWithoutExtension(FullPath);
            Extension = Path.GetExtension(FullPath);
            Directory = Path.GetDirectoryName(FullPath) ?? "\\";
        }
    }
}
