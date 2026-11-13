using TheFinalBattle;

// Establish players of the game.
IPlayer heroesPlayer, monstersPlayer;

InitializePlayers(out heroesPlayer, out monstersPlayer);

// Establish parties of the game.
Party heroes, monsterWave1,
      monsterWave2, monsterWave3;

InitializeHeroes(heroesPlayer, out heroes);
InitializeMonsters(monstersPlayer, out monsterWave1, out monsterWave2, out monsterWave3);

// Establish battle of the game!
var battle = new Battle(heroes, monsterWave1, monsterWave2, monsterWave3);
battle.Run();

/*
 * Initialize players...
 */
static void InitializePlayers(out IPlayer heroesPlayer, out IPlayer monstersPlayer)
{
    switch (AskGameplayMode())
    {
        default:
        case 1:
            {
                heroesPlayer = new HumanPlayer(ConsoleColor.DarkCyan, ConsoleColor.Cyan);
                monstersPlayer = new ComputerPlayer(ConsoleColor.DarkRed, ConsoleColor.Red);
                break;
            }
        case 2:
            {
                heroesPlayer = new ComputerPlayer(ConsoleColor.DarkCyan, ConsoleColor.Cyan);
                monstersPlayer = new ComputerPlayer(ConsoleColor.DarkRed, ConsoleColor.Red);
                break;
            }
        case 3:
            {
                heroesPlayer = new HumanPlayer(ConsoleColor.DarkCyan, ConsoleColor.Cyan);
                monstersPlayer = new HumanPlayer(ConsoleColor.DarkRed, ConsoleColor.Red);
                break;
            }
    }

    static int AskGameplayMode()
    {
        ColoredConsole.WriteLine("Choose a gameplay option (1, 2, or 3):", ConsoleColor.DarkYellow);
        Console.WriteLine("1. Human vs Computer");
        Console.WriteLine("2. Computer vs Computer");
        Console.WriteLine("3. Human vs Human");
        ColoredConsole.Write("\nYour choice: ", ConsoleColor.DarkYellow);
        var userInput = ColoredConsole.ReadLine(ConsoleColor.Yellow);

        try
        {
            var gameplayMode = Convert.ToInt32(userInput);

            if (gameplayMode >= 1 && gameplayMode <= 3) return gameplayMode;
            else throw new Exception("Invalid gameplay mode!");
        }
        catch (Exception)
        {
            ColoredConsole.WriteLine(">> Invalid gameplay mode choice, try again!\n", ConsoleColor.Red);

            return AskGameplayMode();
        }
    }
}

static string AskTrueProgrammerName()
{
    ColoredConsole.Write("What is the name of the \"True Programmer\"? ", ConsoleColor.DarkYellow);

    return ColoredConsole.ReadLine(ConsoleColor.Yellow) ?? "True Programmer";
}

static BattleColor AskColorChoice(string message = "\nChoose color: ")
{
    Console.WriteLine("\nAvailable color options:");
    BattleConsoleColors.PrintColorList();

    try
    {
        Console.Write(message);

        int colorIndex = Convert.ToInt32(Console.ReadLine() ?? string.Empty);

        if (colorIndex >= 1 && colorIndex <= BattleConsoleColors.TotalColors)
            return BattleConsoleColors.GetBattleColor(colorIndex);
        else throw new Exception("Invalid color index!");
    }
    catch (Exception)
    {
        ColoredConsole.WriteLine(">> Invalid color index, try again!\n", ConsoleColor.Red);

        return AskColorChoice();
    }
}

static void InitializeHeroes(IPlayer heroesPlayer, out Party heroes)
{
    heroes = new Party(heroesPlayer);
    // Adding units...
    heroes.Units.Add(new TrueProgrammer(AskTrueProgrammerName()));
    heroes.Units.Add(new VinFletcher());
    heroes.UnequippedGear.Add(new Dagger());
    heroes.UnequippedGear.Add(new Dagger());
    heroes.UnequippedGear.Add(new Dagger());
    heroes.UnequippedGear.Add(new Dagger());

    // Adding consumable items...
    heroes.ConsumablesInventory.Add(new HealthPotion());
    heroes.ConsumablesInventory.Add(new HealthPotion());
    heroes.ConsumablesInventory.Add(new HealthPotion());
    heroes.ConsumablesInventory.Add(new SimulasSoup());
    heroes.ConsumablesInventory.Add(new SimulasSoup());
    heroes.ConsumablesInventory.Add(new SimulasSoup());

    // Assign color to heroes
    heroesPlayer.GameColor = AskColorChoice(" >> Enter your color choice for the heroes: ");
}

static void InitializeMonsters(IPlayer monstersPlayer, out Party monstersWave1, out Party monstersWave2, out Party monstersWave3)
{
    // Monster Wave 1
    monstersWave1 = new Party(monstersPlayer);
    // Adding units and consumables...
    var wave1Unit1 = new Skeleton();
    wave1Unit1.SpecialGearInventory.Add(new Dagger());
    monstersWave1.UnequippedGear.Add(new Dagger());
    monstersWave1.UnequippedGear.Add(new Dagger());
    monstersWave1.UnequippedGear.Add(new Dagger());
    monstersWave1.Units.Add(wave1Unit1);
    monstersWave1.Units.Add(new TheHandOfTheUncodedOne());
    monstersWave1.ConsumablesInventory.Add(new HealthPotion());

    // Monster Wave 2
    monstersWave2 = new Party(monstersPlayer);
    // Adding units and consumables...
    monstersWave2.Units.Add(new Skeleton());
    monstersWave2.Units.Add(new Skeleton());
    monstersWave2.ConsumablesInventory.Add(new HealthPotion());
    monstersWave2.UnequippedGear.Add(new Dagger());
    monstersWave2.UnequippedGear.Add(new Dagger());

    // Monster Wave 3
    monstersWave3 = new Party(monstersPlayer);
    // Adding units and consumables...
    monstersWave3.Units.Add(new StoneAmarok());
    monstersWave3.Units.Add(new StoneAmarok());
    monstersWave3.Units.Add(new StoneAmarok());
    monstersWave3.Units.Add(new StoneAmarok());
    monstersWave3.Units.Add(new TheUncodedOne());
    monstersWave3.ConsumablesInventory.Add(new HealthPotion());

    // Assign color to monsters
    monstersPlayer.GameColor = AskColorChoice(" >> Enter your color choice for the monsters: ");
}
