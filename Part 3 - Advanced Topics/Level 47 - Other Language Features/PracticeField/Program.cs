
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
    IEnumerable<int> SingleDigits()
    {
        for (int number = 1; number <= 10; number++)
            yield return number;
    }
}
