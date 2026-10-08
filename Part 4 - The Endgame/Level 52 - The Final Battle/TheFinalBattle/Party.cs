namespace TheFinalBattle
{
    public class Party
    {
        public List<Character> Units { get; } = new List<Character>();
        public IPlayer Player { get; }

        public Party(IPlayer player) => Player = player;
    }
}
