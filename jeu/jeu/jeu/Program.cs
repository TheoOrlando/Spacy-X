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
        const string CURSORRIGHT = "      ▄  \n▄▄▄▄▄▄██▄\n▀▀▀▀▀▀██▀\n      ▀  ";
        const string CURSORLEFT = "  ▄      \n▄██▄▄▄▄▄▄\n▀██▀▀▀▀▀▀\n  ▀      ";
        const string WALL = " ████████████ \n██████████████\n██████████████\n██████  ██████\n█████    █████";
        const string VESSEL = "     █      \n ▄███████▄ \n███████████\n▀▀▀▀▀▀▀▀▀▀▀";
        const string ALIEN1 = "     ▄▄   \n   ▄████▄ \n  ██▄██▄██\n  ▄▀ ▀▀ ▀▄\n   ▀    ▀ ";
        const string ALIEN2 = "   ▀▄   ▄▀  \n  ▄█▀███▀█▄ \n █▀███████▀█\n ▀ ▀▄▄ ▄▄▀ ▀";
        const string ALIEN3 = " ▄▄▄████▄▄▄ \n███▀▀██▀▀███\n▀▀███▀▀███▀▀\n ▀█▄ ▀▀ ▄█▀ ";
        const string ALIEN4 = "   ▄▄██████▄▄   \n ▄█▀██▀██▀██▀█▄ \n▀▀███▀▀██▀▀███▀▀\n   ▀        ▀   ";

        static int cursorPosition = 0;

        static string[] menuOptions = new string[] { "Play", "Options", "Scores", "About", "Exit" };
        static string[] difficultys = new string[] { @"   ___  __ _ ___ _   _ =  / _ \/ _` / __| | | |= |  __/ (_| \__ \ |_| |=  \___|\__,_|___/\__, |=                  |___/",
                                                     @"                                   _ =  _ __   ___  _ __ _ __ ___   __ _| |=", "", ""};
        static int[,] cursorPositionsMenu = new int[,] { { 37, 18 }, { 30, 27 }, { 32, 36 }, { 32, 44 }, { 37, 53 } };
        static int[,,] cursorPositionOptions = new int[,,] {{{ 23, 15 },{ 88, 15 }},{{ 33, 36 },{ 75, 36 }}};
        static string activePage;
        static int nbOptions;
        static int difficulty = 1;
        static bool music = true;
        static int cursorX = 0;
        static int cursorY = 0;
        static Game game = null;
        static string[] model;

        static void Main(string[] args)
        {
            //set the size of the console
            Console.WindowHeight = 60;
            Console.WindowWidth = 120;
            Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            Console.CursorVisible = false;
            Console.Title = "Spacy X";

            //write the menu
            DisplayMenu();
        }
        /// <summary>
        /// Key interpreter for the game
        /// </summary>
        static void Gamekey()
        {
            while (activePage == "game")
            {
                // recovers the pressed key
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    // shot a laser
                    case ConsoleKey.UpArrow:
                        break;
                    // moves the vessel to the left
                    case ConsoleKey.LeftArrow:
                        game.Vessel.Delete();
                        if (game.Vessel.ColumnPosition > 1)
                            game.Vessel.ColumnPosition--;
                        game.Vessel.Display();
                        break;
                    // moves the vessel to the right
                    case ConsoleKey.RightArrow:
                        game.Vessel.Delete();
                        if (game.Vessel.ColumnPosition < 109)
                            game.Vessel.ColumnPosition++;
                        game.Vessel.Display();
                        break;
                    // erase the other pressed key
                    default:
                        EraseOtherKey();
                        break;
                }
            }
        }
        /// <summary>
        /// Key interpreter for the main menu
        /// </summary>
        static void MenuKey()
        {
            while (activePage == "menu")
            {
                // recovers the pressed key
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    // Turns the slider up a notch
                    case ConsoleKey.UpArrow:
                        EraseCursor();
                        if (cursorPosition > 0)
                            cursorPosition--;
                        DisplayCursor();
                        break;
                    // Moves the slider down a notch
                    case ConsoleKey.DownArrow:
                        EraseCursor();
                        if (cursorPosition < 4)
                            cursorPosition++;
                        DisplayCursor();
                        break;
                    // Chose the entrance the cursor watch
                    case ConsoleKey.Enter:
                        switch (menuOptions[cursorPosition])
                        {
                            // display the page for the pseudo
                            case "Play":
                                DisplayEnterPseudo();
                                break;
                            // display the options page
                            case "Options":
                                DisplayOptions();
                                break;
                            // display the highscores table
                            case "Scores":
                                break;
                            // display the about page
                            case "About":
                                Console.Clear();
                                activePage = "about";
                                DisplayAbout();
                                break;
                            // quits the application
                            case "Exit":
                                Environment.Exit(0);
                                break;
                        }
                        break;
                    // erase the other pressed key
                    default:
                        EraseOtherKey();
                        break;
                }
            }
        }
        /// <summary>
        /// Key interpreter for the option page
        /// </summary>
        static void OptionsKey()
        {
            while (activePage == "options")
            {
                // recovers the pressed key
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    // Turns the slider up a notch
                    case ConsoleKey.UpArrow:
                        EraseCursor();
                        if (cursorPosition > 0)
                            cursorPosition--;
                        DisplayCursor();
                        break;
                    // Moves the slider down a notch
                    case ConsoleKey.DownArrow:
                        EraseCursor();
                        if (cursorPosition < nbOptions)
                            cursorPosition++;
                        DisplayCursor();
                        break;
                    // change de difficuty or the music
                    case ConsoleKey.LeftArrow:
                        // change the difficulty godmod <= easy <= normal <= difficult <= godmod
                        if (cursorPosition == 0)
                        {
                            EraseDifficulty();
                            if (difficulty == 0)
                            {
                                difficulty = 3;
                            }
                            else
                            {
                                difficulty--;
                            }
                            DisplayDifficulty();
                        }
                        // change the music off <= on <= off
                        if (cursorPosition == 1)
                        {
                            EraseMusic();
                            if (music)
                            {
                                music = false;
                            }
                            else
                            {
                                music = true;
                            }
                            DisplayMusic();
                        }
                        break;
                    // change de difficuty or the music
                    case ConsoleKey.RightArrow:
                        // change the difficulty easy => normal => difficult => godmod => easy
                        if (cursorPosition == 0)
                        {
                            EraseDifficulty();
                            if (difficulty == 3)
                            {
                                difficulty = 0;
                            }
                            else
                            {
                                difficulty++;
                            }
                            DisplayDifficulty();
                        }
                        // change the music on => off => on
                        if (cursorPosition == 1)
                        {
                            EraseMusic();
                            if (music == true)
                            {
                                music = false;
                            }
                            else
                            {
                                music = true;
                            }
                            DisplayMusic();
                        }
                        break;
                    // return to the main menu
                    case ConsoleKey.Escape:
                        DisplayMenu();
                        break;
                    // erase the other pressed key
                    default:
                        EraseOtherKey();
                        break;
                }
            }
        }
        /// <summary>
        /// Key interpreter for the about page
        /// </summary>
        static void Aboutkey()
        {
            while (activePage == "about")
            {
                // recovers the pressed key
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    // return to the main menu
                    case ConsoleKey.Escape:
                        DisplayMenu();
                        break;
                    // erase the other pressed key
                    default:
                        EraseOtherKey();
                        break;
                }
            }
        }
        /// <summary>
        /// erase the precedent pressed key
        /// </summary>
        static void EraseOtherKey()
        {
            cursorX = Console.CursorLeft;
            cursorY = Console.CursorTop;
            Console.SetCursorPosition(cursorX - 1, cursorY);
            Console.Write(" ");
            Console.SetCursorPosition(cursorX - 1, cursorY);
        }
        /// <summary>
        /// Display the main menu
        /// </summary>
        static void DisplayMenu()
        {
            Console.Clear();
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
            activePage = "menu";
            nbOptions = 5;
            cursorPosition = 0;
            DisplayCursor();
            // start the key interpreter
            MenuKey();
        }
        static void EraseCursor()
        {
            switch (activePage)
            {
                case "menu":
                    // erase the cursor on the menu
                    string[] model = CURSORRIGHT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(cursorPositionsMenu[cursorPosition, 0], cursorPositionsMenu[cursorPosition, 1] + i);
                        Console.Write("         ");
                    }
                    break;

                case "options":
                    //write the left cursor on the option page
                    model = CURSORLEFT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(cursorPositionOptions[cursorPosition, 0, 0], cursorPositionOptions[cursorPosition, 0, 1] + i);
                        Console.Write("         ");
                    }
                    //write the right cursor on the option page
                    model = CURSORRIGHT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(cursorPositionOptions[cursorPosition, 1, 0], cursorPositionOptions[cursorPosition, 1, 1] + i);
                        Console.Write("         ");
                    }
                    break;
            }

        }
        static void DisplayCursor()
        {
            switch (activePage)
            {
                case "menu":
                    //write the cursor on the menu
                    model = CURSORRIGHT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(cursorPositionsMenu[cursorPosition, 0], cursorPositionsMenu[cursorPosition, 1] + i);
                        Console.Write(model[i]);
                    }
                    break;
                case "options":
                    //write the left cursor on the option page
                    model = CURSORLEFT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(cursorPositionOptions[cursorPosition, 0, 0], cursorPositionOptions[cursorPosition, 0 , 1] + i);
                        Console.Write(model[i]);
                    }
                    //write the right cursor on the option page
                    model = CURSORRIGHT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(cursorPositionOptions[cursorPosition, 1, 0], cursorPositionOptions[cursorPosition, 1, 1] + i);
                        Console.Write(model[i]);
                    }
                    break;
            }

        }
        static void DisplayDifficulty()
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
        static void EraseDifficulty()
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
        static void DisplayMusic()
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
        static void EraseMusic()
        {
            if (music)
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
            else
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
            Console.Clear();
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
            DisplayDifficulty();
            DisplayMusic();
            activePage = "options";
            nbOptions = 1;
            cursorPosition = 0;
            DisplayCursor();
            OptionsKey();
        }
        static void DisplayAbout()
        {
            Console.Clear();
            Console.Write(@"  





                                                _    _                 _
                                               / \  | |__   ___  _   _| |_
                                              / _ \ | '_ \ / _ \| | | | __|
                                             / ___ \| |_) | (_) | |_| | |_
                                            /_/   \_\_.__/ \___/ \__,_|\__|");
            activePage = "about";
            Aboutkey();
        }
        static void DisplayEnterPseudo()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 10);
            Console.Write("Votre pseudo : ");
            string pseudo = Console.ReadLine();
            StartGame(pseudo);
        }
        static void StartGame(string pseudo)
        {
            Console.Clear();
            game = new Game(0, pseudo);

            Vessel vessel = new Vessel(3, 3, 53, 55, VESSEL, "");

            Wall wall1 = new Wall(2, 8, 45, WALL);
            Wall wall2 = new Wall(2, 31, 45, WALL);
            Wall wall3 = new Wall(2, 54, 45, WALL);
            Wall wall4 = new Wall(2, 77, 45, WALL);
            Wall wall5 = new Wall(2, 100, 45, WALL);

            int x = 0;
            int y = 3;
            string model = ALIEN1;
            for (int a = 1; a < 4; a++)
            {
                x = 0;
                switch (a)
                {
                    case 1:
                        model = ALIEN1;
                        break;
                    case 2:
                        model = ALIEN2;
                        break;
                    case 3:
                        model = ALIEN3;
                        break;
                }
                for (int i = 0; i < 6; i++)
                {

                    Alien alien = new Alien(50, 1, x, y, model, "");
                    game.AlienList.Add(alien);
                    x += 14;
                }
                y += 6;
            }

            game.WallList.Add(wall1);
            game.WallList.Add(wall2);
            game.WallList.Add(wall3);
            game.WallList.Add(wall4);
            game.WallList.Add(wall5);

            game.Vessel = vessel;

            game.Vessel.Display();

            foreach (Wall wall in game.WallList)
            {
                wall.Display();
            }

            foreach (Alien alien in game.AlienList)
            {
                alien.Display();
            }

            game.DisplayScore();
            game.DisplayLife();

            activePage = "game";
            Gamekey();
        }
    }
}