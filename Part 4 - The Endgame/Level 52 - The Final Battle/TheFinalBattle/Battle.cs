namespace TheFinalBattle
{
    public class Battle
    {
        private readonly Party _heroes;
        private readonly Party _monsters;
        //private Party _currentPartyTurn;

        public Battle(Party heroes, Party monsters)
        {
            _heroes = heroes;
            _monsters = monsters;
        }

        public void Run()
        {
            /*
             * The battle is run forever (for now), starting with the heroes
             * party.
             */
            // _currentPartyTurn = _heroes;

            do
            {
                TraverseCharacters(_heroes);
                TraverseCharacters(_monsters);
                Thread.Sleep(2_500);
            } while (true);

            static void TraverseCharacters(Party aParty)
            {
                foreach (var character in aParty.Characters)
                {
                    Console.WriteLine($"It is {character.Name}'s turn...");
                    Console.WriteLine($"{character.Name} did {character.Action}.\n");
                }
            }
        }
    }
}
