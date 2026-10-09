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

static void InitializePlayers(out IPlayer heroesPlayer, out IPlayer monstersPlayer)
{
    switch (AskGameplayMode())
    {
        default:
        case "1":
            {
                heroesPlayer = new HumanPlayer();
                monstersPlayer = new ComputerPlayer();
                break;
            }
        case "2":
            {
                heroesPlayer = new ComputerPlayer();
                monstersPlayer = new ComputerPlayer();
                break;
            }
        case "3":
            {
                heroesPlayer = new HumanPlayer();
                monstersPlayer = new HumanPlayer();
                break;
            }
    }

    static string? AskGameplayMode()
    {
        Console.WriteLine("Choose a gameplay option (1, 2, or 3):");
        Console.WriteLine("1. Player vs Computer");
        Console.WriteLine("2. Computer vs Computer");
        Console.WriteLine("3. Player vs Player");
        Console.Write("\nYour choice: ");

        return Console.ReadLine();
    }
}

static string AskTrueProgrammerName()
{
    Console.Write("What is the name of the \"True Programmer\"? ");

    return Console.ReadLine() ?? "True Programmer";
}

static void InitializeHeroes(IPlayer heroesPlayer, out Party heroes)
{
    heroes = new Party(heroesPlayer);
    heroes.Units.Add(new TrueProgrammer(AskTrueProgrammerName()));
}

static void InitializeMonsters(IPlayer monstersPlayer, out Party monstersWave1, out Party monstersWave2, out Party monstersWave3)
{
    // Monster Wave 1
    monstersWave1 = new Party(monstersPlayer);
    monstersWave1.Units.Add(new Skeleton());

    // Monster Wave 2
    monstersWave2 = new Party(monstersPlayer);
    monstersWave2.Units.Add(new Skeleton());
    monstersWave2.Units.Add(new Skeleton());

    // Monster Wave 3
    monstersWave3 = new Party(monstersPlayer);
    monstersWave3.Units.Add(new TheUncodedOne());
}
