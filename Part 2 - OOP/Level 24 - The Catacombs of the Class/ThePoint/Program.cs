Point p1 = new(2, 3),
      p2 = new() { X = -4, Y = 0 };

Console.WriteLine($"P1: ({p1.X}, {p1.Y})");
Console.WriteLine($"P2: ({p2.X}, {p2.Y})");

public class Point
{
    public double X { get; init; }
    public double Y { get; init; }

    public Point() : this(0, 0) { }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}
/*
 * Answer to question:
 * 
 * My X and Y properties are immutable, because I didn't have a plan
 * to allow them to be changed from outside the class in any way.
 */