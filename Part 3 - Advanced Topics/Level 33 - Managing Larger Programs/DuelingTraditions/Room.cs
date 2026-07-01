namespace DuelingTraditions;

public abstract class Room : IRoomSense
{
    public Location Location { get; set; }

    public Room(Location startingLocation) => Location = startingLocation;

    public abstract void DisplayRoomSense(Player player);
}
