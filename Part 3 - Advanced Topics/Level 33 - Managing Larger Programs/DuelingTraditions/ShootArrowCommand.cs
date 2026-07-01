namespace DuelingTraditions;

public class ShootArrowCommand : IPlayerCommand
{
    private readonly GameDirection _direction;

    public ShootArrowCommand(GameDirection direction) => _direction = direction;

    public bool Execute(TheFountainOfObjects gameInstance)
    {
        // Calculate impact location.
        if (!gameInstance.Player.ShootArrow())
        {
            ConsoleHelper.DisplayColoredMessage("You don't have any arrows left!", ConsoleColor.Red);

            return false;
        }
        else
        {
            Location playerLocation = gameInstance.Player.Location,
                     impactLocation;

            if (_direction == GameDirection.North) { impactLocation = playerLocation with { Row = playerLocation.Row - 1 }; }
            else if (_direction == GameDirection.East) { impactLocation = playerLocation with { Column = playerLocation.Column + 1 }; }
            else if (_direction == GameDirection.South) { impactLocation = playerLocation with { Row = playerLocation.Row + 1 }; }
            else /* (_direction == GameDirection.West) */ { impactLocation = playerLocation with { Column = playerLocation.Column - 1 }; }

            // Now I can check for room monster damages...
            Room? impactRoom = gameInstance.WorldMap.GetRoom(impactLocation);

            if (impactRoom is IRoomMonsterEffect impactRoomMonster)
            {
                ConsoleHelper.DisplayColoredMessage("You fire an arrow and hit something!", ConsoleColor.Green);

                return impactRoomMonster.ReceivedPlayerHit(gameInstance);
            }
            else
            {
                ConsoleHelper.DisplayColoredMessage("You fire an arrow and missed.", ConsoleColor.Magenta);

                return false;
            }
        }
    }
}
