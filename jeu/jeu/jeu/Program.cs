using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            int cursorX = 0;
            int cursorY = 0;
            Game game = null;
            

            //write the menu
            DisplayMenu();
            //write the initial cursor
            DisplayCursor(activePage);

            //input choice loop
            while (true)
            {
                //up and down the cursor
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if(activePage == "play")
                        {
                            game.Vessel.VesselShot();
                        }
                        if (activePage == "about")
                            EraseOtherKey(cursorX, cursorY);
                        EraseCursor(activePage);
                        if (cursorPosition > 0)
                            cursorPosition--;
                        DisplayCursor(activePage);
                        break;

                    case ConsoleKey.DownArrow:
                        if (activePage == "about")
                            EraseOtherKey(cursorX, cursorY);
                        EraseCursor(activePage);
                        if (cursorPosition < nbOptions - 1)
                            cursorPosition++;
                        DisplayCursor(activePage);
                        break;

                    case ConsoleKey.LeftArrow:
                        if (activePage == "play")
                        {
                            game.Vessel.EraseVessel();
                            game.Vessel.ColumnPosition -= 1;
                            game.Vessel.DisplayVessel();
                        }
                        if (activePage == "about")
                            EraseOtherKey(cursorX,cursorY);
                        if (activePage == "options" && cursorPosition == 0)
                        {
                            if (difficulty == 0)
                            {
                                EraseDifficulty(difficulty);
                                difficulty = 3;
                                DisplayDifficulty(difficulty);
                            }

                            else
                            {
                                EraseDifficulty(difficulty);
                                difficulty--;
                                DisplayDifficulty(difficulty);
                            }

                        }
                        else if (activePage == "options" && cursorPosition == 1)
                        {
                            if (music == true)
                            {
                                music = false;
                                EraseMusic(music);
                                DisplayMusic(music);
                            }

                            else
                            {
                                music = true;
                                EraseMusic(music);
                                DisplayMusic(music);
                            }

                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (activePage == "play")
                        {
                            game.Vessel.EraseVessel();
                            game.Vessel.ColumnPosition += 1;
                            game.Vessel.DisplayVessel();
                        }
                        if (activePage == "about")
                            EraseOtherKey(cursorX, cursorY);
                        if (activePage == "options" && cursorPosition == 0)
                        {
                            if (difficulty == 3)
                            {
                                EraseDifficulty(difficulty);
                                difficulty = 0;
                                DisplayDifficulty(difficulty);
                            }

                            else
                            {
                                EraseDifficulty(difficulty);
                                difficulty++;
                                DisplayDifficulty(difficulty);
                            }

                        }
                        else if (activePage == "options" && cursorPosition == 1)
                        {
                            if (music == true)
                            {
                                music = false;
                                EraseMusic(music);
                                DisplayMusic(music);
                            }
                            else
                            {
                                music = true;
                                EraseMusic(music);
                                DisplayMusic(music);
                            }
                        }
                        break;

                    case ConsoleKey.Enter:
                        switch (choiceList[cursorPosition])
                        {
                            case "Play":
                                Console.Clear();
                                game = StartGame(DisplayEnterPseudo());
                                activePage = "play";
                                break;
                            case "Options":
                                Console.Clear();
                                DisplayOptions();
                                DisplayDifficulty(difficulty);
                                DisplayMusic(music);
                                activePage = "options";
                                nbOptions = 2;
                                cursorPosition = 0;
                                DisplayCursor(activePage);
                                break;
                            case "Scores":
                                break;
                            case "About":
                                Console.Clear();
                                activePage = "about";
                                DisplayAbout();
                                break;
                            case "Exit":
                                Environment.Exit(0);
                                break;
                        }
                        break;

                    case ConsoleKey.Escape:
                        if(activePage == "options" || activePage == "about" || activePage == "scores")
                        {
                            Console.Clear();
                            DisplayMenu();
                            activePage = "menu";
                            nbOptions = 5;
                            cursorPosition = 0;
                            //write the initial cursor
                            DisplayCursor(activePage);
                        }
                        EraseOtherKey(cursorX, cursorY);
                        break;
                    default:
                        EraseOtherKey(cursorX, cursorY);
                        break;
                }
            }
        }

        static void EraseOtherKey(int cursorX, int cursorY)
        {
            cursorX = Console.CursorLeft;
            cursorY = Console.CursorTop;
            Console.SetCursorPosition(cursorX - 1, cursorY);
            Console.Write(" ");
            Console.SetCursorPosition(cursorX - 1, cursorY);
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
            switch (difficulty)
            {
                case 0:
                    Console.SetCursorPosition(46, 15);
                    Console.Write(@"   ___  __ _ ___ _   _ ");
                    Console.SetCursorPosition(46, 16);
                    Console.Write(@"  / _ \/ _` / __| | | |");
                    Console.SetCursorPosition(46, 17);
                    Console.Write(@" |  __/ (_| \__ \ |_| |");
                    Console.SetCursorPosition(46, 18);
                    Console.Write(@"  \___|\__,_|___/\__, |");
                    Console.SetCursorPosition(46, 19);
                    Console.Write(@"                  |___/ ");
                    break;

                case 1:
                    Console.SetCursorPosition(41, 14);
                    Console.Write(@"                                   _ ");
                    Console.SetCursorPosition(41, 15);
                    Console.Write(@"  _ __   ___  _ __ _ __ ___   __ _| |");
                    Console.SetCursorPosition(41, 16);
                    Console.Write(@" | '_ \ / _ \| '__| '_ ` _ \ / _` | |");
                    Console.SetCursorPosition(41, 17);
                    Console.Write(@" | | | | (_) | |  | | | | | | (_| | |");
                    Console.SetCursorPosition(41, 18);
                    Console.Write(@" |_| |_|\___/|_|  |_| |_| |_|\__,_|_|");
                    break;
                case 2:
                    Console.SetCursorPosition(42, 14);
                    Console.Write(@"     _ _  __  __ _            _ _   ");
                    Console.SetCursorPosition(42, 15);
                    Console.Write(@"  __| (_)/ _|/ _(_) ___ _   _| | |_ ");
                    Console.SetCursorPosition(42, 16);
                    Console.Write(@" / _` | | |_| |_| |/ __| | | | | __|");
                    Console.SetCursorPosition(42, 17);
                    Console.Write(@"| (_| | |  _|  _| | (__| |_| | | |_ ");
                    Console.SetCursorPosition(42, 18);
                    Console.Write(@" \__,_|_|_| |_| |_|\___|\__,_|_|\__|");
                    break;
                case 3:
                    Console.SetCursorPosition(38, 14);
                    Console.Write(@"                 _                     _ ");
                    Console.SetCursorPosition(38, 15);
                    Console.Write(@"  __ _  ___   __| |_ __ ___   ___   __| |");
                    Console.SetCursorPosition(38, 16);
                    Console.Write(@" / _` |/ _ \ / _` | '_ ` _ \ / _ \ / _` |");
                    Console.SetCursorPosition(38, 17);
                    Console.Write(@"| (_| | (_) | (_| | | | | | | (_) | (_| |");
                    Console.SetCursorPosition(38, 18);
                    Console.Write(@" \__, |\___/ \__,_|_| |_| |_|\___/ \__,_|");
                    Console.SetCursorPosition(38, 19);
                    Console.Write(@" |___/                                   ");
                    break;
            }
        }
        static void EraseDifficulty(byte difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    Console.SetCursorPosition(46, 15);
                    Console.Write(@"                       ");
                    Console.SetCursorPosition(46, 16);
                    Console.Write(@"                       ");
                    Console.SetCursorPosition(46, 17);
                    Console.Write(@"                       ");
                    Console.SetCursorPosition(46, 18);
                    Console.Write(@"                       ");
                    Console.SetCursorPosition(46, 19);
                    Console.Write(@"                       ");
                    break;

                case 1:
                    Console.SetCursorPosition(41, 14);
                    Console.Write(@"                                     ");
                    Console.SetCursorPosition(41, 15);
                    Console.Write(@"                                     ");
                    Console.SetCursorPosition(41, 16);
                    Console.Write(@"                                     ");
                    Console.SetCursorPosition(41, 17);
                    Console.Write(@"                                     ");
                    Console.SetCursorPosition(41, 18);
                    Console.Write(@"                                     ");
                    break;
                case 2:
                    Console.SetCursorPosition(42, 14);
                    Console.Write(@"                                    ");
                    Console.SetCursorPosition(42, 15);
                    Console.Write(@"                                    ");
                    Console.SetCursorPosition(42, 16);
                    Console.Write(@"                                    ");
                    Console.SetCursorPosition(42, 17);
                    Console.Write(@"                                    ");
                    Console.SetCursorPosition(42, 18);
                    Console.Write(@"                                    ");
                    break;
                case 3:
                    Console.SetCursorPosition(38, 14);
                    Console.Write(@"                                         ");
                    Console.SetCursorPosition(38, 15);
                    Console.Write(@"                                         ");
                    Console.SetCursorPosition(38, 16);
                    Console.Write(@"                                         ");
                    Console.SetCursorPosition(38, 17);
                    Console.Write(@"                                         ");
                    Console.SetCursorPosition(38, 18);
                    Console.Write(@"                                         ");
                    Console.SetCursorPosition(38, 19);
                    Console.Write(@"                                         ");
                    break;
            }
        }
        static void DisplayMusic(bool music)
        {
            if (music == true)
            {
                Console.SetCursorPosition(51, 36);
                Console.Write(@"   ___  _ __  ");
                Console.SetCursorPosition(51, 37);
                Console.Write(@"  / _ \| '_ \ ");
                Console.SetCursorPosition(51, 38);
                Console.Write(@" | (_) | | | |");
                Console.SetCursorPosition(51, 39);
                Console.Write(@"  \___/|_| |_|");
            }
            if (music == false)
            {
                Console.SetCursorPosition(50, 35);
                Console.Write(@"         __  __ ");
                Console.SetCursorPosition(50, 36);
                Console.Write(@"   ___  / _|/ _|");
                Console.SetCursorPosition(50, 37);
                Console.Write(@"  / _ \| |_| |_ ");
                Console.SetCursorPosition(50, 38);
                Console.Write(@" | (_) |  _|  _|");
                Console.SetCursorPosition(50, 39);
                Console.Write(@"  \___/|_| |_|  ");
            }

        }
        static void EraseMusic(bool music)
        {
            if (music == false)
            {
                Console.SetCursorPosition(51, 36);
                Console.Write(@"              ");
                Console.SetCursorPosition(51, 37);
                Console.Write(@"              ");
                Console.SetCursorPosition(51, 38);
                Console.Write(@"              ");
                Console.SetCursorPosition(51, 39);
                Console.Write(@"              ");
            }
            if (music == true)
            {
                Console.SetCursorPosition(50, 35);
                Console.Write(@"                ");
                Console.SetCursorPosition(50, 36);
                Console.Write(@"                ");
                Console.SetCursorPosition(50, 37);
                Console.Write(@"                ");
                Console.SetCursorPosition(50, 38);
                Console.Write(@"                ");
                Console.SetCursorPosition(50, 39);
                Console.Write(@"                ");
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









                 








                                                __  __           _      
                                               |  \/  |_   _ ___(_) ___ 
                                               | |\/| | | | / __| |/ __|
                                               | |  | | |_| \__ \ | (__ 
                                               |_|  |_|\__,_|___/_|\___|





             ");
        }
        static void DisplayAbout()
        {
            Console.Write(@"  





                                                _    _                 _
                                               / \  | |__   ___  _   _| |_
                                              / _ \ | '_ \ / _ \| | | | __|
                                             / ___ \| |_) | (_) | |_| | |_
                                            /_/   \_\_.__/ \___/ \__,_|\__|");
        }
        static string DisplayEnterPseudo()
        {
            Console.SetCursorPosition(40, 10);
            Console.Write("Votre pseudo : ");
            string pseudo = Console.ReadLine();
            Console.Clear();
            return pseudo;
        }
        static Game StartGame(string pseudo)
        {
            Game game = new Game(0, pseudo);
            Vessel vessel = new Vessel(3, 3, 55, 55, "     █      \n ▄███████▄ \n███████████\n▀▀▀▀▀▀▀▀▀▀▀", "");
            game.Vessel = vessel;
            game.Vessel.DisplayVessel();
            return game;
        }
    }
}