using NumeromechanicalSieve;

Console.WriteLine("Pick filter function for good/evil numbers:");
Console.WriteLine("1 => IsEven.");
Console.WriteLine("2 => IsPositive.");
Console.WriteLine("3 => IsMultipleOfTen.");

int userChoice = int.Parse(Console.ReadLine() ?? "1");

Sieve sieve = userChoice switch
{
    1 => new Sieve(IsEven),
    2 => new Sieve(IsPositive),
    _ => new Sieve(IsMultipleOfTen),
};

while (true)
{
    Console.Write("Enter a number: ");
    int number = Convert.ToInt32(Console.ReadLine() ?? "0");
    string result = sieve.IsGood(number) ? "good" : "evil";
    Console.WriteLine($"The number {number} is {result}.");
}

bool IsEven(int number) => number % 2 == 0;

bool IsPositive(int number) => number > 0;

bool IsMultipleOfTen(int number) => number % 10 == 0;

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
 * This solution is the most straightfoward to me, because is tiny bits of
 * information that change to make the whole thing work. It's like, with 
 * delegaes, swapping the mechanism that is between the parameters and the
 * final result. With inheritance and polymorphism is doable, but I'll have 
 * to create, you know, bunch of types and then somehow have them being able 
 * to call each one their own individual definition of IsGood for instance, 
 * quite the pain I might add.
 */