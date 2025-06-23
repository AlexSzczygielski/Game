using System;
namespace finalSzczygielski
{
    public abstract class IShip:MapEntity
    {
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

        private int _maxspeed;

        private int _speed;
        public int speed
        {
            get { return _speed; }
            set
            {
                if(value<=_maxspeed && value >= -_maxspeed)
                {
                    _speed = value;
                }
            }
        }

        public IShip(int posX, int posY)
        {
            //null at the creation time, set later in movement
            _direction = 0;
            _maxspeed = 2; //Default max speed for IShip object
            speed = _maxspeed;
            collisionRadius = 5;

            _positionX = posX;
            _positionY = posY;
        }

        public void SetMaxSpeed(int value)
        {
            _maxspeed = value;
        }

        public virtual (int,int) CalculateNextMove()
        {
            //Movement during the game, contains logic
            //where to move (resultant vector) - input by artificial or user
            //Uses MapEntinty Move() to make an actual position change
            int newX;
            int newY;
            (newX,newY) = GetVectorEnd(positionX,positionY,speed,direction);
            return (newX, newY);
        }

        public virtual (int x1,int y1) GetVectorEnd(int x0, int y0, int length, int direction)
        {
            //Calculates the next move
            //Get the end of the direction vector - actual movement
            //Convert maritime direction to mathematical angle
            //Console.WriteLine($"x {x0}, y {y0}, length {length}, direction {direction}");
            double angleRad = (90 - direction) * Math.PI / 180.0;

            //Calculate deltas
            double dx = length * Math.Cos(angleRad);
            double dy = length * Math.Sin(angleRad);

            //Compute end point
            int x1 = x0 + (int)Math.Round(dx);
            int y1 = y0 - (int)Math.Round(dy); // y decreases as you go "up" in most screen coords

            //Console.WriteLine($"Vector end: ({x1}, {y1})");
            return (x1, y1);

        }

        public override void SetPosition(int x, int y)
        {
            base.SetPosition(x, y);
        }

        public override string ToString()
        {
            return "Ship";
        }

    }
}

