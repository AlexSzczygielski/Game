using System;
namespace finalSzczygielski
{
    public class EnemyShipCreator:IShipCreator
    {
        public EnemyShipCreator()
        {
        }

        public IShip CreateShip(int x, int y)
        {
            return new EnemyShip(x, y);
        }
    }
}

