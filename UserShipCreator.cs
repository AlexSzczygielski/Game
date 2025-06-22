using System;
namespace finalSzczygielski
{
    public class UserShipCreator:IShipCreator
    {
        public UserShipCreator()
        {
        }

        public IShip CreateShip(uint x, uint y)
        {
            return new UserShip(x, y);
        }
    }
}

