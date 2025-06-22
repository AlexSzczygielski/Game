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

