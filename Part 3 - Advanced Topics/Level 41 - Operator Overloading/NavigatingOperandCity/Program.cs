BlockCoordinate startingCoordinate = new(4, 3);
BlockOffset offset = new(2, 0);
Direction direction = Direction.East;

Console.WriteLine($"Start: {startingCoordinate}\twith offset {offset} => {startingCoordinate + offset}");
Console.WriteLine($"Start: {startingCoordinate}\twith direction {direction} => {startingCoordinate + direction}");

public record BlockCoordinate(int Row, int Column)
{
    public static BlockCoordinate operator +(BlockCoordinate coordinate, BlockOffset offset)
    {
        int arrivalRow = coordinate.Row + offset.RowOffset,
            arrivalColumn = coordinate.Column + offset.ColumnOffset;

        return new BlockCoordinate(arrivalRow, arrivalColumn);
    }

    public static BlockCoordinate operator +(BlockCoordinate coordinate, Direction direction)
    {
        int arrivalRow = coordinate.Row,
            arrivalColumn = coordinate.Column;

        switch (direction)
        {
            case Direction.North: { arrivalRow--; break; }
            case Direction.East: { arrivalColumn++; break; }
            case Direction.South: { arrivalRow++; break; }
            case Direction.West: { arrivalColumn--; break; }
            default: { break; }
        }

        return new BlockCoordinate(arrivalRow, arrivalColumn);
    }
}

public record BlockOffset(int RowOffset, int ColumnOffset);

public enum Direction { North, East, South, West }
