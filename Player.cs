using System;
namespace Game
{
    class Player
    {
        String name;
        char playerSymbol;
        int score=0;
        
        public Player(String name,char playerSymbol,int score){
            this.name=name;
        }

        public void setName(String name){
            this.name=name;
        }

        public String getName(){
            return name;
        }

        public void setPlayerSymbol(char playerSymbol){
            this.playerSymbol=playerSymbol;
        }

        public char getPlayerSymbol(){
            return playerSymbol;
        }
        public void setScore(int score){
            this.score=score;
        }
        public int getScore(){
            return score;
        }
    }
}
