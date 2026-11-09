namespace TheFinalBattle
{
    public static class BattleConsoleColors
    {
        private static readonly ConsoleColor[] LightColors =
        {
            ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Gray
        };
        private static readonly ConsoleColor[] DarkColors =
        {
            ConsoleColor.DarkBlue, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkGray
        };

        public static ConsoleColor GetLightConsoleColor(BattleColors colorChoice)
        {
            foreach (var lightColor in LightColors)
            {
                if (lightColor.ToString().Equals(colorChoice.ToString()))
                {
                    return lightColor;
                }
            }

            throw new Exception("Unable to retrieve light color!");
        }

        public static ConsoleColor GetDarkConsoleColor(BattleColors colorChoice)
        {
            foreach (var darkColor in DarkColors)
            {
                if (darkColor.ToString().Contains(colorChoice.ToString()))
                {
                    return darkColor;
                }
            }

            throw new Exception("Unable to retrieve dark color!");
        }

        public static void PrintColorList()
        {
            int totalColors = LightColors.Length;

            for (int i = 0; i < totalColors; i++)
            {
                ColoredConsole.Write($" {i + 1}. {LightColors[i]}", LightColors[i]);
                ColoredConsole.WriteLine($" and {DarkColors[i]}", DarkColors[i]);
            }
        }
    }

    public sealed class BattleColor
    {
        public ConsoleColor Primary { get; }
        public ConsoleColor Secondary { get; }

        public BattleColor(BattleColors aColor)
        {
            Primary = BattleConsoleColors.GetLightConsoleColor(aColor);
            Secondary = BattleConsoleColors.GetDarkConsoleColor(aColor);
        }
    }

    public enum BattleColors
    {
        Blue, Red, Yellow, Green, Cyan, Magenta, Gray
    }
}
