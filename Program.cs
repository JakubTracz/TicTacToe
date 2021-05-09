using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace LearnC
{
    class Program
    {
        //Initial settings
        static bool gameSwitch = true;
        static int player = 2;
        static int p1 = 0;
        static bool correctField = true;

        static void Main(string[] args)
        {
            Board board = new Board();
            People players = new People();

            board.ResetBoard();
            board.RedrawBoard();

            //game
            while (gameSwitch)
            {
                player = players.ChangePlayer(player, correctField);
                correctField = board.CheckNumber();
                gameSwitch = board.GameOver(player, gameSwitch);
            }
            Console.WriteLine("Game ended.");
            Console.ReadLine();
        }

        class Board
        {
            public string[,] fields = new string[3, 3];  //actual board
            public string[,] fields2 = new string[3, 3]; //board to check for taken fields

            /// <summary>
            /// empty Board constructor
            /// </summary>
            public int ResetBoard()
            {
                int k = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        fields[i, j] = Convert.ToString(k + 1);
                        fields2[i, j] = Convert.ToString(k + 1);
                        k++;
                    }
                }
                return player = 2;
            }
            /// <summary>
            /// redraw the board after choosing the field by the player
            /// </summary>
            public void RedrawBoard()
            {
                Console.Clear();
                Console.WriteLine("       |       |       ");
                Console.WriteLine("   " + (fields[0, 0]) + "   |   " + (fields[0, 1]) + "   |   " + (fields[0, 2]) + "   ");
                Console.WriteLine("_______|_______|_______");
                Console.WriteLine("       |       |       ");
                Console.WriteLine("   " + (fields[1, 0]) + "   |   " + (fields[1, 1]) + "   |   " + (fields[1, 2]) + "   ");
                Console.WriteLine("_______|_______|_______");
                Console.WriteLine("       |       |       ");
                Console.WriteLine("   " + (fields[2, 0]) + "   |   " + (fields[2, 1]) + "   |   " + (fields[2, 2]) + "   ");
                Console.WriteLine("       |       |       ");
            }

            /// <summary>
            /// Check the provided number and mark it on the board/check for incorrect values/taken fields
            /// </summary>
            /// <returns>correctField</returns>
            public bool CheckNumber()
            {
                bool correctField = true;
                bool typeCheck = int.TryParse(Console.ReadLine(), out p1);
                if (typeCheck && (p1 >= 1 && p1 <= 9))
                {
                    for (int i = 0; i < fields.GetLength(0); i++)
                    {
                        for (int j = 0; j < fields.GetLength(1); j++)
                        {
                            string numberstring = Convert.ToString(p1);
                            if (fields[i, j] == numberstring)
                            {
                                fields[i, j] = (player == 1) ? "O" : "X";
                                RedrawBoard();
                            }
                            else
                            {
                                if ((fields[i, j] == "O" || fields[i, j] == "X") && numberstring == fields2[i, j])
                                {
                                    Console.WriteLine("Field already taken. Plese press enter and try again.");
                                    Console.ReadLine();
                                    RedrawBoard();
                                    correctField = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Provided input is invalid. Plese press enter and try again.");
                    Console.ReadLine();
                    RedrawBoard();
                    correctField = false;
                }
                return correctField;
            }

            /// <summary>
            /// Check if P1 or P2 won or if it's a draw.
            /// </summary>
            /// <param name="p"></param>
            /// <returns>gameSwitch</returns>
            public bool GameOver(int p, bool s)
            {
                string psign;
                bool gameSwitch = s;

                psign = (p == 1) ? "O" : "X";

                if ((fields[0, 0] == psign && fields[0, 1] == psign && fields[0, 2] == psign)
                    || (fields[1, 0] == psign && fields[1, 1] == psign && fields[1, 2] == psign)
                    || (fields[2, 0] == psign && fields[2, 1] == psign && fields[2, 2] == psign)
                    || (fields[0, 0] == psign && fields[1, 0] == psign && fields[2, 0] == psign)
                    || (fields[0, 1] == psign && fields[1, 1] == psign && fields[2, 1] == psign)
                    || (fields[0, 2] == psign && fields[1, 2] == psign && fields[2, 2] == psign)
                    || (fields[0, 0] == psign && fields[1, 1] == psign && fields[2, 2] == psign)
                    || (fields[0, 2] == psign && fields[1, 1] == psign && fields[2, 0] == psign))
                {
                    Console.WriteLine($"Player {player} won!" + "\n");
                    return RestartGame(player);

                }
                else if ((fields[0, 0] == "O" || fields[0, 0] == "X")
                    && (fields[0, 1] == "O" || fields[0, 1] == "X")
                    && (fields[0, 2] == "O" || fields[0, 2] == "X")
                    && (fields[1, 0] == "O" || fields[1, 0] == "X")
                    && (fields[1, 1] == "O" || fields[1, 1] == "X")
                    && (fields[1, 2] == "O" || fields[1, 2] == "X")
                    && (fields[2, 0] == "O" || fields[2, 0] == "X")
                    && (fields[2, 1] == "O" || fields[2, 1] == "X")
                    && (fields[2, 2] == "O" || fields[2, 2] == "X"))
                {
                    Console.WriteLine("It's a draw!");
                    return RestartGame(player);
                }
                else
                {
                    return gameSwitch;
                }
            }
            //Restarts the game or ends it
            public bool RestartGame(int player)
            {
                bool ifCorrect = false;

                while (!ifCorrect)
                {
                    Console.WriteLine("Do you want to play again? Please type \"Y\" or \"N\" and press Enter.");
                    string answer = Console.ReadLine();

                    if (answer == "Y")
                    {
                        ifCorrect = true;
                        gameSwitch = true;
                        ResetBoard();
                        RedrawBoard();
                    }
                    else if (answer == "N")
                    {
                        ifCorrect = true;
                        gameSwitch = false;
                    }
                    else
                    {
                        ifCorrect = false;
                        Console.WriteLine("Provided answer is incorrect. Please try again.");
                    }
                }
                return gameSwitch;
            }
        }
    }
    class People
    {
        /// <summary>
        ///  Checks if the player should change and return their name
        /// </summary>
        /// <param name="p"></param>
        /// <param name="correct"></param>
        /// <returns>Name of the next player</returns>
        public int ChangePlayer(int p, bool correct)
        {
            int player = p;

            if (correct)
            {
                player = (p == 1) ? 2 : 1;
            }

            Console.WriteLine();
            Console.Write($"Player {player}: Choose your field (1-9) and press Enter.");
            return player;
        }
    }
}



