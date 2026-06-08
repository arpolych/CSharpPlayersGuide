string userInput;

Console.WriteLine("How many are your provinces?");
userInput = Console.ReadLine();
int provinces = Convert.ToInt32(userInput);

Console.WriteLine("How many are your duchies?");
userInput = Console.ReadLine();
int duchies = Convert.ToInt32(userInput);

Console.WriteLine("How many are your estates?");
userInput = Console.ReadLine();
int estates = Convert.ToInt32(userInput);

int totalScore = (provinces * 6) + (duchies * 3) + estates;

Console.WriteLine("The total score is: " + totalScore + ".");