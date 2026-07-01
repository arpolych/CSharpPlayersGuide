namespace DuelingTraditions;

public class Map
{
    public Room?[,] Rooms;
    public int Rows { get; }
    public int Columns { get; }

    public Map(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Rooms = new Room[rows, columns];
    }

    public bool IsWithinMap(Location aLocation)
    {
        // Row check...
        if (aLocation.Row < 0 || aLocation.Row >= Rows) return false;
        // Column check...
        else if (aLocation.Column < 0 || aLocation.Column >= Columns) return false;
        // Good to go.
        else return true;
    }

    public Room? GetRoom(Location aLocation)
    {
        if (IsWithinMap(aLocation)) return Rooms[aLocation.Row, aLocation.Column];
        else return null;
    }

    public Location Clamp(Location aLocation)
    {
        int row = Math.Clamp(aLocation.Row, 0, Rows),
            column = Math.Clamp(aLocation.Column, 0, Columns);

        return new Location(row, column);
    }

    public bool MoveRoom(Room aMapRoom, Location newLocation)
    {
        if (IsWithinMap(aMapRoom.Location))
        {
            int roomRow = aMapRoom.Location.Row,
                roomColumn = aMapRoom.Location.Column;

            if (aMapRoom == Rooms[roomRow, roomColumn] && IsWithinMap(newLocation))
            {
                Rooms[roomRow, roomColumn] = null;
                Rooms[newLocation.Row, newLocation.Column] = aMapRoom;
                aMapRoom.Location = newLocation;

                return true;
            }
            else return false;
        }
        else return false;
    }

    public bool DestroyRoom(Location roomLocation)
    {
        if (IsWithinMap(roomLocation))
        {
            Rooms[roomLocation.Row, roomLocation.Column] = null;

            return true;
        }
        else return false;
    }

    public static bool IsPlayerInThisRoom<T>(T aRoom, TheFountainOfObjects gameInstance) where T : Room
    {
        Location playerLocation = gameInstance.Player.Location;

        return aRoom.Location.Equals(playerLocation);
    }
}
