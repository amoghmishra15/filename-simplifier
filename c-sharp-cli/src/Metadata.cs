namespace simplify;

public record Metadata {
    public string FullPath { get; init; }
    public string Name { get; init; }
    public string Extension { get; init; }
    public string NameWithExtension { get; init; }
    public string Directory { get; init; }

    public Metadata(string fullPath) {
        FullPath = fullPath;

        // Extroplate
        Name = Path.GetFileNameWithoutExtension(FullPath);
        Extension = Path.GetExtension(FullPath);
        NameWithExtension = Path.GetFileName(FullPath);
        Directory = Path.GetDirectoryName(FullPath) ?? "/";
    }
}