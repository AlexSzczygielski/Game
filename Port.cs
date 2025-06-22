using System;
namespace finalSzczygielski
{
    public class Port
    {
        protected uint positionX;
        protected uint positionY;

        public Port(uint posX, uint posY)
        {
            positionX = posX;
            positionY = posY;
            //Add logic that checks if the port does not collide with anything
        }

        public void Move(uint x, uint y)
        {
            //A kind of setter that can place an object in a specific place
            //Used especially by Map class
            //Where to check map boundaries?
            positionX = x;
            positionY = y;
        }

        public void SaveGame()
        {
            throw new NotImplementedException("SaveGame() not implemented");
        }

        public void GetHint()
        {
            throw new NotImplementedException("GetHint() not implemented");
        }
    }
}

