new Game().Run();

/*
 * The maestro of my little TicTacToe game is this
 * class, the Game. It will do the following:
 * 1. Keep track of the game, that includes:
 *    a. Game board
 *    b. Players
 *    c. And game flow in general
 * 2. Display the game outcome, when an end state has
 * been reached.
 */
public class Game
{
    private readonly Player _playerX = Player.X;
    private readonly Player _playerY = Player.Y;
    private readonly Board _gameBoard = new();
    private Player activePlayer;

    public Game() => activePlayer = _playerX;

    public void Run()
    {
        Console.WriteLine("Welcome to my Tic-Tac-Toe game!");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine($"It is {activePlayer}'s turn.");
            _gameBoard.Display();

            int squareChoice;
            do
            {
                squareChoice = activePlayer.PickBoardSquare();
                if (!_gameBoard.AssignSquare(squareChoice, activePlayer.Symbol))
                {
                    Console.WriteLine("Perhaps there is an issue with the square you've picked... Double-check!");
                }
                else { break; }
            } while (true);

            Console.WriteLine();

            if (_gameBoard.HasWinner())
            {
                _gameBoard.Display();
                Console.WriteLine($"{_gameBoard.GetWinner()} won the game!");
                return;
            }
            else if (_gameBoard.IsDraw())
            {
                _gameBoard.Display();
                Console.WriteLine($"The game is a draw!");
                return;
            }
            else { activePlayer = GetNextPlayer(); }
        }
    }

    private Player GetNextPlayer() => activePlayer == _playerX ? _playerY : _playerX;
}

/*
 * Board class is the one that will:
 * 1. Create our TicTacToe game board.
 * 2. Display it in the console (as instructed and needed).
 * 3. Occupy requested squares for the players
 * 4. Inform us:
 *    a. If a square is within limits.
 *    b. If a square is occupied or not.
 *    c. If there is a winner or no (draw)
 */
public class Board
{
    private readonly char[,] _board;

    public Board()
    {
        _board = new char[3, 3]; // + initialize char

        // Initialize board indexes
        for (int i = 0; i < _board.GetLength(0); i++)
            for (int j = 0; j < _board.GetLength(0); j++)
                _board[i, j] = ' ';
    }

    public bool AssignSquare(int square, char playerSymbol)
    {
        /*
         * A square can be assigned if:
         * 1. If the "square" num is valid
         * 2. It's free
         */
        if (IsValidSquareNumber(square) && IsSquareFree(square)) return SetSquareContents(square, playerSymbol);
        return false;
    }

    public bool IsValidSquareNumber(int squareNumber) => 1 <= squareNumber && squareNumber <= 9;

    public bool IsSquareFree(int square) => GetSquareContents(square) == ' ';

    public bool HasAvailableSquares()
    {
        foreach (char c in _board)
            if (c == ' ')
                return true;
        return false;
    }

    public bool HasWinner() => GetWinner() != ' ';

    public char GetWinner()
    {
        // Winnner from Row check
        if (_board[2, 0] == _board[2, 1] && _board[2, 1] == _board[2, 2]) return _board[2, 0];
        else if (_board[1, 0] == _board[1, 1] && _board[1, 1] == _board[1, 2]) return _board[1, 0];
        else if (_board[0, 0] == _board[0, 1] && _board[0, 1] == _board[0, 2]) return _board[0, 0];
        // Winnner from Column check
        else if (_board[2, 0] == _board[1, 0] && _board[1, 0] == _board[0, 0]) return _board[2, 0];
        else if (_board[2, 1] == _board[1, 1] && _board[1, 1] == _board[0, 1]) return _board[2, 1];
        else if (_board[2, 2] == _board[1, 2] && _board[1, 2] == _board[0, 2]) return _board[2, 2];
        // Winnner from Diagonals
        else if (_board[2, 2] == _board[1, 1] && _board[1, 1] == _board[0, 0]) return _board[2, 2];
        else if (_board[2, 0] == _board[1, 1] && _board[1, 1] == _board[0, 2]) return _board[2, 0];
        else return ' ';
    }

    public bool IsDraw() => !HasAvailableSquares() && !HasWinner();

    private char GetSquareContents(int square)
        => square switch
        {
            1 => _board[2, 0],
            2 => _board[2, 1],
            3 => _board[2, 2],
            4 => _board[1, 0],
            5 => _board[1, 1],
            6 => _board[1, 2],
            7 => _board[0, 0],
            8 => _board[0, 1],
            9 => _board[0, 2],
            _ => '?'
        };

    private bool SetSquareContents(int square, char playerSymbol)
    {
        switch (square)
        {
            case 1: { _board[2, 0] = playerSymbol; return true; }
            case 2: { _board[2, 1] = playerSymbol; return true; }
            case 3: { _board[2, 2] = playerSymbol; return true; }
            case 4: { _board[1, 0] = playerSymbol; return true; }
            case 5: { _board[1, 1] = playerSymbol; return true; }
            case 6: { _board[1, 2] = playerSymbol; return true; }
            case 7: { _board[0, 0] = playerSymbol; return true; }
            case 9: { _board[0, 2] = playerSymbol; return true; }
            case 8: { _board[0, 1] = playerSymbol; return true; }
            default: return false;
        }
    }

    public void Display()
    {
        Console.WriteLine($" {_board[0, 0]} | {_board[0, 1]} | {_board[0, 2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {_board[1, 0]} | {_board[1, 1]} | {_board[1, 2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {_board[2, 0]} | {_board[2, 1]} | {_board[2, 2]} ");
    }
}

/*
 * The Player class will handle the following responsibilities:
 * 1. Remember player's name (for a TicTacToe it's gonna be 
 * simple, either X or Y -- plus, the game really has just 2
 * players, so I can make them constant in a sense).
 * 2. Asking the players what square they want to occupy.
 *    Note:
 *    a. The square must be within the board's limits.
 *    b. If the chosen square is already occupied to re-ask.
 */
public class Player
{
    public string Name { get; }
    public char Symbol { get; }
    public readonly static Player X = new("X", 'X');
    public readonly static Player Y = new("Y", 'Y');

    private Player(string name, char symbol) { Name = name; Symbol = symbol; }

    public int PickBoardSquare()
    {
        Console.Write($"{Name}: What square do you want to play in? ");

        string playerInput = Console.ReadLine() ?? "-1"; // -1 is for error indication.

        return Convert.ToInt32(playerInput);
    }
}