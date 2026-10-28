namespace TheFinalBattle
{
    public class StoneAmarok : Character
    {
        public override string Name => "STONE AMAROK";

        public override NormalAttackType NormalWeapon => new Bite();

        public StoneAmarok() : base(maxHP: 13)
        {
            Defense = new StoneArmor();
            Offense = new Stone();
        }
    }
}
