namespace TheFinalBattle
{
    public class Battle
    {
        public Party Heroes { get; }
        public Party Monsters { get; private set; }
        public Party[] MonsterWaves { get; }
        private bool IsNextWaveAvailable => _currentWaveIndex < MonsterWaves.Length;
        private int _currentWaveIndex = 0;

        public Battle(Party heroes, params Party[] monsterWaves)
        {
            Heroes = heroes;
            MonsterWaves = monsterWaves;
            Monsters = MonsterWaves[_currentWaveIndex];
        }

        public void Run()
        {
            do
            {
                TraversePartyUnits(Heroes);

                if (Monsters.Units.Count == 0)
                {
                    _currentWaveIndex++;

                    if (IsNextWaveAvailable)
                    {
                        ColoredConsole.WriteLine("The heroes won this monster wave!", ConsoleColor.Green);
                        Monsters = MonsterWaves[_currentWaveIndex];
                        continue;
                    }
                    else
                    {
                        ColoredConsole.WriteLine("The heroes won, and the Uncoded One was defeated!", ConsoleColor.Green);
                        return;
                    }
                }

                TraversePartyUnits(Monsters);

                if (Heroes.Units.Count == 0)
                {
                    ColoredConsole.WriteLine("The heroes lost and the Uncoded One's forces have prevailed!", ConsoleColor.Red);
                    return;
                }
            } while (true);

            #region - Helper method in traversing units of each party
            void TraversePartyUnits(Party p)
            {
                foreach (Character unit in p.Units)
                {
                    DisplayBattleStatus(unit);
                    ColoredConsole.WriteLine($"It is {unit.Name}'s turn...", GetPlayerFor(unit).PrimaryColor);
                    p.Player.ChooseAction(unit, this).Run(unit, this);
                    Console.WriteLine();
                }
            }
            #endregion
        }

        public Party GetPartyFor(Character unit)
        {
            if (Heroes.Units.Contains(unit)) return Heroes;
            else return Monsters;
        }

        public Party GetEnemyPartyFor(Character unit)
        {
            if (Heroes.Units.Contains(unit)) return Monsters;
            else return Heroes;
        }

        public IPlayer GetPlayerFor(Character unit) => GetPartyFor(unit).Player;

        public IEnumerable<ActionConsoleMenuItem> GetAvailablePlayerConsoleActions(Character playerUnit)
        {
            var availableActions = new List<ActionConsoleMenuItem>();
            var unitParty = GetPartyFor(playerUnit);

            // Add special attack action...
            if (playerUnit.EquippedGear != null)
            {
                var specialAttackType = playerUnit.EquippedGear.GearAttackType;
                availableActions.Add(new ActionConsoleMenuItem(
                                     new AttackAction(specialAttackType),
                                                      $"Special Attack ({playerUnit.EquippedGear.Name} with {specialAttackType.Name})"));
            }

            // Adds standard attack action...
            availableActions.Add(new ActionConsoleMenuItem(
                     new AttackAction(playerUnit.DefaultAttack),
                     $"Standard Attack ({playerUnit.DefaultAttack.Name})"));

            // Adds the equip gear action...
            if (unitParty.UnequippedGear is List<EquippableGearItem> partyGear
                && partyGear.Count > 0)
            {
                var equipGearAction = new UseInventoryItemAction<EquippableGearItem>(partyGear);
                int gearNum = partyGear.Count;
                availableActions.Add(
                    new ActionConsoleMenuItem(equipGearAction,
                    $"Equip Gear{(gearNum == 1 ? string.Empty : $" ({gearNum})")}"));
            }

            // Adds the use consumables action...
            if (unitParty.ConsumablesInventory is List<ConsumableItem> partyConsumables
                && partyConsumables.Count > 0)
            {
                var useConsumablesAction = new UseInventoryItemAction<ConsumableItem>(partyConsumables);
                int consumableNum = partyConsumables.Count;
                availableActions.Add(
                    new ActionConsoleMenuItem(useConsumablesAction,
                    $"Use Consumables{(consumableNum == 1 ? string.Empty : $" ({consumableNum})")}"));
            }

            // Adds the do-nothing action...
            availableActions.Add(new ActionConsoleMenuItem(
                                 new DoNothingAction(), "Do Nothing"));

            return availableActions;
        }


        public void DisplayBattleStatus(Character unitInTurn)
        {
            var displayUnitStatuses =
                (Party p) =>
                {
                    foreach (Character unit in p.Units)
                    {
                        var unitBattleStatus = $"{unit} >> ( {unit.GetHPStatus()} )";

                        if (unit.Equals(unitInTurn))
                            ColoredConsole.WriteLine(unitBattleStatus, p.Player.SecondaryColor);
                        else Console.WriteLine(unitBattleStatus);
                    }
                };

            ColoredConsole.WriteLine("\n======= *** BATTLE *** =======\n", ConsoleColor.Gray);
            // Display heroes party
            displayUnitStatuses(Heroes);

            ColoredConsole.WriteLine("\n===========   VS   ===========\n", ConsoleColor.Gray);

            // Display monster party
            displayUnitStatuses(Monsters);
            ColoredConsole.WriteLine("\n=========== ****** ===========\n", ConsoleColor.Gray);
        }
    }

    public record class ActionConsoleMenuItem(IAction playerAction, string Description)
    {
        public override string ToString() => Description;

        public static List<T> FindAllTypeMatchedActions<T>(
            IEnumerable<ActionConsoleMenuItem> collection) where T : IAction
        {
            var matchedActions = new List<T>();

            foreach (ActionConsoleMenuItem actionMenuItem in collection)
            {
                if (actionMenuItem.playerAction is T matchedAction)
                {
                    matchedActions.Add(matchedAction);
                }
            }

            return matchedActions;
        }

        public static T FindFirstTypeMatchedAction<T>(
            IEnumerable<ActionConsoleMenuItem> collection) where T : IAction
        {
            foreach (ActionConsoleMenuItem actionMenuItem in collection)
            {
                if (actionMenuItem.playerAction is T matchedAction)
                {
                    return matchedAction;
                }
            }

            throw new Exception("This action type hasn't been found!");
        }
    }
}
