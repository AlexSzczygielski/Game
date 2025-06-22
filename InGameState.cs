using System;
namespace finalSzczygielski
{
    public class InGameState:IState
    {
        public InGameState()
        {
        }

        public override void PerformAction()
        {
            ConsoleKey key;
            base.PerformAction();
            key = this.GatherInputData();
            Update(key);
            //CheckCollisions();
        }

        public void Update(ConsoleKey key)
        {
            foreach (var ship in this.context.gameCore._map.UserShips)
            {
                ship.Movement(key);
            }
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
                Console.WriteLine($"Course: {ship.direction}");
            }
        }

        public ConsoleKey GatherInputData()
        {
            return InputManager.ArrowListener();
        }
    }
}

