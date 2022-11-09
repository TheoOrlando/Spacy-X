using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models
{
    public class Laser : Entity
    {

        public Laser( byte columnPosition, byte rowposition, string model)
        {
            this.ColumnPosition = columnPosition;
            this.RowPosition = rowposition;
            this.Model = model;
            timer = new Timer(new TimerCallback(Movement));
            timer.Change(0, 20);
        }

        Timer timer;

        public void Movement(object state)
        {
            if(RowPosition > 8)
            {
                Console.SetCursorPosition(ColumnPosition + 6, RowPosition);
                Console.Write(" ");
                RowPosition -= 1;
                Console.SetCursorPosition(ColumnPosition + 6, RowPosition);
                Console.Write(Model);
            }
        }
    }
}
