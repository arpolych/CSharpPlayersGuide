namespace TheFinalBattle
{
    public abstract class Character
    {
        public abstract string Name { get; }
        public abstract StandardAttackType DefaultAttack { get; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }
        public EquippableGearItem? EquippedGear { get; set; }

        public void ReceivedHeal(int amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }

        public void ReceivedDamage(int amount)
        {
            CurrentHP -= amount;
            if (CurrentHP < 0) CurrentHP = 0;
        }

        public string GetHPStatus() => $"{CurrentHP}/{MaxHP}";

        public override string ToString()
            => $"{Name} {(EquippedGear != null ? $"[{EquippedGear.Name}]" : string.Empty)}";

        public EquippableGearItem? TransferGear(List<EquippableGearItem> to)
        {
            if (EquippedGear != null)
            {
                var acquiredGear = EquippedGear;

                to.Add(EquippedGear);
                EquippedGear = null;

                return acquiredGear;
            }

            return null;
        }
    }

    public class Skeleton : Character
    {
        public override string Name => "SKELETON";
        public override StandardAttackType DefaultAttack => new BoneCrunch();

        public Skeleton()
        {
            base.MaxHP = 5;
            base.CurrentHP = base.MaxHP;
        }
    }

    public class TrueProgrammer : Character
    {
        public override string Name { get; }
        public override StandardAttackType DefaultAttack => new Punch();

        public TrueProgrammer(string name)
        {
            Name = name;
            base.MaxHP = 35;
            base.CurrentHP = base.MaxHP;
            base.EquippedGear = new Sword();
        }
    }

    public class TheUncodedOne : Character
    {
        public override string Name => "The Uncoded One";

        public override StandardAttackType DefaultAttack => new UnravelingAttack();

        public TheUncodedOne()
        {
            base.MaxHP = 15;
            base.CurrentHP = base.MaxHP;
        }
    }

    public class VinFletcher : Character
    {
        public override string Name => "VIN FLETCHER";

        public override StandardAttackType DefaultAttack => new Punch();

        public VinFletcher()
        {
            base.MaxHP = 15;
            base.CurrentHP = base.MaxHP;
            EquippedGear = new VinsBow();
        }
    }
}
