namespace TheFinalBattle
{
    public class Player
    {
        public Party ControlledUnits { get; set; }

        public Player(Party controlledUnits)
        {
            ControlledUnits = controlledUnits;
        }

        public bool SetUnitAction(Character unit, ref UnitAction action)
        {
            if (ControlledUnits.Characters.Contains(unit))
            {
                /*
                 * Here the player selects the character's action,
                 * for now it's gonna be just the first one... bc
                 * we got only 1.
                 */
                action = unit.Actions[0];

                return true;
            }

            return false;
        }
    }
}
