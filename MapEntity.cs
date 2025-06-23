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
        public MapEntity()
        {
            collisionRadius = 20; //Default value
        }

        public void SetPosition(int x, int y)
        {
            _positionX = x;
            _positionY = y;
        }
    }
}

