namespace TheFinalBattle
{
    public class TrueProgrammer : Character
    {
        public override string Name { get; }
        public override NormalAttackType NormalWeapon => new Punch();
        public override string? Taunt => "My might is true!";

        public TrueProgrammer(string name) : base(maxHP: 25)
        {
            Name = name;
            base.SpecialGearInventory.Add(new Sword());
            base.SpecialGearInventory.Add(new BinaryHelm());
            base.Defense = new ObjectSight();
        }
    }
}
