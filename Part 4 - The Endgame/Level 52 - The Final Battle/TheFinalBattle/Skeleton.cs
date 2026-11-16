namespace TheFinalBattle
{
    /// <summary>
    /// Represents the Skeleton character in the final game.
    /// </summary>
    public class Skeleton : Character
    {
        /// <summary>
        /// Skeleton's name.
        /// </summary>
        public override string Name => "SKELETON";
        /// <summary>
        /// Skeleton's normal attack weapon.
        /// </summary>
        public override NormalAttackType NormalWeapon => new BoneCrunch();
        /// <summary>
        /// Skeleton's taunt in the game.
        /// </summary>
        public override string? Taunt { get; } = "We will repel your spineless assault!";

        /// <summary>
        /// Constructs a simple skeleton.
        /// </summary>
        public Skeleton() : base(maxHP: 9) { }
    }
}
