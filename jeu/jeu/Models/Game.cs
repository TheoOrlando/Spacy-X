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
        private uint points;
        private string pseudo;
        Vessel vessel;
        List<Alien> alienList = new List<Alien>();
        List<Wall> wallList = new List<Wall>();

        public Game(uint points, string pseudo)
        {
            this.points = points;
            this.pseudo = pseudo;
        }

        public uint Points 
        { 
            get => points; 
            set => points = value; 
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


    }
}
