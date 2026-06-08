Console.WriteLine("Recursive countdown begins...");

Countdown(10);

void Countdown(int number)
{
    if (number > 0)
    {
        Console.WriteLine($"{number}...");
        Countdown(--number);
    }
    else Console.WriteLine("The recursive countdown has completed!");
}
