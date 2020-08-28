using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
namespace Game
{
    [Serializable]
    class GameBoard
    {
        public Cell[,] cellArray;
        List<Cell> cellHistory = new List<Cell>();
        public Player player1, player2;
        private int rows, cols;
        private int score = -1;
        private const int winningScore = 5; // change to modify winning score
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
                        //Console.WriteLine("WINNER" + player.getName());
                    }
                }
            }
            catch
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
            //Console.WriteLine("mean Score=" + meanScore);
            if (meanScore == winningScore)
            {
                return meanScore;
            }
            return 0;
        }

        public string computerEasyMove()
        {
            String result = "";
            int lastMoveCol = 0, lastMoveRow = 0;

            var compMoves = new List<Cell>();
            var humanMoves = new List<Cell>();
            foreach (var cell in cellHistory)
            {
                //Console.WriteLine("CELL DETAILS" + cell.toString());
                if (cell.getPlayer().getPlayerSymbol() == 'C')
                {
                    compMoves.Add(cell);
                }
            }

            foreach (var cell in cellHistory)
            {
                if (cell.getPlayer().getName() == player1.getName())
                {
                    humanMoves.Add(cell);
                }
            }

            Console.WriteLine("Total computer moves=" + compMoves.Count);
            Console.WriteLine("Total Human moves=" + humanMoves.Count);

            if (compMoves.Count > 0)
            {
                Console.WriteLine("NOT First move");
                Cell lastCell = compMoves[compMoves.Count - 1];
                Cell lastHumanCell = humanMoves[compMoves.Count - 1];
                Cell secondLastCell;
                try
                {
                    secondLastCell = compMoves[compMoves.Count - 2];
                }
                catch
                {
                    secondLastCell = compMoves[compMoves.Count - 1];
                }
                lastMoveRow = lastCell.getRow();
                lastMoveCol = lastCell.getCol();

                try
                {
                    int[] predictNextHumanMove = bestCoordinates(lastHumanCell.getRow(), lastHumanCell.getCol(), player1);
                    int[] predictedMovePriority1 = bestCoordinates(lastMoveRow, lastMoveCol, player2);
                    int[] predictedMovePriority2 = bestCoordinates(secondLastCell.getRow(), secondLastCell.getCol(), player2);

                    if (predictNextHumanMove[0] >= predictedMovePriority1[0])
                    {
                        result = makeMove(predictNextHumanMove[1], predictNextHumanMove[2], player2);
                    }
                    else
                    {
                        result = makeMove(predictedMovePriority1[1], predictedMovePriority1[2], player2);
                        if (result != "")
                        {
                            result = makeMove(predictedMovePriority2[1], predictedMovePriority2[2], player2);
                        }
                    }

                    //Console.WriteLine("NEXT PREDICTED MOVE WILL BE" + predictedMove[1] + "," + predictedMove[2]);
                }
                catch
                {
                    int[] predictedMove = bestCoordinates(secondLastCell.getRow(), secondLastCell.getCol(), player2);
                    result = makeMove(predictedMove[1], predictedMove[2], player2);
                    //Console.WriteLine("NEXT PREDICTED MOVE WILL BE" + predictedMove[1] + "," + predictedMove[2]);
                }
            }
            else
            {
                Console.WriteLine("First move");
                //result = makeMove((rows / 2) - 1, (cols / 2) - 1, player2); // actual code 5,5
                result = makeMove(0, 0, player2); //position 0,0
                if (result != "")
                {
                    result = makeMove(1, 1, player2); //position 0,0.

                }
            }
            return result;
        }

        public int[] bestCoordinates(int row, int col, Player player)
        {

            List<int> scorePoints = new List<int>();
            List<int> rowCordinates = new List<int>();
            List<int> colCordinates = new List<int>();
            int rowMaxlimit = this.rows - 1;
            int colMaxlimit = this.cols - 1;


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
                        if (cellArray[i, col].isCellOccupied() == false)
                        {
                            rowCordinates.Add(i);
                            colCordinates.Add(col);
                            scorePoints.Add(score);
                        }
                    }
                }
            }

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
                        if (cellArray[i, col].isCellOccupied() == false)
                        {
                            rowCordinates.Add(i);
                            colCordinates.Add(col);
                            scorePoints.Add(score);
                        }
                    }
                }
            }

            //---------------------------------------------------Console.WriteLine("Right side row cell");
            score = 0;
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
                        if (cellArray[row, i].isCellOccupied() == false)
                        {
                            rowCordinates.Add(row);
                            colCordinates.Add(i);
                            scorePoints.Add(score);
                        }
                    }
                }
            }


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
                        if (cellArray[row, i].isCellOccupied() == false)
                        {
                            rowCordinates.Add(row);
                            colCordinates.Add(i);
                            scorePoints.Add(score);
                        }
                    }
                }
            }
            //scorePoints.Add(score);
            //---------------------------------------------------Console.WriteLine("Diagonal right up  cells");
            score = 0;
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
                        if (cellArray[i, j].isCellOccupied() == false)
                        {
                            rowCordinates.Add(i);
                            colCordinates.Add(j);
                            scorePoints.Add(score);
                        }
                    }
                }
            }
            //scorePoints.Add(score);
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
                        if (cellArray[i, col].isCellOccupied() == false)
                        {
                            rowCordinates.Add(i);
                            colCordinates.Add(j);
                            scorePoints.Add(score);
                        }
                    }
                }
            }
            //scorePoints.Add(score);
            //---------------------------------------------------Console.WriteLine("Diagonal left up  cells");
            score = 0;
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
                        if (cellArray[i, col].isCellOccupied() == false)
                        {
                            rowCordinates.Add(i);
                            colCordinates.Add(j);
                            scorePoints.Add(score);
                        }

                    }
                }
            }
            //scorePoints.Add(score);
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
                        if (cellArray[i, col].isCellOccupied() == false)
                        {
                            rowCordinates.Add(i);
                            colCordinates.Add(j);
                            scorePoints.Add(score);
                        }
                    }
                }
            }
            int Maxscore = scorePoints.Max();
            int[] coords = { 0, 0, 0 };
            for (int i = 0; i < (scorePoints.Count - 1); i++)
            {
                if (Maxscore == scorePoints[i])
                {
                    coords[0] = Maxscore;
                    coords[1] = rowCordinates[i];
                    coords[2] = colCordinates[i];
                    break;
                }
            }
            return coords;
        }

        public void saveData()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(@"./data/cellArray", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, this.cellArray);
                stream.Close();

                stream = new FileStream(@"./data/cellHistory", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, this.cellHistory);
                stream.Close();

                stream = new FileStream(@"./data/player1", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, this.player1);
                stream.Close();

                stream = new FileStream(@"./data/player2", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, this.player2);
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR" + e);
            }


        }

        public bool loadData()
        {

            IFormatter formatter = new BinaryFormatter();

            try
            {
                Stream stream = new FileStream(@"./data/cellArray", FileMode.Open, FileAccess.Read);
                this.cellArray = (Cell[,])formatter.Deserialize(stream);
                stream.Close();

                stream = new FileStream(@"./data/cellHistory", FileMode.Open, FileAccess.Read);
                this.cellHistory = (List<Cell>)formatter.Deserialize(stream);
                stream.Close();

                stream = new FileStream(@"./data/player1", FileMode.Open, FileAccess.Read);
                this.player1 = (Player)formatter.Deserialize(stream);
                stream.Close();

                stream = new FileStream(@"./data/player2", FileMode.Open, FileAccess.Read);
                this.player2 = (Player)formatter.Deserialize(stream);
                stream.Close();


                return true;

            }
            catch (FileNotFoundException e)
            {
                //Console.WriteLine("Can not retrive data" + e);
                return false;
            }


            Console.WriteLine("-----------Loaded data--------------");
            Console.WriteLine("cellHistory.size=" + cellHistory.Count);
            Console.WriteLine("Player1" + player1.getName());
            Console.WriteLine("Player2" + player2.getName());
        }

        public void deleteFiles()
        {
            string[] files = Directory.GetFiles(@"./Data/");
            foreach (string file in files)
            {
                File.Delete(file);
                Console.WriteLine($"{file} is deleted.");
            }
        }

    }
}
