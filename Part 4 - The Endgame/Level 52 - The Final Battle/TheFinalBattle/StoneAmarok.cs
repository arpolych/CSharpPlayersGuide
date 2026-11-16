namespace TheFinalBattle
{
    /// <summary>
    /// Represents the Stone Amarok character of the game.
    /// </summary>
    public class StoneAmarok : Character
    {
        /// <summary>
        /// Stone Amarok's name.
        /// </summary>
        public override string Name => "STONE AMAROK";
        /// <summary>
        /// The normal weapon attack used by the Stone Amarok, a bite.
        /// </summary>
        public override NormalAttackType NormalWeapon => new Bite();

        /// <summary>
        /// Constructs a default Stone Amarok.
        /// </summary>
        public StoneAmarok() : base(maxHP: 13)
        {
            Defense = new StoneArmor();
            Offense = new Stone();
        }
    }
}
