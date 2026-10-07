namespace TheFinalBattle
{
    public abstract class Character
    {
        /*
         * This class perhaps could be turned into an inheritance
         * relationship, or be an abstract, for other classes to 
         * extent, but that depends on what we want to do in the 
         * future... - 06-Oct.
         * 
         * Now converting it into abstract, since as I understand now,
         * we'll be having multiple characters in our game that could use
         * this one as their base. - 07-Oct.
         */
        public string Name { get; }
        public string Action { get; } = "NOTHING";

        public Character(string name) => Name = name;
    }

    public class Skeleton : Character
    {
        public Skeleton() : base("SKELETON") { }
    }

    public class Player : Character
    {
        public Player(string name) : base(name) { }
    }

}
