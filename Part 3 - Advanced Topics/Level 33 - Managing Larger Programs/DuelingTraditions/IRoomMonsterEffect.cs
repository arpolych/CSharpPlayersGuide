namespace DuelingTraditions;

public interface IRoomMonsterEffect : IRoomWorldEffect
{
    bool ReceivedPlayerHit(TheFountainOfObjects gameInstance);
}
