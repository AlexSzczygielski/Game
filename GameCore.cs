using System;
namespace finalSzczygielski
{
    public class GameCore
    {
        //This class is responsible for creation of objects used
        //in program

        private Map _map;
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
            for(int i = 0; i < _numberOfShips; i++)
            {
                temp.Add(new IShip(0, 0));
            }

            return temp;
        }

        protected List<Port> CreatePorts()
        {
            throw new NotImplementedException("CreatePorts() method not implemented yet");
        }

        protected void CreateMap()
        {
            _map = new Map(CreateIShips(), CreatePorts(), _mapWidth, _mapHeight);
        }
    }
}

