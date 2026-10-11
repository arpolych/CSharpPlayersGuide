namespace TheFinalBattle
{
    public interface IPlayer
    {
        IAction ChooseAction(Character unit, Battle currentState);
    }

    public class ComputerPlayer : IPlayer
    {
        private readonly Random _random = new();

        public IAction ChooseAction(Character unit, Battle currentState)
        {
            Thread.Sleep(850); // - Thinking...

            /*
             * Action choice chances:
             * 
             * 1. Standard Attack: 95% (gets affected by increase in
             *    consumable increase chance.
             * 2. Do Nothing: 5%
             * 3. Use consumables: (0% or 25%) (when conditions apply)
             *    Condition 1: party has a consumable in inventory.
             *    Condition 2: unit's HP is below half.
             */
            var currentRandomChance = _random.Next(101); // [0 - 100] values.
            var unitItems = currentState.GetPartyFor(unit).Items;
            int useConsumablesChance = CalculateConsumableActionChance(),
                useStandardAttackChance = 95 - useConsumablesChance,
                doNothingChance = 5;

            if (currentRandomChance <= doNothingChance)
                return new DoNothingAction();
            else if (currentRandomChance <= (doNothingChance + useStandardAttackChance))
                return new AttackAction();
            else return new UseConsumableItemAction(unitItems);

            int CalculateConsumableActionChance()
            {
                /*
                 *    Condition 1: party has a consumable in inventory.
                 *    Condition 2: unit's HP is below half.
                 */
                var flag1 = unitItems.Count > 0;
                var flag2 = unit.CurrentHP < (unit.MaxHP / 2);

                return (flag1 && flag2) ? 25 : 0;
            }
        }
    }

    public class HumanPlayer : IPlayer
    {
        public IAction ChooseAction(Character unit, Battle currentState)
        {
            // Present action menu
            var actionChoiceMenu = new ActionChoiceMenu(unit, currentState);

            Console.WriteLine(actionChoiceMenu);
            Console.Write("What's your choice? ");

            var userChoice = Convert.ToInt32(Console.ReadLine()) - 1;

            return actionChoiceMenu.ChoiceMenu[userChoice].ActionItem;
        }
    }

    public class ActionChoiceMenu
    {
        public List<ActionChoiceMenuItem> ChoiceMenu { get; } = new();

        public ActionChoiceMenu(Character unit, Battle state)
        {
            if (state.GetPartyFor(unit).Items is List<ConsumableItem> unitPartyItems && unitPartyItems.Count > 0)
            {
                ChoiceMenu.Add(
                    new ActionChoiceMenuItem($"Use Consumable Item ({unitPartyItems.Count})",
                    new UseConsumableItemAction(unitPartyItems)));
            }

            ChoiceMenu.Add(new ActionChoiceMenuItem("Standard Attack", new AttackAction()));
            ChoiceMenu.Add(new ActionChoiceMenuItem("Do Nothing", new DoNothingAction()));
        }

        public override string ToString()
        {
            string choiceMenu = string.Empty;
            int itemNumber = 1;

            foreach (var actionItem in ChoiceMenu)
            {
                choiceMenu += $"{itemNumber++} - {actionItem.Description}\n";
            }

            return choiceMenu;
        }

        public record class ActionChoiceMenuItem(string Description, IAction ActionItem);
    }
}
