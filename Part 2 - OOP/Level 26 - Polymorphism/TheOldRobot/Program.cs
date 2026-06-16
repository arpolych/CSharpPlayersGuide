Robot myRobot = new();

myRobot.Commands[0] = GetRobotCommand(Console.ReadLine());
myRobot.Commands[1] = GetRobotCommand(Console.ReadLine());
myRobot.Commands[2] = GetRobotCommand(Console.ReadLine());

myRobot.Run();

static RobotCommand? GetRobotCommand(string? command)
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
    public RobotCommand?[] Commands { get; } = new RobotCommand?[3];

    public void Run()
    {
        foreach (RobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public abstract class RobotCommand { public abstract void Run(Robot robot); }

public class OnCommand : RobotCommand { public override void Run(Robot robot) => robot.IsPowered = true; }

public class OffCommand : RobotCommand { public override void Run(Robot robot) => robot.IsPowered = false; }

public class NorthCommand : RobotCommand { public override void Run(Robot robot) { if (robot.IsPowered) robot.Y++; } }

public class SouthCommand : RobotCommand { public override void Run(Robot robot) { if (robot.IsPowered) robot.Y--; } }

public class WestCommand : RobotCommand { public override void Run(Robot robot) { if (robot.IsPowered) robot.X--; } }

public class EastCommand : RobotCommand { public override void Run(Robot robot) { if (robot.IsPowered) robot.X++; } }