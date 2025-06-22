using System;
namespace finalSzczygielski
{
    public static class InputManager
    {
        //This is a helper class containing methods that are used for input
        //It is used through a few classes, a bit like library, so it is defined
        //as static.

        private static Thread _inputThread;
        private static bool _listening;
        private static ConsoleKey? _lastKey = null;
        private static readonly object _lock = new();

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

        public static ConsoleKey KeyListener()
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


        // Start the background input listener thread
        public static void StartListening()
        {
            if (_listening)
                return; // already running

            _listening = true;
            _inputThread = new Thread(() =>
            {
                while (_listening)
                {
                    if (Console.KeyAvailable)
                    {
                        var keyInfo = Console.ReadKey(true);
                        lock (_lock)
                        {
                            _lastKey = keyInfo.Key;
                        }
                    }
                    else
                    {
                        Thread.Sleep(10); // small delay to reduce CPU usage
                    }
                }
            });

            _inputThread.IsBackground = true;
            _inputThread.Start();
        }

        // Stop the input listener thread (optional)
        public static void StopListening()
        {
            _listening = false;
            _inputThread?.Join();
        }

        // Try to get the last pressed key and reset it (non-blocking)
        public static bool TryGetLastKey(out ConsoleKey key)
        {
            lock (_lock)
            {
                if (_lastKey.HasValue)
                {
                    key = _lastKey.Value;
                    _lastKey = null;
                    return true;
                }
                else
                {
                    key = default;
                    return false;
                }
            }
        }
    }
}

