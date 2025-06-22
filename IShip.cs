using System;
namespace finalSzczygielski
{
    public abstract class IShip
    {
        protected uint positionX;
        protected uint positionY;
        private int _direction;
        public int direction
        {
            get { return _direction; }
            set
            {
                Console.WriteLine("setting direction");
                if(value>359)
                {
                    _direction = 0;
                }
                else if (value < 0)
                {
                    _direction = 359;
                }
                else
                {
                    _direction = value;
                }
                
            }
        }
        protected int speed;
        protected uint collisionRadius; //in inherited classes this should be custom
                                        //this value is treated as default

        public IShip(uint posX, uint posY)
        {
            //null at the creation time, set later in movement
            _direction = 0;
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

        public virtual void Movement()
        {
            //Movement during the game, artificial or by user input
            throw new NotImplementedException("Movement() not implemented");
        }

    }
}

