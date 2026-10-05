using Humanizer;

Console.WriteLine($"When is the feast? {DateTime.UtcNow.AddHours(30).Humanize()}");
Console.WriteLine($"When is the feast? {DateTime.UtcNow.AddYears(50).Humanize()}");
Console.WriteLine($"When is the feast? {DateTime.UtcNow.AddSeconds(3).Humanize()}");
Console.WriteLine($"When is the feast? {DateTime.UtcNow.AddMinutes(300).Humanize()}");
