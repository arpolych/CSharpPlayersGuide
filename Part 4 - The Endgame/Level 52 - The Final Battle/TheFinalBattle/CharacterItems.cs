namespace TheFinalBattle
{
    public interface IUnitItem
    {
        string Name { get; }
        void Apply(Character playerUnit, Battle gameState);
    }

    #region - Consumable items for player units/characters.
    public abstract class ConsumableItem : IUnitItem
    {
        public abstract string Name { get; }

        public abstract void Apply(Character playerUnit, Battle gameState);
    }

    public class HealthPotion : ConsumableItem
    {
        public override string Name => "Health Potion";
        public int HealingValue { get; }

        public HealthPotion() => HealingValue = 10;

        public override void Apply(Character playerUnit, Battle gameState)
        {
            var message = $" >> {playerUnit.Name} has used a healing potion! (+{HealingValue} HP)";

            playerUnit.ReceivedHeal(HealingValue);
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

            playerUnit.ReceivedHeal(playerUnit.MaxHP);
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

        public abstract bool StackDuplicates(EquippableGearItem anotherGearItem);

        protected virtual bool StackDuplicates<T>(EquippableGearItem anotherGearItem)
            where T : EquippableGearItem
        {
            if (anotherGearItem is T)
            {
                this.GearAttackType.Multiplier++;

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
            var currentGearInventory = playerUnit.GearInventory;
            Party unitParty = gameState.GetPartyFor(playerUnit)!;


            if (playerUnit.GearInventory.Count == 0)
            {
                playerUnit.GearInventory.Add(this);
                message += $"is now equipped with a gear item - {Name}!";
            }
            else
            {
                message += $"has another gear item, {this.Name}, equipped!";

                bool wasDuplican = false;
                foreach (var gearItem in playerUnit.GearInventory)
                {
                    if (gearItem.StackDuplicates(this))
                    {
                        wasDuplican = true;
                        message += $"\n >> This gear item got stacked with a previous one!!!";
                        break;
                    }
                }

                if (!wasDuplican)
                    playerUnit.GearInventory.Add(this);
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
    #endregion
}
