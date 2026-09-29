using System.Runtime.CompilerServices;
using System.Threading.Tasks;

RunChapterTopic("Iterators And The Yield", IteratorsAndTheYield);
RunChapterTopic("Async Enumerables", AsyncEnumerables);
RunChapterTopic("Constants", Constants);
RunChapterTopic("Attributes", Attributes);
RunChapterTopic("Reflection", Reflection);

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
    Type type = typeof(double);
    Type typeOfClass = typeof(MyClass);
}
class MyClass { }