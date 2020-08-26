using System;
namespace Game
{
    class Cell
    {
        bool cellOccupied = false;
        Player occupiedByPlayer = null;
        int row = 0, col = 0;
        public Cell()
        {
            this.cellOccupied = false;
            this.occupiedByPlayer = null;
            this.row = 0;
            this.col = 0;
        }
        public Cell(bool cellOccupied, Player player, int row, int col)
        {
            this.cellOccupied = cellOccupied;
            this.occupiedByPlayer = player;
            this.row = row;
            this.col = col;
        }
        public bool isCellOccupied()
        {
            return cellOccupied;
        }

        public void setSellOccupied(bool cellOccupied)
        {
            this.cellOccupied = cellOccupied;
        }

        public Player getPlayer()
        {
            return occupiedByPlayer;
        }

        public int getRow()
        {
            return row;
        }
        public int getCol()
        {
            return col;
        }

        public String toString()
        {
            return "Is Occupied" + cellOccupied + "Occupied by player" + this.occupiedByPlayer.getName() + "ROW=" + row + " COL=" + col;
        }
    }
}
