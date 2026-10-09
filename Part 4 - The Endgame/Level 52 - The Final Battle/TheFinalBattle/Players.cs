namespace TheFinalBattle
{
    public interface IPlayer
    {
        IAction ChooseAction(Character unit, Battle currentState);
    }

    public class ComputerPlayer : IPlayer
    {
        public IAction ChooseAction(Character unit, Battle currentState)
        {
            Thread.Sleep(1_500);

            return new AttackAction();
        }
    }

    public class HumanPlayer : IPlayer
    {
        public IAction ChooseAction(Character unit, Battle currentState)
        {
            // Present action menu
            Console.WriteLine($"1 - Standard Attac ({unit.DefaultAttack.Name})");
            Console.WriteLine("2 - Do Nothing");
            // Get User choicel
            // Translate choice to IAction object
            Console.Write("What's your chocie? ");
            var userInput = Console.ReadLine();
            if ("1".Equals(userInput)) return new AttackAction();
            else return new DoNothingAction();
        }
    }
}
