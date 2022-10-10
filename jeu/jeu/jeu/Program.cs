using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace jeu
{
    internal class Program
    {
        public const string CURSOR = "-->";
        static sbyte cursorPosition = 0;
        static void Main(string[] args)
        {
            //set the size of the console
            Console.WindowHeight = 55;
            Console.WindowWidth = 120;
            Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);

            List<string> choiceList = new List<string> { "Play", "Options", "Scores", "About", "Exit" };


            //write the menu
            DisplayMenu();
            //write the initial cursor
            Console.SetCursorPosition(38, 15);
            Console.Write(CURSOR);

            //input choice loop
            while (true)
            {
                //up and down the cursor
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        EraseCursor();
                        if (cursorPosition > 0)
                            cursorPosition--;
                        DisplayCursor();

                        break;
                    case ConsoleKey.DownArrow:
                        EraseCursor();
                        if (cursorPosition < 4)
                            cursorPosition++;
                        DisplayCursor();



                        break;
                    case ConsoleKey.Enter:
                        switch (choiceList[cursorPosition])
                        {
                            case "Play":
                                break;
                            case "Options":
                                Console.Clear();
                                DisplayOptions();
                                break;
                            case "Scores":
                                break;
                            case "About":
                                break;
                            case "Exit":
                                Environment.Exit(0);
                                break;

                        }
                        break;
                    default:
                        DisplayMenu();
                        break;
                }


            }
        }

        /// <summary>
        /// function to write the entire menu
        /// </summary>
        static void DisplayMenu()
        {
            Console.Write(@"



                              _______..______      ___       ______ ____    ____    ___   ___
                             /       ||   _  \    /   \     /      |\   \  /   /    \  \ /  /
                            |   (----`|  |_)  |  /  ^  \   |  ,----' \   \/   /      \  V  /
                             \   \    |   ___/  /  /_\  \  |  |       \_    _/        >   <
                         .----)   |   |  |     /  _____  \ |  `----.    |  |         /  .  \
                         |_______/    | _|    /__/     \__\ \______|    |__|        /__/ \__\



                                                  ____  _
                                                 |  _ \| | __ _ _   _
                                                 | |_) | |/ _` | | | |
                                                 |  __/| | (_| | |_| |
                                                 |_|   |_|\__,_|\__, |
                                                                |___/



                                            ___        _   _
                                           / _ \ _ __ | |_(_) ___  _ __  ___
                                          | | | | '_ \| __| |/ _ \| '_ \/ __|
                                          | |_| | |_) | |_| | (_) | | | \__ \
                                           \___/| .__/ \__|_|\___/|_| |_|___/
                                                |_|



                                             ____
                                            / ___|  ___ ___  _ __ ___  ___
                                            \___ \ / __/ _ \| '__/ _ \/ __|
                                             ___) | (_| (_) | | |  __/\__ \
                                            |____/ \___\___/|_|  \___||___/



                                                _    _                 _
                                               / \  | |__   ___  _   _| |_
                                              / _ \ | '_ \ / _ \| | | | __|
                                             / ___ \| |_) | (_) | |_| | |_
                                            /_/   \_\_.__/ \___/ \__,_|\__|



                                                  _____      _ _
                                                 | ____|_  _(_) |_
                                                 |  _| \ \/ / | __|
                                                 | |___ >  <| | |_
                                                 |_____/_/\_\_|\__|");
        }
        static void EraseCursor()
        {
            //erase the previous cursor
            switch (cursorPosition)
            {
                case 0:
                    Console.SetCursorPosition(38, 15);
                    Console.Write("   ");
                    break;
                case 1:
                    Console.SetCursorPosition(31, 24);
                    Console.Write("   ");
                    break;
                case 2:
                    Console.SetCursorPosition(33, 33);
                    Console.Write("   ");
                    break;
                case 3:
                    Console.SetCursorPosition(33, 41);
                    Console.Write("   ");
                    break;
                case 4:
                    Console.SetCursorPosition(38, 49);
                    Console.Write("   ");
                    break;
                default:
                    Console.SetCursorPosition(38, 15);
                    Console.Write("   ");
                    break;
            }
        }
        static void DisplayCursor()
        {

            //write the cursor on the menu
            switch (cursorPosition)
            {
                case 0:
                    Console.SetCursorPosition(38, 15);
                    Console.Write(CURSOR);
                    break;
                case 1:
                    Console.SetCursorPosition(31, 24);
                    Console.Write(CURSOR);
                    break;
                case 2:
                    Console.SetCursorPosition(33, 33);
                    Console.Write(CURSOR);
                    break;
                case 3:
                    Console.SetCursorPosition(33, 41);
                    Console.Write(CURSOR);
                    break;
                case 4:
                    Console.SetCursorPosition(38, 49);
                    Console.Write(CURSOR);
                    break;
                default:
                    Console.SetCursorPosition(38, 15);
                    Console.Write(CURSOR);
                    break;
            }
        }
        static void DisplayOptions()
        {
            Console.WriteLine("Options");
        }
    }
}