using System;
namespace finalSzczygielski
{
    public abstract class MapEntity
    {
        protected int positionX;
        protected int positionY;
        protected uint collisionRadius; //in inherited classes this should be custom
                                        //this value is treated as default
        public MapEntity()
        {
        }

        public void Move(int x, int y)
        {
            //A kind of setter that can place an object in a specific place
            //Used especially by Map class
            //Where to check map boundaries?
            positionX = x;
            positionY = y;
        }
    }
}

