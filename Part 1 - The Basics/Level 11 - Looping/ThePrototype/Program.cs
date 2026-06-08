int pilotNumber = -1;

do
{
    Console.Write("User 1, enter a number between 0 and 100: ");

    pilotNumber = int.Parse(Console.ReadLine());
} while (pilotNumber < 0 && pilotNumber > 100);

Console.Clear();
Console.WriteLine("User 2, guess the number.");

int userGuess = -1;

do
{
    Console.Write("What is your next guess? ");
    userGuess = int.Parse(Console.ReadLine());

    if (userGuess < pilotNumber) Console.WriteLine($"{userGuess} is too low.");
    else if (userGuess > pilotNumber) Console.WriteLine($"{userGuess} is too high.");
} while (userGuess != pilotNumber);

Console.WriteLine("You guessed the number!");