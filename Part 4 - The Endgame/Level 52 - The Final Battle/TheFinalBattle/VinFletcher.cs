namespace TheFinalBattle
{
    /// <summary>
    /// This class represents our Vin Fletcher character from the book.
    /// </summary>
    public class VinFletcher : Character
    {
        /// <summary>
        /// Character's name, "VIN FLETCHER".
        /// </summary>
        public override string Name => "VIN FLETCHER";
        /// <summary>
        /// Character's normal attack weapon, a punch.
        /// </summary>
        public override NormalAttackType NormalWeapon { get; } = new Punch();

        /// <summary>
        /// Constructs a VinFletcher object.
        /// </summary>
        public VinFletcher() : base(maxHP: 15)
        {
            base.SpecialGearInventory.Add(new VinsBow());
        }
    }
}
