namespace TheFinalBattle
{
    public interface IAttackType
    {
        string Name { get; }
        AttackData GenerateAttackData();
    }

    #region - Standard Character Attack Types.
    public abstract class StandardAttackType : IAttackType
    {
        protected static readonly Random random = new(0);
        public abstract string Name { get; }
        public abstract AttackData GenerateAttackData();
    }

    public class Punch : StandardAttackType
    {
        public override string Name => "PUNCH";

        public override AttackData GenerateAttackData() => new(1);
    }

    public class BoneCrunch : StandardAttackType
    {
        public override string Name => "BONE CRUNCH";

        public override AttackData GenerateAttackData() => new(random.Next(2));
    }

    public class UnravelingAttack : StandardAttackType
    {
        public override string Name => "UNRAVELING ATTACK";

        public override AttackData GenerateAttackData() => new(random.Next(3));
    }
    #endregion

    #region - Special Attack Types
    public abstract class SpecialAttackType : IAttackType
    {
        public abstract string Name { get; }

        public abstract AttackData GenerateAttackData();
    }

    public class SlashAttackType : SpecialAttackType
    {
        public override string Name => "SLASH";

        public override AttackData GenerateAttackData() => new(2);
    }

    public class StabAttackType : SpecialAttackType
    {
        public override string Name => "STAB";

        public override AttackData GenerateAttackData() => new(1);
    }
    #endregion

    public record class AttackData(int Damage);
}
