namespace DuelingTraditions;

public class MoveCommand : IPlayerCommand
{
    private readonly GameDirection _direction;

    public MoveCommand(GameDirection direction) => _direction = direction;

    public bool Execute(TheFountainOfObjects gameInstance)
    {
        Location playerLocation = gameInstance.Player.Location,
                 nextPlayerLocation;

        // Calculation of the next location of the player based on the known direction.
        if (_direction == GameDirection.North)
        {
            nextPlayerLocation = playerLocation with { Row = playerLocation.Row - 1 };
        }
        else if (_direction == GameDirection.East)
        {
            nextPlayerLocation = playerLocation with { Column = playerLocation.Column + 1 };
        }
        else if (_direction == GameDirection.South)
        {
            nextPlayerLocation = playerLocation with { Row = playerLocation.Row + 1 };
        }
        else // _direction == MoveDirection.West
        {
            nextPlayerLocation = playerLocation with { Column = playerLocation.Column - 1 };
        }

        // Here we can ask the map if the new location is valid in order to assign it or not.
        if (gameInstance.WorldMap.IsWithinMap(nextPlayerLocation))
        {
            gameInstance.Player.Location = nextPlayerLocation;

            return true;
        }
        else
        {
            ConsoleHelper.DisplayColoredMessage("There is a wall there.", ConsoleColor.Red);

            return false;
        }
    }
}
