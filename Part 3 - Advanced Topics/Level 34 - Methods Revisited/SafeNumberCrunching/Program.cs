GetValidUserIntInput(out int userInt);
GetValidUserDoubleInput(out double userDouble);
GetValidUserBoolInput(out bool userBool);

Console.WriteLine($"\nYou've entered:\n  >> int: {userInt}\n  >> double: {userDouble}\n  >> bool: {userBool}.");

static void GetValidUserIntInput(out int intInput)
{
    do
    {
        Console.Write("Please enter an integer number: ");
    } while (!int.TryParse(Console.ReadLine(), out intInput));
}

static void GetValidUserDoubleInput(out double doubleInput)
{
    do
    {
        Console.Write("Please enter a real number: ");
    } while (!double.TryParse(Console.ReadLine(), out doubleInput));
}

static void GetValidUserBoolInput(out bool boolInput)
{
    do
    {
        Console.Write("Please enter an bool value: ");
    } while (!bool.TryParse(Console.ReadLine(), out boolInput));
}
