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
        public abstract string Name { get; }
        public abstract SpecialAttackType GearAttackType { get; }

        public virtual void Apply(Character playerUnit, Battle gameState)
        {
            // Electra Polychronova found a bug in this method! - 13-Oct... #NewDevs
            /*
             * Bug Fix Details
             * 
             * >> Inventory swap of items error! #NotImplemented#Forgot#Hahah^^
             */
            var message = $" >> {playerUnit.Name} ";
            var aboutToUnequip = playerUnit.EquippedGear;
            Party unitParty = gameState.GetPartyFor(playerUnit)!;

            playerUnit.EquippedGear = this;
            unitParty.UnequippedGear.Remove(this);

            if (aboutToUnequip == null)
            {
                message += $"is now equipped with a gear item - {Name}!";
            }
            else
            {
                message += $"has had the gear item, {aboutToUnequip.Name}, swapped with {Name}!";
                unitParty.UnequippedGear.Add(aboutToUnequip);
            }

            ColoredConsole.WriteLine(message, ConsoleColor.Magenta);
        }
    }

    public class Sword : EquippableGearItem
    {
        public override string Name => "SWORD";

        public override SpecialAttackType GearAttackType { get; } = new SlashAttackType();
    }

    public class Dagger : EquippableGearItem
    {
        public override string Name => "DAGGER";

        public override SpecialAttackType GearAttackType { get; } = new StabAttackType();
    }

    public class VinsBow : EquippableGearItem
    {
        public override string Name => $"VIN'S BOW";

        public override SpecialAttackType GearAttackType { get; } = new QuickShotAttackType();
    }
    #endregion
}
