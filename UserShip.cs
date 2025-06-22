using System;
namespace finalSzczygielski
{
    public class UserShip:IShip
    {
        public UserShip(uint posX, uint posY) :base(posX,posY)
        {
            Console.WriteLine("UserShip implementation not ready");
        }
    }
}

