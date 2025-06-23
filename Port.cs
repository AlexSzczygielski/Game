using System;
namespace finalSzczygielski
{
    public class Port:MapEntity
    {
        public Port(int posX, int posY)
        {
            _positionX = posX;
            _positionY = posY;
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

