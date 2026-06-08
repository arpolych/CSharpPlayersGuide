string userInput;

Console.WriteLine("How many chocolate eggs were gathered?");
userInput = Console.ReadLine();
int eggsGathered = Convert.ToInt32(userInput);

int eggsPerSister = eggsGathered / 4;
int eggsForDuckbear = eggsGathered % 4;

Console.WriteLine("The sisters will get " + eggsPerSister + " each.");
Console.WriteLine("The duckbear will get " + eggsForDuckbear + ".");

/*
 * Answer to question:
 * 
 * It's when the egg count is 1, 2, 3, 6, and 11.
 */