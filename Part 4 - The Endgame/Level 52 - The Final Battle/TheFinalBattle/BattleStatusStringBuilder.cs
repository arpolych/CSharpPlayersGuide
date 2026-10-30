namespace TheFinalBattle
{
    namespace BattleStatus
    {
        public sealed class InfoLog
        {
            private List<Info> Log { get; } = [];

            public void Append(Info newInfo)
            {
                Log.Add(newInfo);
            }

            public void PrintToConsole()
            {
                foreach (var info in Log)
                {
                    ColoredConsole.Write($"{info.Text}", info.TextColor);
                }
            }
        }

        public class Info
        {
            public string Text { get; private set; }
            public ConsoleColor TextColor { get; } = ConsoleColor.White;
            public int Whitespace { get; } = 0;

            public Info(string text,
                        ConsoleColor textColor = ConsoleColor.White,
                        int whitespace = 0)
            {
                Text = text;
                TextColor = textColor;
                AddWhitespaceToText(whitespace);
            }

            private void AddWhitespaceToText(int whitespace)
            {
                string whitespaceStr = string.Empty;

                for (int i = 0; i < whitespace; i++)
                {
                    whitespaceStr += " ";
                }

                Text = $"{whitespaceStr}{Text}";
            }
        }
    }
}
