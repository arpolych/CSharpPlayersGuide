namespace TheFinalBattle
{
    public abstract class Player : IPlayer
    {
        public BattleColor GameColor { get; set; }

        public Player()
            => GameColor = BattleConsoleColors.GetRandomBattleColor();

        public Player(ConsoleColor primaryColor, ConsoleColor secondaryColor)
            => GameColor = new(primaryColor, secondaryColor);

        public abstract IAction ChooseAction(Character playerUnit, Battle gameState);

        public abstract ItemGroupTypeInfo ChooseItemTypeGroup(IEnumerable<ItemGroupTypeInfo> itemTypeGroups);
    }
}
