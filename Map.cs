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

        protected Dictionary<(int, int), MapCell> mapGrid; //mapGrid array

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

            //initialize mapGrid
            mapGrid = new Dictionary<(int, int), MapCell>();
            int halfWidth = (int)mapWidth / 2;
            int halfHeight = (int)mapHeight / 2;

            for (int x = -halfWidth; x < halfWidth; x++)
            {
                for (int y = -halfHeight; y < halfHeight; y++)
                {
                    mapGrid[(x, y)] = new MapCell();
                }
            }

            DistributeObjects();
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
            int temp =  0;
            foreach(MapEntity var in mapEntities)
            {
                var.SetPosition(temp,temp);
                temp += 100;

            }

            mapEntities.First().SetPosition(0, 400);

        }

        public void CreateMap()
        {
            //Sequential logic of map handling in each round
            //This should be called in each round
            ResetMap();
            CreateBoundaries();
            //PrintMap();
            UpdateMapPositions();
            UpdateMapArray();
            CheckCollisions();

        }

        public void ResetMap()
        {
            foreach (MapCell var in mapGrid.Values)
            {
                var.Reset();
            }
        }

        public void CreateBoundaries()
        {
            int halfWidth = (int)mapWidth / 2;
            int halfHeight = (int)mapHeight / 2;

            for (int i = -halfWidth; i < halfWidth; i++)
            {
                if (mapGrid.ContainsKey((i, -halfHeight)))
                    mapGrid[(i, -halfHeight)].SetBoundary(); // Top
                if (mapGrid.ContainsKey((i, halfHeight - 1)))
                    mapGrid[(i, halfHeight - 1)].SetBoundary(); // Bottom
            }

            for (int j = -halfHeight; j < halfHeight; j++)
            {
                if (mapGrid.ContainsKey((-halfWidth, j)))
                    mapGrid[(-halfWidth, j)].SetBoundary(); // Left
                if (mapGrid.ContainsKey((halfWidth - 1, j)))
                    mapGrid[(halfWidth - 1, j)].SetBoundary(); // Right
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

        public void UpdateMapArray()
        {
            foreach (MapEntity entity in mapEntities)
            {
                foreach (var (x, y) in entity.ReturnOccupiedCoordinates())
                {
                    if (mapGrid.ContainsKey((x, y)))
                    {
                        mapGrid[(x, y)].SetOwner(entity.id);
                    }
                }
            }
        }

        public void CheckCollisions()
        {
            foreach(MapCell cell in mapGrid.Values)
            {
                //Console.WriteLine($"Collision detected: ID:{cell.ownerId} and ID:{cell.lastOwnerId}");
            }
        }

        public void PrintMap()
        {
            int halfWidth = (int)mapWidth / 2;
            int halfHeight = (int)mapHeight / 2;

            for (int y = halfHeight - 1; y >= -halfHeight; y--)
            {
                for (int x = -halfWidth; x < halfWidth; x++)
                {
                    if (mapGrid.TryGetValue((x, y), out var cell))
                    {
                        Console.Write(cell.ownerId == -1 ? "." : cell.ownerId.ToString());
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

