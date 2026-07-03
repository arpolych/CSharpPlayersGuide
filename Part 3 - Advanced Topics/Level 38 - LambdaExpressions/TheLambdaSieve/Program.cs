using NumeromechanicalSieve;

Console.WriteLine("Pick filter function for good/evil numbers:");
Console.WriteLine("1 => IsEven.");
Console.WriteLine("2 => IsPositive.");
Console.WriteLine("3 => IsMultipleOfTen.");

int userChoice = int.Parse(Console.ReadLine() ?? "1");

Sieve sieve = userChoice switch
{
    1 => new Sieve(n => n % 2 == 0),
    2 => new Sieve(n => n > 9),
    _ => new Sieve(n => n % 10 == 0),
};

while (true)
{
    Console.Write("Enter a number: ");
    int number = Convert.ToInt32(Console.ReadLine() ?? "0");
    string result = sieve.IsGood(number) ? "good" : "evil";
    Console.WriteLine($"The number {number} is {result}.");
}

namespace NumeromechanicalSieve
{
    public class Sieve
    {
        private readonly Func<int, bool> _decisionFunction;

        public Sieve(Func<int, bool> decisionFunction) => _decisionFunction = decisionFunction;

        public bool IsGood(int number) { return _decisionFunction.Invoke(number); }
    }
}

/*
 * Answer to question:
 * 
 * The program is definitely shorter, as for readability for this
 * size of a program it's more or less the same.
 */