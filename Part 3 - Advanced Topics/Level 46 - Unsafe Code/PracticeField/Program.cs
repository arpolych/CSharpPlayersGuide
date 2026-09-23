RunChapterTopic("Pointer Types", PointerTypes);
RunChapterTopic("Fixed Statements", FixedStatements);

static void RunChapterTopic(string topicName, Action topicFunc)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\t->> Topic: {topicName}\n");
    Console.ForegroundColor = ConsoleColor.White;

    topicFunc();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n\t\t-<< End\n");
    Console.ForegroundColor = ConsoleColor.White;
}

static void PointerTypes()
{
    UnsafeClass.DoSmthUnsafe();
}

static unsafe void FixedStatements()
{
    var p = new Point();
    Console.WriteLine($"Point: {p.X}|{p.Y}");

    fixed (double* x = &p.X)
    {
        (*x)++;
    }
    Console.WriteLine($"Point: {p.X}|{p.Y}");

    fixed (double* x = &p.X, y = &p.Y)
    {
        (*x)++;
        (*y)++;
    }
    Console.WriteLine($"Point: {p.X}|{p.Y}");
}

public unsafe class UnsafeClass
{
    private unsafe int _n;

    public static unsafe void DoSmthUnsafe()
    {
        int x;
        unsafe
        {
            int* p = &x;
            *p = 3;
            Console.WriteLine(p->GetType());
        }
    }
}

public class Point
{
    public double X;
    public double Y;
}