RecentNumbers recentNumbers = new();

Thread thread = new(GenerateNums);

thread.Start(recentNumbers);

while (true)
{
    Console.ReadKey(true);

    lock (recentNumbers)
    {
        if (recentNumbers.AreRecentsDuplicate()) Console.WriteLine("You found the duplicates!");
        else Console.WriteLine("Those are not duplicates!");
    }
}

static void GenerateNums(object? o)
{
    if (o == null || o is not RecentNumbers) return;

    int randNum = -1;
    Random randGen = new();
    RecentNumbers recentNumbers = (RecentNumbers)o;

    while (true)
    {
        randNum = randGen.Next(0, 10);

        lock (recentNumbers)
        {
            recentNumbers.AddNewNumber(randNum);
        }

        Console.WriteLine(randNum);
        Thread.Sleep(1000);
    }
}

public class RecentNumbers
{
    private int? _num1;
    private int? _num2;

    public void AddNewNumber(int number) => (_num1, _num2) = (_num2, number);

    public bool AreRecentsDuplicate() => _num1 == _num2;
}