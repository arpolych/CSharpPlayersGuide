namespace TheFinalBattle
{
    /// <summary>
    /// This class enables colorful input and output in a console application.
    /// </summary>
    public static class ColoredConsole
    {
        /// <summary>
        /// Reads user's colorful text input.
        /// </summary>
        /// <param name="inputColor">The color of the input text.</param>
        /// <returns>The user's input.</returns>
        public static string ReadLine(ConsoleColor inputColor)
        {
            Console.ForegroundColor = inputColor;
            var userInput = Console.ReadLine() ?? "(read-error)#ColoredConsole";

            Console.ForegroundColor = ConsoleColor.White;

            return userInput;
        }

        /// <summary>
        /// Writes a console message in a specified color.
        /// </summary>
        /// <param name="message">The message to write in the console.</param>
        /// <param name="messageColor">The color of the message in the console.</param>
        public static void Write(string message, ConsoleColor messageColor)
        {
            Console.ForegroundColor = messageColor;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Writes a console message in a specified color and changes line.
        /// </summary>
        /// <param name="message">The message to write in the console.</param>
        /// <param name="color">The color of the message in the console.</param>
        public static void WriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
