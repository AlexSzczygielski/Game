using System;
namespace finalSzczygielski
{
    public class StartDebugState:StartState
    {
        public StartDebugState()
        {
            //Console.WriteLine($"DEBUG MODE ({this.GetType()})");
        }

        public override void PrintText()
        {
            base.PrintText();
            Console.WriteLine("!!DEBUG StartState MODE!! NO USER INPUT");
        }

        public override void GatherInputData()
        {
            this.context.levels = 3;
            this.context.mapSize = 5;
        }
    }
}

