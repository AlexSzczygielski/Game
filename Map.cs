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

        public List<MapEntity> mapEntities { get; protected set; }

        public IEnumerable<IShip> ships => mapEntities.OfType<IShip>();
        public IEnumerable<UserShip> UserShips => mapEntities.OfType<UserShip>(); // '=>' means readonly
        public IEnumerable<EnemyShip> EnemyShips => mapEntities.OfType<EnemyShip>();

        protected MapCell [,] mapGrid; //mapGrid array

        public Map(List<IShip> shipsIn, List<Port> portsIn, uint width, uint height)
        {
            mapEntities = new List<MapEntity>();
            foreach(IShip ship in shipsIn)
            {
                mapEntities.Add(ship);
            }
            foreach(Port port in portsIn)
            {
                mapEntities.Add(port);
            }

            mapWidth = width;
            mapHeight = height;

            mapGrid = new MapCell[mapWidth, mapHeight];
            //DistributeObjects();
        }

        public bool CheckCollisions()
        {
            foreach(MapEntity var in mapEntities)
            {
                int upperEdgeX = var.positionX + (int)var.collisionRadius;
                int upperEdgeY = var.positionY + (int)var.collisionRadius;
                return true;
            }
            return false;
        }

        public bool IsInMapBoundaries(int x, int y)
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

        public void CreateBoundaries()
        {
            // Top and bottom boundaries
            for (int i = 0; i < mapWidth; i++)
            {
                mapGrid[i, 0].SetBoundary();                       // Top
                mapGrid[i, mapHeight - 1].SetBoundary();           // Bottom
            }

            // Left and right boundaries
            for (int j = 0; j < mapHeight; j++)
            {
                mapGrid[0, j].SetBoundary();                       // Left
                mapGrid[mapWidth - 1, j].SetBoundary();            // Right
            }
        }

        public void UpdateMapArray()
        {
            foreach(MapEntity entity in mapEntities)
            {
                foreach(var (x,y) in entity.ReturnOccupiedCoordinates())
                {
                    mapGrid[x, y].SetOwner(entity.id);
                }
            }
        }

        public void UpdateMapPositions()
        {
            //Updates moveable objects positions
            foreach (IShip ship in ships)
            {
                var (newX, newY) = ship.CalculateNextMove();
                TryMoveSingleObject(ship, newX, newY);
            }
        }

        public void TryMoveSingleObject(MapEntity entity, int x, int y)
        {
            //A kind of setter that can place an object in a specific place
            //Used especially by Map class
            //Where to check map boundaries?
            if (IsInMapBoundaries(x, y) == true)
            {
                entity.SetPosition(x, y);
            }
        }

        public void ResetMap()
        {
            foreach(MapCell var in mapGrid)
            {
                var.Reset();
            }
        }

        public void CreateMap()
        {
            //Sequential logic of map handling in each round
            ResetMap();
            CreateBoundaries();
            UpdateMapPositions();
            UpdateMapArray();
            //CheckCollisions();
            
        }
    }
}

