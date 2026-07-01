namespace DuelingTraditions;

public class EnableFountainCommand : IPlayerCommand
{
    public bool Execute(TheFountainOfObjects gameInstance)
    {
        if (Map.IsPlayerInThisRoom(gameInstance.FountainRoom, gameInstance))
        {
            gameInstance.FountainRoom.EnableFountain();

            return true;
        }
        else
        {
            ConsoleHelper.DisplayColoredMessage("The fountain is not in this room. There was no effect.", ConsoleColor.Red);

            return false;
        }
    }
}
