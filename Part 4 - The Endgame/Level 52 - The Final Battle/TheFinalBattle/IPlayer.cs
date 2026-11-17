namespace TheFinalBattle
{
    public interface IPlayer
    {
        BattleColor GameColor { get; set; }
        ConsoleColor PrimaryColor { get => GameColor.Primary; }
        ConsoleColor SecondaryColor { get => GameColor.Secondary; }
        IAction ChooseAction(Character playerUnit, Battle gameState);
        ItemGroupTypeInfo ChooseItemTypeGroup(IEnumerable<ItemGroupTypeInfo> itemTypeGroups);
    }
}
