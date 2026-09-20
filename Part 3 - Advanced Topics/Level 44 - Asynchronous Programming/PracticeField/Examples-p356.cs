// Example 1 with await:
int result1 = await AddOnEuropa(2, 3);
int result2 = await AddOnEuropa(4, 5);
int result3 = await AddOnEuropa(result1, result2);

Console.WriteLine(result3);

// Example 2 with await:
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