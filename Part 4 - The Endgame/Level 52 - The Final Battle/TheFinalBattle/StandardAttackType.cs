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
            => new(damage: 1, missRate: 15);
    }

    public class BoneCrunch : StandardAttackType
    {
        public override string Name => "BONE CRUNCH";

        public override AttackData GenerateAttackData()
            => new(damage: Battle.Randomness.Next(2), missRate: 12);
    }

    public class UnravelingAttack : StandardAttackType
    {
        public override string Name => "UNRAVELING";

        public override AttackData GenerateAttackData()
            => new(damage: Battle.Randomness.Next(4),
                   missRate: 10,
                   damageType: DamageType.Decoding);
    }

    public class Bite : StandardAttackType
    {
        public override string Name => "BITE";

        public override AttackData GenerateAttackData()
            => new(damage: 1, missRate: 5);
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
            => new(damage: 2, missRate: 21);
    }

    public class StabAttackType : SpecialAttackType
    {
        public override string Name => "STAB";

        public override AttackData GenerateAttackData()
            => new(damage: 1, missRate: 23);
    }

    public class QuickShotAttackType : SpecialAttackType
    {
        public override string Name => "Quick Shot";

        public override AttackData GenerateAttackData()
            => new(damage: 3, missRate: 50);
    }
    #endregion

    public class AttackData
    {
        public int Damage { get; private set; }
        public int MissRate { get; private set; }
        public DamageType DamageType { get; private set; }
        private bool? _hasMissed;

        public AttackData(int damage, int missRate, DamageType damageType = default)
        {
            Damage = damage;
            MissRate = missRate;
            DamageType = damageType;
        }

        public bool HasMissedTarget()
        {
            if (!_hasMissed.HasValue)
            {
                var rolledValue = Battle.Randomness.Next(100); // [0, 99] values...

                _hasMissed = (0 <= rolledValue) && (rolledValue < MissRate);
            }

            return _hasMissed.Value;
        }

        public int IncreaseDamage(int amount)
        {
            if (amount <= 0) return 0;
            else
            {
                Damage += amount;

                return amount;
            }
        }

        public int DecreaseDamage(int amount)
        {
            int newDamage = Damage - amount;
            if (newDamage < 0) newDamage = 0;

            if (Damage <= newDamage) return 0;
            else
            {
                int reduction = Damage - newDamage;

                Damage = newDamage;

                return reduction;
            }
        }
    }
}
