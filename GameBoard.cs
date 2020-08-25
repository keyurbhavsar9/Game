using System;
using System.Collections.Generic;
namespace Game
{
    class GameBoard
    {
        public Cell[,] cellArray;
        List<Cell> cellHistory = new List<Cell>();
        private Player player1, player2;
        private int rows, cols;
        private int score = -1;
        private const int winningScore = 5; // change to modify winning score

        private bool firstMove = true;
        // change according to winningscore
        public GameBoard(int cols, int rows, Player player1, Player player2)
        {
            this.rows = rows;
            this.cols = cols;
            this.player1 = player1;
            this.player2 = player2;
            this.cellArray = new Cell[rows, cols];
            fillarray();
        }

        private void fillarray()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Cell cell = new Cell();
                    cellArray[i, j] = cell;
                }
            }
        }

        public String makeMove(int row, int col, Player player)
        {
            String Error = "";
            try
            {
                int index = col;//-65 to convert character to index
                Cell ArrayCell = cellArray[row, index];

                if (row > this.rows)
                {
                    Error = "Invalid row selection " + (row + 1);
                }
                else if (index > this.cols)
                {
                    Error = "Invalid column selection" + col;
                }
                else if (ArrayCell.isCellOccupied())
                {
                    Error = "Cell already occupied by" + ArrayCell.getPlayer().getName();
                }
                else
                {
                    Cell ModifiedCell = new Cell(true, player, row, col);
                    cellArray[row, index] = ModifiedCell;
                    cellHistory.Add(ModifiedCell);
                    int winScore = scoreCalculation(row, index, player);
                    if (winScore == winningScore)
                    {
                        Error = "WIN";
                    }
                }
            }
            catch (Exception e)
            {
                Error = "Invalid Move";
            }


            return Error;
        }

        public int scoreCalculation(int row, int col, Player player)
        {
            int rowMaxlimit = this.rows - 1;
            int colMaxlimit = this.cols - 1;
            int meanScore = 0;
            int alpha = 0, beta = 0; // alpha and beta are integers to find out the mean
            //---------------------------------------------------Console.WriteLine("Upside column");
            score = 0;
            for (int i = row; i > (row - winningScore); i--)
            {
                if (i >= 0)
                {
                    Cell cell = cellArray[i, col];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;

                        if (score == winningScore)
                        {
                            return score;
                        }

                    }
                    else
                    {
                        break;
                    }
                }
            }
            alpha = score;
            //---------------------------------------------------Console.WriteLine("down side column");
            score = 0;
            for (int i = row; i < (row + winningScore); i++)
            {
                if (i <= rowMaxlimit)
                {
                    Cell cell = cellArray[i, col];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                        if (score == winningScore)
                            return score;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            beta = score;
            meanScore = (alpha + beta) - 1;
            //Console.WriteLine("mean Score="+meanScore);
            if (meanScore == winningScore)
            {
                return meanScore;
            }

            //---------------------------------------------------Console.WriteLine("Right side row cell");
            meanScore = alpha = beta = score = 0;
            for (int i = col; i < (col + winningScore); i++)
            {
                if (i <= colMaxlimit)
                {
                    Cell cell = cellArray[row, i];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                        if (score == winningScore)
                            return score;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            alpha = score;
            //---------------------------------------------------Console.WriteLine("Left side row cell");
            score = 0;
            for (int i = col; i > (col - winningScore); i--)
            {
                if (i >= 0)
                {
                    Cell cell = cellArray[row, i];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                        if (score == winningScore)
                            return score;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            beta = score;
            meanScore = (alpha + beta) - 1;
            //Console.WriteLine("mean Score="+meanScore);
            if (meanScore == winningScore)
            {
                return meanScore;
            }

            //---------------------------------------------------Console.WriteLine("Diagonal right up  cells");
            meanScore = alpha = beta = score = 0;
            for (int i = row, j = col; i > (row - winningScore) && j < (col + winningScore); i--, j++)
            {
                if (i >= 0 && j <= colMaxlimit)
                {
                    Cell cell = cellArray[i, j];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                        if (score == winningScore)
                            return score;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            alpha = score;
            //---------------------------------------------------Console.WriteLine("Diagonal left down  cells");
            score = 0;
            for (int i = row, j = col; i < (row + winningScore) && j > (col - winningScore); i++, j--)
            {
                if (i <= rowMaxlimit && j >= 0)
                {
                    Cell cell = cellArray[i, j];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                        if (score == winningScore)
                            return score;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            beta = score;
            meanScore = (alpha + beta) - 1;
            if (meanScore == winningScore)
            {
                return meanScore;
            }
            //---------------------------------------------------Console.WriteLine("Diagonal left up  cells");
            alpha = beta = score = 0;
            for (int i = row, j = col; i > (row - winningScore) && j > (col - winningScore); i--, j--)
            {
                if (i >= 0 && j >= 0)
                {
                    Cell cell = cellArray[i, j];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                        if (score == winningScore)
                            return score;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            alpha = score;
            //---------------------------------------------------Console.WriteLine("Diagonal right down  cells");
            score = 0;
            for (int i = row, j = col; i < (row + winningScore) && j < (col + winningScore); i++, j++)
            {
                if (i <= rowMaxlimit && j <= colMaxlimit)
                {
                    Cell cell = cellArray[i, j];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                        if (score == winningScore)
                            return score;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            beta = score;
            meanScore = (alpha + beta) - 1;
            Console.WriteLine("mean Score=" + meanScore);
            if (meanScore == winningScore)
            {
                return meanScore;
            }
            return 0;
        }

        public void computerEasyMove()
        {
            String result = "";
            if (firstMove == true)
            {
                int move = (rows / 2) - 1;
                //result = makeMove(move,move,player2); // if(5,5) is already occupied
                result = makeMove(0, 0, player2);
                if (result != "")
                    makeMove(move + 1, move + 1, player2); //move to 6,6
                Console.WriteLine("result===" + result);
                firstMove = false;
            }
            else
            {
                Console.WriteLine("NOT First Move");
                //lets find last move made by computer
                var compMoves = new List<Cell>();
                foreach (var cell in cellHistory)
                {
                    if (cell.getPlayer().getPlayerSymbol() == 'C')
                    {
                        compMoves.Add(cell);
                    }
                }

                int lastMoveCol, lastMoveRow;
                if (compMoves.Count > 0)
                {
                    Cell cell = compMoves[compMoves.Count - 1];
                    lastMoveRow = cell.getRow();
                    lastMoveCol = cell.getCol();
                }

                //int [] scores=bestCoordinates(); 
                // for(int i =0 ;i<=8;i++){
                //     Console.WriteLine("");
                // }
            }
        }

        public int[] bestCoordinates(int row, int col)
        {
            Player player = player2;
            int[] scorePoints = new int[8];
            int rowMaxlimit = this.rows - 1;
            int colMaxlimit = this.cols - 1;

            int meanScore = 0;
            int alpha = 0, beta = 0; // alpha and beta are integers to find out the mean
            //---------------------------------------------------Console.WriteLine("Upside column");
            score = 0;
            for (int i = row; i > (row - winningScore); i--)
            {
                if (i >= 0)
                {
                    Cell cell = cellArray[i, col];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            alpha = score;
            scorePoints[0] = score;
            //---------------------------------------------------Console.WriteLine("down side column");
            score = 0;
            for (int i = row; i < (row + winningScore); i++)
            {
                if (i <= rowMaxlimit)
                {
                    Cell cell = cellArray[i, col];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            beta = score;
            meanScore = (alpha + beta) - 1;
            //Console.WriteLine("mean Score="+meanScore);
            scorePoints[1] = score;

            //---------------------------------------------------Console.WriteLine("Right side row cell");
            meanScore = alpha = beta = score = 0;
            for (int i = col; i < (col + winningScore); i++)
            {
                if (i <= colMaxlimit)
                {
                    Cell cell = cellArray[row, i];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            alpha = score;
            scorePoints[2] = score;
            //---------------------------------------------------Console.WriteLine("Left side row cell");
            score = 0;
            for (int i = col; i > (col - winningScore); i--)
            {
                if (i >= 0)
                {
                    Cell cell = cellArray[row, i];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            beta = score;
            meanScore = (alpha + beta) - 1;
            //Console.WriteLine("mean Score="+meanScore);
            scorePoints[3] = score;
            //---------------------------------------------------Console.WriteLine("Diagonal right up  cells");
            meanScore = alpha = beta = score = 0;
            for (int i = row, j = col; i > (row - winningScore) && j < (col + winningScore); i--, j++)
            {
                if (i >= 0 && j <= colMaxlimit)
                {
                    Cell cell = cellArray[i, j];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            alpha = score;
            scorePoints[4] = score;
            //---------------------------------------------------Console.WriteLine("Diagonal left down  cells");
            score = 0;
            for (int i = row, j = col; i < (row + winningScore) && j > (col - winningScore); i++, j--)
            {
                if (i <= rowMaxlimit && j >= 0)
                {
                    Cell cell = cellArray[i, j];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            beta = score;
            meanScore = (alpha + beta) - 1;
            scorePoints[5] = score;
            //---------------------------------------------------Console.WriteLine("Diagonal left up  cells");
            alpha = beta = score = 0;
            for (int i = row, j = col; i > (row - winningScore) && j > (col - winningScore); i--, j--)
            {
                if (i >= 0 && j >= 0)
                {
                    Cell cell = cellArray[i, j];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            alpha = score;
            scorePoints[6] = score;
            //---------------------------------------------------Console.WriteLine("Diagonal right down  cells");
            score = 0;
            for (int i = row, j = col; i < (row + winningScore) && j < (col + winningScore); i++, j++)
            {
                if (i <= rowMaxlimit && j <= colMaxlimit)
                {
                    Cell cell = cellArray[i, j];
                    if (cell.isCellOccupied() == true && cell.getPlayer() == player)
                    {
                        score++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            beta = score;
            meanScore = (alpha + beta) - 1;
            Console.WriteLine("mean Score=" + meanScore);
            scorePoints[7] = score;
            return scorePoints;
        }
    }
}
