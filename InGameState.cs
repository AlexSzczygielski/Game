using System;
using System.Text;

namespace finalSzczygielski
{
    public class InGameState:IState
    {
        private StringBuilder sb = new StringBuilder(); //Smooth GUI
        private ConsoleKey key;
        public InGameState()
        {
        }

        public override void PerformAction()
        {
            InputManager.StartListening(); //Constant key listen for PerfomAction()
            base.PerformAction();
            Update();
            InputManager.StopListening();  
        }

        public void Update()
        {
            //Exit condition
            if (key == ConsoleKey.Q)
            {
                this.context.ChangeState(new EndState());
            }

            //Update user input direction
            foreach (var ship in this.context.gameCore._map.UserShips)
            {
                ship.SetSteeringParams(key);
            }

            //Update position
            this.context.gameCore._map.RefreshMap(this);
        }

        public bool CheckCollisions()
        {
            throw new NotImplementedException("CheckCollisions() not implemented");
        }

        public override void PrintText()
        {
            base.PrintText();
            sb.Append("This will be eventually printed from file\n" +
                "This is InGameState \n" +
                "provide steering input" +
                $"Map Boundaries: ");
            foreach (var ship in this.context.gameCore._map.UserShips)
            {
                sb.Append($"\nUser Course: {ship.direction}, Speed: {ship.speed}");
            }

            foreach(var entity in this.context.gameCore._map.mapEntities)
            {
                sb.Append($"\n[{entity}][ID: {entity.id}] position: ({entity.positionX},{entity.positionY})");
            }

            Console.WriteLine(sb);

        }

        public override void GatherInputData()
        {
            if (InputManager.TryGetLastKey(out ConsoleKey pressedKey))
            {
                key = pressedKey;
            }
        }
    }
}

