using TheFinalBattle;

var heroes = new Party(new ComputerPlayer());
heroes.Units.Add(new TrueProgrammer(AskTrueProgrammerName()));

var monsters = new Party(new ComputerPlayer());
monsters.Units.Add(new Skeleton());

var battle = new Battle(heroes, monsters);
battle.Run();

static string AskTrueProgrammerName()
{
    Console.Write("What is the name of the \"True Programmer\"? ");

    return Console.ReadLine() ?? "True Programmer";
}
