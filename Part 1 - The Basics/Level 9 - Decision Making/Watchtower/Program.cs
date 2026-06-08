string userInput;

Console.Write("Enter coordinate x: ");
userInput = Console.ReadLine();
int x = int.Parse(userInput);

Console.Write("Enter coordinate y: ");
userInput = Console.ReadLine();
int y = int.Parse(userInput);

if (x < 0 && y > 0) Console.WriteLine("The enemy is to the northwest!");
else if (x == 0 && y > 0) Console.WriteLine("The enemy is to the north!");
else if (x > 0 && y > 0) Console.WriteLine("The enemy is to the northeast!");
else if (x < 0 && y == 0) Console.WriteLine("The enemy is to the west!");
else if (x == 0 && y == 0) Console.WriteLine("The enemy is here!");
else if (x > 0 && y == 0) Console.WriteLine("The enemy is to the east!");
else if (x < 0 && y < 0) Console.WriteLine("The enemy is to the southwest!");
else if (x == 0 && y < 0) Console.WriteLine("The enemy is to the south!");
else Console.WriteLine("The enemy is to the southeast!"); // Case of x > 0 and y < 0.
