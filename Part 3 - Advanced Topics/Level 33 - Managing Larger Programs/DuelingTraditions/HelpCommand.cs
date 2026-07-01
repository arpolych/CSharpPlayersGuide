namespace DuelingTraditions;

public class HelpCommand : IPlayerCommand
{
    public bool Execute(TheFountainOfObjects gameInstance)
    {
        ConsoleHelper.DisplayColoredMessage(">> 'move north':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Moves player by one space to the north. ");
        ConsoleHelper.DisplayColoredMessage(">> 'move east':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Moves player by one space to the east.");
        ConsoleHelper.DisplayColoredMessage(">> 'move south':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Moves player by one space to the south.");
        ConsoleHelper.DisplayColoredMessage(">> 'move west':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Moves player by one space to the west.");
        ConsoleHelper.DisplayColoredMessage(">> 'shoot north':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Shoots an arrow towards the north.");
        ConsoleHelper.DisplayColoredMessage(">> 'shoot east':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Shoots an arrow towards the east.");
        ConsoleHelper.DisplayColoredMessage(">> 'shoot south':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Shoots an arrow towards the south.");
        ConsoleHelper.DisplayColoredMessage(">> 'shoot west':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Shoots an arrow towards the west.");
        ConsoleHelper.DisplayColoredMessage(">> 'enable fountain':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Activates the fountain of objects.");
        ConsoleHelper.DisplayColoredMessage(">> 'help':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Displays all in-game player commands.");

        return true;
    }
}
