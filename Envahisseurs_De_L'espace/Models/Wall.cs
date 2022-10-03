namespace Models
{
    public class Wall : Entity
    {

        public Wall(byte lifepoints, byte columnPosition, byte rowposition, string model)
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