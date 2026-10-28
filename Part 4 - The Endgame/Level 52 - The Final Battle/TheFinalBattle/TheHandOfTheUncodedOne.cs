namespace TheFinalBattle
{
    public class TheHandOfTheUncodedOne : Character
    {
        public override string Name { get; } = "THE HAND OF THE UNCODED ONE";
        public override NormalAttackType NormalWeapon { get; } = new Punch();

        public TheHandOfTheUncodedOne() : base(30)
        {
            SpecialGearInventory.Add(new BinaryBlade());
        }
    }
}
