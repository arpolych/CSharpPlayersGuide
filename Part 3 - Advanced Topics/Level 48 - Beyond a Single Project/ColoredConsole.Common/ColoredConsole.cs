public static class ColoredConsole
{
    public static string Prompt(string question)
    {
        Console.Write($"{question}");
        Console.ForegroundColor = ConsoleColor.Cyan;

        string response = Console.ReadLine()!;

        Console.ForegroundColor = ConsoleColor.White;

        return response;
    }

    public static void WriteLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"{text}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Write(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write($"{text}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}