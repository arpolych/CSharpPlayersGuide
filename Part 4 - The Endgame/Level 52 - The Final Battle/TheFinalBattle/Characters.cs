namespace TheFinalBattle
{
    public abstract class Character
    {
        public abstract string Name { get; }
        public abstract AttackType DefaultAttack { get; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }

        public void ReceivedDamage(int amount)
        {
            CurrentHP -= amount;
            if (CurrentHP < 0) CurrentHP = 0;
        }

        public string GetHPStatus() => $"{CurrentHP}/{MaxHP}";
    }

    public class Skeleton : Character
    {
        public override string Name => "SKELETON";
        public override AttackType DefaultAttack => new BoneCrunch();

        public Skeleton()
        {
            base.MaxHP = 5;
            base.CurrentHP = base.MaxHP;
        }
    }

    public class TrueProgrammer : Character
    {
        public override string Name { get; }
        public override AttackType DefaultAttack => new Punch();

        public TrueProgrammer(string name)
        {
            Name = name;
            base.MaxHP = 35;
            base.CurrentHP = base.MaxHP;
        }
    }

    public class TheUncodedOne : Character
    {
        public override string Name => "The Uncoded One";

        public override AttackType DefaultAttack => new UnravelingAttack();

        public TheUncodedOne()
        {
            base.MaxHP = 15;
            base.CurrentHP = base.MaxHP;
        }
    }
}
