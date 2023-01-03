/********************************************************
 *Auteur: Theo Orlando
 *Date: 10.10.2022
 *Lieux: ETML/Domicile
 *
 *Description: programme de jeu s'inspirant grandement du
 *jeu space invaders
 *
 ********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Timers;
using System.Threading;

namespace jeu
{
    internal class Program
    {
        const string CURSORRIGHT = "      ▄  \n▄▄▄▄▄▄██▄\n▀▀▀▀▀▀██▀\n      ▀  ";
        const string CURSORLEFT = "  ▄      \n▄██▄▄▄▄▄▄\n▀██▀▀▀▀▀▀\n  ▀      ";
        static readonly string[] MENUOPTIONS = new string[] { "Play", "Options", "Scores", "About", "Exit" };
        static readonly string[] DIFFICULTYS = new string[] { @"   ___  __ _ ___ _   _ =  / _ \/ _` / __| | | |= |  __/ (_| \__ \ |_| |=  \___|\__,_|___/\__, |=                  |___/",
                                                     @"                                   _ =  _ __   ___  _ __ _ __ ___   __ _| |= | '_ \ / _ \| '__| '_ ` _ \ / _` | |= | | | | (_) | |  | | | | | | (_| | |= |_| |_|\___/|_|  |_| |_| |_|\__,_|_|",
                                                     @"     _ _  __  __ _            _ _   =  __| (_)/ _|/ _(_) ___ _   _| | |_ = / _` | | |_| |_| |/ __| | | | | __|=| (_| | |  _|  _| | (__| |_| | | |_ = \__,_|_|_| |_| |_|\___|\__,_|_|\__|",
                                                     @"                 _                     _=  __ _  ___   __| |_ __ ___   ___   __| |= / _` |/ _ \ / _` | '_ ` _ \ / _ \ / _` |=| (_| | (_) | (_| | | | | | | (_) | (_| |= \__, |\___/ \__,_|_| |_| |_|\___/ \__,_|= |___/"};
        static readonly string[] MUSICS = new string[] { @"   ___  _ __  =  / _ \| '_ \ = | (_) | | | |=  \___/|_| |_|",
                                                @"         __  __ =   ___  / _|/ _|=  / _ \| |_| |_ = | (_) |  _|  _|=  \___/|_| |_|  =" };
        static readonly int[,] CURSORPOSITIONMENU = new int[,] { { 37, 18 }, { 30, 27 }, { 32, 36 }, { 32, 44 }, { 37, 53 } };
        static readonly int[,,] CURSORPOSITIONOPTIONS = new int[,,] { { { 23, 15 }, { 88, 15 } }, { { 33, 36 }, { 75, 36 } } };
        static readonly int[,] DIFFICULTYSOPTIONS = new int[,] { { 46, 15 }, { 41, 14 }, { 42, 14 }, { 38, 14 } };
        static readonly int[,] MUSICOPTIONS = new int[,] { { 51, 36 }, { 50, 35 } };
        const int WINDOWHEIGHT = 60;
        const int WINDOWWIDTH = 120;
        const bool CURSORVISIBLE = false;
        const string TITLE = "Spacy X";
        const int MINOPTIONSMENU = 1;
        const int MAXOPTIONSMENU = 5;
        const int DIFFICULTYCURSORPOSITION = 0;
        const int MUSICCURSORPOSITION = 1;
        const int MINOPTIONS = 0;
        const int MAXOPTIONSMENUOPTIONS = 2;
        const int MINDIFFICULTY = 0;
        const int MAXDIFFICULTY = 3;

        static int cursorPosition = 1;
        static string activePage;
        static int nbOptions;
        static int difficulty = 1;
        static bool music = true;
        static string[] model;

        static void Main(string[] args)
        {
            //set the size of the console
            Console.WindowHeight = WINDOWHEIGHT;
            Console.WindowWidth = WINDOWWIDTH;
            Console.SetWindowSize(WINDOWWIDTH, WINDOWHEIGHT);
            Console.SetBufferSize(WINDOWWIDTH, WINDOWHEIGHT);

            Console.CursorVisible = CURSORVISIBLE;
            Console.Title = TITLE;

            //write the menu
            DisplayMenu();
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
                        if (cursorPosition > MINOPTIONSMENU)
                            cursorPosition--;
                        DisplayCursor();
                        break;
                    // Moves the slider down a notch
                    case ConsoleKey.DownArrow:
                        EraseCursor();
                        if (cursorPosition < MAXOPTIONSMENU)
                            cursorPosition++;
                        DisplayCursor();
                        break;
                    // Chose the entrance the cursor watch
                    case ConsoleKey.Enter:
                        switch (MENUOPTIONS[cursorPosition - 1])
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
                        if (cursorPosition > MINOPTIONS)
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
                        if (cursorPosition == DIFFICULTYCURSORPOSITION)
                        {
                            EraseDifficulty();
                            if (difficulty == MINDIFFICULTY)
                            {
                                difficulty = MAXDIFFICULTY;
                            }
                            else
                            {
                                difficulty--;
                            }
                            DisplayDifficulty();
                        }
                        // change the music off <= on <= off
                        if (cursorPosition == MUSICCURSORPOSITION)
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
                        if (cursorPosition == DIFFICULTYCURSORPOSITION)
                        {
                            EraseDifficulty();
                            if (difficulty == MAXDIFFICULTY)
                            {
                                difficulty = MINDIFFICULTY;
                            }
                            else
                            {
                                difficulty++;
                            }
                            DisplayDifficulty();
                        }
                        // change the music on => off => on
                        if (cursorPosition == MUSICCURSORPOSITION)
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
            nbOptions = MAXOPTIONSMENU;
            cursorPosition = MINOPTIONSMENU;
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
                        Console.SetCursorPosition(CURSORPOSITIONMENU[cursorPosition - 1, 0], CURSORPOSITIONMENU[cursorPosition - 1, 1] + i);
                        Console.Write("         ");
                    }
                    break;

                case "options":
                    //write the left cursor on the option page
                    model = CURSORLEFT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(CURSORPOSITIONOPTIONS[cursorPosition, 0, 0], CURSORPOSITIONOPTIONS[cursorPosition, 0, 1] + i);
                        Console.Write("         ");
                    }
                    //write the right cursor on the option page
                    model = CURSORRIGHT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(CURSORPOSITIONOPTIONS[cursorPosition, 1, 0], CURSORPOSITIONOPTIONS[cursorPosition, 1, 1] + i);
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
                        Console.SetCursorPosition(CURSORPOSITIONMENU[cursorPosition - 1, 0], CURSORPOSITIONMENU[cursorPosition - 1, 1] + i);
                        Console.Write(model[i]);
                    }
                    break;
                case "options":
                    //write the left cursor on the option page
                    model = CURSORLEFT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        //cursorPosition = 0;
                        Console.SetCursorPosition(CURSORPOSITIONOPTIONS[cursorPosition, 0, 0], CURSORPOSITIONOPTIONS[cursorPosition, 0, 1] + i);
                        Console.Write(model[i]);
                    }
                    //write the right cursor on the option page
                    model = CURSORRIGHT.Split('\n');
                    for (int i = 0; i < model.Length; i++)
                    {
                        Console.SetCursorPosition(CURSORPOSITIONOPTIONS[cursorPosition, 1, 0], CURSORPOSITIONOPTIONS[cursorPosition, 1, 1] + i);
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
            model = DIFFICULTYS[difficulty].Split('=');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(DIFFICULTYSOPTIONS[difficulty, 0], DIFFICULTYSOPTIONS[difficulty, 1] + i);
                Console.Write(model[i]);
            }

        }
        /// <summary>
        /// erase the actual difficulty
        /// </summary>
        static void EraseDifficulty()
        {
            //write the cursor on the menu
            model = DIFFICULTYS[difficulty].Split('=');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(DIFFICULTYSOPTIONS[difficulty, 0], DIFFICULTYSOPTIONS[difficulty, 1] + i);
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
                model = MUSICS[0].Split('=');
                for (int i = 0; i < model.Length; i++)
                {
                    Console.SetCursorPosition(MUSICOPTIONS[0, 0], MUSICOPTIONS[0, 1] + i);
                    Console.Write(model[i]);
                }
            }
            else
            {
                //write the cursor on the menu
                model = MUSICS[1].Split('=');
                for (int i = 0; i < model.Length; i++)
                {
                    Console.SetCursorPosition(MUSICOPTIONS[1, 0], MUSICOPTIONS[1, 1] + i);
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
                model = MUSICS[1].Split('=');
                for (int i = 0; i < model.Length; i++)
                {
                    Console.SetCursorPosition(MUSICOPTIONS[1, 0], MUSICOPTIONS[1, 1] + i);
                    Console.Write("                    ");
                }
            }
            else
            {
                //write the cursor on the menu
                model = MUSICS[1].Split('=');
                for (int i = 0; i < model.Length; i++)
                {
                    Console.SetCursorPosition(MUSICOPTIONS[1, 0], MUSICOPTIONS[1, 1] + i);
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
            nbOptions = MINOPTIONSMENU;
            cursorPosition = MINOPTIONS;
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
                                            /_/   \_\_.__/ \___/ \__,_|\__|


                    Ce jeu à été réalisé dans le cadre du projet P_DEV au sein de l'ETML par Theo Orlando");
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
            Game game = new Game(pseudo);
            activePage = "game";
        }
    }
}