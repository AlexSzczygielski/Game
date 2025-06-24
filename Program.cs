namespace finalSzczygielski;
class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.WriteLine("This is a final project from \"Designing and creating high-level object-oriented applications course\".");
        string questionsJsonFilePath = "questions.json";
        string textsJsonFilePath = "texts.json";
        //Debugging mode selection
        bool debug = true;
        IState startState = debug ? new StartDebugState() : new StartState();
        //

        //Create GameEngine 
        GameManager manager = new GameManager(startState,questionsJsonFilePath,textsJsonFilePath);
        Console.CursorVisible = true;
    }
}
