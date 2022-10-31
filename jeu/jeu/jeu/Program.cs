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

            string[] choiceList = new string[] { "Play", "Options", "Scores", "About", "Exit" };
            string activePage = "menu";
            byte nbOptions = 5;
            string[] difficultyList = new string[] { "easy", "normal", "difficult", "godmod" };
            byte difficulty = 1;
            bool music = true;



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
                        EraseCursor(activePage);
                        if (cursorPosition > 0)
                            cursorPosition--;
                        DisplayCursor(activePage);

                        break;
                    case ConsoleKey.DownArrow:
                        EraseCursor(activePage);
                        if (cursorPosition < nbOptions - 1)
                            cursorPosition++;
                        DisplayCursor(activePage);

                        break;
                    case ConsoleKey.LeftArrow:
                        if (activePage == "options" && cursorPosition == 0)
                        {
                            if (difficulty == 0)
                                difficulty = 3;
                            else
                                difficulty --;
                        }
                        else if (activePage == "options" && cursorPosition == 1)
                        {
                            if (music == true)
                                music = false;
                            else
                                music = true;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (activePage == "options" && cursorPosition == 0)
                        {
                            if (difficulty == 3)
                                difficulty = 0;
                            else
                                difficulty++;
                        }
                        else if (activePage == "options" && cursorPosition == 1)
                        {
                            if (music == true)
                                music = false;
                            else
                                music = true;
                        }
                        break;

                    case ConsoleKey.Enter:
                        switch (choiceList[cursorPosition])
                        {
                            case "Play":
                                break;
                            case "Options":
                                Console.Clear();
                                DisplayOptions();
                                activePage = "options";
                                nbOptions = 2;
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
                        Console.Clear();
                        DisplayMenu();
                        activePage = "menu";
                        nbOptions = 5;
                        cursorPosition = 0;
                        //write the initial cursor
                        Console.SetCursorPosition(38, 15);
                        Console.Write(CURSOR);
                        break;
                }

            }
        }

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
        static void EraseCursor(string activePage)
        {
            switch (activePage)
            {
                case "menu":
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
                    break;

                case "options":
                    switch (cursorPosition)
                    {
                        case 0:
                            //write the left arrow
                            Console.SetCursorPosition(23, 15);
                            Console.WriteLine(@"   ");
                            Console.SetCursorPosition(23, 16);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(23, 17);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(23, 18);
                            Console.WriteLine(@"   ");

                            //write the right arrow
                            Console.SetCursorPosition(88, 15);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(88, 16);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(88, 17);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(88, 18);
                            Console.WriteLine(@"         ");
                            break;
                        case 1:
                            //write the left arrow
                            Console.SetCursorPosition(33, 36);
                            Console.WriteLine(@"   ");
                            Console.SetCursorPosition(33, 37);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(33, 38);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(33, 39);
                            Console.WriteLine(@"   ");

                            //write the right arrow
                            Console.SetCursorPosition(75, 36);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(75, 37);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(75, 38);
                            Console.WriteLine(@"         ");
                            Console.SetCursorPosition(75, 39);
                            Console.WriteLine(@"         ");
                            break;
                    }
                    break;
            }

        }
        static void DisplayCursor(string activePage)
        {
            switch (activePage)
            {
                case "menu":
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
                    break;
                case "options":
                    switch (cursorPosition)
                    {
                        case 0:
                            //write the left arrow
                            Console.SetCursorPosition(23, 15);
                            Console.WriteLine(@"  ▄");
                            Console.SetCursorPosition(23, 16);
                            Console.WriteLine(@"▄██▄▄▄▄▄▄");
                            Console.SetCursorPosition(23, 17);
                            Console.WriteLine(@"▀██▀▀▀▀▀▀");
                            Console.SetCursorPosition(23, 18);
                            Console.WriteLine(@"  ▀");

                            //write the right arrow
                            Console.SetCursorPosition(88, 15);
                            Console.WriteLine(@"      ▄  ");
                            Console.SetCursorPosition(88, 16);
                            Console.WriteLine(@"▄▄▄▄▄▄██▄");
                            Console.SetCursorPosition(88, 17);
                            Console.WriteLine(@"▀▀▀▀▀▀██▀");
                            Console.SetCursorPosition(88, 18);
                            Console.WriteLine(@"      ▀  ");
                            break;
                        case 1:
                            //write the left arrow
                            Console.SetCursorPosition(33, 36);
                            Console.WriteLine(@"  ▄");
                            Console.SetCursorPosition(33, 37);
                            Console.WriteLine(@"▄██▄▄▄▄▄▄");
                            Console.SetCursorPosition(33, 38);
                            Console.WriteLine(@"▀██▀▀▀▀▀▀");
                            Console.SetCursorPosition(33, 39);
                            Console.WriteLine(@"  ▀");

                            //write the right arrow
                            Console.SetCursorPosition(75, 36);
                            Console.WriteLine(@"      ▄  ");
                            Console.SetCursorPosition(75, 37);
                            Console.WriteLine(@"▄▄▄▄▄▄██▄");
                            Console.SetCursorPosition(75, 38);
                            Console.WriteLine(@"▀▀▀▀▀▀██▀");
                            Console.SetCursorPosition(75, 39);
                            Console.WriteLine(@"      ▀  ");
                            break;
                    }
                    break;
            }

        }
        static void DisplayDifficulty(byte difficulty)
        {

        }
        static void DisplayMusic(bool music)
        {
            if (music == true)
            {
                Console.SetCursorPosition(23, 40);
                Console.WriteLine(@"   ___  _ __  ");
                Console.WriteLine(@"  / _ \| '_ \ ");
                Console.WriteLine(@" | (_) | | | |");
                Console.WriteLine(@"  \___/|_| |_|");
            }
            if (music == false)
            {
                Console.WriteLine(@"         __  __ ");
                Console.WriteLine(@"   ___  / _|/ _|");
                Console.WriteLine(@"  / _ \| |_| |_ ");
                Console.WriteLine(@" | (_) |  _|  _|");
                Console.WriteLine(@"  \___/|_| |_|  ");
            }    
                
        }
        static void DisplayOptions()
        {
            Console.WriteLine(@"



                                         ____  _  __  __ _            _ _         
                                        |  _ \(_)/ _|/ _(_) ___ _   _| | |_ _   _ 
                                        | | | | | |_| |_| |/ __| | | | | __| | | |
                                        | |_| | |  _|  _| | (__| |_| | | |_| |_| |
                                        |____/|_|_| |_| |_|\___|\__,_|_|\__|\__, |
                                                                            |___/ 




                                                                            _ 
                                           _ __   ___  _ __ _ __ ___   __ _| |            
                                          | '_ \ / _ \| '__| '_ ` _ \ / _` | |         
                                          | | | | (_) | |  | | | | | | (_| | |          
                                          |_| |_|\___/|_|  |_| |_| |_|\__,_|_|                  








                                                __  __           _      
                                               |  \/  |_   _ ___(_) ___ 
                                               | |\/| | | | / __| |/ __|
                                               | |  | | |_| \__ \ | (__ 
                                               |_|  |_|\__,_|___/_|\___|




                                                      ___  _ __  
                                                     / _ \| '_ \ 
                                                    | (_) | | | |
                                                     \___/|_| |_| 
             ");
        }
    }
}