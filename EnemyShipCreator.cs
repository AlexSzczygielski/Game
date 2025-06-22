using System;
namespace finalSzczygielski
{
    public class EnemyShipCreator:IShipCreator
    {
        public EnemyShipCreator()
        {
        }

        public IShip CreateShip(uint x, uint y)
        {
            return new EnemyShip(x, y);
        }
    }
}

