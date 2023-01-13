/********************************************************
 *Auteur: Theo Orlando
 *Date: 10.10.2022
 *Lieux: ETML/Domicile
 *
 *Description: classe Laser du programme défini les 
 *propriété d'un laser tiré par un vaisseau ou un alien
 *
 ********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Models
{
    /// <summary>
    /// class for a laser
    /// </summary>
    public class Laser : Entity
    {
        private readonly string[] MODEL = new string[] { "|"};
        private bool _direction;
        public Laser(int columnPosition, int rowPosition, Game game, bool direction) : base(columnPosition, rowPosition, game)
        {
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            Model = MODEL;
            Width = Model[0].Length;
            Height = Model.Count();
            Game = game;
            Direction = direction;
        }

        public bool Direction { get => _direction; set => _direction = value; }

        /// <summary>
        /// Move the laser in the enemy direction
        /// </summary>
        /// <param name="state"></param>
        public void Move()
        {
            if(_direction)
            {
                if (RowPosition != 2)
                {
                    this.Erase();
                    RowPosition -= 1;
                    this.Display();
                }
                else
                {
                    Console.SetCursorPosition(ColumnPosition, RowPosition);
                    Console.Write(" ");
                    this.Model = null;
                }
                foreach (Wall wall in Game.WallList.ToArray())
                {
                    if (ColumnPosition < wall.ColumnPosition + wall.Width && ColumnPosition + Width > wall.ColumnPosition && RowPosition - 1 < wall.RowPosition + wall.Height && Height + RowPosition > wall.RowPosition)
                    {
                        wall.LifePoints -= 1;
                        this.Erase();
                        wall.Display();
                        Game.LasersVesselList.Remove(this);
                    }
                }

                foreach (Alien alien in Game.AlienList.ToArray())
                {
                    if (ColumnPosition < alien.ColumnPosition + alien.Width && ColumnPosition + Width > alien.ColumnPosition && RowPosition - 1 < alien.RowPosition + alien.Height && Height + RowPosition > alien.RowPosition)
                    {
                        alien.Erase();
                        alien.Remove();
                        Game.WaitTimeAlienShot += 2;
                        Game.AlienList.Remove(alien);
                        Game.LasersVesselList.Remove(this);
                        this.Erase();
                    }
                }
            }
            else
            {
                if (RowPosition != 59)
                {
                    this.Erase();
                    RowPosition += 1;
                    this.Display();
                }
                else
                {
                    Console.SetCursorPosition(ColumnPosition, RowPosition);
                    Console.Write(" ");
                    this.Model = null;
                    Game.LasersAlienList.Remove(this);
                }
                foreach (Alien alien in Game.AlienList.ToArray())
                {
                    if (ColumnPosition < alien.ColumnPosition + alien.Width && ColumnPosition + Width > alien.ColumnPosition && RowPosition - 1 < alien.RowPosition + alien.Height && Height + RowPosition > alien.RowPosition)
                    {
                        Game.LasersAlienList.Remove(this);
                        this.Erase();
                    }
                }
                foreach (Wall wall in Game.WallList.ToArray())
                {
                    if (ColumnPosition < wall.ColumnPosition + wall.Width && ColumnPosition + Width > wall.ColumnPosition && RowPosition - 1 < wall.RowPosition + wall.Height && Height + RowPosition > wall.RowPosition)
                    {
                        wall.LifePoints -= 1;
                        this.Erase();
                        wall.Display();
                        Game.LasersAlienList.Remove(this);
                    }
                }
                if (ColumnPosition < Game.Vessel.ColumnPosition + Game.Vessel.Width && ColumnPosition + Width > Game.Vessel.ColumnPosition && RowPosition - 1 < Game.Vessel.RowPosition + Game.Vessel.Height && Height + RowPosition > Game.Vessel.RowPosition && Game.Vessel.Movable)
                {
                    Game.LasersAlienList.Remove(this);
                    Game.Vessel.BeenHit();
                    Game.DisplayLife();
                }
            }
        }
    }
}
