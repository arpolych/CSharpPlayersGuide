namespace TheFinalBattle
{
    public interface IPlayer
    {
        /// <summary>
        /// Player's battle color that will appear in the console.
        /// </summary>
        BattleColor GameColor { get; set; }
        ConsoleColor PrimaryColor { get => GameColor.Primary; }
        ConsoleColor SecondaryColor { get => GameColor.Secondary; }
        IAction ChooseAction(Character playerUnit, Battle gameState);
        ItemGroupTypeInfo ChooseItemTypeGroup(IEnumerable<ItemGroupTypeInfo> itemTypeGroups);
    }
}
