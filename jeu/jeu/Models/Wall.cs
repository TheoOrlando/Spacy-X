using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Wall : Entity
    {

        public Wall(sbyte lifepoints, byte columnPosition, byte rowposition, string model)
        {
            this.LifePoints = lifepoints;
            this.ColumnPosition = columnPosition;
            this.RowPosition = rowposition;
            this.Model = model;
        }


        public void WallBreak()
        {

        }

    }
}
