TheFountainOfObjects mainGame = new(GetGameSize());

mainGame.Run();

static GameSize GetGameSize()
{
    string playerInput;

    do
    {
        playerInput = ConsoleHelper.ReadColoredInput("Do you want to play a small, medium, or large game? ", ConsoleColor.Cyan) ?? "inputError";

        switch (playerInput)
        {
            case "small": { return GameSize.Small; }
            case "medium": { return GameSize.Medium; }
            case "large": { return GameSize.Large; }
            default: { ConsoleHelper.DisplayColoredMessage($"'{playerInput}' doesn't match any of the specified sizes.", ConsoleColor.Red); continue; }
        }
    } while (true);
}

public class TheFountainOfObjects
{
    public Player Player { get; }
    public Map WorldMap { get; }
    public FountainRoom FountainRoom { get; }
    public CavernEntrance CavernEntrance { get; }

    public TheFountainOfObjects(GameSize option)
    {
        if (option == GameSize.Small)
        {
            int rows = 4, columns = 4;
            WorldMap = new Map(rows, columns);

            CavernEntrance = new CavernEntrance(new Location(0, 0));
            FountainRoom = new FountainRoom(new Location(0, 2));

            FillSmallGameWorldMap();
        }
        else if (option == GameSize.Medium)
        {
            int rows = 6, columns = 6;
            WorldMap = new Map(rows, columns);

            CavernEntrance = new CavernEntrance(new Location(5, 0));
            FountainRoom = new FountainRoom(new Location(2, 4));

            FillMediumGameWorldMap();
        }
        else /* option == GameSize.Large */
        {
            int rows = 8, columns = 8;
            WorldMap = new Map(rows, columns);

            CavernEntrance = new CavernEntrance(new Location(3, 7));
            FountainRoom = new FountainRoom(new Location(4, 2));

            FillLargeGameWorldMap();
        }

        // Adding base game rooms in the world map.
        WorldMap.Rooms[CavernEntrance.Location.Row, CavernEntrance.Location.Column] = CavernEntrance;
        WorldMap.Rooms[FountainRoom.Location.Row, FountainRoom.Location.Column] = FountainRoom;

        Player = new(CavernEntrance.Location, Quiver._defaultArrowCapacity);
    }

    private void FillSmallGameWorldMap()
    {
        // Adding pit rooms in the world map.
        WorldMap.Rooms[3, 2] = new Pit(new Location(3, 2));

        // Adding maelstroms rooms in the world map.
        WorldMap.Rooms[2, 0] = new Maelstroms(new Location(2, 0));

        // Adding amaroks rooms in the world map.
        WorldMap.Rooms[0, 3] = new Amaroks(new Location(0, 3));
    }

    private void FillMediumGameWorldMap()
    {
        // Adding pit rooms in the world map.
        WorldMap.Rooms[0, 2] = new Pit(new Location(0, 2));
        WorldMap.Rooms[3, 0] = new Pit(new Location(3, 0));

        // Adding maelstroms rooms in the world map.
        WorldMap.Rooms[2, 2] = new Maelstroms(new Location(2, 2));

        // Adding amaroks rooms in the world map.
        WorldMap.Rooms[1, 0] = new Amaroks(new Location(1, 0));
        WorldMap.Rooms[5, 3] = new Amaroks(new Location(5, 3));
    }

    private void FillLargeGameWorldMap()
    {
        // Adding pit rooms in the world map.
        WorldMap.Rooms[0, 5] = new Pit(new Location(0, 5));
        WorldMap.Rooms[3, 2] = new Pit(new Location(3, 2));
        WorldMap.Rooms[4, 5] = new Pit(new Location(4, 5));
        WorldMap.Rooms[7, 0] = new Pit(new Location(7, 0));

        // Adding maelstroms rooms in the world map.
        WorldMap.Rooms[1, 3] = new Maelstroms(new Location(1, 3));
        WorldMap.Rooms[5, 5] = new Maelstroms(new Location(5, 5));

        // Adding amaroks rooms in the world map.
        WorldMap.Rooms[1, 1] = new Amaroks(new Location(1, 1));
        WorldMap.Rooms[5, 2] = new Amaroks(new Location(5, 2));
        WorldMap.Rooms[7, 5] = new Amaroks(new Location(7, 5));
    }

