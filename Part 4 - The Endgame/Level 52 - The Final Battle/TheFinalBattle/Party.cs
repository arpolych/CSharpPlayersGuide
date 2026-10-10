namespace TheFinalBattle
{
    public class Party
    {
        public List<Character> Units { get; } = new List<Character>();
        public IPlayer Player { get; }
        public List<ConsumableItem> Items { get; } = new List<ConsumableItem>();

        public Party(IPlayer player) => Player = player;
    }
}
