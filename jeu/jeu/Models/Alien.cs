using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Alien : EntityWithLife
    {
        private int points;
        private bool _right = true;
        private int _alienType;

        private readonly string[] ALIEN1 = new string[] { "       ▄▄     ", "     ▄████▄   ", "    ██▄██▄██  ", "    ▄▀ ▀▀ ▀▄  ", "     ▀    ▀   " };
        private readonly string[] ALIEN2 = new string[] { "    ▀▄   ▄▀   ", "   ▄█▀███▀█▄  ", "  █▀███████▀█ ", "  ▀ ▀▄▄ ▄▄▀ ▀ " };
        private readonly string[] ALIEN3 = new string[] { "  ▄▄▄████▄▄▄  ", " ███▀▀██▀▀███ ", " ▀▀███▀▀███▀▀ ", "  ▀█▄ ▀▀ ▄█▀  " };
        private readonly string[] ALIEN4 = new string[] { "   ▄▄██████▄▄   ", " ▄█▀██▀██▀██▀█▄ ", "▀▀███▀▀██▀▀███▀▀", "   ▀        ▀   " };

        public Alien( int maxLife, int columnPosition, int rowPosition, Game game, bool right, int alienType) : base( maxLife, columnPosition, rowPosition, game)
        {
            LifePoints = maxLife;
            MaxLife = maxLife;
            Game = game;
            Right = right;
            Points = points;
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            switch(alienType)
            {
                case 1:
                    Model = ALIEN1;
                    Points = 30;
                    break;
                case 2:
                    Model = ALIEN2;
                    Points = 20;
                    break;
                case 3:
                    Model = ALIEN3;
                    Points = 10;
                    break;
                case 4:
                    Model = ALIEN4;
                    Points = 50;
                    break;
            }
            Width = Model[0].Length;
            Height = Model.Count();
        }

        public int Points
        {
            get => points;
            set => points = value;
        }
        public bool Right { get => _right; set => _right = value; }
        public int AlienType { get => _alienType; set => _alienType = value; }

        public void Remove()
        {
            
            Game.AlienList.Remove(this);
            Game.Score += points;
            Game.DisplayScore();
        }

        public void Move(int x, int y)
        {
            Console.MoveBufferArea(ColumnPosition, RowPosition, Width, Height, ColumnPosition += x, RowPosition += y);
        }

        /// <summary>
        /// The vessel shot a laser
        /// </summary>
        public void Shot()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Laser laser = new Laser(ColumnPosition + Width/2, RowPosition + Height, Game, false);
            Console.SetCursorPosition(laser.ColumnPosition, laser.RowPosition);
            Console.Write(laser.Model[0]);
            Game.LasersAlienList.Add(laser);
            laser.Move();
            laser.Move();
        }
    }
}