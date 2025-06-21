using System;
namespace finalSzczygielski
{
    public static class InputManager
    {
        //This is a helper class containing methods that are used for input
        //It is used through a few classes, a bit like library, so it is defined
        //as static.

        public static uint Parse(string s)
        {
            if (uint.TryParse(s, out uint number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Invalid input!");
                throw new FormatException("Failed to parse input as unsigned integer.");
            }
        }
    }
}

