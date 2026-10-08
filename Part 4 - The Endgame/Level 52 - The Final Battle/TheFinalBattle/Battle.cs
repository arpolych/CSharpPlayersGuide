namespace TheFinalBattle
{
    public class Battle
    {
        public Party Heroes { get; }
        public Party Monsters { get; }

        public Battle(Party heroes, Party monsters)
        {
            Heroes = heroes;
            Monsters = monsters;
        }

        public void Run()
        {
            do
            {
                TraversePartyUnits(Heroes);
                if (Monsters.Units.Count == 0)
                {
                    Console.WriteLine("The heroes won, and the Uncoded One was defeated!");
                    return;
                }

                TraversePartyUnits(Monsters);
                if (Heroes.Units.Count == 0)
                {
                    Console.WriteLine("The heroes lost and the Uncoded One's forces have prevailed!");
                    return;
                }
            } while (true);

            #region - Helper method in traversing units of each party
            void TraversePartyUnits(Party p)
            {
                foreach (Character unit in p.Units)
                {
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
    }
}
