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

        public Laser( int columnPosition, int rowposition, string model)
        {
            this.ColumnPosition = columnPosition;
            this.RowPosition = rowposition;
            this.Model = model;
        }

        /// <summary>
        /// Move the laser in the enemy direction
        /// </summary>
        /// <param name="state"></param>
        public void Movement(Object source, ElapsedEventArgs e)
        {
                Console.MoveBufferArea(ColumnPosition, RowPosition, 1, 1, ColumnPosition, RowPosition -=1);
        }
    }
}
