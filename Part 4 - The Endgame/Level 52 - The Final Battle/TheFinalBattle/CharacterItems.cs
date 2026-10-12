namespace TheFinalBattle
{
    public interface IUnitItem
    {
        string Name { get; }
    }

    #region - Consumable character items
    public abstract class ConsumableItem : IUnitItem
    {
        public abstract string Name { get; }

        public abstract void ApplyEffect(Character activatedBy, Battle battleState);
    }

    public class HealthPotion : ConsumableItem
    {
        public override string Name => "Health Potion";
        public int HealingValue { get; }

        public HealthPotion() => HealingValue = 10;

        public override void ApplyEffect(Character activatedBy, Battle battleState)
        {
            var message = $"{activatedBy.Name} has used a healing potion! (+{HealingValue} HP)";

            activatedBy.ReceivedHeal(HealingValue);
            ColoredConsole.WriteLine(message, ConsoleColor.Magenta);
        }
    }
    #endregion

    #region - Equippable character gear items
    public interface IEquippableGearItem : IUnitItem
    {
        SpecialAttackType GearAttackType { get; }
    }

    public class Sword : IEquippableGearItem
    {
        public string Name => "SWORD";

        public SpecialAttackType GearAttackType { get; } = new SlashAttackType();
    }

    public class Dagger : IEquippableGearItem
    {
        public string Name => "DAGGER";

        public SpecialAttackType GearAttackType { get; } = new StabAttackType();
    }
    #endregion
}
