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
            SetQuestionAvailability(false);
            this.context.gameCore._map.DeleteEnemy();
            this.context.roundCounter--;
            return new InGameState();
        }

        public IState HandleWrongAnswer(IState state)
        {
            this.context.wrongAnsCounter++;
            sb.Clear();
            sb.Append("Wrong answer! \n" +
                "Try again? y/n \n" +
                "If you want to delete this question, press 'd'");
            Console.WriteLine(sb);
            string ans = Console.ReadLine();
            if (ans == "n")
            {
                return new InGameState();
            }
            else
            {
                if(ans == "d")
                {
                    this.context.deletedAnsCounter++;
                    SetQuestionAvailability(false);
                }
                return state;
            }

        }

        protected void SetQuestionAvailability(bool isAvailable)
        {
            //sets availability of given question through sqlManager
            this.context.sqlManager.SetAvailabilityFlag(question, isAvailable);
        }
    }
}

