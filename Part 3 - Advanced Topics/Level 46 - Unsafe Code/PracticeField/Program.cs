using System.Runtime.InteropServices;

RunChapterTopic("Pointer Types", PointerTypes);
RunChapterTopic("Fixed Statements", FixedStatements);
RunChapterTopic("Stack Allocations", StackAllocations);
RunChapterTopic("Fixed Size Arrays", FixedSizeArrays);
RunChapterTopic("Size Of Operator", SizeOfOperator);
RunChapterTopic("Calling Native Code", CallingNativeCode);

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

static unsafe void StackAllocations()
{
    int* arrInStack = stackalloc int[5];

    arrInStack[0] = 10;
    *arrInStack += 10;

    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine(arrInStack[i]);
    }

    *arrInStack += 100;

    Console.WriteLine(arrInStack[0]);
}

static unsafe void FixedSizeArrays()
{
    UnsafeStruct s = new();

    Console.WriteLine(s.val1);
    Console.WriteLine(s.MoreValues[0]);
}

static unsafe void SizeOfOperator()
{
    byte[] byteArr = new byte[sizeof(int) * 4];

    Console.WriteLine("Size of double: " + sizeof(double));
    Console.WriteLine("Size of my custom struct: " + sizeof(LongAndBool));
}

static unsafe void CallingNativeCode()
{
    try
    {
        int result = DllWrapper.Add(3, 4);
    }
    catch (Exception)
    {
        Console.WriteLine("Sry, but the .dll you called doesn't exist yet ;(");
    }
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

public unsafe struct UnsafeStruct
{
    public int val1;
    public int val2;
    public fixed int MoreValues[10];
}

struct LongAndBool
{
    long l;
    bool b;
}

public static class DllWrapper
{
    [DllImport("MyDll.dll", EntryPoint = "add")]
    internal static extern int Add(int a, int b);
}