namespace TheFinalBattle
{
    public interface IAction
    {
        void Run(Character unit, Battle battle);
    }

    #region - Actions with no extra options when chosen.
    public class DoNothingAction : IAction
    {
        public void Run(Character unit, Battle battle)
            => Console.WriteLine($"{unit.Name} did NOTHING.");
    }

    public class AttackAction : IAction
    {
        public void Run(Character unit, Battle battle)
        {
            var attackType = unit.DefaultAttack;
            var target = battle.GetEnemyPartyFor(unit).Units[0];
            var attackData = attackType.GenerateAttackData();
            int damageAmount = attackData.Damage;

            target.ReceivedDamage(damageAmount);

            Console.WriteLine($"{unit.Name} used {attackType.Name} on {target.Name}.");
            Console.WriteLine($"{attackType.Name} dealt {damageAmount} to {target.Name}.");
            Console.WriteLine($"{target.Name} is now at {target.GetHPStatus()}");

            if (target.CurrentHP == 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
                battle.GetPartyFor(target).Units.Remove(target);
            }
        }
    }
    #endregion

    public abstract class MultipleChoiceAction<T> : IAction where T : IUnitItem
    {
        public List<T> Choices { get; } = new();

        public MultipleChoiceAction(List<T> choices) => Choices = choices;

        public abstract void Run(Character unit, Battle battle);

        public T ReceivePlayerChoice(Character activatedBy, Battle state, IPlayer player)
        {
            var choiceMenu = from c in Choices
                             group c by c.Name into nameGroup
                             select (Name: nameGroup.Key, Value: nameGroup.Count());
            int choiceNum = 1;

            /*
             * At some point from the Run function this one is called and the player
             * depending on his type gets the chance to pick one of the Choices.
             */
            foreach (var elem in choiceMenu)
                Console.WriteLine($"{choiceNum++}. {elem.Name} ({elem.Value})");

            throw new NotImplementedException();
        }
    }

    #region - Actions with multiple options when chosen.
    public class UseConsumableItemAction : MultipleChoiceAction<ConsumableItem>
    {
        public UseConsumableItemAction(List<ConsumableItem> itemsToUse) : base(itemsToUse) { }

        public override void Run(Character activatedBy, Battle battleState)
        {
            var consumableItemChoice = ReceivePlayerChoice(activatedBy, battleState, battleState.GetPlayerFor(activatedBy));
            /*
             * At this stage we assume that all potions are healing ones.
             * Thus, it will straight-up use the first in the inventory.
             */

            if (base.Choices.FirstOrDefault() is ConsumableItem itemToUse)
            {
                itemToUse?.ApplyEffect(activatedBy, battleState);
                base.Choices.Remove(itemToUse!);
            }
        }
    }

    public class EquipGearAction : MultipleChoiceAction<IEquippableGearItem>
    {
        public EquipGearAction(List<IEquippableGearItem> availableGear) : base(availableGear) { }

        public override void Run(Character unit, Battle battle)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
