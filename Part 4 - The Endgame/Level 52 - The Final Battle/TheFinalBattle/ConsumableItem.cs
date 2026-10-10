namespace TheFinalBattle
{
    public abstract class ConsumableItem
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
            => activatedBy.ReceivedHeal(HealingValue);
    }
}
