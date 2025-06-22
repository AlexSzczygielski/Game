using System;
namespace finalSzczygielski
{
    public class InGameState:IState
    {
        private ConsoleKey key;
        public InGameState()
        {
        }

        //~InGameState()
        //{
        //    InputManager.StopListening();
        //    Console.WriteLine("Destructor: InGameState destroyed");
        //}

        public override void PerformAction()
        {
            InputManager.StartListening(); //Constant key listen for PerfomAction()
            GatherInputData();
            base.PerformAction();
            Update();
            //CheckCollisions();
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
                ship.Movement(key);
            }

            //Update position
            this.context.gameCore._map.UpdateMapPositions();
        }

        public bool CheckCollisions()
        {
            throw new NotImplementedException("CheckCollisions() not implemented");
        }

        public override void PrintText()
        {
            base.PrintText();
            Console.WriteLine("This will be eventually printed from file\n" +
                "This is InGameState \n" +
                "provide steering input");
            foreach (var ship in this.context.gameCore._map.UserShips)
            {
                Console.WriteLine($"User Course: {ship.direction}, Speed: {ship.speed}");
            }
        }

        public void GatherInputData()
        {
            if (InputManager.TryGetLastKey(out ConsoleKey pressedKey))
            {
                key = pressedKey;
            }
        }
    }
}

