using System;
namespace finalSzczygielski
{
    public class UserShip:IShip
    {
        public UserShip(uint posX, uint posY) :base(posX,posY)
        {
            Console.WriteLine("UserShip implementation not ready");
        }

        public void Movement(ConsoleKey key)
        {
            if(key == ConsoleKey.LeftArrow)
            {
                direction--;
            }
            else if(key == ConsoleKey.RightArrow)
            {
                direction++;
            }
        }
    }
}

