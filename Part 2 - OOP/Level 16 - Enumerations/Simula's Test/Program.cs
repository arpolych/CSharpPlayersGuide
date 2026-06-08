ChestState simulasChest = ChestState.Locked;

do
{
    DisplayChestState(simulasChest);

    string userAnswer = Console.ReadLine();

    simulasChest = PerformAction(simulasChest, userAnswer);
} while (true);

void DisplayChestState(ChestState chest)
{
    string chestStateStr = chest switch
    {
        ChestState.Open => "open",
        ChestState.Closed => "unlocked",
        ChestState.Locked => "locked",
        _ => "unknown"
    };

    Console.Write($"The chest is {chestStateStr}. What do you want to do? ");
}

ChestState PerformAction(ChestState currentState, string userAction)
{
    switch (userAction)
    {
        case "close": { if (currentState == ChestState.Open) return ChestState.Closed; break; }
        case "lock": { if (currentState == ChestState.Closed) return ChestState.Locked; break; }
        case "unlock": { if (currentState == ChestState.Locked) return ChestState.Closed; break; }
        case "open": { if (currentState == ChestState.Closed) return ChestState.Open; break; }
        default: { return currentState; }
    }
    return currentState;
}

enum ChestState : int { Open = 0, Closed = 1, Locked = 2 }