Console.Title = "Defense of Consolas";

string userInput;

Console.Write("Target Row? ");
userInput = Console.ReadLine();
int row = int.Parse(userInput);

Console.Write("Target Column? ");
userInput = Console.ReadLine();
int column = int.Parse(userInput);

Console.WriteLine("Deploy to:");
Console.WriteLine($"({row}, {column - 1})");
Console.WriteLine($"({row - 1}, {column})");
Console.WriteLine($"({row}, {column + 1})");
Console.WriteLine($"({row + 1}, {column})");

Console.Beep(440, 1500);