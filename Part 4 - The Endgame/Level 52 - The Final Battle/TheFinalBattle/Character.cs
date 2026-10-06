namespace TheFinalBattle
{
    public class Character
    {
        /*
         * This class perhaps could be turned into an inheritance
         * relationship, or be an abstract, for other classes to 
         * extent, but that depends on what we want to do in the 
         * future... - 06-Oct.
         */
        public string Name { get; }
        public string Action { get; } = "NOTHING";

        public Character(string name) => Name = name;
    }
}
