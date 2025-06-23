using System;
namespace finalSzczygielski
{
    public class EnemyShip:IShip
    {
        public EnemyShip(int posX, int posY):base(posX,posY)
        {
            Console.WriteLine("EnemyShip implementation not ready");
        }

        public override void SetPosition(int x, int y)
        {
            Console.Write("[ENEMY]");
            base.SetPosition(x, y);
        }
    }
}

