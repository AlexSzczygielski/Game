using System;
using System.Xml.Linq;

namespace finalSzczygielski
{
    public class Map
    {
        //Map is responsible for managing
        //position of objects   
        //Map's constructor should take 
        //width and height as an input.

        protected uint mapWidth;
        protected uint mapHeight;
        //protected uint collisionRadius;
        protected List<IShip> ships;
        protected List<Port> ports;

        public Map(List<IShip> ships, List<Port> ports, uint width, uint height)
        {
            throw new NotImplementedException("Map not implemented yet");
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

