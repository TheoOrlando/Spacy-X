using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Threading;


namespace Models
{
    public class Game
    {
        private Random _random = new Random();
        static double _waitTimeAlienShot = 10;
        private int _score = 0;
        private string _pseudo;
        private Vessel _vessel;
        private List<Alien> _alienList = new List<Alien>();
        private List<Wall> _wallList = new List<Wall>();
        private List<Laser> _lasersVesselList = new List<Laser>();
        private List<Laser> _lasersAlienList = new List<Laser>();
        private double _vesselInvicibleTime;

        static string[,] beforeNextWave = new string[,] { {"                _ ", "   __ _  ___   | |", "  / _` |/ _ \\  | |", " | (_| | (_) | |_|", "  \\__, |\\___/  (_)", "  |___/           "},
            {"  _ "," / |", " | |", " | |", " |_|", ""},
            { "  ____  ", " |___ \\ ", "   __) |", "  / __/ ", " |_____|", ""},
            { "  _____ ", " |___ / ", "   |_ \\ ", "  ___) |", " |____/ ", "" }};

        public Game(string pseudo)
        {
            this._pseudo = pseudo;
            this.StartGame();
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }
        public string Pseudo
        {
            get => _pseudo;
            set => _pseudo = value;
        }
        public Vessel Vessel
        {
            get => _vessel;
            set => _vessel = value;
        }
        public List<Alien> AlienList
        {
            get => _alienList;
            set => _alienList = value;
        }
        public List<Wall> WallList
        {
            get => _wallList;
            set => _wallList = value;
        }
        public List<Laser> LasersVesselList
        { 
            get => _lasersVesselList; 
            set => _lasersVesselList = value; 
        }
        public double WaitTimeAlienShot 
        { 
            get => _waitTimeAlienShot; 
            set => _waitTimeAlienShot = value; 
        }
        public List<Laser> LasersAlienList 
        { 
            get => _lasersAlienList; 
            set => _lasersAlienList = value; 
        }
        public double VesselInvicibleTime 
        { 
            get => _vesselInvicibleTime; 
            set => _vesselInvicibleTime = value; 
        }

