namespace TheFinalBattle
{
    public interface IPlayer
    {
        /// <summary>
        /// Player's battle color that will appear in the console.
        /// </summary>
        BattleColor GameColor { get; set; }
        /// <summary>
        /// Primary color.
        /// </summary>
        ConsoleColor PrimaryColor { get => GameColor.Primary; }
        /// <summary>
        /// Secondary color.
        /// </summary>
        ConsoleColor SecondaryColor { get => GameColor.Secondary; }
        /// <summary>
        /// Picks an action.
        /// </summary>
        /// <param name="playerUnit">The acting unit.</param>
        /// <param name="gameState">The current game state.</param>
        /// <returns></returns>
        IAction ChooseAction(Character playerUnit, Battle gameState);
        ItemGroupTypeInfo ChooseItemTypeGroup(IEnumerable<ItemGroupTypeInfo> itemTypeGroups);
    }
}
