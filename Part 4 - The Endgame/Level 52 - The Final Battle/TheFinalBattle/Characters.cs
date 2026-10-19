namespace TheFinalBattle
{
    public abstract class Character
    {
        public abstract string Name { get; }
        public abstract StandardAttackType DefaultAttack { get; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }
        public EquippableGearItem? EquippedGear { get; set; }
        public DefensiveAttackModifier? Defense { get; set; }
        public OffensiveAttackModifier? Offense { get; set; }

        public Character(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }

        public void ReceivedHeal(int amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }

        public int WasAttacked(AttackData anAttack)
        {
            // Apply Defense
            Defense?.Apply(anAttack); // The modifier application may reduce the damage...
            int receivedDamage = anAttack.Damage;

            // Calculate new HP value
            CurrentHP -= receivedDamage;
            if (CurrentHP < 0) CurrentHP = 0;

            // Return damage value received
            return receivedDamage;
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

        public Skeleton() : base(maxHP: 5) { }
    }

    public class TrueProgrammer : Character
    {
        public override string Name { get; }
        public override StandardAttackType DefaultAttack => new Punch();

        public TrueProgrammer(string name) : base(maxHP: 35)
        {
            Name = name;
            base.EquippedGear = new Sword();
            base.Defense = new ObjectSight();
        }
    }

    public class TheUncodedOne : Character
    {
        public override string Name => "The Uncoded One";

        public override StandardAttackType DefaultAttack => new UnravelingAttack();

        public TheUncodedOne() : base(maxHP: 15) { }
    }

    public class VinFletcher : Character
    {
        public override string Name => "VIN FLETCHER";

        public override StandardAttackType DefaultAttack => new Punch();

        public VinFletcher() : base(maxHP: 15)
        {
            base.EquippedGear = new VinsBow();
        }
    }
    public class StoneAmarok : Character
    {
        public override string Name => "STONE AMAROK";

        public override StandardAttackType DefaultAttack => new Bite();

        public StoneAmarok() : base(maxHP: 7)
        {
            Defense = new StoneArmor();
            Offense = new Stone();
        }
    }
}
