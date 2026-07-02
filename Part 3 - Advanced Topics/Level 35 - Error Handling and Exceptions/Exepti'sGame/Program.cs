using ExeptiGame;

try
{
    CookieGame exeptiGame = new();

    exeptiGame.Run();
}
catch (OatmealCookieException e)
{
    Console.WriteLine("Exception details:");
    Console.WriteLine(e);
}
finally
{
    Console.WriteLine("\nThank you for playing Exepti's Game!");
}

namespace ExeptiGame
{
    public class CookieGame
    {
        private readonly int _oatmealRaisinIndex;
        private readonly int _minValidIndex = 0;
        private readonly int _maxValidIndex = 9;
        private readonly List<int> _playerNumberHistory;

        public CookieGame()
        {
            _oatmealRaisinIndex = new Random(Seed: 0).Next(_minValidIndex, _maxValidIndex + 1);
            _playerNumberHistory = new List<int>(10);
        }

        public void Run()
        {
            string currentPlayer = "Player 1";

            while (true)
            {
                Console.Write($"{currentPlayer}, pick a number between {_minValidIndex} and {_maxValidIndex}: ");
                string? playerInput = Console.ReadLine();

                if (int.TryParse(playerInput, out int playerNumber))
                {
                    if (!_playerNumberHistory.Contains(playerNumber))
                    {
                        _playerNumberHistory.Add(playerNumber);

                        if (playerNumber == _oatmealRaisinIndex) throw new OatmealCookieException("Expired Oatmeal Cookie! Hahaha!");
                        else { currentPlayer = NextPlayerTurn(); continue; }
                    }
                    else
                    {
                        Console.WriteLine("That number was previously used, try another one...");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("That wasn't quite a number, try again...");
                    continue;
                }
            }

            string NextPlayerTurn()
                => currentPlayer.Equals("Player 1") ? "Player 2" : "Player 1";
        }
    }

    public class OatmealCookieException : Exception
    {
        public OatmealCookieException() { }

        public OatmealCookieException(string? message) : base(message) { }

        public OatmealCookieException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}

/*
 * Answer to question 1:
 * 
 * Yes, I made a custom exception type for learning purposes and because it's fun.
 * Also, it's a bit more interesting to address the oatmeal exception for the game
 * in a more detailed manner.
 * 
 * Answer to question 2:
 * 
 * I wouldn't have used exceptions, because with simple logic, I could have avoided
 * the extra burden and lines of code that came with it.
 */
