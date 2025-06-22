using System;
namespace finalSzczygielski
{
    public class UserShip:IShip
    {
        public UserShip(int posX, int posY) :base(posX,posY)
        {
            Console.WriteLine("UserShip implementation not ready");
            SetMaxSpeed(5);
        }

        public void Movement(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                direction--;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                direction++;
            }
            else if (key == ConsoleKey.UpArrow)
            {
                //Do nothing, stop change
            }
            else if(key == ConsoleKey.W)
            {
                speed++;
            }
            else if(key == ConsoleKey.S)
            {
                speed--;
            }
        }
    }
}

