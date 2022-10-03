namespace Models
{
    public class Entity
    {
        private byte lifePoints;
        private byte columnPosition;
        private byte rowPosition;
        private string model = "";
        private string destructionModel = "";

        public byte LifePoints
        {
            get => lifePoints;
            set => lifePoints = value;
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

    }
}