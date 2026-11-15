namespace TheFinalBattle
{
    /// <summary>
    /// Main template of the characters in the "Final Battle"
    /// </summary>
    public abstract class Character
    {
        /// <summary>
        /// Character's name.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Character's max health points.
        /// </summary>
        public int MaxHP { get; protected set; }
        /// <summary>
        /// Character's current health points.
        /// </summary>
        public int CurrentHP { get; protected set; }
        /// <summary>
        /// Character's taunt.
        /// </summary>
        public virtual string? Taunt { get; }
        /// <summary>
        /// Is alive status, true for alive; otherwise, false.
        /// </summary>
        public bool IsAlive => CurrentHP > 0;
        /// <summary>
        /// Is dead status, true for dead; otherwise, false;
        /// </summary>
        public bool IsDead => CurrentHP == 0;

        /// <summary>
        /// Character's normal attack weapon.
        /// </summary>
        public abstract NormalAttackType NormalWeapon { get; }
        /// <summary>
        /// Character's special attack weapons/gear.
        /// </summary>
        public List<EquippableGearItem> SpecialGearInventory { get; } = new();
        /// <summary>
        /// Defensive attack modifiers of the character.
        /// </summary>
        public DefensiveAttackModifier? Defense { get; set; }
        /// <summary>
        /// Offensive attack modifiers of the character.
        /// </summary>
        public OffensiveAttackModifier? Offense { get; set; }

        /// <summary>
        /// Construction of a character object based on a maximum health
        /// point value.
        /// </summary>
        /// <param name="maxHP">Character's maximum health points.</param>
        public Character(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }

        /// <summary>
        /// Checks whether a character is at or below a certain HP percentage.
        /// </summary>
        /// <param name="percentage">The HP percentage to examine.</param>
        /// <returns>True, if at or below; otherwise, false.</returns>
        public bool IsHPAtOrBelow(int percentage)
        {
            double currentPercentage = (double)CurrentHP / (double)MaxHP;

            return currentPercentage <= (percentage / 100.0);
        }

        /// <summary>
        /// Applies a healing amount to the character's current HP.
        /// Note that the current HP won't exceed the value of the max
        /// HP.
        /// </summary>
        /// <param name="healingValue">The healing value.</param>
        public void ApplyHeal(int healingValue)
        {
            CurrentHP += healingValue;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }

        /// <summary>
        /// Applies enemy's attack onto this character.
        /// </summary>
        /// <param name="enemyAttackData">Enemy's attack data.</param>
        /// <returns>The amount of damage that was inflicted to this character.</returns>
        public int ApplyEnemyAttack(AttackData enemyAttackData)
        {
            // Apply Defense from hero's defense modification
            Defense?.ApplyOn(enemyAttackData); // The modifier application may reduce the damage...

            // Apply Defense modifications from hero's gear
            foreach (var specialGear in SpecialGearInventory)
            {
                specialGear.GearDefenseModifier?.ApplyOn(enemyAttackData);
            }

            int receivedDamage = enemyAttackData.Damage;

            // Calculate new HP value
            CurrentHP -= receivedDamage;
            if (CurrentHP < 0) CurrentHP = 0;

            // Return damage value received
            return receivedDamage;
        }

        /// <summary>
        /// Retrieves the HP status. That is, the value of the current HP divided 
        /// with the maximum HP of the character.
        /// </summary>
        /// <returns>String representation of CurrentHP/MaxHP.</returns>
        public string GetHPStatus() => $"{CurrentHP}/{MaxHP}";

        /// <summary>
        /// The ToString of the character consists of its name followed by the 
        /// special weapons/gear the character has equipped.
        /// </summary>
        /// <returns>String representation of any Character object.</returns>
        public override string ToString()
        {
            string toString = $"{Name}";

            if (SpecialGearInventory.Count > 0)
            {
                toString += $" {EquippableGearItem.GetGearNames(SpecialGearInventory)}";
            }

            return toString;
        }

        /// <summary>
        /// Transfers character's inventory of special weapons to another inventory.
        /// </summary>
        /// <param name="to">The inventory the gear will be transfered to.</param>
        /// <returns>A list containing the transfered gear or null when no transfer occured.</returns>
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
