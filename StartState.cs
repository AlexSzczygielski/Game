using System;
namespace finalSzczygielski
{
    public class StartState:IState
    {
        public StartState()
        {
        }

        public override void RenderWindow()
        {
            base.RenderWindow();
            //throw new NotImplementedException("Not implemented");
        }

        public override void PrintText()
        {
            base.PrintText();
            Console.WriteLine("This will be eventually printed from file\n" +
                "This is welcome page from StartState \n" +
                "provide input for GameCore");
        }

        public override void GatherInputData()
        {
            Console.WriteLine("Now provide input data (CHANGE TO .TXT)" +
                "this prompts user with data to start the game \n");

            //Input logic
            while (true)
            {
                try
                {
                    Console.WriteLine("Provide levels ");
                    uint levels = InputManager.Parse(Console.ReadLine());
                    this.context.levels = levels; //number of ships
                    Console.WriteLine("Provide mapSize");
                    uint mapSize = InputManager.Parse(Console.ReadLine());
                    this.context.mapSize = mapSize;
                    break;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
            }

        }

        public override void PerformAction()
        {
            try
            {
                base.PerformAction();
                this.context.ChangeState(new InGameState());
            }

            catch (NotImplementedException ex)
            {
                Console.WriteLine($"A method is not implemented: {ex.Message}");
            }
        }

        public void CreateGameCore()
        {

        }
    }
}

