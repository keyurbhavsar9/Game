using System;
namespace Game
{
    class Cell
    {
        bool cellOccupied= false;
        Player occupiedByPlayer = null;
        public Cell(){
            this.cellOccupied=false;
            this.occupiedByPlayer=null;
        }
        public Cell(bool cellOccupied, Player player ){
            this.cellOccupied= cellOccupied;
            this.occupiedByPlayer = player;
        }
        public bool isCellOccupied(){
            return cellOccupied;
        }

        public void setSellOccupied(bool cellOccupied){
            this.cellOccupied = cellOccupied;
        }
    }
}
