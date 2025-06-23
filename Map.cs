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

        public IEnumerable<UserShip> UserShips => ships.OfType<UserShip>(); // '=>' means readonly
        public IEnumerable<EnemyShip> EnemyShips => ships.OfType<EnemyShip>();

        public Map(List<IShip> shipsIn, List<Port> portsIn, uint width, uint height)
        {
            ships = shipsIn;
            ports = portsIn;

            mapWidth = width;
            mapHeight = height;
            //DistributeObjects();
        }

        public bool CheckCollision()
        {
            throw new NotImplementedException("CheckCollision() not implemented yet");
        }

        public bool InMapBoundaries(int x, int y)
        {
            //Checks if an object is still within a map's boundaries
            int maximumX = (int)mapWidth / 2;
            int minimumX = -maximumX;
            int maximumY = (int)mapHeight / 2;
            int minimumY = -maximumY;

            if ((x > minimumX && x < maximumX) && (y>minimumY && y<maximumY)) 
            {
                return true;
            }
            else
            {
                Console.WriteLine("Not in boundaries");
                return false;
            }
        }

        protected void DistributeObjects()
        {
            //places objects on the map
            //contains logic of random placement
            //contains check so that two object are not placed in the same spot
            throw new NotImplementedException("DistributeObjects() not implemented");
        }

        public void UpdateMapPositions()
        {
            foreach (IShip ship in ships)
            {
                var (newX, newY) = ship.CalculateNextMove();
                TryMoveObject(ship, newX, newY);
            }
        }

        public void TryMoveObject(MapEntity entity, int x, int y)
        {
            //A kind of setter that can place an object in a specific place
            //Used especially by Map class
            //Where to check map boundaries?
            if (InMapBoundaries(x, y) == true)
            {
                entity.SetPosition(x, y);
            }
        }
    }
}

