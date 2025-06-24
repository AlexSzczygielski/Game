using System;
namespace finalSzczygielski
{
    public class EndState:IState
    {
        public EndState()
        {
            Console.WriteLine("EndState not finished yet");
        }

        public override void PrintText()
        {
            base.PrintText();
            Console.WriteLine(this.context.sqlText.GetText("End1"));
            this.context.StopEngine();
        }
    }
}

