using System.Dynamic;

int robotID = 1;

while (true)
{
    Console.WriteLine($"You are producing robot #{robotID}.");

    dynamic robot = new ExpandoObject();
    robot.ID = robotID;

    Console.Write($"Do you want to name this robot? ");
    if ("yes".Equals(Console.ReadLine()))
    {
        Console.Write($"What is its name? ");
        robot.Name = Console.ReadLine();
    }

    Console.Write($"Does this robot have a specific size? ");
    if ("yes".Equals(Console.ReadLine()))
    {
        Console.Write($"What is its height? ");
        robot.Height = Convert.ToString(Console.ReadLine());

        Console.Write($"What is its width? ");
        robot.Width = Convert.ToString(Console.ReadLine());
    }

    Console.Write($"Does this robot need to be a specific color? ");
    if ("yes".Equals(Console.ReadLine()))
    {
        Console.Write($"What is its color? ");
        robot.Color = Console.ReadLine();
    }

    DisplayRobotDetails(robot);
    robotID++;

    Console.WriteLine();
}

static void DisplayRobotDetails(dynamic robot)
{
    foreach (KeyValuePair<string, object> property in (IDictionary<string, object>)robot)
    {
        Console.WriteLine($"{property.Key}: {property.Value}");
    }
}
