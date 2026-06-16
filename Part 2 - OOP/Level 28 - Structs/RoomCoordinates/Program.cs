Coordinate coordinate1 = new(1, 1);
Coordinate coordinate2 = new(2, 1);
Coordinate coordinate3 = new();
Coordinate coordinate4 = new() { Row = 3, Column = 1 };

TestAdjacency(coordinate1, coordinate1);
TestAdjacency(coordinate2, coordinate1);
TestAdjacency(coordinate3, coordinate4);
TestAdjacency(coordinate4, coordinate2);

static void TestAdjacency(Coordinate coordinate1, Coordinate coordinate2)
{
    Console.WriteLine($"Is {coordinate1} adjacent to {coordinate2}? {(coordinate1.IsAdjacentTo(coordinate2) ? "Yes" : "No")}");
}

public struct Coordinate
{
    public int Row { get; init; }
    public int Column { get; init; }

    public Coordinate() : this(0, 0) { }

    public Coordinate(int row, int column) { Row = row; Column = column; }

    public bool IsAdjacentTo(Coordinate anotherCoordinate)
    {
        // Coordinates differ by one row.
        if (Column == anotherCoordinate.Column
            && Row == anotherCoordinate.Row + 1) return true;
        else if (Column == anotherCoordinate.Column
                 && Row == anotherCoordinate.Row - 1) return true;
        // Coordinates differ by one column.
        else if (Row == anotherCoordinate.Row
                 && Column == anotherCoordinate.Column + 1) return true;
        else if (Row == anotherCoordinate.Row
                 && Column == anotherCoordinate.Column - 1) return true;
        else return false;
    }

    public override string ToString() => $"({Row}, {Column})";
}