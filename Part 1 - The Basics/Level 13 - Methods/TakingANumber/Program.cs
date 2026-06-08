// This is my program from Level 11 - Looping
int pilotNumber = AskForNumberInrage("User 1, enter a number between 0 and 100: ", 0, 100);

Console.Clear();
Console.WriteLine("User 2, guess the number.");

int userGuess = pilotNumber - 1;

do
{
    userGuess = AskForNumber("What is your next guess? ");

    if (userGuess < pilotNumber) Console.WriteLine($"{userGuess} is too low.");
    else if (userGuess > pilotNumber) Console.WriteLine($"{userGuess} is too high.");
} while (userGuess != pilotNumber);

Console.WriteLine("You guessed the number!");

int AskForNumber(string text)
{
    Console.Write(text);

    return int.Parse(Console.ReadLine());
}

int AskForNumberInrage(string text, int min, int max)
{
    int userNumber = min - 1;

    while (userNumber < min || userNumber > max)
    {
        Console.Write(text);
        userNumber = int.Parse(Console.ReadLine());
    }

    return userNumber;
}
