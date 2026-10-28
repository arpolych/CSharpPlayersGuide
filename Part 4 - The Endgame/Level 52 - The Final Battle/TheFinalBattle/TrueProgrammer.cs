namespace TheFinalBattle
{
    public class TrueProgrammer : Character
    {
        public override string Name { get; }
        public override NormalAttackType NormalWeapon => new Punch();

        public TrueProgrammer(string name) : base(maxHP: 25)
        {
            Name = name;
            base.SpecialGearInventory.Add(new Sword());
            base.SpecialGearInventory.Add(new BinaryHelm());
            base.Defense = new ObjectSight();
        }
    }
}
