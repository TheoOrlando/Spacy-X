﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Models
{
    public class Game
    {
        private int score;
        private string pseudo;
        private Vessel vessel;
        private List<Alien> alienList = new List<Alien>();
        private List<Wall> wallList = new List<Wall>();

        public Game(int points, string pseudo)
        {
            this.score = points;
            this.pseudo = pseudo;
        }

        public int Score
        {
            get => score;
            set => score = value;
        }
        public string Pseudo
        {
            get => pseudo;
            set => pseudo = value;
        }
        public Vessel Vessel
        {
            get => vessel;
            set => vessel = value;
        }
        public List<Alien> AlienList
        {
            get => alienList;
            set => alienList = value;
        }
        public List<Wall> WallList
        {
            get => wallList;
            set => wallList = value;
        }
        /// <summary>
        /// Display the actual player's score
        /// </summary>
        public void DisplayScore()
        {
            Console.SetCursorPosition(8, 1);
            Console.Write("Score: " + score);
        }
        /// <summary>
        /// Display the actual player's lifes
        /// </summary>
        public void DisplayLife()
        {
            Console.SetCursorPosition(90, 1);
            Console.Write("Life: " + vessel.LifePoints);
        }

        public void GameOver()
        {
            Environment.Exit(0);
        }
        public void AliensMovement(Object source, ElapsedEventArgs e)
        {
            bool changeDirection = false;
            foreach (Alien alien in AlienList)
            {
                if (alien.Right)
                {
                    alien.Move(1, 0);
                }
                else
                {
                    alien.Move(-1, 0);
                }
                if (alien.ColumnPosition == 105 || alien.ColumnPosition == 0)
                {
                    foreach(Alien alien2 in AlienList)
                    {
                        alien2.Move(0, 1);
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
                foreach (Alien alien in AlienList)
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
    }
}
