using System;
namespace finalSzczygielski
{
    public class GameCore
    {
        //This class is responsible for creation of objects used
        //in program

        public Map _map { get; private set; }
        //protected IWeather weather;
        private uint _numberOfShips; //this is also number of level/rounds
        private uint _mapWidth;
        private uint _mapHeight;

        public GameCore(uint numberOfShips, uint mapSize)
        {
            _numberOfShips = numberOfShips; //Passed as levels/roundCounter from StartState
            _mapHeight = mapSize;
            _mapWidth = mapSize;
            CreateMap();
        }

        protected List<IShip> CreateIShips()
        {
            //All ships are created at 0,0
            //Their position is later changed in Map 
            List<IShip> temp = new List<IShip>();
            IShipCreator creator;

            //Create Useer
            creator = new UserShipCreator();
            temp.Add(creator.CreateShip(0, 0));

            //Create enemy bots
            creator = new EnemyShipCreator();
            for (int i = 0; i < _numberOfShips; i++)
            {
                //This loop can be customized to create various types
                //of enemies, depending e.g. on the counter value
                //this ensures randomization of game in each turn

                temp.Add(creator.CreateShip(0, 0));
            }

            return temp;
        }

        protected List<Port> CreatePorts()
        {
            //Number of ports should be linked to the map size
            //Default position is 0,0, it is later distributed
            //in Map class
            List<Port> temp = new List<Port>();

            temp.Add(new Port(0, 0));

            return temp;
        }

        protected void CreateMap()
        {
            _map = new Map(CreateIShips(), CreatePorts(), _mapWidth, _mapHeight);
        }
    }
}

