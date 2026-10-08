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
}
