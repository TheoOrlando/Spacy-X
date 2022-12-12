using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Alien : Entity
    {
        private int points;
        private bool _right = true;

        public Alien(int points, int lifepoints, int columnPosition, int rowposition, string model,bool right)
        {
            Right = right;
            Points = points;
            LifePoints = lifepoints;
            ColumnPosition = columnPosition;
            RowPosition = rowposition;
            Model = model;
            string[] modeln = model.Split('\n');
            Width = modeln[0].Length;
            Height = modeln.Count();
        }

        public int Points
        {
            get => points;
            set => points = value;
        }
        public bool Right { get => _right; set => _right = value; }

        /// <summary>
        /// Display The Alien in multiple ligne
        /// </summary>
        public void Display()
        {
            string[] model = Model.Split('\n');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                Console.Write(model[i]);
            }
        }

        public void Erase()
        {
            string[] model = Model.Split('\n');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                Console.Write("               ");
            }
        }

        public void Move(int x, int y)
        {
            string[] model = Model.Split('\n');
            Console.MoveBufferArea(ColumnPosition, RowPosition, model[0].Length, model.Length, ColumnPosition += x, RowPosition += y);
        }
    }
}