    public void Run()
    {
        DateTime gameStartTime = DateTime.Now;
        DisplayGameIntro();

        while (Player.Alive && !IsPlayerVictorious())
        {
            ConsoleHelper.DisplayDashline();
            ConsoleHelper.DisplayColoredMessage($"You are in the room at {Player.Location}.", ConsoleColor.Gray);
            ConsoleHelper.DisplayColoredMessage($"You have {Player.Quiver.CurrentCapacity} arrows remaining.", ConsoleColor.Gray);
            DisplayRoomSenses();
            GetPlayerCommand().Execute(this);

            Room? currentRoom = WorldMap.GetRoom(Player.Location);

            if (currentRoom is IRoomWorldEffect currentRoomWorldEffect)
            {
                currentRoomWorldEffect.ApplyWorldEffect(this);
            }
        }

        DateTime gameEndTime = DateTime.Now;
        TimeSpan elapsedGameTime = gameEndTime - gameStartTime;

        if (Player.Alive)
        {
            string victoryMessage = "The Fountain of Objects has been reactivated, and you have escaped with your life!\nYou win!";
            ConsoleHelper.DisplayColoredMessage(victoryMessage, ConsoleColor.Green);
        }

        ConsoleHelper.DisplayColoredMessage($"Elapsed game time: {elapsedGameTime.TotalSeconds:0.00}(s)", ConsoleColor.DarkCyan);
    }

    public bool IsPlayerVictorious()
        => FountainRoom.Activated && Map.IsPlayerInThisRoom<CavernEntrance>(CavernEntrance, this);

    public static void DisplayGameIntro()
    {
        Console.WriteLine("----------------------------------------------------------------------------------");
        Console.WriteLine("* You enter the Cavern of Objects, a maze of rooms filled with dangerous pits in \nsearch of the Fountain of Objects");
        Console.WriteLine("* Light is visible only in the entrance, and no other light is seen anywhere in \nthe caverns.");
        Console.WriteLine("* You must navigate the Caverns with your other senses.");
        Console.WriteLine("* Find the Fountain of Objects, activate it, and return to the entrance.");
        Console.WriteLine("----------------------------------------------------------------------------------");
        Console.WriteLine("  >> Look out for pits. You will feel a breeze if a pit is in an adjacent room. \n" +
                          "     If you enter a room with a pit, you will die.");
        Console.WriteLine("  >> Maelstroms are violent forces of sentient wind. Entering a room with one could\n" +
                          "     transport you to any other location in the caverns. You will be able to hear\n" +
                          "     their growling and groaning in the nearby rooms.");
        Console.WriteLine("  >> Amaroks roam the caverns. Encountering one is certain death, but you can smell\n" +
                          "     their rotten stench in nearby rooms.");
        Console.WriteLine("  >> You carry with you a bow and a quiver of arrows. You can use them to shoot \n" +
                          "     monsters in the caverns but be warned: you have a limited supply.");
    }

    public void DisplayRoomSenses()
    {
        Location playerLocation = Player.Location;
        WorldMap.GetRoom(playerLocation)?.DisplayRoomSense(Player);

        for (int i = 0; i < WorldMap.Rooms.GetLength(0); i++)
        {
            for (int j = 0; j < WorldMap.Rooms.GetLength(1); j++)
            {
                if (!WorldMap.Rooms[i, j]?.Location.Equals(playerLocation) ?? false)
                {
                    WorldMap.Rooms[i, j]!.DisplayRoomSense(Player);
                }
            }
        }
    }

    public static IPlayerCommand GetPlayerCommand()
    {
        string playerInput;

        do
        {
            playerInput = ConsoleHelper.ReadColoredInput("What do you want to do? ", ConsoleColor.Cyan) ?? "inputError";

            switch (playerInput)
            {
                // Here are the valid commands of the IPlayerCommand interface
                case "move north": { return new MoveCommand(GameDirection.North); }
                case "move east": { return new MoveCommand(GameDirection.East); }
                case "move south": { return new MoveCommand(GameDirection.South); }
                case "move west": { return new MoveCommand(GameDirection.West); }
                case "shoot north": { return new ShootArrowCommand(GameDirection.North); }
                case "shoot east": { return new ShootArrowCommand(GameDirection.East); }
                case "shoot south": { return new ShootArrowCommand(GameDirection.South); }
                case "shoot west": { return new ShootArrowCommand(GameDirection.West); }
                case "enable fountain": { return new EnableFountainCommand(); }
                case "help": { return new HelpCommand(); }
                // Anything unrecognized goes in here...
                default: { ConsoleHelper.DisplayColoredMessage($"The '{playerInput}' is not a valid player command...", ConsoleColor.Red); continue; }
            }
        }
        while (true);
    }
}

public enum GameSize { Small, Medium, Large }

public class Player
{
    public bool Alive { get; private set; } = true;
    public Quiver Quiver { get; }

    public Location Location { get; set; }

    public Player(Location startingLocation, int arrowCapacity)
    {
        Location = startingLocation;
        Quiver = new(arrowCapacity);
    }

    public bool Kill()
    {
        if (Alive)
        {
            Alive = false;

            return true;
        }
        else return false;
    }

    public bool ShootArrow() => Quiver.UseArrow();
}

public class Quiver
{
    public static readonly int _defaultArrowCapacity = 5;
    public int ArrowCapacity { get; }
    public int CurrentCapacity { get; private set; }

    public Quiver() : this(_defaultArrowCapacity) { }

