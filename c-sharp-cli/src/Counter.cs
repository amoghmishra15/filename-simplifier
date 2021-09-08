namespace simplify;
class Counter {
    public int Unchanged { get; set; }
    public int Conflict { get; set; }
    public int Renamed { get; set; }

    public Counter(int unchanged, int conflicted, int renamed) {
        Unchanged = unchanged;
        Conflict = conflicted;
        Renamed = renamed;
    }
}

