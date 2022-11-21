using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Entity
    {
        private int lifePoints;
        private int maxLife;
        private int columnPosition;
        private int rowPosition;
        private string model = "";
        private string destructionModel = "";

        public int LifePoints
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
        public int MaxLife { get => maxLife; set => maxLife = value; }
    }
}
