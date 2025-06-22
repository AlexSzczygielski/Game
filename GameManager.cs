using System;
namespace finalSzczygielski
{
    public class GameManager
    {
        //This is a game engine, serves as a context to state pattern

        private uint _levels; //number of rounds, also number of enemies

        public uint levels
        {
            get { return _levels; }
            set
            {
                if (_state is StartState or StartDebugState)
                {
                    _levels = value;
                    _roundCounter = value;
                }
                else
                {
                    throw new InvalidOperationException("levels can only be modified in StartState.");
                }
            }
        }

        private uint _roundCounter; //counter of rounds, starts equal to level

        public uint roundCounter
        {
            get { return _roundCounter; }
            set
            {
                if (_state is (InGameState or StartState))
                {
                    _roundCounter = value;
                }
                else
                {
                    throw new InvalidOperationException("roundCounter can only be modified in StartState or InGameState.");
                }
            }
        }

        private uint _mapSize;

        public uint mapSize
        {
            get { return _mapSize; }
            set
            {
                if(_state is StartState)
                {
                    _mapSize = value;
                }
                else
                {
                    throw new InvalidOperationException("mapSize can only be modified in StartState");
                }
            }
        }

        private bool _running;
        public uint deletedAnsCounter { get; private set; }
        public uint lastQuestionId { get; protected set; } // FIX
        public GameCore gameCore { get; private set; } //initialized in CreateGameCore
        //SqlManager sqlManager;
        //WindowManager windowManager;
        //Printer printer;
        //InputManager inputManager;
        private IState _state = null;

        public GameManager(IState state)
        {
            //set init state
            this.ChangeState(state);

            //reset values
            ResetCounters();

            //start
            startEngine();
        }

        public void ChangeState(IState state)
        {
            //changes state and updates state's context (this)
            this._state = state;
            this._state.SetContext(this);
            Console.WriteLine($"Successfully changed to state {_state.GetType()}");

        }

        public void ResetCounters()
        {
            levels = 1; //levels have to be non 0
            _running = false; //Game not started yet
            deletedAnsCounter = 0;
            lastQuestionId = 0;
        }

        public void CreateGameCore(uint numberOfShips, uint mapSize)
        {
            //This method creates GameCore, works with StartState implementation
            try
            {
                if (_state is StartState or StartDebugState)
                {
                    gameCore = new GameCore(numberOfShips, mapSize);
                }
                else
                {
                    throw new InvalidOperationException($"CreateGameCore allowed only in StartState, current state: {_state.GetType()}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation exception: {ex.Message}");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        public void startEngine()
        {
            //Starts the game
            _running = true;
            while (_running)
            {
                this._state.PerformAction();
            }
        }

        public void StopEngine()
        {
            _running = false;
        }

        public void Test()
        {
            //Temporary function to test the encapsulation
            Console.WriteLine($"Test, roundCounter: {roundCounter}, _roundCounter: {_roundCounter}");
        }
        //RenderWindow
        //PrintText()
        //GatherInput
        //PerformAction
    }
}

