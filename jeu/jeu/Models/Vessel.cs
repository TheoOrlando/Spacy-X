using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models
{
    public class Vessel : Entity
    {
        private Thread trd;
        Timer timer = null;

        public Vessel(sbyte lifepoints,sbyte maxLife, byte columnPosition, byte rowposition, string model, string destructionModel)
        {
            this.MaxLife = maxLife;
            this.LifePoints = lifepoints;
            this.ColumnPosition = columnPosition;
            this.RowPosition = rowposition;
            this.Model = model;
            this.DestructionModel = destructionModel;
        }

        public void VesselMove()
        {

        }

        public void VesselShot()
        {
            timer = new Timer(new TimerCallback(Shot));
            timer.Change(0, 1);
            timer.Dispose();
        }

        public void VesselDestroy()
        {

        }

        public void DisplayVessel()
        {
            string[] model = Model.Split('\n');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                Console.Write(model[i]);
            }

        }

        public void EraseVessel()
        {
            string[] model = Model.Split('\n');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(ColumnPosition, RowPosition + i);
                Console.Write("            ");
            }

        }

        public void Shot(object state)
        {
            Laser laser = new Laser(ColumnPosition, RowPosition, "│");
        }
    }
}
