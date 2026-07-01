namespace DuelingTraditions;

public class Quiver
{
    public static readonly int _defaultArrowCapacity = 5;
    public int ArrowCapacity { get; }
    public int CurrentCapacity { get; private set; }

    public Quiver() : this(_defaultArrowCapacity) { }

    public Quiver(int arrowCapacity)
    {
        ArrowCapacity = arrowCapacity;
        CurrentCapacity = arrowCapacity;
    }

    public bool UseArrow()
    {
        if (CurrentCapacity > 0)
        {
            CurrentCapacity--;

            return true;
        }
        else return false;
    }
}
