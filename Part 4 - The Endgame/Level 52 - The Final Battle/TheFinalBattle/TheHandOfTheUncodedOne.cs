namespace TheFinalBattle
{
    /// <summary>
    /// Represents an elite comrade of The Uncoded One.
    /// </summary>
    public class TheHandOfTheUncodedOne : Character
    {
        /// <summary>
        /// The name of The Hand of the Uncoded One.
        /// </summary>
        public override string Name => "THE HAND OF THE UNCODED ONE";
        /// <summary>
        /// The normal attack weapon (punch) used by The Hand of the Uncoded One.
        /// </summary>
        public override NormalAttackType NormalWeapon { get; } = new Punch();

        /// <summary>
        /// Constructs an elite comrade of The Uncoded One.
        /// </summary>
        public TheHandOfTheUncodedOne() : base(30)
        {
            SpecialGearInventory.Add(new BinaryBlade());
        }
    }
}
