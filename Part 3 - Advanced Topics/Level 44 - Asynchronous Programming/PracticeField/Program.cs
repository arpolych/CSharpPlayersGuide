Task<int> firstAdd = AddOnEuropa(2, 3);
Task<int> secondAdd = AddOnEuropa(4, 5);

int result = await AddOnEuropa(await firstAdd, await secondAdd);
Console.WriteLine(result);

Task<int> AddOnEuropa(int a, int b)
{
    return Task.Run(() =>
    {
        Thread.Sleep(3_000);
        return a + b;
    });
}
