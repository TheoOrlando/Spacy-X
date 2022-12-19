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
        private readonly string[] MODEL = new string[] { "|"};
        public Laser(int columnPosition, int rowPosition, Game game) : base(columnPosition, rowPosition, game)
        {
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            Model = MODEL;
            Width = Model[0].Length;
            Height = Model.Count();
            Game = game;
        }

        /// <summary>
        /// Move the laser in the enemy direction
        /// </summary>
        /// <param name="state"></param>
        public void Move()
        {
            foreach(Wall wall in Game.WallList.ToArray())
            {
                if(ColumnPosition < wall.ColumnPosition + wall.Width && ColumnPosition + Width > wall.ColumnPosition && RowPosition-2 < wall.RowPosition + wall.Height && Height + RowPosition > wall.RowPosition)
                {
                    wall.LifePoints -= 1;
                    wall.Display();
                    this.Erase();
                    Game.Lasers.Remove(this);
                }
            }

            foreach(Alien alien in Game.AlienList.ToArray())
            {
                if (ColumnPosition < alien.ColumnPosition + alien.Width && ColumnPosition + Width > alien.ColumnPosition && RowPosition - 2 < alien.RowPosition + alien.Height && Height + RowPosition > alien.RowPosition)
                {
                    alien.Erase();
                    Game.AlienList.Remove(alien);
                    this.Erase();
                    Game.Lasers.Remove(this);
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
