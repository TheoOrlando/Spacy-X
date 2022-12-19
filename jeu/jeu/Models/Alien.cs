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

        public Alien(int lifePoints, int maxLife, int columnPosition, int rowPosition, Game game, int points, bool right, int alienType) : base(lifePoints, maxLife, columnPosition, rowPosition, game)
        {
            LifePoints = lifePoints;
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
                    break;
                case 2:
                    Model = ALIEN2;
                    break;
                case 3:
                    Model = ALIEN3;
                    break;
                case 4:
                    Model = ALIEN4;
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

        public override void Erase()
        {
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(" ");
                }
            }
            Game.Score += points;
            Game.DisplayScore();
        }

        public void Move(int x, int y)
        {
            Console.MoveBufferArea(ColumnPosition, RowPosition, Width, Width, ColumnPosition += x, RowPosition += y);
        }
    }
}