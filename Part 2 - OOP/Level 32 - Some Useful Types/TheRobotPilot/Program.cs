// Game's starting state
int manticoreHP = 10, consolasHP = 15, gameRound = 1;
int manticoreRange = SetUpManticoreDistance();

// Running the game
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

int SetUpManticoreDistance()
{
    int minRange = 0, maxRange = 100;

    return new Random().Next(minRange, maxRange + 1);
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

/*
 * Answer to question:
 * 
 * Firstly, I want to note that the only difference between the single player
 * and the two player game experience is mainly the set-up. So, a simple way
 * would be to have an abstract Game class, and two sub-classes that derive 
 * from it and each implementing their own version of the 'Run" method; which,
 * of course in turn would through polymorphism allow the program to execute
 * the appropriate 'Run" method based on the instance's type.
 */