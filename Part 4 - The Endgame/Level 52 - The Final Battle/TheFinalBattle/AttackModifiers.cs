namespace TheFinalBattle
{
    public interface IAttackModifier
    {
        string Name { get; }
        AttackData Apply(AttackData anAttack);
    }

    public abstract class DefensiveAttackModifier : IAttackModifier
    {
        public abstract string Name { get; }
        public abstract int DamageReduction { get; }

        public AttackData Apply(AttackData anAttack)
        {
            return ApplyDefense(anAttack);
        }

        protected abstract AttackData ApplyDefense(AttackData anAttack);

        protected AttackData ApplyDamageReduction(AttackData anAttack)
        {
            int reduction = anAttack.DecreaseDamage(DamageReduction);

            if (reduction > 0)
            {
                Console.WriteLine($"   >> {Name} reduced the attack by {reduction} point.");
            }

            return anAttack;
        }
    }

    public class StoneArmor : DefensiveAttackModifier
    {
        public override string Name { get; } = "STONE ARMOR";
        public override int DamageReduction { get; } = 1;

        protected override AttackData ApplyDefense(AttackData anAttack)
        {
            return base.ApplyDamageReduction(anAttack);
        }
    }

    public class ObjectSight : DefensiveAttackModifier
    {
        public override string Name { get; } = "OBJECT SIGHT";
        public override int DamageReduction { get; } = 2;

        protected override AttackData ApplyDefense(AttackData anAttack)
        {
            if (anAttack.DamageType == DamageType.Decoding)
            {
                return base.ApplyDamageReduction(anAttack);
            }

            return anAttack;
        }
    }
}
