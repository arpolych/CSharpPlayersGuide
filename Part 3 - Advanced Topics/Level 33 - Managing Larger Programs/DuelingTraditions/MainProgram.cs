namespace DuelingTraditions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TheFountainOfObjects mainGame = new(GetGameSize());

            mainGame.Run();
        }

        static GameSize GetGameSize()
        {
            string playerInput;

            do
            {
                playerInput = ConsoleHelper.ReadColoredInput("Do you want to play a small, medium, or large game? ", ConsoleColor.Cyan) ?? "inputError";

                switch (playerInput)
                {
                    case "small": { return GameSize.Small; }
                    case "medium": { return GameSize.Medium; }
                    case "large": { return GameSize.Large; }
                    default: { ConsoleHelper.DisplayColoredMessage($"'{playerInput}' doesn't match any of the specified sizes.", ConsoleColor.Red); continue; }
                }
            } while (true);
        }
    }
}

/*
 * Answer to question:
 * 
 * Personally, I would say the traditional entry point, because it requires me
 * to spell out everything, meaning the structure of the program. On the other
 * hand, the top-level statements are a way to quicker and easier read 
 * written code.
 */