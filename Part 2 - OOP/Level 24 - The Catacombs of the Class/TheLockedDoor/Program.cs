Console.Write("Set passcode for the door: ");
Door newDoor = new(Console.ReadLine());
Console.Clear();

while (true)
{
    DisplayDoorCommands(newDoor);
    RunDoorCommand(Console.ReadLine(), newDoor);
}

void DisplayDoorCommands(Door aDoor)
{
    Console.WriteLine("-----------------------------");
    Console.WriteLine("Available door commands:");
    Console.WriteLine("Open");
    Console.WriteLine("Close");
    Console.WriteLine("Lock");
    Console.WriteLine("Unlock");
    Console.WriteLine("ChangePasscode");
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"Current door state: {aDoor.CurrentState}");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("What do you want to do? ");
    Console.ForegroundColor = ConsoleColor.White;
}

void RunDoorCommand(string? command, Door aDoor)
{
    if (command == null) return;
    else if (command != "ChangePasscode")
    {
        if (aDoor.PerformDoorCommand(command))
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Transition successful!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Transition failed!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    else
    {
        Console.Write("Door's passcode: ");
        string? userCurrentPasscodeInput = Console.ReadLine();

        Console.Write("Door's new passcode: ");
        string? userNewPasscodeInput = Console.ReadLine();

        if (aDoor.ChangePasscode(userCurrentPasscodeInput, userNewPasscodeInput))
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Passcode changed!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Passcode error!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

public class Door
{
    public DoorState CurrentState { get; private set; } = DoorState.Locked;
    private string Passcode { get; set; }

    public Door(string? passcode)
    {
        Passcode = passcode ?? "1234";
    }

    public bool PerformDoorCommand(string? userAction)
    {
        switch (userAction)
        {
            case "Close":
                {
                    if (CurrentState == DoorState.Open) { CurrentState = DoorState.Closed; return true; }
                    else return false;
                }
            case "Lock":
                {
                    if (CurrentState == DoorState.Closed) { CurrentState = DoorState.Locked; return true; }
                    else return false;
                }
            case "Unlock":
                {
                    if (CurrentState == DoorState.Locked)
                    {
                        // Needs passcode to unlock door...
                        Console.Write("Door's passcode: ");
                        string? userInput = Console.ReadLine();

                        if (userInput == Passcode)
                        {
                            CurrentState = DoorState.Closed;

                            return true;
                        }
                    }

                    return false;
                }
            case "Open":
                {
                    if (CurrentState == DoorState.Closed) { CurrentState = DoorState.Open; return true; }
                    else return false;
                }
            default: { return false; }
        }
    }

    public bool ChangePasscode(string? currentPasscodeUserInput, string? newPasscodeUserInput)
    {
        if (Passcode == currentPasscodeUserInput)
        {
            Passcode = newPasscodeUserInput ?? "1234";
            return true;
        }

        return false;
    }
}

public enum DoorState : int { Open = 0, Closed = 1, Locked = 2 }
