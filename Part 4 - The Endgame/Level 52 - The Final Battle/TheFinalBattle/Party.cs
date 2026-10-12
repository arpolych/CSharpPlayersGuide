namespace TheFinalBattle
{
    public class Party
    {
        public List<Character> Units { get; } = new List<Character>();
        public IPlayer Player { get; }
        public List<ConsumableItem> Consumables { get; } = new();
        public List<IEquippableGearItem> UnequippedGear { get; } = new();
        public Party(IPlayer player) => Player = player;
    }
}
