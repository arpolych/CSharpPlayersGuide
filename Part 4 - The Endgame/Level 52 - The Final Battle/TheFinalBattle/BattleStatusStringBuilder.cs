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

            public void AppendWithinWhitespace(Info newInfo, int whitespace)
            {
                int textLength = newInfo.Text.Length;

                whitespace -= textLength;
                newInfo.ChangeWhitespace(whitespace);
                Log.Add(newInfo);
            }

            public void PrintToConsole()
            {
                foreach (var info in Log)
                {
                    ColoredConsole.Write($"{info}", info.TextColor);
                }
            }
        }

        public class Info
        {
            public string Text { get; private set; }
            public ConsoleColor TextColor { get; } = ConsoleColor.White;
            public int Whitespace { get; private set; } = 0;
            private string _whitespaceStr = string.Empty;

            public Info(string text,
                        ConsoleColor textColor = ConsoleColor.White,
                        int whitespace = 0)
            {
                Text = text;
                TextColor = textColor;
                Whitespace = whitespace;
                ConstructWhitespaceStr();
            }

            private void ConstructWhitespaceStr()
            {
                _whitespaceStr = string.Empty;

                for (int i = 0; i < Whitespace; i++)
                {
                    _whitespaceStr += " ";
                }
            }

            public void ChangeWhitespace(int newWhitespace)
            {
                Whitespace = newWhitespace;
                ConstructWhitespaceStr();
            }

            public override string ToString()
            {
                return $"{_whitespaceStr}{Text}";
            }
        }
    }
}
