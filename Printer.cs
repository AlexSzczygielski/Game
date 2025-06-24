using System;
namespace finalSzczygielski
{
    public class Printer
    {
        //This is a static class that handles different options of printing
        //to the terminal

        public static void PrintColour(ConsoleColor colour, string s)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = colour;
            Console.WriteLine(s);
            Console.ForegroundColor = currentColor;
        }

        public static void PrintGreen(string s)
        {
            PrintColour(ConsoleColor.Green, s);
        }

        public static void PrintBlue(string s)
        {
            PrintColour(ConsoleColor.Cyan, s);
        }

        public static void PrintRed(string s)
        {
            PrintColour(ConsoleColor.Red, s);
        }

        public static void PrintMagenta(string s)
        {
            PrintColour(ConsoleColor.Magenta, s);
        }
    }
}

