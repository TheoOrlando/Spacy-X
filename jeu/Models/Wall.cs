/********************************************************
 *Auteur: Theo Orlando
 *Date: 10.10.2022
 *Lieux: ETML/Domicile
 *
 *Description: classe Wall du programme défini les 
 *propriété d'un mur les seuls alliés du joueur le 
 *protégeant des tirs des aliens
 *
 ********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// class for a wall
    /// </summary>
    public class Wall : EntityWithLife
    {
        private readonly string[] MODEL = new string[] { " ████████████ ", "██████████████", "██████████████", "██████  ██████", "█████    █████" };
        public Wall(int maxLife, int columnPosition, int rowPosition,  Game game) : base( maxLife, columnPosition, rowPosition, game)
        {
            MaxLife = maxLife;
            LifePoints = maxLife;
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            Game = game;
            Model = MODEL;
            Width = MODEL[0].Length;
            Height = MODEL.Count();
        }

        /// <summary>
        /// Display the wall
        /// </summary>
        public override void Display()
        {
            switch (LifePoints)
            {
                case 4:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
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
                for (int i = 0; i < Height; i++)
                {
                    Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                    Console.Write(Model[i]);
                }
            }
        }
        /// <summary>
        /// Erase the Wall
        /// </summary>
        public override void Erase()
        {
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(" ");
                }
            }
            Game.WallList.Remove(this);
        }
    }
}
