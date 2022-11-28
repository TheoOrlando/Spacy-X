using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Game 
    {
        private int score;
        private string pseudo;
        Vessel vessel;
        List<Alien> alienList = new List<Alien>();
        List<Wall> wallList = new List<Wall>();

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

    }
}
