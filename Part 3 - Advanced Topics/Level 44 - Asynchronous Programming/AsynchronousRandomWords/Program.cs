using System.Text;

Console.WriteLine($"{RandomlyRecreate("hi")}");

int RandomlyRecreate(string word)
{
    int attempts = 0;
    StringBuilder wordRecreated = new();
    Random rand = new();

    do
    {
        wordRecreated.Clear();

        for (int i = 0; i < word.Length; i++)
        {
            wordRecreated.Append((char)('a' + rand.Next(26)));
        }

        attempts++;
    } while (!word.Equals(wordRecreated.ToString()));

    return attempts;
}

Task<int> RandomlyRecreateAsync(string word)
{
    // I need to reread this situation with the last 3 chapters...
    // quite hard to answer what's going on...
}
