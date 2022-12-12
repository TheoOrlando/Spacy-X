using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Models
{
    public class Vessel : Entity
    {
        private Game game;

        public Game Game 
        { 
            get => game; 
            set => game = value; 
        }

        public Vessel(int lifepoints,int maxLife, int columnPosition, int rowposition, string model, string destructionModel,Game game)
        {
            this.MaxLife = maxLife;
            this.LifePoints = lifepoints;
            this.ColumnPosition = columnPosition;
            this.RowPosition = rowposition;
            this.Model = model;
            this.DestructionModel = destructionModel;
            this.Game = game;
        }

        /// <summary>
        /// Display the vessel
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
        /// <summary>
        /// Delete the vessel
        /// </summary>
        public void Delete()
        {
            string[] model = Model.Split('\n');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                Console.Write("           ");
            }
        }
        /// <summary>
        /// The vessel shot a laser
        /// </summary>
        public void Shot()
        {
            Laser laser = new Laser(ColumnPosition + 5, RowPosition -1, "|");
            Console.SetCursorPosition(laser.ColumnPosition, laser.RowPosition);
            Console.Write(laser.Model);
            Game.Lasers.Add(laser);
        }
    }
}
