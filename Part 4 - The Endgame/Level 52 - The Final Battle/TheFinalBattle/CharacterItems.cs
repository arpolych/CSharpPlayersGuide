namespace TheFinalBattle
{
    public interface IUnitItem
    {
        string Name { get; }
        void Apply(Character playerUnit, Battle gameState);

        public static string GetGearNamesUnique<T>(List<T> gearInventory)
            where T : IUnitItem
        {
            if (gearInventory.Count == 0) return string.Empty;
            else
            {
                var itemTypeGroup = UseInventoryItemAction<T>.GetDistictItemGroups(gearInventory);
                string gearItemsToString = "";

                foreach (var gearItem in itemTypeGroup)
                {
                    if (gearItem.Quantity > 1)
                    {
                        gearItemsToString += $"{gearItem.Name} ({gearItem.Quantity}), ";
                    }
                    else if (gearItem.Quantity == 1)
                    {
                        gearItemsToString += $"{gearItem.Name}, ";
                    }
                    else
                    {
                        throw new Exception("What's the quantity of this gear type!?");
                    }
                }
                gearItemsToString = gearItemsToString[..^2];

                return $"{gearItemsToString}.";
            }
        }
    }

    #region - Consumable items for player units/characters.
    public abstract class ConsumableItem : IUnitItem
    {
        public abstract string Name { get; }

        public abstract void Apply(Character playerUnit, Battle gameState);

        public static string GetConsumableNames(List<ConsumableItem> consumablesInventory)
        {
            if (consumablesInventory.Count == 0) return string.Empty;
            else
            {
                string consumableItemsString = "[";

                foreach (ConsumableItem consumableItem in consumablesInventory)
                {
                    consumableItemsString += $"{consumableItem.Name}, ";
                }
                consumableItemsString = consumableItemsString[..^2];

                return $"{consumableItemsString}]";
            }
        }
    }

    public class HealthPotion : ConsumableItem
    {
        public override string Name => "Health Potion";
        public int HealingValue { get; }

        public HealthPotion() => HealingValue = 10;

        public override void Apply(Character playerUnit, Battle gameState)
        {
            var message = $" >> {playerUnit.Name} has used a healing potion! (+{HealingValue} HP)";

            playerUnit.WasHealed(HealingValue);
            gameState.GetPartyFor(playerUnit)!.ConsumablesInventory.Remove(this);
            ColoredConsole.WriteLine(message, ConsoleColor.Magenta);
        }
    }

    public class SimulasSoup : ConsumableItem
    {
        public override string Name { get; } = "Simula's Soup";

        public override void Apply(Character playerUnit, Battle gameState)
        {
            var message = $" >> {playerUnit.Name} has consumed simulas' soup and is now back to full HP!";

            playerUnit.WasHealed(playerUnit.MaxHP);
            gameState.GetPartyFor(playerUnit)!.ConsumablesInventory.Remove(this);
            ColoredConsole.WriteLine(message, ConsoleColor.Magenta);
        }
    }
    #endregion

    #region - Equippable gear items for player units/characters.
    public abstract class EquippableGearItem : IUnitItem
    {
        public abstract string Name { get; protected set; }
        public abstract SpecialAttackType GearAttackType { get; }
        public DefensiveAttackModifier? GearDefenseModifier { get; }
        public OffensiveAttackModifier? GearOffenseModifier { get; }

        public EquippableGearItem() { }

        public EquippableGearItem(DefensiveAttackModifier defModifier,
                                  OffensiveAttackModifier offModifier)
        {
            GearDefenseModifier = defModifier;
            GearOffenseModifier = offModifier;
        }

        public abstract bool StackDuplicates(EquippableGearItem anotherGearItem);

        protected virtual bool StackDuplicates<T>(EquippableGearItem anotherGearItem)
            where T : EquippableGearItem
        {
            if (anotherGearItem is T)
            {
                this.GearAttackType.Multiplier += anotherGearItem.GearAttackType.Multiplier;

                var examineChar = Name[^1];
                if (Char.IsDigit(examineChar))
                {
                    Name = $"{Name[..^2]}x{GearAttackType.Multiplier}";
                }
                else Name += $"x{GearAttackType.Multiplier}";

                return true;
            }
            else return false;
        }

        public virtual void Apply(Character playerUnit, Battle gameState)
        {
            // Electra Polychronova found a bug in this method! - 13-Oct... #NewDevs
            /*
             * Bug Fix Details
             * 
             * >> Inventory swap of items error! #NotImplemented#Forgot#Hahah^^
             */
            var message = $" >> {playerUnit.Name} ";
            var currentGearInventory = playerUnit.SpecialGearInventory;
            Party unitParty = gameState.GetPartyFor(playerUnit)!;


            if (playerUnit.SpecialGearInventory.Count == 0)
            {
                playerUnit.SpecialGearInventory.Add(this);
                message += $"is now equipped with a gear item - {Name}!";
            }
            else
            {
                message += $"has another gear item, {this.Name}, equipped!";

                bool wasDuplican = false;
                foreach (var gearItem in playerUnit.SpecialGearInventory)
                {
                    if (gearItem.StackDuplicates(this))
                    {
                        wasDuplican = true;
                        message += $"\n >> This gear item got stacked with a previous one!!!";
                        break;
                    }
                }

                if (!wasDuplican)
                    playerUnit.SpecialGearInventory.Add(this);
            }

            unitParty.UnequippedGear.Remove(this);
            ColoredConsole.WriteLine(message, ConsoleColor.Magenta);
        }

        public static string GetGearNames(List<EquippableGearItem> gearInventory)
        {
            if (gearInventory.Count == 0) return string.Empty;
            else
            {
                string gearItemsToString = "[";

                foreach (EquippableGearItem gearItem in gearInventory)
                {
                    gearItemsToString += $"{gearItem.Name}, ";
                }
                gearItemsToString = gearItemsToString[..^2];

                return $"{gearItemsToString}]";
            }
        }
    }

    public class Sword : EquippableGearItem
    {
        public override string Name { get; protected set; } = "SWORD";

        public override SpecialAttackType GearAttackType { get; } = new SlashAttackType();

        public override bool StackDuplicates(EquippableGearItem anotherGearItem)
            => StackDuplicates<Sword>(anotherGearItem);
    }

    public class Dagger : EquippableGearItem
    {
        public override string Name { get; protected set; } = "DAGGER";

        public override SpecialAttackType GearAttackType { get; } = new StabAttackType();

        public override bool StackDuplicates(EquippableGearItem anotherGearItem)
            => StackDuplicates<Dagger>(anotherGearItem);
    }

    public class VinsBow : EquippableGearItem
    {
        public override string Name { get; protected set; } = $"VIN'S BOW";

        public override SpecialAttackType GearAttackType { get; } = new QuickShotAttackType();

        public override bool StackDuplicates(EquippableGearItem anotherGearItem)
            => StackDuplicates<VinsBow>(anotherGearItem);
    }

    public class BinaryBlade : EquippableGearItem
    {
        public override string Name { get; protected set; } = "BINARY BLADE";
        public override SpecialAttackType GearAttackType { get; } = new BitSlash();

        public override bool StackDuplicates(EquippableGearItem anotherGearItem)
            => StackDuplicates<BinaryBlade>(anotherGearItem);
    }

    public class BinaryHelm : EquippableGearItem
    {
        public override string Name { get; protected set; } = "BINARY HELM";
        public override SpecialAttackType GearAttackType { get; }

        public BinaryHelm()
            : base(new BinaryHelmArmor(), new BinaryHelmIron())
        {
            GearAttackType = new BinaryHelmPoweredSlash(this);
        }

        public override bool StackDuplicates(EquippableGearItem anotherGearItem)
            => StackDuplicates<BinaryHelm>(anotherGearItem);
    }
    #endregion
}
