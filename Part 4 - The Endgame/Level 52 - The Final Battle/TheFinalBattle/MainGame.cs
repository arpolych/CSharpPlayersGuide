using TheFinalBattle;

var gameChar1 = new Player(AskPlayerName());
var gameChar2 = new Skeleton();

var heroes = new Party();
var monsters = new Party();
heroes.AddCharacter(gameChar1);
monsters.AddCharacter(gameChar2);

var battle = new Battle(heroes, monsters);
battle.Run();

static string AskPlayerName()
{
    Console.Write("What's the name of our True Programmer hero? ");

    return Console.ReadLine() ?? "True Programmer";
}
