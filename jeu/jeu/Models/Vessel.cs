using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Models
{
    public class Vessel : EntityWithLife
    {
        private readonly string[] MODEL = new string[] { "     █      ", " ▄███████▄ ", "███████████", "▀▀▀▀▀▀▀▀▀▀▀" };
        public Vessel(int lifePoints,int maxLife, int columnPosition, int rowPosition, Game game) : base(lifePoints, maxLife, columnPosition, rowPosition, game)
        {
            MaxLife = maxLife;
            LifePoints = lifePoints;
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            Model = MODEL;
            Game = game;
            Width = Model[0].Length;
            Height = Model.Count();
        }

        /// <summary>
        /// The vessel shot a laser
        /// </summary>
        public void Shot()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Laser laser = new Laser(ColumnPosition + 5, RowPosition -1,Game);
            Console.SetCursorPosition(laser.ColumnPosition, laser.RowPosition);
            Console.Write(laser.Model);
            Game.Lasers.Add(laser);
        }
    }
}
