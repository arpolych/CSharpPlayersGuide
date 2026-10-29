namespace TheFinalBattle
{
    namespace BattleStatus
    {
        public sealed class InfoLog
        {
            public const int ConsolePadding = 80;
            private List<Info> Log { get; } = [];

            public void Append(Info newInfo)
            {
                Log.Add(newInfo);
            }

            public void PrintToConsole()
            {
                foreach (var info in Log)
                {


                    if (info.Alignment == ConsoleAlignment.RIGHT)
                        ColoredConsole.Write($"{info.Text,ConsolePadding}", info.TextColor);
                    else
                        ColoredConsole.Write($"{info.Text,-ConsolePadding}", info.TextColor);
                
                    if (info.TextAppendNewLine)
                    {
                        ColoredConsole.Write($"{info.Text}", info.TextColor);
                        Console.WriteLine();
                    }
                    else
                        continue;

                }
            }
        }

        public class Info
        {
            public string Text { get; }
            public ConsoleColor TextColor { get; } = ConsoleColor.White;
            public bool TextAppendNewLine { get; } = true;
            public ConsoleAlignment Alignment { get; }

            public Info(string text, ConsoleAlignment alignment, bool textAppendNewLine)
                : this(text, ConsoleColor.White, alignment, textAppendNewLine) { }

            public Info(string text,
                        ConsoleColor textColor = ConsoleColor.White,
                        ConsoleAlignment alignment = ConsoleAlignment.LEFT,
                        bool textAppendNewLine = true)
            {
                Text = text;
                TextColor = textColor;
                Alignment = alignment;
                TextAppendNewLine = textAppendNewLine;
            }
        }

        public enum ConsoleAlignment
        {
            LEFT,
            RIGHT
        }
    }
}
