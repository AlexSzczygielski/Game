using System;
namespace finalSzczygielski
{
    public abstract class IShip
    {
        protected uint positionX;
        protected uint positionY;
        protected uint direction;
        protected int speed;
        protected uint collisionRadius; //in inherited classes this should be custom
                                        //this value is treated as default

        public IShip(uint posX, uint posY)
        {
            //null at the creation time, set later in movement
            direction = 0;
            speed = 0;
            collisionRadius = 5;

            positionX = posX;
            positionY = posY;
        }

        public void Move(uint x, uint y)
        {
            //A kind of setter that can place an object in a specific place
            //Used especially by Map class
            //Where to check map boundaries?
            positionX = x;
            positionY = y;
        }

        protected void Movement()
        {
            //Movement during the game, artificial or by user input
            throw new NotImplementedException("Movement() not implemented");
        }

    }
}

