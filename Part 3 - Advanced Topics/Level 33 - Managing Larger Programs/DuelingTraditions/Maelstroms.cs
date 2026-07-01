namespace DuelingTraditions;

public class Maelstroms : Room, IRoomMonsterEffect
{
    public bool Alive { get; private set; } = true;

    public Maelstroms(Location startingLocation) : base(startingLocation) { }

    public bool ApplyWorldEffect(TheFountainOfObjects gameInstance)
    {
        Player player = gameInstance.Player;

        if (Location.Equals(player.Location))
        {
            ConsoleHelper.DisplayColoredMessage("You have encountered a maelstrom and have been swept away to another room!", ConsoleColor.DarkMagenta);
            // Calculate new locations of the player and the maelstroms.
            Location nextPlayerLocation = Location with { Row = Location.Row - 1, Column = Location.Column + 2 },
                     nextMaelstromLocation = Location with { Row = Location.Row + 1, Column = Location.Column - 2 };

            // Clamp the new locations and move the room in the worldmap.
            player.Location = gameInstance.WorldMap.Clamp(nextPlayerLocation);
            nextMaelstromLocation = gameInstance.WorldMap.Clamp(nextMaelstromLocation);
            gameInstance.WorldMap.MoveRoom(this, nextMaelstromLocation);

            return true;
        }
        else return false;
    }

    public override void DisplayRoomSense(Player player)
    {
        if (Location.IsAdjacentTo(player.Location))
        {
            ConsoleHelper.DisplayColoredMessage("You hear the growling and groaning of a maelstrom nearby.", ConsoleColor.Magenta);
        }
    }

    public bool ReceivedPlayerHit(TheFountainOfObjects gameInstance)
    {
        if (Alive)
        {
            Alive = false;
            gameInstance.WorldMap.DestroyRoom(Location);

            return true;
        }
        else return false;
    }
}
