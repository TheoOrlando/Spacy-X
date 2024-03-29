﻿/********************************************************
 *Auteur: Theo Orlando
 *Date: 10.10.2022
 *Lieux: ETML/Domicile
 *
 *Description: classe Vessel du programme défini les 
 *propriété d'un vaisseau l'élément contrôlé par le joueur
 *
 ********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Models
{
    /// <summary>
    /// class for a vessel
    /// </summary>
    public class Vessel : EntityWithLife
    {
        private readonly string[] MODEL = new string[] { "     █      ", " ▄███████▄ ", "███████████", "▀▀▀▀▀▀▀▀▀▀▀" };
        private bool _movable;

        public bool Movable { get => _movable; set => _movable = value; }

        public Vessel(int maxLife, int columnPosition, int rowPosition, Game game) : base( maxLife, columnPosition, rowPosition, game)
        {
            MaxLife = maxLife;
            LifePoints = maxLife;
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            Game = game;
            Model = MODEL;
            Width = Model[0].Length;
            Height = Model.Count();
            Movable = true;
        }

        /// <summary>
        /// The vessel shot a laser
        /// </summary>
        public void Shot()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Laser laser = new Laser(ColumnPosition + 5, RowPosition -1,Game, true);
            Console.SetCursorPosition(laser.ColumnPosition, laser.RowPosition);
            Console.Write(laser.Model[0]);
            Game.LasersVesselList.Add(laser);
        }
        /// <summary>
        /// Put the vessel on a invicible state and made him lose a lifepoint
        /// </summary>
        public void BeenHit()
        {
            _movable = false;
            LifePoints--;
            if(LifePoints == 0)
            {
                Game.GameOver();
            }
        }
    }
}
