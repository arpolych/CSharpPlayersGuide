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
            => Console.WriteLine($" >> {unit.Name} did NOTHING.");
    }

    public class AttackAction : IAction
    {
        public IAttackType AttackType { get; }
        private AttackData _attackData;

        public AttackAction(IAttackType attackType)
        {
            AttackType = attackType;
            _attackData = attackType.GenerateAttackData();
        }

        public void Run(Character unit, Battle battle)
        {
            var target = battle.GetEnemyPartyFor(unit).Units[0];
            int damageAmount = _attackData.Damage;

            target.ReceivedDamage(damageAmount);
            Console.WriteLine($" >> {unit.Name} used {AttackType.Name} on {target.Name}.");
            Console.WriteLine($" >> {AttackType.Name} dealt {damageAmount} to {target.Name}.");
            Console.WriteLine($" >> {target.Name} is now at {target.GetHPStatus()}.");

            if (target.CurrentHP == 0)
            {
                ColoredConsole.WriteLine($" >> {target.Name} has been defeated!", ConsoleColor.DarkYellow);
                battle.GetPartyFor(target).Units.Remove(target);
            }
        }

        public static AttackAction RetrieveAttackActionWithType<T>(List<AttackAction> attackActions) where T : IAttackType
        {
            foreach (var attackAction in attackActions)
            {
                if (attackAction.AttackType is T) return attackAction;
            }

            throw new Exception("This attack doesn't exist yet!");
        }
    }
    #endregion

    public class UseInventoryItemAction<T> : IAction where T : IUnitItem
    {
        public List<T> InventoryItems { get; }

        public UseInventoryItemAction(List<T> inventoryItems)
            => InventoryItems = inventoryItems;

        public virtual void Run(Character playerUnit, Battle gameState)
        {
            // Step 1: Retrieve player's item choice for this inventory...
            var itemChoice = RetrievePlayerItemChoice(playerUnit, gameState, gameState.GetPlayerFor(playerUnit));

            // Step 2: Apply the item to the player's unit...
            // Step 3: Remove the used item from the unit's party inventory...
            if (itemChoice is T itemToUse)
            {
                itemToUse?.Apply(playerUnit, gameState);
            }
        }

        public T? RetrievePlayerItemChoice(Character playerUnit, Battle gameState, IPlayer player)
        {
            // Step 1: Group the items found in this inventory...
            var itemGroups = from items in InventoryItems
                             let categoryKey = (ItemType: items.GetType(), ItemName: items.Name)
                             group items by categoryKey into itemCategories
                             select new ItemGroupTypeInfo(Type: itemCategories.Key.ItemType,
                                                       Name: itemCategories.Key.ItemName,
                                                       Quantity: itemCategories.Count());

            // Step 2: Request the player's choice of item group...
            var itemTypeGroupByPlayer = player.ChooseItemTypeGroup(itemGroups);

            return InventoryItems.Find(item =>
            {
                return item.GetType().Equals(itemTypeGroupByPlayer.Type);
            });
        }
    }

    public record class ItemGroupTypeInfo(Type Type, string Name, int Quantity)
    {
        public override string ToString()
            => $"{Name} {((Quantity == 1) ? string.Empty : $"({Quantity})")}";
    }
}
