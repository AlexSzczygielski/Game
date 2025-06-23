using System;
namespace finalSzczygielski
{
    public class Port:MapEntity
    {
        public Port(int posX, int posY)
        {
            _positionX = posX;
            _positionY = posY;
        }

        public void SaveGame()
        {
            throw new NotImplementedException("SaveGame() not implemented");
        }

        public void GetHint()
        {
            throw new NotImplementedException("GetHint() not implemented");
        }

        public override string ToString()
        {
            return "Port";
        }
    }
}

