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
        public static int TotalColors { get; }

        static BattleConsoleColors()
        {
            TotalColors = LightColors.Length;
        }

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

        public static BattleColor GetBattleColor(int colorListIndex)
        {
            // Reminder: Color index presented to user started from 1.
            --colorListIndex;

            var lightColor = LightColors[colorListIndex];
            var darkColor = DarkColors[colorListIndex];

            return new BattleColor(lightColor, darkColor);
        }

        public static BattleColor GetRandomBattleColor()
            => GetBattleColor(Battle.Randomness.Next(TotalColors + 1));

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

        public BattleColor(ConsoleColor primary, ConsoleColor secondary)
        {
            Primary = primary;
            Secondary = secondary;
        }
    }

    public enum BattleColors
    {
        Blue, Red, Yellow, Green, Cyan, Magenta, Gray
    }
}
