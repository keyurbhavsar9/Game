using System;
namespace Game
{
    class Game
    {
        static int choice = 0;
        static Gomoku gomoku = null;
        static bool gamemode; //true = human // false = computer  

        static Player player1;
        static Player player2;
        static void Main(string[] args)
        {
            Console.ResetColor();
            while (choice != 9)
            {
                print("-----Welcome to game-----");
                print("Press 1 to start Gomoku");
                print("Press 2 to start Othello/Reversi");
                print("Press 3 to start 9 Mens Morris");
                print("Press 9 to Quit game");
                try
                {
                    choice = Convert.ToInt16(Console.ReadLine());
                }
                catch
                {
                    choice = 0;
                }

                switch (choice)
                {
                    case 1:
                        runGomoku();
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

                if (choice == 99)
                {
                    break;
                }
            }
            printInfo("Final Board");
            gomoku.drawGomokuBoard();
            print("Thanks for playing");
        }

        private static void runGomoku()
        {
            Console.Clear();
            bool controlFlag = true; // flag to control loop
            gomoku = new Gomoku(10, 10, null, null); // rows,cols
            bool savedData = gomoku.loadData();
            bool newGame = true;

            Console.WriteLine(" SAVED DATA " + savedData);
            if (savedData)
            {

                Console.WriteLine("Old game data found do you want to restore it ?. press Y to restore, anything else for new game");
                char restore;
                try
                {
                    restore = Convert.ToChar(Console.ReadLine());
                }
                catch
                {
                    restore = 'N';
                }
                if (restore == 'Y' || restore == 'y')
                {
                    Player p1 = gomoku.player1;
                    Player p2 = gomoku.player2;
                    player1 = p1;
                    player2 = p2;
                    newGame = false;
                }
                else
                {
                    gomoku.deleteFiles();
                    newGame = true;
                }
            }

            if (newGame)
            {
                printInfo("--------Welcome to Gomoku--------");
                printInfo("Please select your game mode \nPress 1 to play with human player press 2 to play with Computer");
                while (controlFlag)
                {
                    try
                    {
                        int userchoice = Convert.ToInt16(Console.ReadLine());
                        if (userchoice == 1)
                        {
                            gamemode = true;
                            controlFlag = false;
                        }
                        else if (userchoice == 2)
                        {
                            gamemode = false;
                            controlFlag = false;
                        }
                        else
                        {
                            printError("Invalid selection");
                            controlFlag = true;
                        }
                    }
                    catch
                    {
                        printError("Invalid selection");
                        controlFlag = true;
                    }

                }


                if (gamemode == true)
                {
                    player1 = capturePlayer(1);
                    player2 = capturePlayer(2);
                }
                else
                {
                    player1 = capturePlayer(1);
                    player2 = new Player("Computer", 'C', 0);
                }

                gomoku = new Gomoku(10, 10, player1, player2); // rows,cols
            }






            Console.Clear();
            printInfo("GAME STARTS NOW");
            printInfo("Initial state of board");

            bool controlFlag1 = true;
            bool controlFlag2 = true;
            while (controlFlag1 && controlFlag2)
            {
                gomoku.drawGomokuBoard();
                controlFlag1 = PlayerMove(player1);
                if (!controlFlag1)
                    break;
                gomoku.drawGomokuBoard();
                controlFlag2 = PlayerMove(player2);
                if (!controlFlag2)
                    break;
            }
        }

        public static void print(String message)
        {
            Console.WriteLine(message);
        }

        public static void printInfo(String message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void printError(String message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static bool PlayerMove(Player player)
        {
            Console.WriteLine("MOVE METHOD CALLED");
            bool controlFlag = true;
            String result = "";
            while (controlFlag)
            {
                if (player.getName() == "Computer")
                {
                    result = gomoku.computerEasyMove();
                }
                else
                {
                gotoLabel:
                    try
                    {
                        printInfo("please select command");
                        print("1 to make move");
                        print("2 to save game");

                        int command = Convert.ToInt16(Console.ReadLine());
                        switch (command)
                        {
                            case 1:
                                printInfo("Hint: click on space(-) where you want to make move it makes easier to identify ROW and COLUMN");
                                print(player.getName() + "'s move");
                                print("select column, as example A");
                                char playerColumn = Convert.ToChar(Console.ReadLine());


                                print("select row, as example 1");
                                int playerRow = Convert.ToInt16(Console.ReadLine()) - 1; // -1 adjustment made for identifying current index
                                int index = Convert.ToInt32(playerColumn) - 65;
                                result = gomoku.makeMove(playerRow, index, player);
                                break;
                            case 2:
                                gomoku.deleteFiles();
                                gomoku.saveData();
                                printInfo("GAME DATA SAVED");
                                Environment.Exit(0);
                                break;
                            default:
                                result = "Invalid choice";
                                break;
                        }


                    }
                    catch
                    {
                        printError("Invalid move");
                        goto gotoLabel;
                    }
                }
                if (result == "WIN")
                {
                    printInfo("Player " + player.getName() + " has won the game");
                    return false; // to end the move loop
                }
                else if (result != "")
                {
                    printError(result);
                }
                else
                {
                    controlFlag = false;
                }
            }
            return !controlFlag;  // riverse the control flag to monitor player
        }

        public static Player capturePlayer(int i)
        {
            bool controlFlag = true;
            while (controlFlag)
            {

                try
                {
                    print("Enter Player " + i + " Name");
                    String playerName = Console.ReadLine();

                enterChar:
                    print("Please enter a character as your symbol e.g. %,!,*,$,A...Z,a...z ");
                    printError(" '-'(Hyphen/dash) and 'C' are invalid charachters ");

                    char pcharacter = Convert.ToChar(Console.ReadLine());

                    if (pcharacter == '-')
                    {
                        printError("'-' (dash/hyphen) is not a valid character");
                        goto enterChar;
                    }
                    else if (pcharacter == 'C')
                    {
                        print("'C' is already occupied by the system so please choose a valid character again");
                        goto enterChar;
                    }

                    print("Player " + i + " is " + playerName + " and Player " + i + " character is " + pcharacter);


                    printInfo("Press Y for confirm, anything else if you want to change name and character for player " + i);

                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y' || choice == 'y')
                    {
                        controlFlag = false;
                        Player player = new Player(playerName, pcharacter, 0);
                        return player;
                    }
                }
                catch
                {
                    printError("Invalid name or character please enter valid details");
                    controlFlag = true;
                }
            }
            return null;

        }

        public static void displayPlayerInfo(Player player, int playerRank)
        {
            String name = player.getName();
            char pchar = player.getPlayerSymbol();
            printInfo("Player " + playerRank + " is " + name + " and " + name + "'s Play character is " + pchar);
        }
    }


}
