namespace TheFinalBattle
{
    public class Party
    {
        public List<Character> Units { get; } = new List<Character>();
        public IPlayer Player { get; }
        public List<ConsumableItem> ConsumablesInventory { get; } = new();
        public List<EquippableGearItem> UnequippedGear { get; } = new();
        public Party(IPlayer player) => Player = player;
    }
}
