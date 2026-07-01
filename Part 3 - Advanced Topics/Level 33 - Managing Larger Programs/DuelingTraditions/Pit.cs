namespace DuelingTraditions;

public class Pit : Room, IRoomWorldEffect
{
    public Pit(Location startingLocation) : base(startingLocation) { }

    public bool ApplyWorldEffect(TheFountainOfObjects gameInstance)
        => IRoomWorldEffect.ApplyWorldEffectKillPlayer("You have fallen into a pit and died.\nYou lost.", Location, gameInstance);

    public override void DisplayRoomSense(Player player)
    {
        if (Location.IsAdjacentTo(player.Location))
        {
            ConsoleHelper.DisplayColoredMessage("You feel a draft. There is a pit in a nearby room.", ConsoleColor.DarkGray);
        }
    }
}
