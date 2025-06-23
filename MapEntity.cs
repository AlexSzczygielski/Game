using System;
namespace finalSzczygielski
{
    public abstract class MapEntity
    {
        protected int _positionX;
        public int positionX
        {
            get { return _positionX; }
            protected set { _positionX = value; }
        }
        protected int _positionY;
        public int positionY
        {
            get { return _positionY; }
            protected set { _positionY = value; }
        }
        public uint collisionRadius { get; protected set; }//in inherited classes this should be custom
                                                           //this value is treated as default
        private static int _idCounter = 0;
        public int id { get; private set; }
        public MapEntity()
        {
            collisionRadius = 20; //Default value
            _idCounter++;
            id = _idCounter;
        }

        public virtual void SetPosition(int x, int y)
        {
            _positionX = x;
            _positionY = y;
            //Console.WriteLine($"Ship {this.id} position: ({x},{y})");
        }

        public virtual List<(int x, int y)> ReturnOccupiedCoordinates()
        {
            //Left Bottom point of rectangle
            int startPointX = positionX - (int)collisionRadius;
            int startPointY = positionY - (int)collisionRadius;

            var occupied = new List<(int, int)>();

            for (int i = startPointX; i < startPointX + collisionRadius; i++)
            {
                for (int j = startPointY; j < startPointY + collisionRadius; j++)
                {
                    occupied.Add((i, j));
                    //mapGrid[i, j].SetOwner(id);
                }
            }

            return occupied;
        }
    }
}

