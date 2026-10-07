using TheFinalBattle;

// Create characters
var gameChar1 = new TrueProgrammer(AskPlayerName());
var gameChar2 = new Skeleton();

// Create parties
var heroes = new Party();
var monsters = new Party();
heroes.AddCharacter(gameChar1);
monsters.AddCharacter(gameChar2);

// Create players
var player1 = new Player(heroes);
var player2 = new Player(monsters);

// Create battle
var battle = new Battle(player1, player2);
battle.Run();

static string AskPlayerName()
{
    Console.Write("What's the name of our True Programmer hero? ");

    return Console.ReadLine() ?? "True Programmer";
}
