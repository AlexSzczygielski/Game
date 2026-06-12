# Class reference

Namespace: `finalSzczygielski` (exception: `Printer` is in `homework6`).
One class per file; file name = class name.

## Program (Program.cs)
Entry point. Hardcoded `debug = true` selects `StartDebugState` vs
`StartState`, then constructs `GameManager`, which immediately starts the loop.

## GameManager (GameManager.cs)
Game engine and State-pattern context.

- `levels` (uint) - number of rounds and number of enemy ships. Settable only
  in `StartState`/`StartDebugState`; setter also resets `roundCounter`.
- `roundCounter` (uint) - settable only in `StartState`/`InGameState`.
- `mapSize` (uint) - settable only in `StartState`.
- `deletedAnsCounter`, `lastQuestionId` - reserved for the planned
  question/answer mechanic.
- `gameCore` (GameCore) - created via `CreateGameCore(numberOfShips, mapSize)`,
  which is guarded to start states and wraps errors in try/catch.
- `ChangeState(IState)` - swaps state and injects itself as context.
- `startEngine()` - main loop: `_state.PerformAction()` + 100 ms sleep while
  `_running`.
- `StopEngine()` - clears `_running`, ends the loop.
- `ResetCounters()` - levels=1, counters zeroed.

## IState (IState.cs)
Abstract base state (originally a UML interface; see ARCHITECTURE.md).
Holds `protected GameManager context`. Virtual methods with default behavior:
`SetContext`, `RenderWindow` (placeholder), `PrintText` (clears console),
`GatherInputData` (no-op), `PerformAction` (RenderWindow + PrintText).

## StartState (StartState.cs) : IState
Setup phase. `GatherInputData()` loops until valid `uint` input for levels and
mapSize (`FormatException` handled). `PerformAction()` sequence: base ->
gather input -> `CreateGameCore()` -> wait for key -> transition to
`InGameState`. `resetAvailableFlags()` / `resetDeleteCounter()` are
unimplemented stubs for the question mechanic.

## StartDebugState (StartDebugState.cs) : StartState
Overrides `GatherInputData()` with fixed values (levels=3, mapSize=2000).
Lets the game run unattended for testing.

## InGameState (InGameState.cs) : IState
Main gameplay. `PerformAction()`: start key listener thread -> read last key
-> render/print -> `Update()` -> stop listener. `Update()` handles `Q` (to
`EndState`), forwards the key to every `UserShip.SetSteeringParams`, then calls
`Map.UpdateMapPositions()`. `CheckCollisions()` not implemented.

## EndState (EndState.cs) : IState
Prints a quit message and calls `context.StopEngine()`.

## PortState, QuestionAnswerState : IState
Stubs; constructors throw `NotImplementedException`.

## GameCore (GameCore.cs)
Composition root. Constructor takes `numberOfShips` (= levels) and `mapSize`
(square map). `CreateIShips()` builds 1 `UserShip` + N `EnemyShip` via the
factories; `CreatePorts()` currently creates a single port at (0,0);
`CreateMap()` assembles the `Map`. Exposes `_map` (public get, private set).

## Map (Map.cs)
Owns `List<MapEntity> mapEntities` (ships + ports). Exposed filtered views:
`ships` (IShip), `UserShips`, `EnemyShips` - read-only LINQ `OfType<>`
queries.

- `UpdateMapPositions()` - per tick: each ship's `CalculateNextMove()` then
  `TryMoveObject()`.
- `TryMoveObject(entity, x, y)` - moves only if `InMapBoundaries(x, y)`.
- `InMapBoundaries(x, y)` - map is centered on the origin: valid range is
  (-w/2, w/2) x (-h/2, h/2), exclusive.
- `CheckCollision()` - incomplete (returns after first entity).
- `DistributeObjects()` - planned random, non-overlapping placement; not
  implemented (currently everything spawns at (0,0)).

## MapEntity (MapEntity.cs), abstract
Base for placeable objects: `positionX`/`positionY` (protected set),
`SetPosition(x, y)`, `collisionRadius` (default 20).

## IShip (IShip.cs), abstract : MapEntity
Movable entity with maritime navigation.

- `direction` (int, degrees 0-359) - setter wraps >359 to 0 and <0 to 359.
- `speed` (int) - setter silently clamps to [-maxspeed, +maxspeed];
  default maxspeed 2, `SetMaxSpeed()` to change. `collisionRadius` = 5.
- `CalculateNextMove()` -> `GetVectorEnd(x0, y0, length, direction)` -
  converts maritime course (0 = north, clockwise) to math angle
  (`90 - direction`), computes dx/dy with cos/sin, inverts y for screen
  coordinates, rounds to ints.

## UserShip (UserShip.cs) : IShip
Max speed 5. `SetSteeringParams(ConsoleKey)`: LeftArrow/RightArrow = course
-1/+1 degree, `W`/`S` = speed +1/-1, UpArrow reserved.

## EnemyShip (EnemyShip.cs) : IShip
Placeholder; AI movement not implemented (currently moves with default
course/speed like any IShip).

## Port (Port.cs) : MapEntity
Static map object. `SaveGame()` and `GetHint()` are unimplemented stubs.

## IShipCreator (IShipCreator.cs), interface
Factory Method: `IShip CreateShip(int x, int y)`. Implemented by
`UserShipCreator` and `EnemyShipCreator`.

## InputManager (InputManager.cs), static
- `Parse(string)` - `uint` parse, throws `FormatException` on bad input.
- `WaitForInput()` - blocking "press any key".
- `StartListening()` / `StopListening()` - background daemon thread polls
  `Console.KeyAvailable` every 10 ms, stores last key under a lock.
- `TryGetLastKey(out ConsoleKey)` - non-blocking read-and-clear of the last
  key.
- `KeyListener()` - older blocking variant, candidate for removal.

## Printer (Printer.cs), static
`PrintColour` plus `PrintGreen/Blue/Red/Magenta` shortcuts. Currently unused
and still in the `homework6` namespace.
