namespace TheFinalBattle
{
    public class VinFletcher : Character
    {
        public override string Name => "VIN FLETCHER";
        public override NormalAttackType NormalWeapon => new Punch();

        public VinFletcher() : base(maxHP: 15)
        {
            base.SpecialGearInventory.Add(new VinsBow());
        }
    }
}
