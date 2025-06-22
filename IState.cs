using System;
namespace finalSzczygielski
{
   //public interface IState
   // {
   //     //This is an interface class for the state pattern
   //     //used by GameManager (context)
   //     void RenderWindow();
   //     void PrintText();
   //     void GatherInputData();
   //     void PerformAction();
   //     void SetContext(GameManager context);
   // }

    public abstract class IState
    {
        protected GameManager context = null;

        public virtual void SetContext(GameManager context)
        {
            this.context = context;
            Console.WriteLine($"Context in {this.GetType()} updated");
        }

        public virtual void RenderWindow()
        {
            Console.WriteLine("Window Rendering Not Implemented");
        }

        public virtual void PrintText()
        {
            //Console.Clear();
        }
        public virtual void GatherInputData()
        {
            //
        }

        public virtual void PerformAction()
        {
            RenderWindow();
            PrintText();
        }
    }
}

