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
        public abstract string Name { get; }
        public abstract AttackData GenerateAttackData();
    }

    public class Punch : StandardAttackType
    {
        public override string Name => "PUNCH";

        public override AttackData GenerateAttackData()
            => new(Damage: 1, MissRate: 15);
    }

    public class BoneCrunch : StandardAttackType
    {
        public override string Name => "BONE CRUNCH";

        public override AttackData GenerateAttackData()
            => new(Damage: Battle.Randomness.Next(2), MissRate: 12);
    }

    public class UnravelingAttack : StandardAttackType
    {
        public override string Name => "UNRAVELING";

        public override AttackData GenerateAttackData()
            => new(Damage: Battle.Randomness.Next(3), MissRate: 10);
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

        public override AttackData GenerateAttackData()
            => new(Damage: 2, MissRate: 21);
    }

    public class StabAttackType : SpecialAttackType
    {
        public override string Name => "STAB";

        public override AttackData GenerateAttackData()
            => new(Damage: 1, MissRate: 23);
    }

    public class QuickShotAttackType : SpecialAttackType
    {
        public override string Name => "Quick Shot";

        public override AttackData GenerateAttackData()
            => new(Damage: 3, MissRate: 50);
    }
    #endregion

    public record class AttackData(int Damage, int MissRate)
    {
        private bool? _hasMissed;

        public bool HasMissedTarget()
        {
            if (!_hasMissed.HasValue)
            {
                var rolledValue = Battle.Randomness.Next(100); // [0, 99] values...

                _hasMissed = (0 <= rolledValue) && (rolledValue < MissRate);
            }

            return _hasMissed.Value;
        }
    }
}
