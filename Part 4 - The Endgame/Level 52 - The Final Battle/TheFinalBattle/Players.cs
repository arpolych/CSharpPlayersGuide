namespace TheFinalBattle
{
    public interface IPlayer
    {
        ConsoleColor Color { get; }
        IAction ChooseAction(Character playerUnit, Battle gameState);
        ItemsGroupInfo ChooseItemTypeGroup(IEnumerable<ItemsGroupInfo> itemTypeGroups);
    }

    public abstract class Player : IPlayer
    {
        protected readonly Random _random = new();
        public ConsoleColor Color { get; protected set; }

        protected readonly static ConsoleColor[,] GameColors = {
            // Light Colors - first array
            { ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Gray },
            // Equivalent Dark Colors - second array
            { ConsoleColor.DarkBlue, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkGray } };

        public Player() => AssignRandomColor();

        public Player(ConsoleColor playerColor) => Color = playerColor;

        public abstract IAction ChooseAction(Character playerUnit, Battle gameState);

        public abstract ItemsGroupInfo ChooseItemTypeGroup(IEnumerable<ItemsGroupInfo> itemTypeGroups);

        protected virtual void AssignRandomColor()
        {
            int index1 = _random.Next(GameColors.GetLength(0)),
                index2 = _random.Next(GameColors.GetLength(1));

            Color = GameColors[index1, index2];
        }
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
            : base() { }

        public ComputerPlayer(ConsoleColor playerColor)
            : base(playerColor) { }

        public override IAction ChooseAction(Character unit, Battle currentState)
        {
            Thread.Sleep(850); // - Thinking...

            /*
             * Action choice chances:
             * 
             * 1. Standard Attack: 95% (gets affected by increase in
             *    consumable increase chance.
             * 2. Do Nothing: 5%
             * 3. Use consumables: (0% or 25%) (when conditions apply)
             *    Condition 1: party has a consumable in inventory.
             *    Condition 2: unit's HP is below half.
             */
            var currentRandomChance = _random.Next(101); // [0 - 100] values.
            var unitItems = currentState.GetPartyFor(unit).ConsumablesInventory;
            int useConsumablesChance = CalculateConsumableActionChance(),
                useStandardAttackChance = 95 - useConsumablesChance,
                doNothingChance = 5;

            if (currentRandomChance <= doNothingChance)
                return new DoNothingAction();
            else if (currentRandomChance <= (doNothingChance + useStandardAttackChance))
                return new AttackAction();
            else return new UseInventoryItemAction<ConsumableItem>(unitItems);

            int CalculateConsumableActionChance()
            {
                /*
                 *    Condition 1: party has a consumable in inventory.
                 *    Condition 2: unit's HP is below half.
                 */
                var flag1 = unitItems.Count > 0;
                var flag2 = unit.CurrentHP < (unit.MaxHP / 2);

                return (flag1 && flag2) ? 25 : 0;
            }
        }

        public override ItemsGroupInfo ChooseItemTypeGroup(IEnumerable<ItemsGroupInfo> itemTypeGroups)
        {
            throw new NotImplementedException();
        }
    }

    public class HumanPlayer : Player
    {
        public ConsoleColor InputColor { get; protected set; }

        public HumanPlayer()
            : base() => AssignRandomColor();

        public HumanPlayer(ConsoleColor playerColor, ConsoleColor playerInputColor)
            : base(playerColor) => InputColor = playerInputColor;

        public override IAction ChooseAction(Character playerUnit, Battle gameState)
        {
            var availableActions = gameState.GetAvailablePlayerActions(playerUnit);
            var consoleMenu = new ConsoleMenu<ActionConsoleMenuItem>(availableActions);

            return ChooseFromConsoleMenu(consoleMenu).playerAction;
        }

        private T ChooseFromConsoleMenu<T>(ConsoleMenu<T> displayedMenu, string userMessage = "What are you going to choose? ")
        {
            Console.WriteLine(displayedMenu);

            do
            {
                try
                {
                    string playerInput = GetConsoleInput(userMessage);

                    return displayedMenu.RetrieveElement(Convert.ToInt32(playerInput));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            while (true);
        }

        public override ItemsGroupInfo ChooseItemTypeGroup(IEnumerable<ItemsGroupInfo> itemTypeGroups)
        {
            var consoleMenu = new ConsoleMenu<ItemsGroupInfo>(itemTypeGroups);
            Console.WriteLine(consoleMenu);

            do
            {
                try
                {
                    string playerInput = GetConsoleInput("Which item do you want? ");

                    return consoleMenu.RetrieveElement(Convert.ToInt32(playerInput));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            while (true);
        }

        public string GetConsoleInput(string userMessage)
        {
            ColoredConsole.Write(userMessage, Color);

            return ColoredConsole.ReadLine(InputColor);
        }

        protected override void AssignRandomColor()
        {
            int index = _random.Next(GameColors.GetLength(1));

            Color = GameColors[0, index];
            InputColor = GameColors[1, index];
        }
    }

    public class ConsoleMenu<T>
    {
        private readonly string? _menuHeading;
        private readonly IEnumerable<T> _collection;

        public ConsoleMenu(IEnumerable<T> collection, string? menuHeading = null)
        {
            _menuHeading = menuHeading;
            _collection = collection;
        }

        public T RetrieveElement(int elementNum) => _collection.ElementAt(elementNum - 1);

        public override string ToString()
        {
            string consoleText = (_menuHeading != null) ? $"{_menuHeading}\n" : string.Empty;
            int elementNum = 1;

            foreach (var element in _collection)
            {
                consoleText += $"{elementNum++}. {element}\n";
            }

            return consoleText;
        }
    }
}
