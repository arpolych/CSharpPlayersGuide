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
            var message = $"{playerUnit.Name} has used a healing potion! (+{HealingValue} HP)";

            playerUnit.ReceivedHeal(HealingValue);
            ColoredConsole.WriteLine(message, gameState.GetPlayerFor(playerUnit).Color);
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
            var message = $"{playerUnit.Name} ";

            if (playerUnit.EquippedGear == null)
                message += $"is now equipped with a gear item - {Name}!";
            else
                message += $"has had the gear item, {playerUnit.EquippedGear.Name}, swapped with {Name}!";

            ColoredConsole.WriteLine(message, gameState.GetPlayerFor(playerUnit).Color);
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
    #endregion
}
