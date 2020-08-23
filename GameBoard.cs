using System;
namespace Game
{
    class GameBoard
    {
        private Cell[,] cellArray;
        private Player player1, player2;
        private int rows,cols;

        public GameBoard(int cols,int rows,Player player1,Player player2){
            this.rows=rows;
            this.cols= cols;
            this.cellArray= new Cell[rows,cols];
            this.player1 = player1;
            this.player2 = player2;
            fillarray();
        }
        
        private void fillarray(){
            for(int i=0;i<rows;i++){
                for(int j=0;j<rows;j++){
                    Cell cell = new Cell();
                    cellArray[i,j]= cell;
                }
            }
        }

        public Cell[,] getCellArray(){
            return cellArray;
        }

        public bool makeMove(int row,char col, Player player){
            int index= Convert.ToInt32(col)-65; //-65 to convert character to index
            Console.WriteLine("Selected row ="+ row);
            Console.WriteLine("Selected col ="+ index);
            //cellArray[row,index].setSellOccupied(true);
            //Console.WriteLine("Select char is"+index);
            // Cell selectedCell=cellArray[row,index]; 
            // if(selectedCell == null){
            //     Console.WriteLine("Null Cell");
            // }
            // if(selectedCell.isCellOccupied()){
            //     return false;
            // }
            // Cell cell= new Cell(true, player);
            // selectedCell = cell;
            // return true;
            for(int i=0;i<rows;i++){
                for(int j=0;j<cols;j++){
                    Cell cell=cellArray[i,j];
                    Console.Write(" "+cell.isCellOccupied()+" ");
                }
                Console.WriteLine();
            }
            //Console.WriteLine("Array size"+cellArray[0,0].);
            return true;
        }
    }
}
