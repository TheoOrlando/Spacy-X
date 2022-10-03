using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Alien : Entity
    {
        private uint points;

        public Alien(uint points, sbyte lifepoints, byte columnPosition, byte rowposition, string model, string destructionModel)
        {
            this.Points = points;
            this.LifePoints = lifepoints;
            this.ColumnPosition = columnPosition;
            this.RowPosition = rowposition;
            this.Model = model;
            this.DestructionModel = destructionModel;
        }

        public uint Points
        {
            get => points;
            set => points = value;
        }

        public void AlienDestroy()
        {

        }

        public void AlienShot()
        {

        }


    }
}
