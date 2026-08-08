AddOnEuropa(2, 3, result => { Console.WriteLine(result); });

void AddOnEuropa(int a, int b, Action<int> callback)
{
    Thread thread = new Thread(() =>
    {
        Thread.Sleep(3_000);
        int result = a + b;
        callback(result);
    });
    thread.Start();
}
