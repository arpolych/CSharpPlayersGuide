namespace TheFinalBattle
{
    public class Skeleton : Character
    {
        public override string Name => "SKELETON";
        public override NormalAttackType NormalWeapon => new BoneCrunch();
        public override string? Taunt { get; } = "We will repel your spineless assault!";

        public Skeleton() : base(maxHP: 9) { }
    }
}
