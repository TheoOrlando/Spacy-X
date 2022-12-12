using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Models
{
    public class Laser : Entity
    {
        private Game game;

        public Game Game
        {
            get => game;
            set => game = value;
        }
        public Laser(int columnPosition, int rowposition, string model, Game game)
        {
            ColumnPosition = columnPosition;
            RowPosition = rowposition;
            Model = model;
            Game = game;
            string[] modeln = model.Split('\n');
            Width = modeln[0].Length;
            Height = modeln.Count();
        }

        /// <summary>
        /// Move the laser in the enemy direction
        /// </summary>
        /// <param name="state"></param>
        public void Move()
        {
            
            foreach(Wall wall in game.WallList)
            {
                if(ColumnPosition < wall.ColumnPosition + wall.Width && )
                {

                }
            }

            if(RowPosition != 2)
            {
                Console.MoveBufferArea(ColumnPosition, RowPosition, 1, 1, ColumnPosition, RowPosition -= 1);
            }
            else
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition);
                Console.Write(" ");
                this.Model = null;
            }
        }
    }
}
