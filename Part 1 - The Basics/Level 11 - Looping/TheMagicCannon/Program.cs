for (int i = 1; i <= 100; i++)
{
    // When both activate, the fire blast with the electric blast, we get a combined blast.
    if (i % 3 == 0 && i % 5 == 0)
    {
        // Combined Blast (Electric + Fire)
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{i}: Combined");
    }
    // Every 5th turn of the crank activates an electric blast.
    else if (i % 5 == 0)
    {
        // Electric Blast
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{i}: Electric");
    }
    // Every 3rd turn of the crank activates a fire blast.
    else if (i % 3 == 0)
    {
        // Fire Blast
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{i}: Fire");
    }
    else
    {
        // Normal Blast
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{i}: Normal");
    }
}

Console.ForegroundColor = ConsoleColor.White;
