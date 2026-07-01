namespace DuelingTraditions;

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

        if (!Player.Alive) return;
        else
        {
            string victoryMessage = "The Fountain of Objects has been reactivated, and you have escaped with your life!\nYou win!";
            ConsoleHelper.DisplayColoredMessage(victoryMessage, ConsoleColor.Green);
        }
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
