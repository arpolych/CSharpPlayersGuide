namespace DuelingTraditions;

public static class ConsoleHelper
{
    public static void DisplayDashline()
        => Console.WriteLine("----------------------------------------------------------------------------------");

    public static void DisplayColoredMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static string? ReadColoredInput(string message, ConsoleColor color)
    {
        Console.Write(message);
        Console.ForegroundColor = color;
        string? userInput = Console.ReadLine();
        Console.ResetColor();

        return userInput;
    }
}