using System;
namespace finalSzczygielski
{
    public class MapCell
    {
        //Handles single map cells
        //Contains logic for operation on these cells
        //-1 is treated as EMPTY

        public int ownerId { get; private set; } = -1;
        public int lastOwnerId { get; private set; } = -1;
        public bool occupiedFlag { get; private set; } = false;
        public bool boundaryFlag { get; private set; } = false;

        public MapCell()
        {
        }

        public void SetOwner(int newownerId)
        {
            if (ownerId != newownerId && ownerId != -1)
            {
                lastOwnerId = ownerId;
                occupiedFlag = true;
            }

            ownerId = newownerId;
        }

        public void Reset()
        {
            ownerId = -1;
            lastOwnerId = -1;
            occupiedFlag = false;
        }

        public void SetBoundary()
        {
            boundaryFlag = true;
        }
    }
}

