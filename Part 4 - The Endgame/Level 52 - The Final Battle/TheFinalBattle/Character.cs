namespace TheFinalBattle
{
    public abstract class Character
    {
        public abstract string Name { get; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }
        public virtual string? Taunt { get; }
        public bool IsAlive => CurrentHP > 0;
        public bool IsDead => CurrentHP == 0;

        public abstract NormalAttackType NormalWeapon { get; }
        public List<EquippableGearItem> SpecialGearInventory { get; } = new();
        public DefensiveAttackModifier? Defense { get; set; }
        public OffensiveAttackModifier? Offense { get; set; }

        public Character(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }

        public void WasHealed(int amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }

        public int WasAttacked(AttackData attackData)
        {
            // Apply Defense
            Defense?.ApplyOn(attackData); // The modifier application may reduce the damage...

            foreach (var specialGear in SpecialGearInventory)
            {
                specialGear.GearDefenseModifier?.ApplyOn(attackData);
            }

            int receivedDamage = attackData.Damage;

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

            if (SpecialGearInventory.Count > 0)
            {
                toString += $" {EquippableGearItem.GetGearNames(SpecialGearInventory)}";
            }

            return toString;
        }

        public List<EquippableGearItem>? TransferGear(List<EquippableGearItem> to)
        {
            if (SpecialGearInventory.Count > 0)
            {
                var acquiredGearItems = new List<EquippableGearItem>();

                foreach (EquippableGearItem gearItem in SpecialGearInventory)
                {
                    acquiredGearItems.Add(gearItem);
                    to.Add(gearItem);
                }
                SpecialGearInventory.Clear();

                return acquiredGearItems;
            }

            return null;
        }
    }
}