    public Quiver(int arrowCapacity)
    {
        ArrowCapacity = arrowCapacity;
        CurrentCapacity = arrowCapacity;
    }

    public bool UseArrow()
    {
        if (CurrentCapacity > 0)
        {
            CurrentCapacity--;

            return true;
        }
        else return false;
    }
}

public record Location(int Row, int Column)
{
    public override string ToString() => $"(Row = {Row}, Column = {Column})";

    public bool IsAdjacentTo(Location otherLocation)
        => Math.Abs(Row - otherLocation.Row) == 1 && Math.Abs(Column - otherLocation.Column) == 1
           || Math.Abs(Row - otherLocation.Row) == 0 && Math.Abs(Column - otherLocation.Column) == 1
           || Math.Abs(Row - otherLocation.Row) == 1 && Math.Abs(Column - otherLocation.Column) == 0;
}

public interface IPlayerCommand
{
    bool Execute(TheFountainOfObjects gameInstance);
}

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

public class HelpCommand : IPlayerCommand
{
    public bool Execute(TheFountainOfObjects gameInstance)
    {
        ConsoleHelper.DisplayColoredMessage(">> 'move north':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Moves player by one space to the north. ");
        ConsoleHelper.DisplayColoredMessage(">> 'move east':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Moves player by one space to the east.");
        ConsoleHelper.DisplayColoredMessage(">> 'move south':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Moves player by one space to the south.");
        ConsoleHelper.DisplayColoredMessage(">> 'move west':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Moves player by one space to the west.");
        ConsoleHelper.DisplayColoredMessage(">> 'shoot north':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Shoots an arrow towards the north.");
        ConsoleHelper.DisplayColoredMessage(">> 'shoot east':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Shoots an arrow towards the east.");
        ConsoleHelper.DisplayColoredMessage(">> 'shoot south':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Shoots an arrow towards the south.");
        ConsoleHelper.DisplayColoredMessage(">> 'shoot west':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Shoots an arrow towards the west.");
        ConsoleHelper.DisplayColoredMessage(">> 'enable fountain':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Activates the fountain of objects.");
        ConsoleHelper.DisplayColoredMessage(">> 'help':", ConsoleColor.DarkYellow);
        Console.WriteLine("   ** Displays all in-game player commands.");

        return true;
    }
}

public enum GameDirection { North, East, South, West }

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

public interface IRoomSense
{
    void DisplayRoomSense(Player player);
}

public abstract class Room : IRoomSense
{
    public Location Location { get; set; }

    public Room(Location startingLocation) => Location = startingLocation;

    public abstract void DisplayRoomSense(Player player);
}

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
public interface IRoomWorldEffect
{
    bool ApplyWorldEffect(TheFountainOfObjects gameInstance);

    static bool ApplyWorldEffectKillPlayer(string message, Location roomLocation, TheFountainOfObjects gameInstance)
    {
        Player player = gameInstance.Player;

        if (player.Alive && roomLocation.Equals(player.Location))
        {
            player.Kill();
            ConsoleHelper.DisplayColoredMessage(message, ConsoleColor.Red);

            return true;
        }
        else return false;
    }
}

public interface IRoomMonsterEffect : IRoomWorldEffect
{
    bool ReceivedPlayerHit(TheFountainOfObjects gameInstance);
}

public class Pit : Room, IRoomWorldEffect
{
    public Pit(Location startingLocation) : base(startingLocation) { }

    public bool ApplyWorldEffect(TheFountainOfObjects gameInstance)
        => IRoomWorldEffect.ApplyWorldEffectKillPlayer("You have fallen into a pit and died.\nYou lost.", Location, gameInstance);

    public override void DisplayRoomSense(Player player)
    {
        if (Location.IsAdjacentTo(player.Location))
        {
            ConsoleHelper.DisplayColoredMessage("You feel a draft. There is a pit in a nearby room.", ConsoleColor.DarkGray);
        }
    }
}

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

public class Amaroks : Room, IRoomMonsterEffect
{
    public bool Alive { get; private set; } = true;

    public Amaroks(Location startingLocation) : base(startingLocation) { }

    public bool ApplyWorldEffect(TheFountainOfObjects gameInstance)
        => IRoomWorldEffect.ApplyWorldEffectKillPlayer("You have encountered an amarok and have died.", Location, gameInstance);

    public override void DisplayRoomSense(Player player)
    {
        if (Location.IsAdjacentTo(player.Location))
        {
            ConsoleHelper.DisplayColoredMessage("You can smell the rotten stench of an amarok in a nearby room.", ConsoleColor.Red);
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

public static class ConsoleHelper
{
    public static void DisplayDashline()
        => Console.WriteLine("----------------------------------------------------------------------------------");

    public static void DisplayColoredMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static string? ReadColoredInput(string message, ConsoleColor color)
    {
        Console.Write(message);
        Console.ForegroundColor = color;
        string? userInput = Console.ReadLine();
        Console.ResetColor();

        return userInput;
    }
}