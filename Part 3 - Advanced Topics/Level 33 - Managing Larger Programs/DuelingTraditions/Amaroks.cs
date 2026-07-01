namespace DuelingTraditions;

public class Amaroks : Room, IRoomMonsterEffect
{
    public bool Alive { get; private set; } = true;

    public Amaroks(Location startingLocation) : base(startingLocation) { }

    public bool ApplyWorldEffect(TheFountainOfObjects gameInstance)
        => IRoomWorldEffect.ApplyWorldEffectKillPlayer("You have encountered an amarok and have died.", Location, gameInstance);

    public override void DisplayRoomSense(Player player)
    {
        if (Location.IsAdjacentTo(player.Location))
        {
            ConsoleHelper.DisplayColoredMessage("You can smell the rotten stench of an amarok in a nearby room.", ConsoleColor.Red);
        }
    }

    public bool ReceivedPlayerHit(TheFountainOfObjects gameInstance)
    {
        if (Alive)
        {
            Alive = false;
            gameInstance.WorldMap.DestroyRoom(Location);

            return true;
        }
        else return false;
    }
}
