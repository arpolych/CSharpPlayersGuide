Random enhancedRandom = new();

Console.WriteLine($"Extension Methods for Random:");
Console.WriteLine($"NextDouble: {enhancedRandom.NextDouble(10)}");
Console.WriteLine($"NextString: {enhancedRandom.NextString("up", "down", "left", "right")}");
Console.WriteLine($"CoinFlip (fair): {enhancedRandom.CoinFlip()}");
Console.WriteLine($"CoinFlip (unfair): {enhancedRandom.CoinFlip(75)}");

public static class RandomExtensions
{
    public static double NextDouble(this Random rand, int maxValue)
        => rand.NextDouble() * maxValue;

    public static string NextString(this Random rand, params string[] words)
        => words[rand.Next(words.Length)];

    public static bool CoinFlip(this Random rand, int headsChance = 50)
        => rand.Next(100) < headsChance;
}
/*
 * Answer to question:
 * 
 * I would say it is much better to just add this method instead of going
 * for a strong relation between a new class and the Random. That is, because
 * we do not need access any new instance fields.
 */