namespace TheFinalBattle
{
    public class HumanPlayer : Player
    {
        public HumanPlayer()
            : base() { }

        public HumanPlayer(ConsoleColor primaryColor, ConsoleColor secondaryColor)
            : base(primaryColor, secondaryColor) { }

        public override IAction ChooseAction(Character playerUnit, Battle gameState)
        {
            var availableActions = gameState.GetAvailablePlayerConsoleActions(playerUnit);
            var consoleMenu = new ConsoleMenu<ActionConsoleMenuItem>(availableActions);

            return ChooseFromConsoleMenu(consoleMenu).PlayerAction;
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

        public override ItemGroupTypeInfo ChooseItemTypeGroup(IEnumerable<ItemGroupTypeInfo> itemTypeGroups)
        {
            var consoleMenu = new ConsoleMenu<ItemGroupTypeInfo>(itemTypeGroups);

            return ChooseFromConsoleMenu(consoleMenu, "What item to use? ");
        }

        public string GetConsoleInput(string userMessage)
        {
            ColoredConsole.Write(userMessage, ((IPlayer)this).PrimaryColor);

            return ColoredConsole.ReadLine(((IPlayer)this).SecondaryColor);
        }
    }
}
