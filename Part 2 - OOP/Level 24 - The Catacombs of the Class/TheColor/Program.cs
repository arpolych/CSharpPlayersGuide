Color c1 = new() { R = 155, G = 133, B = 222 },
      c2 = Color.Purple;

Console.WriteLine($"C1: R:{c1.R} G:{c1.G} B:{c1.B}");
Console.WriteLine($"C2: R:{c2.R} G:{c2.G} B:{c2.B}");

public class Color
{
    public byte R { get; init; }
    public byte G { get; init; }
    public byte B { get; init; }
    public static Color White => new(255, 255, 255);
    public static Color Black => new(0, 0, 0);
    public static Color Red => new(255, 0, 0);
    public static Color Orange => new(255, 165, 0);
    public static Color Yellow => new(255, 255, 0);
    public static Color Green => new(0, 128, 0);
    public static Color Purple => new(128, 0, 128);

    public Color() : this(0, 0, 0) { }

    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }
}