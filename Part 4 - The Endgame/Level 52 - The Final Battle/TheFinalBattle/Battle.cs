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
                    Console.WriteLine($"It is {unit.Name}'s turn...");
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

        public void DisplayBattleStatus(Character unitInTurn)
        {
            var displayUnitStatuses =
                (Party p) =>
                {
                    foreach (Character unit in p.Units)
                    {
                        var unitBattleStatus = $"{unit.Name} >> ( {unit.GetHPStatus()} )";

                        if (unit.Equals(unitInTurn)) ColoredConsole.WriteLine(unitBattleStatus, ConsoleColor.Yellow);
                        else Console.WriteLine(unitBattleStatus);
                    }
                };

            Console.WriteLine("\n======= *** BATTLE *** =======\n");
            // Display heroes party
            displayUnitStatuses(Heroes);

            Console.WriteLine("\n===========   VS   ===========\n");

            // Display monster party
            displayUnitStatuses(Monsters);
            Console.WriteLine("\n=========== ****** ===========\n");
        }
    }
}
