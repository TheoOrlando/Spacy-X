using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    abstract public class Entity
    {
        private int columnPosition;
        private int rowPosition;
        private int width;
        private int height;
        private string[] model = new string[10];
        private Game game;

        protected Entity( int columnPosition, int rowPosition, Game game)
        {
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            Game = game;
        }



        public int ColumnPosition
        {
            get => columnPosition;
            set => columnPosition = value;

        }
        public int RowPosition
        {
            get => rowPosition;
            set => rowPosition = value;
        }
        public int Width 
        { 
            get => width; 
            set => width = value; 
        }
        public int Height 
        { 
            get => height; 
            set => height = value; 
        }
        public Game Game 
        { 
            get => game; 
            set => game = value; 
        }
        public string[] Model 
        { 
            get => model; 
            set => model = value; 
        }

        public virtual void Display()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                Console.Write(model[i]);
            }
        }

        public virtual void Erase()
        {
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                for(int j = 0; j < Width; j++)
                {
                    Console.Write(" ");
                }
            }
        }
    }
}
