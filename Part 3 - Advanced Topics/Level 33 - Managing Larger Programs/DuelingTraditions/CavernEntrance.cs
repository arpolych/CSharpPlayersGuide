namespace DuelingTraditions;

public class CavernEntrance : Room
{
    public CavernEntrance(Location startingLocation) : base(startingLocation) { }

    public override void DisplayRoomSense(Player player)
    {
        if (Location.Equals(player.Location))
        {
            ConsoleHelper.DisplayColoredMessage("You see light coming from the cavern entrance.", ConsoleColor.Yellow);
        }
    }
}
