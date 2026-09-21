while (true)
{
    Console.WriteLine("Enter a new word anytime!");

    string? word = Console.ReadLine();

    Console.WriteLine($"Calculations began for {word}...");

    RandomRecreationAsync(word);
}

int RandomlyRecreate(string? word)
{
    if (word == null) return 0;

    Random random = new();
    int wordLength = word.Length,
        attempts = 0;
    string guess = string.Empty;

    while (!guess.Equals(word))
    {
        attempts++;
        guess = string.Empty;

        for (int i = 0; i < wordLength; i++)
        {
            guess += (char)('a' + random.Next(26));
        }
    }

    return attempts;
}

Task<int> RandomlyRecreateAsync(string? word)
{
    return Task.Run(() => RandomlyRecreate(word));
}

void RandomRecreationAsync(string? word)
{
    Task.Run(async () =>
    {
        var preTask = DateTime.Now;
        int attempts = await RandomlyRecreateAsync(word);
        var postTask = DateTime.Now;

        string output = $"\nAttempts for {word}: {attempts}\n";
        output += $"Elapsed time: {(postTask - preTask).TotalSeconds:0.000}s";

        Console.WriteLine(output);
    });
}
