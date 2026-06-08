// Game's starting state
int manticoreHP = 10, consolasHP = 15, gameRound = 1;
int manticoreRange = SetUpGame();

// Running the game
Console.WriteLine("Player 2, it is your turn.");

int cannonRoundDamage;
while (consolasHP > 0)
{
    Console.WriteLine("------------------------------------------------------------");
    cannonRoundDamage = GetCannonDamage();
    DisplayGameStatus(gameRound, consolasHP, manticoreHP);
    AttackManticore();

    if (manticoreHP > 0)
    {
        consolasHP--;
    }
    else
    {
        Console.WriteLine("The Manticore has been destroyed! The city of Consolas has been saved!");
        return;
    }

    gameRound++;
}

Console.WriteLine("The city of Consolas has fallen! You failed to save the city!");

int SetUpGame()
{
    string gameMessage = "Player 1, how far away from the city do you want to station the Manticore (0 and 100)? ";
    int manticoreRange = AskForNumberInrage(gameMessage, 0, 100);
    Console.Clear();
    return manticoreRange;
}

int AskForNumberInrage(string text, int min, int max)
{
    int userNumber = min - 1;

    while (userNumber < min || userNumber > max)
    {
        Console.Write(text);
        userNumber = int.Parse(Console.ReadLine());
    }

    return userNumber;
}

int GetCannonDamage()
{
    if (gameRound % 3 == 0 && gameRound % 5 == 0) return 10;
    else if (gameRound % 3 == 0 || gameRound % 5 == 0) return 3;
    else return 1;
}

void DisplayGameStatus(int gameRound, int citysHP, int manticoreHP)
{
    Console.WriteLine(@$"STATUS: Round: {gameRound}  City: {citysHP}/15  Manticore: {manticoreHP}/10");

    if (cannonRoundDamage == 10) Console.ForegroundColor = ConsoleColor.Blue;
    else if (cannonRoundDamage == 3) Console.ForegroundColor = ConsoleColor.Green;
    else Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine($"The cannon is expected to deal {cannonRoundDamage} damage this round.");
    Console.ForegroundColor = ConsoleColor.White;
}

void AttackManticore()
{
    // Get target range
    Console.Write("Enter desired cannon range: ");
    int targetRange = Convert.ToInt32(Console.ReadLine());

    if (manticoreRange == targetRange)
    {
        Console.WriteLine("That round was a DIRECT HIT!");
        manticoreHP -= GetCannonDamage();
    }
    else if (targetRange < manticoreRange) Console.WriteLine("That round FELL SHORT of the target.");
    else Console.WriteLine("That round OVERSHOT the target.");
}
