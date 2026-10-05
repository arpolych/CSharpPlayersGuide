namespace SpaceDude.Common
{
    public class DudeConsole
    {
        public static void WriteLine(string msg) => Console.WriteLine($"{msg}, dudes!");

        public static void Write(string msg) => Console.WriteLine($"{msg}, dude");
    }
}