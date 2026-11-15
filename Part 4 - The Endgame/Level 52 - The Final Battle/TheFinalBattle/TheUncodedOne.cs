namespace TheFinalBattle
{
    /// <summary>
    /// The Uncoded One, the final boss, in the game.
    /// </summary>
    public class TheUncodedOne : Character
    {
        /// <summary>
        /// The Uncoded One's name.
        /// </summary>
        public override string Name => "The Uncoded One";
        /// <summary>
        /// The Uncoded One's normal attack weapon, an unraveling attack!
        /// </summary>
        public override NormalAttackType NormalWeapon { get; } = new UnravelingAttack();
        /// <summary>
        /// The Uncoded One's taunt during the battle.
        /// </summary>
        public override string? Taunt { get; } = "THE UNRAVELLING OF ALL THINGS IS INEVITABLE";

        /// <summary>
        /// Constructs The Uncoded One with 15 MaxHP.
        /// </summary>
        public TheUncodedOne() : base(maxHP: 15) { }
    }
}
