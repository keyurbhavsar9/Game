using System;
namespace Game  
{
    class Game 
    {
        static int choice = 0;
        static Riversi riversi= null;

        static void Main(string[] args)
        {
            while(choice != 9){
                print("-----Welcome to game-----");
                print("Press 1 to start Gomoku/Reversi");
                print("Press 2 to start Othello");
                print("Press 3 to start 9 Mens Morris");
                print("Press 9 to Quit game");
                try{
                    choice = Convert.ToInt16(Console.ReadLine()); 
                }catch(Exception e){
                   choice=0;
                }
                
                switch(choice){
                    case 1:
                        runRiversi();
                        choice = 99; // flag to break the while loop
                        break;
                    case 2:
                        print("Feature is not avilable yet");
                        break;
                    case 3:
                        print("Feature is not avilable yet");
                        break;
                    case 9:
                        print("Quitting game");
                        break;
                    default:
                        print("Invalid choice");
                        break;
                }  

                if(choice == 99){
                    break;
                }
            }
            print("Thanks for playing");
        }

        private static void runRiversi(){
            Console.Clear();
            bool controlFlag= true; // flag to control loop
            String player1Name="",player2Name="";
            char p1character=' ',p2character=' ';
            printInfo("--------Welcome to reversi/Gomoku--------");
            // while(controlFlag){

            //     try{
            //         print("Enter Player 1 Name");
            //         player1Name = Console.ReadLine(); 

            //         enterChar:
            //         print("Please enter a character as your symbol e.g. %,!,*,$,A...Z,a...z");
            //         p1character = Convert.ToChar(Console.ReadLine());

            //         print("Player 1 is "+player1Name +" and Player 1 character is "+p1character);
            //         Console.ForegroundColor=ConsoleColor.Blue;
                
            //         printInfo("Press Y for confirm, anything else if you want to change name and character for player 1 ");
                
            //         char choice = Convert.ToChar(Console.ReadLine());
            //         if(choice == 'Y' || choice=='y')
            //             controlFlag = false;

            //         if(p1character=='-'){
            //         print("'-' (dash/hyphen) is not a valid character");
            //             goto enterChar;
            //         }
            //     }catch(Exception e){
            //         printError("Invalid name or character please enter valid details");
            //         controlFlag= true;
            //     }
            // }
            
            // controlFlag = true;

            // while(controlFlag){
            //     try{
            //         print("Enter Player 2 Name");
            //         player2Name = Console.ReadLine(); 

            //         redo:
            //         print("Please enter a character as your symbol e.g. %,!,*,$,A...Z,a...z");
            //         p2character = Convert.ToChar(Console.ReadLine());

            //         print("Player 2 is "+player2Name +" and Player 2 character is "+p2character);
            //         Console.ForegroundColor=ConsoleColor.Blue;
                
            //         printInfo("Press Y for confirm, anything else if you want to change name and character for player 2 ");
                    
            //         char choice = Convert.ToChar(Console.ReadLine());
            //         if(choice == 'Y' || choice=='y')
            //             controlFlag = false;

            //         if(p2character=='-'){
            //             printError("'-' (dash/hyphen) is not a valid character");
            //             goto redo;
            //         }

            //         if(p2character==p1character){
            //             printError(p2character+" is already occupoed by player 1. please choose different charachter for player 2");
            //             goto redo;
            //         }
            //     }catch(Exception e){
            //         printError("Invalid name or character please enter valid details");
            //         controlFlag= true;
            //     }

                
            // }
            // Console.Clear();
            // printInfo("Player 1 is "+player1Name +" and Player 1 character is "+p1character);
            // printInfo("Player 2 is "+player2Name +" and Player 2 character is "+p2character);
            
            player1Name = "Keyur";
            p1character='K' ;
            player2Name= "John";
            p2character = 'J';
            Player player1,player2;
            
            player1 = new Player(player1Name,p1character,0);
            player2 = new Player(player2Name,p2character,0);
            riversi = new Riversi(10,10,player1,player2); // rows,cols

            

            controlFlag = true;
            while(controlFlag){
                PlayerMove(player1);
                
                PlayerMove(player2);
            }
            
        }

        public static void print(String message){
            Console.WriteLine(message);
        }

        public static void printInfo(String message){
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void printError(String message){
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PlayerMove(Player player){
            bool controlFlag = true;
            
            while(controlFlag){
                print(player.getName()+"'s move");
                print("select column, as example A");
                char playerColumn=Convert.ToChar(Console.ReadLine());
                print("select row, as example 1");
                int playerRow=Convert.ToInt16(Console.ReadLine())-1; // -1 adjustment made for identifying current index
                String result=riversi.makeMove(playerRow,playerColumn,player);
                if(result != ""){
                   printError(result);          
                }else{
                    controlFlag = false;
                }
            }
            riversi.drawGomokuBoard();
            
        }

    
    }

    
}
