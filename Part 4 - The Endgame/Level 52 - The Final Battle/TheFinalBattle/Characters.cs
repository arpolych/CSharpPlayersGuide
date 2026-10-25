namespace TheFinalBattle
{
    public abstract class Character
    {
        public abstract string Name { get; }
        public abstract StandardAttackType DefaultAttack { get; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }
        public List<EquippableGearItem> GearInventory { get; } = new();
        public DefensiveAttackModifier? Defense { get; set; }
        public OffensiveAttackModifier? Offense { get; set; }
        public virtual string? Taunt { get; }

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
        {
            string toString = $"{Name}";

            if (GearInventory.Count > 0)
            {
                toString += $" {EquippableGearItem.GetGearNames(GearInventory)}";
            }

            return toString;
        }

        public List<EquippableGearItem>? TransferGear(List<EquippableGearItem> to)
        {
            if (GearInventory.Count > 0)
            {
                var acquiredGearItems = new List<EquippableGearItem>();

                foreach (EquippableGearItem gearItem in GearInventory)
                {
                    acquiredGearItems.Add(gearItem);
                    to.Add(gearItem);
                }
                GearInventory.Clear();

                return acquiredGearItems;
            }

            return null;
        }
    }

    public class Skeleton : Character
    {
        public override string Name => "SKELETON";
        public override StandardAttackType DefaultAttack => new BoneCrunch();
        public override string? Taunt { get; } = "We will repel your spineless assault!";

        public Skeleton() : base(maxHP: 5) { }
    }

    public class TrueProgrammer : Character
    {
        public override string Name { get; }
        public override StandardAttackType DefaultAttack => new Punch();

        public TrueProgrammer(string name) : base(maxHP: 75)
        {
            Name = name;
            base.GearInventory.Add(new Sword());
            base.Defense = new ObjectSight();
        }
    }

    public class TheUncodedOne : Character
    {
        public override string Name => "The Uncoded One";
        public override StandardAttackType DefaultAttack => new UnravelingAttack();
        public override string? Taunt { get; } = "THE UNRAVELLING OF ALL THINGS IS INEVITABLE";

        public TheUncodedOne() : base(maxHP: 15) { }
    }

    public class VinFletcher : Character
    {
        public override string Name => "VIN FLETCHER";
        public override StandardAttackType DefaultAttack => new Punch();

        public VinFletcher() : base(maxHP: 35)
        {
            base.GearInventory.Add(new VinsBow());
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

    public class TheHandOfTheUncodedOne : Character
    {
        public override string Name { get; } = "THE HAND OF THE UNCODED ONE";
        public override StandardAttackType DefaultAttack { get; } = new Punch();

        public TheHandOfTheUncodedOne() : base(30)
        {
            GearInventory.Add(new BinaryBlade());
        }
    }
}
