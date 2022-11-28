using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Timers;

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
                                                     @"                                   _ =  _ __   ___  _ __ _ __ ___   __ _| |= | '_ \ / _ \| '__| '_ ` _ \ / _` | |= | | | | (_) | |  | | | | | | (_| | |= |_| |_|\___/|_|  |_| |_| |_|\__,_|_|",
                                                     @"     _ _  __  __ _            _ _   =  __| (_)/ _|/ _(_) ___ _   _| | |_ = / _` | | |_| |_| |/ __| | | | | __|=| (_| | |  _|  _| | (__| |_| | | |_ = \__,_|_|_| |_| |_|\___|\__,_|_|\__|",
                                                     @"                 _                     _=  __ _  ___   __| |_ __ ___   ___   __| |= / _` |/ _ \ / _` | '_ ` _ \ / _ \ / _` |=| (_| | (_) | (_| | | | | | | (_) | (_| |= \__, |\___/ \__,_|_| |_| |_|\___/ \__,_|= |___/"};
        static string[] musics = new string[] { @"   ___  _ __  =  / _ \| '_ \ = | (_) | | | |=  \___/|_| |_|",
                                                @"         __  __ =   ___  / _|/ _|=  / _ \| |_| |_ = | (_) |  _|  _|=  \___/|_| |_|  =" };
        static int[,] cursorPositionsMenu = new int[,] { { 37, 18 }, { 30, 27 }, { 32, 36 }, { 32, 44 }, { 37, 53 } };
        static int[,,] cursorPositionOptions = new int[,,] { { { 23, 15 }, { 88, 15 } }, { { 33, 36 }, { 75, 36 } } };
        static int[,] difficultysPositions = new int[,] { { 46, 15 }, { 41, 14 }, { 42, 14 }, { 38, 14 } };
        static int[,] musicspositions = new int[,] { { 51, 36 }, { 50, 35 }};
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
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    // shot a laser
                    case ConsoleKey.UpArrow:
                        break;
                    // moves the vessel to the left
                    case ConsoleKey.LeftArrow:
                        game.Vessel.Delete();
                        if (game.Vessel.ColumnPosition > 1)
                            game.Vessel.ColumnPosition -= 2;
                        game.Vessel.Display();
                        break;
                    // moves the vessel to the right
                    case ConsoleKey.RightArrow:
                        game.Vessel.Delete();
                        if (game.Vessel.ColumnPosition < 109)
                            game.Vessel.ColumnPosition += 2;
                        game.Vessel.Display();
                        break;
                    // erase the other pressed key
                    default:
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
                ConsoleKey key = Console.ReadKey(true).Key;
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
                ConsoleKey key = Console.ReadKey(true).Key;
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
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    // return to the main menu
                    case ConsoleKey.Escape:
                        DisplayMenu();
                        break;
                    // erase the other pressed key
                    default:
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
        /// <summary>
        /// erase the actual cursor
        /// </summary>
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
        /// <summary>
        /// display the actual cursor
        /// </summary>
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
                        Console.SetCursorPosition(cursorPositionOptions[cursorPosition, 0, 0], cursorPositionOptions[cursorPosition, 0, 1] + i);
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
        /// <summary>
        /// display the actual difficulty
        /// </summary>
        static void DisplayDifficulty()
        {

            //write the cursor on the menu
            model = difficultys[difficulty].Split('=');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(difficultysPositions[difficulty,0], difficultysPositions[difficulty, 1] + i);
                Console.Write(model[i]);
            }

        }
        /// <summary>
        /// erase the actual difficulty
        /// </summary>
        static void EraseDifficulty()
        {
            //write the cursor on the menu
            model = difficultys[difficulty].Split('=');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(difficultysPositions[difficulty, 0], difficultysPositions[difficulty, 1] + i);
                Console.Write("                                         ");
            }
        }
        /// <summary>
        /// display the actual music state
        /// </summary>
        static void DisplayMusic()
        {
            if (music)
            {
                //write the cursor on the menu
                model = musics[0].Split('=');
                for (int i = 0; i < model.Length; i++)
                {
                    Console.SetCursorPosition(musicspositions[0, 0], musicspositions[0, 1] + i);
                    Console.Write(model[i]);
                }
            }
            else
            {
                //write the cursor on the menu
                model = musics[1].Split('=');
                for (int i = 0; i < model.Length; i++)
                {
                    Console.SetCursorPosition(musicspositions[1, 0], musicspositions[1, 1] + i);
                    Console.Write(model[i]);
                }
            }

        }
        /// <summary>
        /// erase the actual music state
        /// </summary>
        static void EraseMusic()
        {
            if (music)
            {
                //write the cursor on the menu
                model = musics[1].Split('=');
                for (int i = 0; i < model.Length; i++)
                {
                    Console.SetCursorPosition(musicspositions[1, 0], musicspositions[1, 1] + i);
                    Console.Write("                    ");
                }
            }
            else
            {
                //write the cursor on the menu
                model = musics[1].Split('=');
                for (int i = 0; i < model.Length; i++)
                {
                    Console.SetCursorPosition(musicspositions[1, 0], musicspositions[1, 1] + i);
                    Console.Write("                 ");
                }
            }
        }
        /// <summary>
        /// display the options page
        /// </summary>
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
        /// <summary>
        /// display the about page
        /// </summary>
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
        /// <summary>
        /// display the enter pseudo page
        /// </summary>
        static void DisplayEnterPseudo()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 10);
            Console.Write("Votre pseudo : ");
            string pseudo = Console.ReadLine();
            StartGame(pseudo);
        }
        /// <summary>
        /// start the player game
        /// </summary>
        /// <param name="pseudo"></param>
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

            int x = 2;
            int y = 3;
            string model = ALIEN1;
            for (int a = 1; a < 4; a++)
            {
                x = 2;
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

            Timer timer = new Timer(50); 
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += game.AliensMovement;
            timer.Start();

            activePage = "game";
            Gamekey();
        }
    }
}