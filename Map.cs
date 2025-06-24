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

            //Fill mapGrid with cells
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

            mapEntities.First().SetPosition(600, 300);
            mapEntities[1].SetPosition(600, 350);
            ((IShip)mapEntities[1]).speed = -2;
            mapEntities[2].SetPosition(100, 200);

        }

        public IState RefreshMap(IState state)
        {
            //Sequential logic of map handling in each round
            //This should be called in each round
            ResetMap();
            CreateBoundaries();
            UpdateMapPositions();
            UpdateMapArray();
            state = CheckCollisions(state);
            //PrintMap();
            return state;

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

        public IState CheckCollisions(IState state)
        {
            //Checks if any collisions occured
            //state to return provided by HandleCollision
            //Handling collisions happens in HandleCollision (if detected)
            HashSet<(int, int)> reportedCollisions = new HashSet<(int, int)>(); //Does not allow duplicates

            foreach (var pair in mapGrid)
            {
                var cell = pair.Value; //Pair - MapCell
                if ((cell.ownerId != cell.lastOwnerId) && (cell.lastOwnerId != -1))
                {
                    int id1 = cell.ownerId;
                    int id2 = cell.lastOwnerId;

                    // Ensure each pair is reported only once
                    var orderedPair = id1 < id2 ? (id1, id2) : (id2, id1);
                    if (reportedCollisions.Contains(orderedPair))
                        continue;   //If found, skip this iteration

                    reportedCollisions.Add(orderedPair);
                    

                    //Search for entity with this ID
                    MapEntity entity1 = null;
                    foreach (var e in mapEntities)
                    {
                        if (e.id == id1)
                        {
                            entity1 = e;
                            break;
                        }
                    }

                    MapEntity entity2 = null;
                    foreach (var e in mapEntities)
                    {
                        if (e.id == id2)
                        {
                            entity2 = e;
                            break;
                        }
                    }

                    //Handle crash of the MapEntity objects
                    state = HandleCollision(entity1, entity2, state);

                }
            }
            return state;
        }

        public void ForceRandomMove(MapEntity entity)
        {
            //Forces random move for bots, used to revert collisions
            Random rand = new Random();
            int offsetX = rand.Next(-50, 51);
            int offsetY = rand.Next(-50, 51);

            int newX = entity.positionX + offsetX;
            int newY = entity.positionY + offsetY;
            if (IsInMapBoundaries(newX, newY))
            {
                TryMoveSingleObject(entity, newX,newY);
            }
        }

        protected IState HandleCollision(MapEntity entity1, MapEntity entity2, IState state)
        {
            //Collision handling, state to return is changed only when UserShip triggered the collision
            //state change provided by HandleUserCollision
            //Move Crashing EnemyShip entities
            if (entity1 is EnemyShip && (entity2 is EnemyShip || entity2 is Port))
            {
                //Console.WriteLine($"Enemy ship, ID: {entity1.id} must avoid collision next round.");
                ForceRandomMove(entity1);
            }

            if (entity2 is EnemyShip && (entity1 is EnemyShip || entity1 is Port))
            {
                //Console.WriteLine($"Enemy ship, ID: {entity2.id} must avoid collision next round.");
                ForceRandomMove(entity2);
            }

            if(entity1 is UserShip || entity2 is UserShip)
            {
                state = HandleUserCollision(entity1, entity2, state);
            }

            return state;
        }

        protected IState HandleUserCollision(MapEntity e1, MapEntity e2, IState state)
        {
            foreach (UserShip ship in UserShips)
            {
                //This ensures that User's ship will not immediately reenter desired state after leaving it (returning back to the game)
                ForceRandomMove(ship);
            }

            if ((e1 is UserShip && (e2 is EnemyShip)) || (e2 is UserShip && (e1 is EnemyShip)))
            {
                state = new QuestionAnswerState();
            }

            if ((e1 is UserShip && (e2 is Port)) || (e2 is UserShip && (e1 is Port)))
            {
                state = new PortState();
            }

            return state;
        }

        public void PrintMap()
        {
            foreach(var kvp in mapGrid)
            {
                var cell = kvp.Value;
                (int posX, int posY) = kvp.Key;
                if(cell.occupiedFlag == true)
                {
                    if(cell.ownerId == 1) Console.Write($"ID: {cell.ownerId}, cell_pos ({posX},{posY}) \n");
                }
            }
        }
    }
}

