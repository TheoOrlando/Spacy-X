using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vessel : Entity
    {

        public Vessel(byte lifepoints, byte columnPosition, byte rowposition, string model, string destructionModel)
        {
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
    }
}