        /// <summary>
        /// Display the actual player's score
        /// </summary>
        public void DisplayScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(8, 1);
            Console.Write("Score: " + _score);
        }
        /// <summary>
        /// Display the actual player's lifes
        /// </summary>
        public void DisplayLife()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(90, 1);
            Console.Write("Life: " + _vessel.LifePoints);
        }

        public void GameOver()
        {
            Environment.Exit(0);
        }
        public void AliensMovement()
        {
            bool changeDirection = false;
            foreach (Alien alien in AlienList.ToArray())
            {
                if (alien.Right)
                {
                    alien.Erase();
                    alien.ColumnPosition += 1;
                    alien.Display();
                }
                else
                {
                    alien.Erase();
                    alien.ColumnPosition -= 1;
                    alien.Display();
                }
                if (alien.ColumnPosition == 105 || alien.ColumnPosition == 0)
                {
                    foreach(Alien alien2 in AlienList.ToArray())
                    {
                        alien2.Erase();
                        alien2.RowPosition += 1;
                        alien2.Display();
                        changeDirection = true;
                        if (alien2.RowPosition == 40)
                        {
                            this.GameOver();
                        }
                    }
                }
            }
            if(changeDirection)
            {
                foreach (Alien alien in AlienList.ToArray())
                {
                    if(alien.Right)
                    {
                        alien.Right = false;
                    }
                    else
                    {
                        alien.Right = true;
                    }
                }
            }
        }
        public void boucle()
        {
            ConsoleKey? key = null;
            double lastLaser = DateTime.Now.TimeOfDay.TotalMilliseconds - 250;
            double waitTime = 0;
            double startOfInvicibleVessel = 0;
            _waitTimeAlienShot = 10;
            for (int i = 1; i < 61; i++)
            {
                key = null;
                key = ConsoleKey.A;
                double start = DateTime.Now.TimeOfDay.TotalMilliseconds;
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }

                if (i % 1 == 0)
                {
                    if (!_vessel.Movable && startOfInvicibleVessel == 0)
                    {
                        startOfInvicibleVessel = DateTime.Now.TimeOfDay.TotalMilliseconds;
                    }
                    if (DateTime.Now.TimeOfDay.TotalMilliseconds - startOfInvicibleVessel >= 2000 && !_vessel.Movable)
                    {
                        _vessel.Movable = true;
                        startOfInvicibleVessel = 0;
                    }
                    //laser move
                    if (key == ConsoleKey.UpArrow && DateTime.Now.TimeOfDay.TotalMilliseconds - lastLaser >= 500)
                    {
                        _vessel.Shot();
                        lastLaser = DateTime.Now.TimeOfDay.TotalMilliseconds;
                    }

                    if (key == ConsoleKey.LeftArrow && _vessel.Movable)
                    {
                        Vessel.Erase();
                        if (_vessel.ColumnPosition > 1)
                            _vessel.ColumnPosition -= 2;
                        _vessel.Display();
                    }
                    if (key == ConsoleKey.RightArrow && _vessel.Movable)
                    {
                        _vessel.Erase();
                        if (_vessel.ColumnPosition < 109)
                            _vessel.ColumnPosition += 2;
                        _vessel.Display();
                    }
                }
                if (i % 1 == 0)
                {
                    foreach (Laser laser in _lasersVesselList.ToArray())
                    {
                        laser.Move();
                    }
                }
                if(i % 2 == 0)
                {
                    foreach (Laser laser in _lasersAlienList.ToArray())
                    {
                        laser.Move();
                    }
                }
                if (i % 3 == 0)
                {
                    //alien move
                    this.AliensMovement();
                }
                //clignotement du vaisseau touché
                if(i %1 == 0 && !_vessel.Movable)
                {
                    _vessel.Display();
                }
                if (i % 2 == 0 && !_vessel.Movable)
                {
                    _vessel.Erase();
                }

                if (i % WaitTimeAlienShot == 0)
                {
                    if (_alienList.Count > 0)
                    {
                        _alienList[_random.Next(0, _alienList.Count)].Shot();
                    }
                }
                if (i == 60)
                {
                    i = 0;
                    if (_alienList.Count == 0)
                    {
                        DisplaySucessWave();
                        break;
                    }
                }

                //calculating the time to wait for a frame
                double end = DateTime.Now.TimeOfDay.TotalMilliseconds;
                double executionTime = end - start;
                if (executionTime < 17)
                {
                    waitTime = 8.33 - executionTime;
                }
                waitTime = Math.Round(waitTime, 0);
                if (waitTime > 0)
                {
                    Thread.Sleep(Convert.ToInt32(waitTime));
                }
            }
        }
        public void DisplaySucessWave()
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Laser laser in _lasersVesselList.ToArray())
            {
                laser.Erase();
                _lasersVesselList.Remove(laser);
            }
            Console.SetCursorPosition(0, 17);
            Console.Write(@"
                        _   _           _                                _        
                       | \ | | _____  _| |_  __      ____ ___   _____   (_)_ __   
                       |  \| |/ _ \ \/ / __| \ \ /\ / / _` \ \ / / _ \  | | '_ \  
                       | |\  |  __/>  <| |_   \ V  V / (_| |\ V /  __/  | | | | | 
                       |_| \_|\___/_/\_\\__|   \_/\_/ \__,_| \_/ \___|  |_|_| |_| ");
            for (int j = 3; j > -1; j--)
            {
                DisplayBeforeNextWave(j);
                Thread.Sleep(1000);
                EraseBeforeNextWave(j);
            }
            ContinueGame();
        }
        public void EraseSucessWave()
        {
            
            for(int x = 0; x < 6; x++)
            {
                Console.SetCursorPosition(0, 17 + x);
                Console.Write("                                                                                   ");
            }
        }

        public void DisplayBeforeNextWave(int index)
        {
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(83, 18 + i);
                Console.Write(beforeNextWave[index, i]);
            }
        }
        public void EraseBeforeNextWave(int index)
        {
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(83, 18 + i);
                Console.Write("                      ");
            }
        }

        public void StartGame()
        {
            Console.Clear();
            Vessel vessel = new Vessel(3, 53, 55, this);

            Wall wall1 = new Wall(4, 8, 45, this);
            Wall wall2 = new Wall(4, 31, 45, this);
            Wall wall3 = new Wall(4, 54, 45, this);
            Wall wall4 = new Wall(4, 77, 45, this);
            Wall wall5 = new Wall(4, 100, 45, this);

            int x = 1;
            int y = 3;
            for (int a = 1; a < 4; a++)
            {
                for (int i = 0; i < 5; i++)
                {
                    Alien alien = new Alien(1, x, y, this, true, a);
                    _alienList.Add(alien);
                    x += 14;
                }
                y += 6;
                x = 1;
            }

            _wallList.Add(wall1);
            _wallList.Add(wall2);
            _wallList.Add(wall3);
            _wallList.Add(wall4);
            _wallList.Add(wall5);

            _vessel = vessel;

            _vessel.Display();

            foreach (Wall wall in _wallList)
            {
                wall.Display();
            }

            foreach (Alien alien in _alienList)
            {
                alien.Display();
            }

            this.DisplayScore();
            this.DisplayLife();

            this.boucle();
        }
        public void ContinueGame()
        {
            int x = 1;
            int y = 3;
            for (int a = 1; a < 4; a++)
            {
                for (int i = 0; i < 5; i++)
                {
                    Alien alien = new Alien(1, x, y, this, true, a);
                    _alienList.Add(alien);
                    x += 14;
                }
                y += 6;
                x = 1;
            }

            foreach (Alien alien in _alienList)
            {
                alien.Display();
            }

            Vessel.Erase();
            Vessel.ColumnPosition = 53;
            Vessel.RowPosition = 55;
            Vessel.Display();

            Vessel.LifePoints += 1;
            DisplayLife();

            this.EraseSucessWave();
            this.boucle();
        }
    }
}
