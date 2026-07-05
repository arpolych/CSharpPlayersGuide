BlockCoordinate startingCoordinate = new(4, 3);

BlockOffset offset = new(2, 0);
Direction direction = Direction.East;

Console.WriteLine($"Start: {startingCoordinate}\twith offset {offset} => {startingCoordinate + offset}");
Console.WriteLine($"Start: {startingCoordinate}\twith direction {direction} => {startingCoordinate + direction}");

Console.WriteLine(direction);
Console.WriteLine((BlockOffset) direction);

public record BlockCoordinate(int Row, int Column)
{
    public int this[int index]
    {
        get
        {
            if (index == 0) return Row;
            else return Column;
        }
    }

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

public record BlockOffset(int RowOffset, int ColumnOffset)
{
    public static implicit operator BlockOffset(Direction direction)
    {
        return direction switch
        {
            Direction.North => new BlockOffset(-1, 0),
            Direction.East => new BlockOffset(0, 1),
            Direction.South => new BlockOffset(1, 0),
            Direction.West => new BlockOffset(0, -1),
            _ => new BlockOffset(0, 0)
        };
    }
}

public enum Direction { North, East, South, West }

/*
 * Answer to question:
 * 
 * Implicit, because there is no loss of information from my 
 * perspective in this conversion.
 */