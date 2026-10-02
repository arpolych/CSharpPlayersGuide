using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

RunChapterTopic("Iterators And The Yield", IteratorsAndTheYield);
RunChapterTopic("Async Enumerables", AsyncEnumerables);
RunChapterTopic("Constants", Constants);
RunChapterTopic("Attributes", Attributes);
RunChapterTopic("Reflection", Reflection);
RunChapterTopic("Name Of Operator", NameOfOperator);
RunChapterTopic("Bit Manipulations", BitManipulations);

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

static void IteratorsAndTheYield()
{
    var myNums = SingleDigits();

    foreach (int number in myNums)
    {
        Console.WriteLine($"First loop: {number}");
        if (number == 5) break;
    }

    foreach (int number in myNums)
    {
        Console.WriteLine($"Second loop: {number}");
        // Thread.Sleep(3_000);
    }

    /*
     * var nums = AlternatingPattern().ToList();
     * Console.WriteLine("Shall never get here!");
     */

    Console.WriteLine("Welcome to infinite loop!");
    foreach (int n in AlternatingPattern())
    {
        Console.WriteLine($"Third loop: {n}");
        Thread.Sleep(1_500);
        break;
    }

    static IEnumerable<int> SingleDigits()
    {
        for (int number = 1; number <= 10; number++)
            yield return number;
        yield break;
    }

    static IEnumerable<int> AlternatingPattern()
    {
        while (true)
        {
            yield return +1;
            yield return -1;
        }
    }
}

static async void AsyncEnumerables()
{
    string[] urls = new string[] {
        "http://google.com",
        "http://amazon.com",
        "http://microsoft.com"};

    await foreach (string url in GetManySiteContents(urls))
    {
        Console.WriteLine(url);
    }

    static async Task<string> GetSiteContents(string url)
    {
        await Task.Delay(3_000);
        return "content ;)";
    }

    static async IAsyncEnumerable<string> GetManySiteContents(string[] manyUrls)
    {
        foreach (string url in manyUrls)
        {
            yield return await GetSiteContents(url);
        }
    }
}

static void Constants()
{
    const double TwoPi = 2 * Math.PI;

    Console.WriteLine($"This is my 2-pies number: {TwoPi}");
}

static void Attributes()
{
    OldDeadMethod();
}

[Obsolete("Use NewDeadMethod", false)]
static void OldDeadMethod()
{
    Console.WriteLine("I'm too old to function...");
}

static void Reflection()
{
    Type type = typeof(int);
    Type typeOfClass = typeof(MyClass);
    var myObjClass = new MyClass();
    var typeOfMyObj = myObjClass.GetType();
    ConstructorInfo[] constructors = typeOfMyObj.GetConstructors();
    MethodInfo[] methods = typeOfMyObj.GetMethods();
    ConstructorInfo? specificConstructor = typeOfMyObj.GetConstructor(new Type[] { typeof(int), typeof(double) });
    MethodInfo? specificMethod = typeOfMyObj.GetMethod("MethodName", new Type[] { typeof(int) });
    object? newObj = specificConstructor?.Invoke(new object[] { 17 });
    specificMethod?.Invoke(newObj, new object[] { 4 });
}

static void NameOfOperator()
{
    DisplayNumbersAB(4, 5);
    DisplayNumbersFirstSecond(4, 5);
    DisplayNumbersNameOf(4, 5);

    void DisplayNumbersAB(int a, int b) => Console.WriteLine($"Display nums AB: a={a} and b={b}");
    void DisplayNumbersFirstSecond(int first, int second) => Console.WriteLine($"Display first and second: a={first} and b={second}");
    void DisplayNumbersNameOf(int nameOfFirst, int nameOfSecond) => Console.WriteLine($"Display nameof parameters: {nameof(nameOfFirst)}={nameOfFirst} and {nameof(nameOfSecond)}={nameOfSecond}");
}

static void BitManipulations()
{
    int controllerState = 0b00010001;
    int shiftedLeft = controllerState << 2;
    int shiftedRight = controllerState >> 3;

    Console.WriteLine($"Original bit state: {controllerState}");
    Console.WriteLine($"Left shifted << 2: {shiftedLeft}");
    Console.WriteLine($"Right shifted >> 3: {shiftedRight}");
    Console.WriteLine($"bitwise-and: {shiftedLeft & shiftedRight}");
    Console.WriteLine($"bitwise-or: {shiftedLeft | shiftedRight}");
    Console.WriteLine($"exclusive-bitwise-or: {shiftedRight ^ shiftedRight}");
    Console.WriteLine($"bitwise-complement: {~shiftedRight}");

    Buttons state = Buttons.Start;
    Console.WriteLine($"Custom enum : byte w/[Flags]: {state}");
}

[Flags]
public enum Buttons : byte
{
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    A = 1 << 4,
    B = 1 << 5,
    Start = 1 << 6,
    Select = 1 << 7
}

class MyClass { }

public class Door
{
    private enum DoorState { Open, Closed, Locked }

    private DoorState _doorState = DoorState.Closed;

    public bool IsOpen => _doorState == DoorState.Open;
    public bool IsLocked => _doorState == DoorState.Locked;
}
