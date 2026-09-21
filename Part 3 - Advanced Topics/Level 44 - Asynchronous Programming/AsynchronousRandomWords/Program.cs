Console.Write("Enter word: ");

string? word = Console.ReadLine();

Console.Write($"Attempts for {word}: ");
var preTask = DateTime.Now;
Console.WriteLine($"{await RandomlyRecreateAsync(word)}");
var postTask = DateTime.Now;
Console.WriteLine($"Program finished successfully!\ntime: {(postTask - preTask).TotalSeconds:0.000}s");

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
