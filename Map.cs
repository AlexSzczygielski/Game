using System;
using System.Xml.Linq;

namespace finalSzczygielski
{
    public class Map
    {
        //Map is responsible for managing
        //position of objects   
        //Map's constructor should take 
        //width and height,
        //Listso of ships and ports as an input.

        protected uint mapWidth;
        protected uint mapHeight;
        //protected uint collisionRadius;
        public List<IShip> ships { get; protected set; }
        public List<Port> ports { get; protected set; }

        public IEnumerable<UserShip> UserShips => ships.OfType<UserShip>(); // => means readonly
        public IEnumerable<EnemyShip> EnemyShips => ships.OfType<EnemyShip>();

        public Map(List<IShip> shipsIn, List<Port> portsIn, uint width, uint height)
        {
            ships = shipsIn;
            ports = portsIn;
            //DistributeObjects();
        }

        public bool CheckCollision()
        {
            throw new NotImplementedException("CheckCollision() not implemented yet");
        }

        protected void DistributeObjects()
        {
            //places objects on the map
            //contains logic of random placement
            //contains check so that two object are not placed in the same spot
            throw new NotImplementedException("DistributeObjects() not implemented");
        }
    }
}

