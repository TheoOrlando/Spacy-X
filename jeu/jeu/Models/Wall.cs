using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Wall : EntityWithLife
    {
        private readonly string[] MODEL = new string[] { " ████████████ ", "██████████████", "██████████████", "██████  ██████", "█████    █████" };
        public Wall(int lifePoints, int columnPosition, int rowPosition, int maxLife, Game game) : base(lifePoints, maxLife, columnPosition, rowPosition, game)
        {
            MaxLife = 4;
            LifePoints = lifePoints;
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            Model = MODEL;
            Width = MODEL[0].Length;
            Height = MODEL.Count();
            Game = game;
        }

        /// <summary>
        /// Display the wall
        /// </summary>
        public override void Display()
        {
            switch (LifePoints)
            {
                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 0:
                    this.Erase();
                    break;
                default:
                    break;
            }
            if (LifePoints > 0)
            {
                for (int i = 0; i < Width; i++)
                {
                    Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                    Console.Write(Model[i]);
                }
            }
        }
    }
}
