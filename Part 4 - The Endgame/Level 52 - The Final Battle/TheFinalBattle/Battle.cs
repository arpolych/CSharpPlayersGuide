namespace TheFinalBattle
{
    public class Battle
    {
        private readonly Player _player1;
        private readonly Player _player2;
        //private Party _currentPartyTurn;

        public Battle(Player p1, Player p2)
        {
            _player1 = p1;
            _player2 = p2;
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
                TraversePlayerUnits(_player1);
                TraversePlayerUnits(_player2);
                Thread.Sleep(2_500);
            } while (true);

            static void TraversePlayerUnits(Player p)
            {
                /*
                 * The game as it traverses the characters of the player's 
                 * party should give him some time to choose the character's 
                 * action.
                 */
                foreach (var character in p.ControlledUnits.Characters)
                {
                    Console.WriteLine($"It is {character.Name}'s turn...");

                    // This is where the player will announce the char's action.
                    UnitAction unitAction = UnitAction.NOTHING;

                    p.SetUnitAction(character, ref unitAction);

                    Console.WriteLine($"{character.Name} did {unitAction}.\n");
                }
            }
        }
    }
}
