namespace DuelingTraditions;

public class Player
{
    public bool Alive { get; private set; } = true;
    public Quiver Quiver { get; }

    public Location Location { get; set; }

    public Player(Location startingLocation, int arrowCapacity)
    {
        Location = startingLocation;
        Quiver = new(arrowCapacity);
    }

    public bool Kill()
    {
        if (Alive)
        {
            Alive = false;

            return true;
        }
        else return false;
    }

    public bool ShootArrow() => Quiver.UseArrow();
}
