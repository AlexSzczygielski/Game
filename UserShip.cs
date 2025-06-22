using System;
namespace finalSzczygielski
{
    public class UserShip:IShip
    {
        protected ConsoleKey lastPressedKey;
        public UserShip(int posX, int posY) :base(posX,posY)
        {
            Console.WriteLine("UserShip implementation not ready");
            lastPressedKey = ConsoleKey.NoName; //To compare with Movement on the first input
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
                //Do nothing
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

