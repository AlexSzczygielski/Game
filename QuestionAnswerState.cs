using System;
using System.Text;

namespace finalSzczygielski
{
    public class QuestionAnswerState:IState
    {
        private StringBuilder sb = new StringBuilder(); //Smooth GUI
        protected Question question;
        private string userAnswer = "";
        private IState temp;
        public QuestionAnswerState()
        {
        }

        public override void PerformAction()
        {
            userAnswer = "";
            temp = this;
            question = this.context.sqlManager.GetRandomQuestion();
            base.PerformAction();
            GatherInputData();
            //InputManager.WaitForInput();
            if(CheckAnswer() == true)
            {
                temp = HandleCorrectAnswer(this);
            }
            else
            {
                temp = HandleWrongAnswer(this);
            }

            if ((temp is QuestionAnswerState) == false)
            {
                this.context.ChangeState(temp);
            }
        }

        public override void PrintText()
        {
            base.PrintText();
            sb.Append("This is Question/Answer State\n" +
                "This will be finally input from a file \n" +
                "Answer questions: \n");
            sb.Append(question);
            sb.Append("\nYour answer: ");
            Console.WriteLine(sb);
        }

        public override void GatherInputData()
        {
            userAnswer = Console.ReadLine();
        }

        public bool CheckAnswer()
        {
            if (userAnswer == question.correctAnswer)
            {
                return true;
            }
            else { return false; }
        }

        public IState HandleCorrectAnswer(IState state)
        {
            sb.Clear();
            sb.Append("Correct answer! \n" +
                "getting back to the sea!");
            Console.WriteLine(sb);
            InputManager.WaitForInput();
            this.context.gameCore._map.DeleteEnemy();
            this.context.roundCounter--;
            return new InGameState();
        }

        public IState HandleWrongAnswer(IState state)
        {
            sb.Clear();
            sb.Append("Wrong answer! \n" +
                "Try again? y/n");
            Console.WriteLine(sb);
            if (Console.ReadLine() == "n")
            {
                return new InGameState();
            }
            else
            {
                return state;
            }

        }
    }
}

