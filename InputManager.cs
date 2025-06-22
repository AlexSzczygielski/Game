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

        public static void WaitForInput()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true); // true = don't show the key pressed
        }

        public static ConsoleKey ArrowListener()
        {
            var keyInfo = Console.ReadKey(true); // 'true' prevents key from being shown in console

            if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                return ConsoleKey.LeftArrow;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                return ConsoleKey.RightArrow;
            }

            return keyInfo.Key; // return whatever key was pressed
        }
    }
}

