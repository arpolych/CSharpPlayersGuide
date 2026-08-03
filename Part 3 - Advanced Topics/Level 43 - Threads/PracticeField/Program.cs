/*
 * To observe the unexpected behavior with the reads and
 * writes you must go with the option "Start Without 
 * Debugging (Ctrl + F5)" in Visual Studio.
 */
int was2 = 0, iteration = 1_000, totalIterations = 0;
for (int i = 0; i < iteration; i++)
{
    totalIterations++;
    SharedData sharedData = new();
    Thread thread = new(sharedData.Increment);

    thread.Start();
    sharedData.Increment();
    thread.Join();

    if (sharedData.Number != 2) ;
    else was2++;
    Console.WriteLine($"Iteration index: {i}...");
}

Console.WriteLine($"Final result: {was2}/{totalIterations}");

class SharedData
{
    private object _numberLock = new();
    private object _numberLock2 = new();
    private int _number;
    public int Number
    {
        get
        {
            lock (this)
            {
                lock (_numberLock)
                {
                    return _number;

                }
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