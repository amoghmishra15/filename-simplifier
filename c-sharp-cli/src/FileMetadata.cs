namespace simplify;

public record FileMetadata {
    public string FullPath { get; init; }
    public string Name { get; init; }
    public string Extension { get; init; }
    public string NameWithExtension { get; init; }
    public string Directory { get; init; }

    public FileMetadata(string fullPath) {
        FullPath = fullPath;

        // Extroplate
        Name = Path.GetFileNameWithoutExtension(FullPath);
        Extension = Path.GetExtension(FullPath);
        NameWithExtension = Path.GetFileName(FullPath);
        Directory = Path.GetDirectoryName(FullPath) ?? "/";
    }
}

public record FolderMetadata {
    public string FullPath { get; init; }
    public string ParentDirectory { get; init; }
    public string Name { get; init; }

    public FolderMetadata(string fullPath) {
        FullPath = fullPath;

        // Extrapolate
        string tempDir = Path.GetDirectoryName(FullPath) ?? "NULL FOLDER METADATA";
        ParentDirectory = tempDir.Replace('\\', '/');
        Name = FullPath.Split('/')[^1];
    }
}