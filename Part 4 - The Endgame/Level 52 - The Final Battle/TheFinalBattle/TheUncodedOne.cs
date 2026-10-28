namespace TheFinalBattle
{
    public class TheUncodedOne : Character
    {
        public override string Name => "The Uncoded One";
        public override NormalAttackType NormalWeapon => new UnravelingAttack();
        public override string? Taunt { get; } = "THE UNRAVELLING OF ALL THINGS IS INEVITABLE";

        public TheUncodedOne() : base(maxHP: 15) { }
    }
}
