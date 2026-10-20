namespace TheFinalBattle
{
    public class Battle
    {
        public static Random Randomness { get; } = new Random();
        public Party Heroes { get; }
        public Party Monsters { get; private set; }
        public Party[] MonsterWaves { get; }
        private bool HasMoreMonsterWaves => _currentWaveIndex < MonsterWaves.Length;
        private int _currentWaveIndex = 0;

        public Battle(Party heroes, params Party[] monsterWaves)
        {
            Heroes = heroes;
            MonsterWaves = monsterWaves;
            Monsters = MonsterWaves[_currentWaveIndex];
        }

        public void Run()
        {
            bool gameOver = false;
            (Party Name, Func<bool> WinningCondition) playingParty = (Name: Heroes,
                                                                      WinningCondition: HeroesWinningCondition);

            do
            {
                TraversePartyUnits(playingParty.Name, playingParty.WinningCondition);

                if (!gameOver) NextPlayingParty();
                else return;
            } while (true);

            #region - Helper methods in cycling units, parties, as well as the winning conditions...
            void TraversePartyUnits(Party p, Func<bool> winningCondition)
            {
                var partyPlayer = p.Player;

                foreach (Character unit in p.Units)
                {
                    DisplayBattleStatus(unit);

                    ColoredConsole.WriteLine($"It is {unit.Name}'s turn...", partyPlayer.PrimaryColor);
                    
                    if (unit.Taunt is string)
                        ColoredConsole.WriteLine($" >> {unit.Taunt}", partyPlayer.PrimaryColor);

                    p.Player.ChooseAction(unit, this).Run(unit, this);
                    gameOver = winningCondition.Invoke();
                    Console.WriteLine();
                    if (gameOver) return;
                }
            }

            void NextPlayingParty()
            {
                var currentParty = playingParty.Name;

                if (currentParty == Heroes) playingParty = (Monsters, MonstersWinningCondition);
                else if (currentParty == Monsters) playingParty = (Heroes, HeroesWinningCondition);
                else throw new Exception("Unable to retrieve next playing party!");
            }

            bool HeroesWinningCondition()
            {
                if (Monsters.Units.Count == 0)
                {
                    _currentWaveIndex++;

                    if (HasMoreMonsterWaves)
                    {
                        ColoredConsole.WriteLine("The heroes won this monster wave!", ConsoleColor.Green);
                        Monsters = MonsterWaves[_currentWaveIndex];
                        return false;
                    }
                    else
                    {
                        ColoredConsole.WriteLine("The heroes won, and the Uncoded One was defeated!", ConsoleColor.Green);
                        return true;
                    }
                }

                return false;
            }

            bool MonstersWinningCondition()
            {
                if (Heroes.Units.Count == 0)
                {
                    ColoredConsole.WriteLine("The heroes lost and the Uncoded One's forces have prevailed!", ConsoleColor.Red);
                    return true;
                }

                return false;
            }
            #endregion
        }

        public string GetBattlePartyName(Party party)
        {
            if (party == Heroes) return "Heroes";
            else if (party == Monsters) return "Monsters";
            else throw new Exception("Can't match this party to retrieve a name!");
        }

        public Party? GetPartyFor(Character unit)
        {
            if (Heroes.Units.Contains(unit)) return Heroes;
            else if (Monsters.Units.Contains(unit)) return Monsters;
            else return null;
        }

        public Party? GetEnemyPartyFor(Character unit)
        {
            if (Heroes.Units.Contains(unit)) return Monsters;
            else if (Monsters.Units.Contains(unit)) return Heroes;
            else return null;
        }

        public IPlayer? GetPlayerFor(Character unit) => GetPartyFor(unit)?.Player;

        public IEnumerable<ActionConsoleMenuItem> GetAvailablePlayerConsoleActions(Character playerUnit)
        {
            var availableActions = new List<ActionConsoleMenuItem>();
            var unitParty = GetPartyFor(playerUnit)!;

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
            if (unitParty.UnequippedGear is List<EquippableGearItem> partyGear && partyGear.Count > 0)
            {
                var equipGearAction = new UseInventoryItemAction<EquippableGearItem>(partyGear);
                int gearNum = partyGear.Count;

                availableActions.Add(new ActionConsoleMenuItem(equipGearAction,
                                     $"Equip Gear{(gearNum == 1 ? string.Empty : $" ({gearNum})")}"));
            }

            // Adds the use consumables action...
            if (unitParty.ConsumablesInventory is List<ConsumableItem> partyConsumables
                && partyConsumables.Count > 0)
            {
                var useConsumablesAction = new UseInventoryItemAction<ConsumableItem>(partyConsumables);
                int consumableNum = partyConsumables.Count;

                availableActions.Add(new ActionConsoleMenuItem(useConsumablesAction,
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

                        if (unit.Equals(unitInTurn)) ColoredConsole.WriteLine(unitBattleStatus,
                                                                              p.Player.SecondaryColor);
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

    public record class ActionConsoleMenuItem(IAction PlayerAction, string Description)
    {
        public override string ToString() => Description;

        public static List<T> FindAllTypeMatchedActions<T>(IEnumerable<ActionConsoleMenuItem> collection)
            where T : IAction
        {
            var matchedActions = new List<T>();

            foreach (ActionConsoleMenuItem actionMenuItem in collection)
            {
                if (actionMenuItem.PlayerAction is T matchedAction)
                {
                    matchedActions.Add(matchedAction);
                }
            }

            return matchedActions;
        }

        public static T FindFirstTypeMatchedAction<T>(IEnumerable<ActionConsoleMenuItem> collection)
            where T : IAction
        {
            foreach (ActionConsoleMenuItem actionMenuItem in collection)
            {
                if (actionMenuItem.PlayerAction is T matchedAction)
                {
                    return matchedAction;
                }
            }

            throw new Exception("This action type hasn't been found!");
        }
    }
}
