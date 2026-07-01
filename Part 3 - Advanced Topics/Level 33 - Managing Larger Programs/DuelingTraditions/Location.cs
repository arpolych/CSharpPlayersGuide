namespace DuelingTraditions;

public record Location(int Row, int Column)
{
    public override string ToString() => $"(Row = {Row}, Column = {Column})";

    public bool IsAdjacentTo(Location otherLocation)
        => Math.Abs(Row - otherLocation.Row) == 1 && Math.Abs(Column - otherLocation.Column) == 1
           || Math.Abs(Row - otherLocation.Row) == 0 && Math.Abs(Column - otherLocation.Column) == 1
           || Math.Abs(Row - otherLocation.Row) == 1 && Math.Abs(Column - otherLocation.Column) == 0;
}
