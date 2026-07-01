namespace DuelingTraditions;

public class FountainRoom : Room
{
    public bool Activated { get; private set; } = false;

    public FountainRoom(Location startingLocation) : base(startingLocation) { }

    public override void DisplayRoomSense(Player player)
    {
        if (Location.Equals(player.Location))
        {
            string message;

            if (!Activated) message = "You hear water dripping in this room. The Fountain of Objects is here!";
            else message = "You hear the rushing waters from the Fountain of Objects. It has been reactivated!";

            ConsoleHelper.DisplayColoredMessage(message, ConsoleColor.Blue);
        }
    }

    public bool EnableFountain()
    {
        if (!Activated)
        {
            Activated = true;

            return true;
        }
        else return false;
    }
}
