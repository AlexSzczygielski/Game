using System;
namespace finalSzczygielski
{
    public class UserShipCreator:IShipCreator
    {
        public UserShipCreator()
        {
        }

        public IShip CreateShip(int x, int y)
        {
            return new UserShip(x, y);
        }
    }
}

