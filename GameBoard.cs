using System;
namespace Game
{
    class GameBoard
    {
        public Cell[,] cellArray;
        private Player player1, player2;
        private int rows,cols;
        private int score = -1;

        private int winningScore = 5;
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

        public String makeMove(int row,char col, Player player){
                String Error="";
                try{
                    int index= Convert.ToInt32(col)-65; //-65 to convert character to index
                Cell ArrayCell = cellArray[row,index];

                if(row > this.rows){
                    Error = "Invalid row selection "+(row+1);
                }else if(index > this.cols){
                    Error = "Invalid column selection"+col;
                }else if(ArrayCell.isCellOccupied()){
                    Error = "Cell already occupied by"+ArrayCell.getPlayer().getName();
                }
                else{
                    Cell ModifiedCell= new Cell(true, player);
                    cellArray[row,index]= ModifiedCell;
                    int winScore=scoreCalculation(row,index,player);
                    if(winScore == 5){

                    }
                }

                // for(int i = 0 ; i < rows; i++){
                //     for(int j = 0 ; j < rows; j++){
                //         Console.Write(cellArray[i,j].getPlayer().getPlayerSymbol());
                //     }    
                //     Console.WriteLine("");
                // }
                }catch(Exception e){
                    Error = "Invalid Move";
                }
                

            return Error;
        }

        public int scoreCalculation(int row,int col,Player player){
            int rowMaxlimit=this.rows-1;
            int colMaxlimit=this.cols-1;
            
            //---------------------------------------------------Console.WriteLine("Upside column");
            score =0;
            for(int i = row;i>(row-5);i--){
                if(i>=0){                
                    Cell cell = cellArray[i,col];
                    if(cell.isCellOccupied() == true && cell.getPlayer()==player ){                        
                        score++;  
                        if(score == 5)
                            return score;
                    }else{
                        break;
                    }
                }
            }

            //---------------------------------------------------Console.WriteLine("down side column");
            score =0;
            for(int i = row;i<(row+5);i++){
                if(i<=rowMaxlimit){
                    Cell cell = cellArray[i,col];
                    if(cell.isCellOccupied() == true && cell.getPlayer()==player ){                        
                        score++;  
                        if(score == 5)
                            return score;
                    }else{
                        break;
                    }
                }
            }

            //---------------------------------------------------Console.WriteLine("Right side row cell");
            score =0;
            for(int i = col;i<(col+5);i++){
                if(i<=colMaxlimit){
                    Cell cell = cellArray[row,i];
                    if(cell.isCellOccupied() == true && cell.getPlayer()==player ){                        
                        score++;  
                        if(score == 5)
                            return score;
                    }else{
                        break;
                    }
                }                      
            }

            //---------------------------------------------------Console.WriteLine("Left side row cell");
            score =0;
            for(int i = col;i>(col-5);i--){
                if(i>=0){
                    Cell cell = cellArray[row,i];
                    if(cell.isCellOccupied() == true && cell.getPlayer()==player ){                        
                        score++;  
                        if(score == 5)
                            return score;
                    }else{
                        break;
                    }
                }
            }

            //---------------------------------------------------Console.WriteLine("Diagonal right up  cells");
            score =0;
            for(int i=row,j=col;i>(row-5) && j<(col+5);i-- , j++){
                    if(i>0 && j<= colMaxlimit ){
                        Cell cell = cellArray[i,j];
                        if(cell.isCellOccupied() == true && cell.getPlayer()==player ){                        
                            score++;  
                            if(score == 5)
                                return score;
                        }else{
                            break;
                        }  
                    }
            }

            //---------------------------------------------------Console.WriteLine("Diagonal right down  cells");
            score =0;
            for(int i=row,j=col;i<(row+5) && j<(col+5);i++ , j++){
                    if(i<=rowMaxlimit && j<= colMaxlimit ){
                        Cell cell = cellArray[i,j];
                        if(cell.isCellOccupied() == true && cell.getPlayer()==player ){                    
                            score++;  
                            if(score == 5)
                                return score;
                        }else{
                            break;
                        } 
                    }
            }

            //---------------------------------------------------Console.WriteLine("Diagonal left up  cells");
            score =0;
            for(int i=row,j=col;i>(row-5) && j>(col-5);i-- , j--){
                    if(i>=0 && j>=0 ){
                        Cell cell = cellArray[i,j];
                        if(cell.isCellOccupied() == true && cell.getPlayer()==player ){                        
                            score++;  
                            if(score == 5)
                                return score;
                        }else{
                            break;
                        }
                    }
            }

            //---------------------------------------------------Console.WriteLine("Diagonal left down  cells");
            score =0;
            for(int i=row,j=col;i<(row+5) && j>(col-5);i++ , j--){
                    if(i<=rowMaxlimit && j>= 0 ){
                        Cell cell = cellArray[i,j];
                        if(cell.isCellOccupied() == true && cell.getPlayer()==player ){                        
                            score++;  
                            if(score == 5)
                                return score;
                        }else{
                            break;
                        }  
                    }
            }
            return 0;

        }


        public int getPlayerScore(Player player){
            int score = 0;
            if(player.getName().Equals(this.player1.getName()))
                score = this.player1.getScore();
            else
                score = this.player2.getScore();
            return score;
        }

    }
}
