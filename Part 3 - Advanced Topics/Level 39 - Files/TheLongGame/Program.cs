Console.WriteLine("Island of IO: The Long Game");
Console.Write("What's your name? ");

string? playerName = Console.ReadLine();

int playerScore = 0;

if (File.Exists($"{playerName}.txt")) { playerScore = int.Parse(File.ReadAllText($"{playerName}.txt")); }
else { playerScore = 0; }

Console.WriteLine("Starting game...\n");

while (true)
{
    Console.WriteLine($"Current keystroke score: {playerScore}.");

    ConsoleKeyInfo enteredKey = Console.ReadKey(true);

    if (enteredKey.Key == ConsoleKey.Enter) { break; }
    else { playerScore++; continue; }
}

File.WriteAllText($"{playerName}.txt", playerScore.ToString());