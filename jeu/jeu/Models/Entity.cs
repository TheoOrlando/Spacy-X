using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Entity
    {
        private sbyte lifePoints;
        private sbyte maxLife;
        private byte columnPosition;
        private byte rowPosition;
        private string model = "";
        private string destructionModel = "";

        public sbyte LifePoints
        {
            get => lifePoints;
            set
            {
                if(value < 0)
                    lifePoints = 0;
                else if(value > maxLife)
                    lifePoints = maxLife;
                else
                    lifePoints = value;
            }
        }

        public byte ColumnPosition
        {
            get => columnPosition;
            set => columnPosition = value;
        }

        public byte RowPosition
        {
            get => rowPosition;
            set => rowPosition = value;
        }

        public string Model
        {
            get => model;
            set => model = value;
        }

        public string DestructionModel
        {
            get => destructionModel;
            set => destructionModel = value;
        }
        public sbyte MaxLife { get => maxLife; set => maxLife = value; }
    }
}
