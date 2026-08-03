/*
 * To observe the unexpected behavior with the reads and
 * writes you must go with the option "Start Without 
 * Debugging (Ctrl + F5)" in Visual Studio.
 */
RunSafeThreadExample();
RunUnsafeThreadExample();

static void RunUnsafeThreadExample()
{
    const int totalIterations = 1_000;
    int counterWas2 = 0;

    for (int i = 0; i < totalIterations; i++)
    {
        SharedData sharedData = new();
        Thread thread = new(sharedData.Increment);

        thread.Start();
        sharedData.Increment();
        thread.Join();

        if (sharedData.Number == 2) counterWas2++;

        // Console.WriteLine($"Iteration index: {i}...");
    }

    Console.WriteLine($"Unsafe way results: {counterWas2}/{totalIterations}");
}

static void RunSafeThreadExample()
{
    const int totalIterations = 1_000;
    int counterWas2 = 0;

    for (int i = 0; i < totalIterations; i++)
    {
        SharedDataLocked sharedDataLocked = new();
        Thread thread = new(sharedDataLocked.Increment);

        thread.Start();
        sharedDataLocked.Increment();
        thread.Join();

        if (sharedDataLocked.Number == 2) counterWas2++;

        // Console.WriteLine($"Iteration index: {i}...");
    }

    Console.WriteLine($"Safe way results: {counterWas2}/{totalIterations}");
}

class SharedData
{
    private int _number;
    public int Number => _number;
    public void Increment() => _number++;
}

class SharedDataLocked
{
    private int _number;
    public int Number
    {
        get
        {
            lock (this)
            {
                return _number;
            }
        }
    }

    public void Increment()
    {
        lock (this)
        {
            _number++;
        }
    }
}
