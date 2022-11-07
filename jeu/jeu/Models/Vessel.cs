using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vessel : Entity
    {

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

        }

        public void VesselDestroy()
        {

        }

        public void DisplayVessel()
        {
            Console.SetCursorPosition(10, 45);
            string[] model = Model.Split('\n');
            for (int i = 0; i < model.Length; i++)
            {
                Console.SetCursorPosition(55, 55 + i);
                Console.Write(model[i]);
            }

        }
    }
}
