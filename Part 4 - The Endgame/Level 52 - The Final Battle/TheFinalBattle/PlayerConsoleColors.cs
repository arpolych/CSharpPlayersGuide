namespace TheFinalBattle
{
    public static class PlayerConsoleColors
    {
        public readonly static ConsoleColor[,] GameColors = {
            // Light Colors - first array
            { ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Gray },
            // Equivalent Dark Colors - second array
            { ConsoleColor.DarkBlue, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkGray } };
        public readonly static int totalColors = GameColors.GetLength(1);

        public static void DisplayColorPairs()
        {
            for (int i = 0; i < totalColors; i++)
            {
                ColoredConsole.Write($"{i + 1}. {GameColors[0, i].ToString()}", GameColors[0, i]);
                ColoredConsole.WriteLine($" and {GameColors[1, i].ToString()}", GameColors[1, i]);
            }
        }
    }
}
