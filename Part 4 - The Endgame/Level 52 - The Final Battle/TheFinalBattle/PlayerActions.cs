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
            => ColoredConsole.WriteLine($" >> {unit.Name} did NOTHING.", ConsoleColor.Magenta);
    }

    public class AttackAction : IAction
    {
        public IAttackType AttackType { get; }
        private AttackData AttackData { get; set; }

        public AttackAction(IAttackType attackType)
        {
            AttackType = attackType;
            AttackData = attackType.GenerateAttackData();
        }

        public void Run(Character playerUnit, Battle gameState)
        {
            var unitParty = gameState.GetPartyFor(playerUnit);

            if (AttackData.HasMissedTarget())
            {
                ColoredConsole.WriteLine($" >> {playerUnit.Name} used {AttackType.Name} and MISSED!", ConsoleColor.Magenta);
                return;
            }

            var enemyParty = gameState.GetEnemyPartyFor(playerUnit)!;
            var target = GetRandomEnemyUnit();

            Character GetRandomEnemyUnit()
            {
                int totalEnemyUnits = enemyParty.Units.Count;
                int enemyUnitIndex = Battle.Randomness.Next(totalEnemyUnits);

                return enemyParty.Units[enemyUnitIndex];
            }

            ColoredConsole.WriteLine($" >> {playerUnit.Name} used {AttackType.Name} on {target.Name}.", ConsoleColor.Magenta);

            if (playerUnit!.Offense != null) AttackData = playerUnit.Offense.ApplyOn(AttackData);

            int targetDamageReceived = target.WasAttacked(AttackData);
            Console.WriteLine($"   >> {AttackType.Name} dealt {targetDamageReceived} to {target.Name}.");
            Console.WriteLine($"   >> {target.Name} is now at {target.GetHPStatus()} HP.");

            if (target.CurrentHP == 0)
            {
                ColoredConsole.WriteLine($"   >> {target.Name} has been defeated!", unitParty!.Player.PrimaryColor);

                var acquiredGearItems = target.TransferGear(unitParty.UnequippedGear);
                enemyParty.Units.Remove(target);

                if (acquiredGearItems != null)
                {
                    Console.WriteLine($"   >> {target.Name}'s gear ({EquippableGearItem.GetGearNames(acquiredGearItems)}) was acquired!");
                }

                if (enemyParty.Units.Count == 0)
                {
                    var unitPartyName = gameState.GetBattlePartyName(unitParty);
                    var enemyPartyName = gameState.GetBattlePartyName(enemyParty);

                    bool lootedGear = enemyParty.TransferGearItems(unitParty.UnequippedGear,
                        $"   >> The {enemyPartyName}'s party gear is gone!");
                    bool lootedConsumables = enemyParty.TransferConsumables(unitParty.ConsumablesInventory,
                        $"   >> The {enemyPartyName}'s party consumables are gone!");

                    var message = string.Empty;
                    if (!lootedGear && !lootedConsumables)
                        message = $" >> The {unitPartyName} party had nothing to loot from enemy's inventory!";
                    else if (!lootedGear && lootedConsumables)
                        message = $" >> The {unitPartyName} party looted all the consumables!";
                    else if (lootedGear && !lootedConsumables)
                        message = $" >> The {unitPartyName} party looted all the gear!";
                    else
                        message = $" >> The {unitPartyName} party looted everything!";

                    ColoredConsole.WriteLine(message, ConsoleColor.Magenta);
                }
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
            var itemChoice = RetrievePlayerItemChoice(playerUnit, gameState, gameState.GetPlayerFor(playerUnit)!);

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
