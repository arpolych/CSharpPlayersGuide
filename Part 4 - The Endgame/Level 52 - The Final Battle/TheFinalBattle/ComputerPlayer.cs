namespace TheFinalBattle
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
            : base() { }

        public ComputerPlayer(ConsoleColor primaryColor, ConsoleColor secondaryColor)
            : base(primaryColor, secondaryColor) { }

        public override IAction ChooseAction(Character playerUnit, Battle gameState)
        {
            // Step 1: Retrieve available player actions and prep essentials...
            var availableActions = gameState.GetAvailablePlayerConsoleActions(playerUnit);
            var unitParty = gameState.GetPartyFor(playerUnit);
            var roll = Battle.Randomness.Next(100); // [0 - 99] values...
            int chanceStacks = 0;

            Thread.Sleep(Battle.ChoiceTimeForPC); // Small delay prior to decision making...

            /*
             * Step 2 & 3: Evaluate each action's chance and return it...
             */
            // Priority action #1: Equip Gear
            int equipChance = GetEquipGearActionChance(playerUnit, unitParty!);
            // [0 - 49] or not happening
            if ((chanceStacks <= roll) && (roll < equipChance))
            {
                return ActionConsoleMenuItem.FindFirstTypeMatchedAction
                    <UseInventoryItemAction<EquippableGearItem>>(availableActions);
            }
            else chanceStacks += equipChance;

            // Priority action #2: Use Consumables
            int consumablesChance = GetConsumablesActionChance(playerUnit, unitParty!.ConsumablesInventory.Count);
            // [50 - 74] or [0 - 24]
            if ((chanceStacks <= roll) && (roll < chanceStacks + consumablesChance))
            {
                return ActionConsoleMenuItem.FindFirstTypeMatchedAction
                    <UseInventoryItemAction<ConsumableItem>>(availableActions);
            }
            else chanceStacks += consumablesChance;

            // Priority action #3: Do Nothing
            int doNothingChance = GetDoNothingActionChance(playerUnit);
            // [75 - 84] or [25 - 34] or [0 - 9]
            if ((chanceStacks <= roll) && (roll < chanceStacks + doNothingChance))
            {
                return ActionConsoleMenuItem.FindFirstTypeMatchedAction
                    <DoNothingAction>(availableActions);
            }

            // Priority action #4: Attack (playerUnit might have 2 ways of attacking)
            // >> If roll didn't follow any previous flow path, then it is surely an attack action.
            int specialAttakChance = 0,
                standardAttackChance = 100;

            List<AttackAction> availableAttackActions = ActionConsoleMenuItem.FindAllTypeMatchedActions<AttackAction>(availableActions);
            if (playerUnit.SpecialGearInventory.Count != 0)
            {
                unsafe
                {
                    CalcAttackActionChance(playerUnit, &specialAttakChance, &standardAttackChance);
                }
                roll = Battle.Randomness.Next(100); // [0 - 99] re-roll for attack type decision...
            }
            else roll = 99;

            if ((0 <= roll) && (roll < specialAttakChance))
            {
                return AttackAction.RetrieveAttackActionWithType
                    <SpecialAttackType>(availableAttackActions);
            }
            else if ((specialAttakChance <= roll) && (roll < 100))
            {
                return AttackAction.RetrieveAttackActionWithType
                    <NormalAttackType>(availableAttackActions);
            }
            else
            {
                throw new NotImplementedException("This attack type doesn't exist yet!");
            }
        }

        private unsafe void CalcAttackActionChance
            (Character playerUnit, int* pSpecialAttackChance, int* pStandardAttackChance)
        {
            if (playerUnit.SpecialGearInventory != null)
                *pSpecialAttackChance = (60 + Battle.Randomness.Next(21)); // [60 - 80] values...
            else
                *pSpecialAttackChance = 0;

            *pStandardAttackChance = 100 - *pSpecialAttackChance; // 100 or [40 - 20] values...
        }

        private int GetDoNothingActionChance(Character playerUnit)
        {
            /*
             * Mine idea: the more HP a unit is missing the more chance it got to do
             * nothing in the fight. That chance is capped at 10% of the time.
             */
            return Battle.Randomness.Next(playerUnit.MaxHP - playerUnit.CurrentHP) % 10;
        }

        private int GetConsumablesActionChance(Character playerUnit, int consumableNum)
        {
            /*
             *    Condition 1: party has a consumable in inventory.
             *    Condition 2: unit's HP is below half.
             */
            var flag1 = consumableNum > 0;
            var flag2 = playerUnit.CurrentHP < (playerUnit.MaxHP / 2);

            return (flag1 && flag2) ? 25 : 0;
        }

        private int GetEquipGearActionChance(Character playerUnit, Party unitParty)
        {
            /*
             * Condition 1: unitParty has available gear.
             */
            bool flag1 = unitParty.UnequippedGear.Count > 0;

            return flag1 ? 50 : 0;
        }

        public override ItemGroupTypeInfo ChooseItemTypeGroup(IEnumerable<ItemGroupTypeInfo> itemTypeGroups)
        {
            var array = itemTypeGroups.ToArray();
            var itemGroupTypeChoice = array[Battle.Randomness.Next(array.Length)];

            return itemGroupTypeChoice;
        }
    }
}
