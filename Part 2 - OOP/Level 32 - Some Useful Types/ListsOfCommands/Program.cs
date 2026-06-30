Robot myRobot = new();
string userInput;

while (true)
{
    userInput = Console.ReadLine() ?? "inputError";

    if (userInput.Equals("stop")) break;
    else myRobot.Commands.Add(GetRobotCommand(userInput));
}

myRobot.Run();

static IRobotCommand? GetRobotCommand(string? command)
    => command switch
    {
        "on" => new OnCommand(),
        "off" => new OffCommand(),
        "north" => new NorthCommand(),
        "south" => new SouthCommand(),
        "west" => new WestCommand(),
        "east" => new EastCommand(),
        _ => null
    };

public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public List<IRobotCommand?> Commands { get; } = new();

    public void Run()
    {
        foreach (IRobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public interface IRobotCommand { void Run(Robot robot); }

public class OnCommand : IRobotCommand { public void Run(Robot robot) => robot.IsPowered = true; }

public class OffCommand : IRobotCommand { public void Run(Robot robot) => robot.IsPowered = false; }

public class NorthCommand : IRobotCommand { public void Run(Robot robot) { if (robot.IsPowered) robot.Y++; } }

public class SouthCommand : IRobotCommand { public void Run(Robot robot) { if (robot.IsPowered) robot.Y--; } }

public class WestCommand : IRobotCommand { public void Run(Robot robot) { if (robot.IsPowered) robot.X--; } }

public class EastCommand : IRobotCommand { public void Run(Robot robot) { if (robot.IsPowered) robot.X++; } }

/*
 * Answer to question:
 * 
 * I feel an interface is more suitable in this case, because the IRobotCommand was
 * setting a role for anyone who implements it. I mean that it defines the way a 
 * class implementing the interface to run a robotic command.
 *  +++ Reading the author's answer I realize that inheritance is a much stronger 
 *  relation that simply an interface implementation.
 */