using System;
using System.Text;

namespace finalSzczygielski
{
    public class QuestionAnswerState:IState
    {
        private StringBuilder sb = new StringBuilder(); //Smooth GUI
        protected Question question;
        public QuestionAnswerState()
        {
        }

        public override void PerformAction()
        {
            question = this.context.sqlManager.GetRandomQuestion();
            base.PerformAction();
            InputManager.WaitForInput();
            //CheckAnswer();
        }

        public override void PrintText()
        {
            base.PrintText();
            sb.Append("This is Question/Answer State\n" +
                "This will be finally input from file \n" +
                "Answer questions: \n");
            sb.Append(question);
            Console.WriteLine(sb);
        }
    }
}

