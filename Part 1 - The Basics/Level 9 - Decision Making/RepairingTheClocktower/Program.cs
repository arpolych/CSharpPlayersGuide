string userInput;

Console.Write("Enter the number please: ");
userInput = Console.ReadLine();

int number = int.Parse(userInput);

if (number % 2 == 0) Console.WriteLine("Tick");
else Console.WriteLine("Tock");
