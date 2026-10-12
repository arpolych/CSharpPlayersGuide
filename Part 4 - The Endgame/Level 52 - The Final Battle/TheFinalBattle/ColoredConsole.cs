namespace TheFinalBattle
{
    public static class ColoredConsole
    {
        public static string ReadLine(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            var userInput = Console.ReadLine() ?? "(read-error)#ColoredConsole";

            Console.BackgroundColor = ConsoleColor.White;

            return userInput;
        }

        public static void Write(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteLine(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
