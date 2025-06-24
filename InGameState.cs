using System;
using System.Text;

namespace finalSzczygielski
{
    public class InGameState:IState
    {
        private StringBuilder sb = new StringBuilder(); //Smooth GUI
        private ConsoleKey key;
        private IState temp;
        public InGameState()
        {
        }

        public override void PerformAction()
        {
            temp = new InGameState();
            InputManager.StartListening(); //Constant key listen for PerfomAction()
            GatherInputData();
            base.PerformAction();
            Update();
            InputManager.StopListening();
            if((temp is InGameState) == false)
            {
                this.context.ChangeState(temp); //Change to next state, if user collision detected
            }
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
            temp = this.context.gameCore._map.RefreshMap(this);
        }
        
        public override void PrintText()
        {
            base.PrintText();
            sb.Append(this.context.sqlText.GetText("InGame1"));
            foreach (var ship in this.context.gameCore._map.UserShips)
            {
                sb.Append(this.context.sqlText.GetText("InGame2"));
                sb.Append($"{ship.direction}, ");
                sb.Append(this.context.sqlText.GetText("InGame3"));
                sb.Append($"{ship.speed}");
                
            }

            foreach(var entity in this.context.gameCore._map.mapEntities)
            {
                sb.Append($"\n[{entity}][ID: {entity.id}] ");
                sb.Append(this.context.sqlText.GetText("InGame4"));
                sb.Append($"({entity.positionX},{entity.positionY})");
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

