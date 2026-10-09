namespace TheFinalBattle
{
    public abstract class AttackType
    {
        protected static readonly Random random = new(0);
        public abstract string Name { get; }
        public abstract AttackData GenerateAttackData();
    }

    #region - Standard Character Attack Types.
    public class Punch : AttackType
    {
        public override string Name => "PUNCH";

        public override AttackData GenerateAttackData() => new(1);
    }

    public class BoneCrunch : AttackType
    {
        public override string Name => "BONE CRUNCH";

        public override AttackData GenerateAttackData() => new(random.Next(2));
    }

    public class UnravelingAttack : AttackType
    {
        public override string Name => "UNRAVELING ATTACK";

        public override AttackData GenerateAttackData() => new(random.Next(3));
    }
    #endregion

    public record class AttackData(int Damage);
}
