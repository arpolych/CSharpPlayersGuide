namespace TheFinalBattle
{
    public interface IAttackModifier
    {
        string Name { get; }
        AttackData ApplyOn(AttackData attackData);
    }

    public abstract class OffensiveAttackModifier : IAttackModifier
    {
        public abstract string Name { get; }
        public abstract int DamageIncrease { get; }

        public AttackData ApplyOn(AttackData attackData)
        {
            return ApplyOffenseOn(attackData);
        }

        protected abstract AttackData ApplyOffenseOn(AttackData attackData);

        protected AttackData ApplyDamageIncreaseOn(AttackData attackData)
        {
            int increase = attackData.IncreaseDamage(DamageIncrease);

            if (increase > 0)
            {
                Console.WriteLine($"   >> {Name} increased the attack by {increase} points.");
            }

            return attackData;
        }
    }

    public class Stone : OffensiveAttackModifier
    {
        public override string Name { get; } = "Stone";
        public override int DamageIncrease { get; } = 3;

        protected override AttackData ApplyOffenseOn(AttackData attackData)
        {
            return base.ApplyDamageIncreaseOn(attackData);
        }
    }

    public class BinaryHelmIron : OffensiveAttackModifier
    {
        public override string Name => "Binary Helm Iron";
        public override int DamageIncrease
        {
            get
            {
                string temp = string.Empty;

                for (int i = 0; i < 3; i++)
                    temp += Battle.Randomness.Next(2);

                return Convert.ToInt32(temp);
            }
        }

        protected override AttackData ApplyOffenseOn(AttackData attackData)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class DefensiveAttackModifier : IAttackModifier
    {
        public abstract string Name { get; }
        public abstract int DamageReduction { get; }

        public AttackData ApplyOn(AttackData attackData)
        {
            return ApplyDefenseOn(attackData);
        }

        protected abstract AttackData ApplyDefenseOn(AttackData attackData);

        protected AttackData ApplyDamageReductionOn(AttackData attackData)
        {
            int reduction = attackData.DecreaseDamage(DamageReduction);

            if (reduction > 0)
            {
                Console.WriteLine($"   >> {Name} reduced the attack by {reduction} point.");
            }

            return attackData;
        }
    }

    public class StoneArmor : DefensiveAttackModifier
    {
        public override string Name { get; } = "STONE ARMOR";
        public override int DamageReduction { get; } = 1;

        protected override AttackData ApplyDefenseOn(AttackData attackData)
        {
            return base.ApplyDamageReductionOn(attackData);
        }
    }

    public class ObjectSight : DefensiveAttackModifier
    {
        public override string Name { get; } = "OBJECT SIGHT";
        public override int DamageReduction { get; } = 2;

        protected override AttackData ApplyDefenseOn(AttackData attackData)
        {
            if (attackData.DamageType == DamageType.Decoding)
            {
                return base.ApplyDamageReductionOn(attackData);
            }

            return attackData;
        }
    }

    public class BinaryHelmArmor : DefensiveAttackModifier
    {
        public override string Name => "B-HELM ARMOR";
        public override int DamageReduction
        {
            get
            {
                string temp = string.Empty;

                for (int i = 0; i < 3; i++)
                    temp += Battle.Randomness.Next(3);

                return Convert.ToInt32(temp);
            }
        }

        protected override AttackData ApplyDefenseOn(AttackData attackData)
        {
            return base.ApplyDamageReductionOn(attackData);
        }
    }
}
