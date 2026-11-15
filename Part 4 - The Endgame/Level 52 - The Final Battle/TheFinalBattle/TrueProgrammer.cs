namespace TheFinalBattle
{
    /// <summary>
    /// The True Programmer character in the final battle of the book.
    /// </summary>
    public class TrueProgrammer : Character
    {
        /// <summary>
        /// True Programmer's name (inputted by the user).
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// The normal attack weapon used by the True Programmer, a punch.
        /// </summary>
        public override NormalAttackType NormalWeapon { get; } = new Punch();
        /// <summary>
        /// The taunt of the True Programmer in the final battle.
        /// </summary>
        public override string? Taunt => "My might is true!";

        /// <summary>
        /// Constructs a True Programmer character based on the name given
        /// by the user.
        /// </summary>
        /// <param name="name">The name that will be assigned to the True Programmer.</param>
        public TrueProgrammer(string name) : base(maxHP: 25)
        {
            Name = name;
            base.SpecialGearInventory.Add(new Sword());
            base.SpecialGearInventory.Add(new BinaryHelm());
            base.Defense = new ObjectSight();
        }
    }
}
