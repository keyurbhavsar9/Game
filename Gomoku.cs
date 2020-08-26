using System;

namespace Game
{
    class Gomoku : GameBoard
    {
        int rows;
        int cols;
        char[,] gameBoardArray;

        public Gomoku(int rows, int cols, Player player1, Player player2) : base(rows, cols, player1, player2)
        {
            this.cols = cols;
            this.rows = rows;
            this.gameBoardArray = new char[rows, cols]; //rows, cols
            fillArray();
        }

        private void fillArray()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    gameBoardArray[i, j] = '-';
                }
            }
        }

        public char[,] getGameBoardArray()
        {
            return gameBoardArray;
        }


        public string drawGomokuBoard()
        {


            for (int fl = 0; fl < cols; fl++)
            {
                int firstChar = 65;
                Console.Write(" " + (char)(firstChar + fl) + " ");
            }
            Console.WriteLine();

            //board logic
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Cell cell = base.cellArray[i, j];

                    if (cell.isCellOccupied())
                    {
                        Console.Write(" " + cell.getPlayer().getPlayerSymbol() + " ");    // print player char
                    }
                    else
                    {
                        Console.Write(" " + gameBoardArray[i, j] + " "); // print -
                    }

                    if (j == cols - 1)
                    {
                        Console.Write(" " + (i + 1));
                    }
                }
                Console.WriteLine();
            }
            return "";
        }
    }
}
