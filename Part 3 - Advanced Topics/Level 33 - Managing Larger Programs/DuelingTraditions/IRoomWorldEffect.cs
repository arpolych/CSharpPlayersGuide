namespace DuelingTraditions;

public interface IRoomWorldEffect
{
    bool ApplyWorldEffect(TheFountainOfObjects gameInstance);

    static bool ApplyWorldEffectKillPlayer(string message, Location roomLocation, TheFountainOfObjects gameInstance)
    {
        Player player = gameInstance.Player;

        if (player.Alive && roomLocation.Equals(player.Location))
        {
            player.Kill();
            ConsoleHelper.DisplayColoredMessage(message, ConsoleColor.Red);

            return true;
        }
        else return false;
    }
}
