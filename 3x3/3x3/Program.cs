using System;

class Program
{
    static char[,] board = new char[3, 3];
    static char currentPlayer;

    static void Main(string[] args)
    {
        InitializeBoard();
        currentPlayer = 'X';
        bool gameEnded = false;

        while (!gameEnded)
        {
            Console.Clear();
            DrawBoard();
            Console.WriteLine($"Текущий игрок: {currentPlayer}");
            Console.Write("Введите строку (0, 1, 2) и столбец (0, 1, 2): ");

            string input = Console.ReadLine();
            string[] parts = input.Split(',');

            if (parts.Length != 2 ||
                !int.TryParse(parts[0], out int row) ||
                !int.TryParse(parts[1], out int col) ||
                !IsValidMove(row, col))
            {
                Console.WriteLine("Некорректный ход. Попробуйте снова.");
                Console.ReadKey();
                continue;
            }

            board[row, col] = currentPlayer;

            if (CheckWin())
            {
                Console.Clear();
                DrawBoard();
                Console.WriteLine($"Игрок {currentPlayer} победил!");
                gameEnded = true;
            }
            else if (CheckDraw())
            {
                Console.Clear();
                DrawBoard();
                Console.WriteLine("Ничья!");
                gameEnded = true;
            }
            else
            {
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }
    }

    static void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = ' ';
    }

    static void DrawBoard()
    {
        Console.WriteLine("  0   1   2");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write($"| {board[i, j]} ");
            }
            Console.WriteLine("|");
            Console.WriteLine("  ---------");
        }
    }

    static bool IsValidMove(int row, int col)
    {
        return row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ';
    }

    static bool CheckWin()
    {
        // Проверка строк, столбцов и диагоналей
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) ||
                (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer))
            {
                return true;
            }
        }
        if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
            (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
        {
            return true;
        }
        return false;
    }

    static bool CheckDraw()
    {
        foreach (var cell in board)
        {
            if (cell == ' ')
                return false;
        }
        return true;
    }
}