# Architecture

Console-based naval game written in C# (.NET 6). Final project for the course
"Designing and creating high-level object-oriented applications" (3rd year EE).

The design was UML-first: class and state diagrams were drawn before coding,
and the implementation deliberately follows them (deviations are recorded in
`docs/ROADMAP.md` and `notes.txt`).

## High-level structure

```
Program (entry point)
   |
   v
GameManager  -- game engine / main loop; CONTEXT of the State pattern
   |  holds                              |  holds
   v                                     v
IState (abstract base state)         GameCore -- composition root for game objects
   |                                     |
   +-- StartState                        +-- Map -- owns and positions all entities
   |     +-- StartDebugState                  |
   +-- InGameState                            +-- MapEntity (abstract)
   +-- EndState                                     +-- IShip (abstract)
   +-- PortState (stub)                             |     +-- UserShip
   +-- QuestionAnswerState (stub)                   |     +-- EnemyShip
                                                    +-- Port
Static helpers: InputManager, Printer
Factories: IShipCreator -> UserShipCreator, EnemyShipCreator
```

## Layers and responsibilities

### Game loop and flow control
- `Program` selects the initial state (`StartState` or `StartDebugState` via a
  hardcoded `debug` flag) and constructs `GameManager`.
- `GameManager` runs the main loop: `startEngine()` calls
  `_state.PerformAction()` every 100 ms until `StopEngine()` clears the
  `_running` flag. It is the context of the State pattern and the single owner
  of cross-state data (`levels`, `roundCounter`, `mapSize`, `gameCore`).

### States (game flow)
Each screen/phase of the game is a state implementing the `IState` contract:

| State | Role | Status |
|---|---|---|
| `StartState` | Welcome screen, reads `levels` and `mapSize` from the user, creates `GameCore`, transitions to `InGameState` | working |
| `StartDebugState` | Subclass of `StartState`; overrides `GatherInputData()` with fixed mock values (levels=3, mapSize=2000) so the game can run without typing input | working |
| `InGameState` | Main gameplay: listens for keys on a background thread, applies steering to user ships, updates all positions, `Q` transitions to `EndState` | working |
| `EndState` | Stops the engine and exits | working |
| `PortState` | Docking at a port (save game, hints) | stub |
| `QuestionAnswerState` | Quiz mechanic tied to ports | stub |

State transitions implemented so far:

```
StartState/StartDebugState --> InGameState --(Q)--> EndState
```

Planned (per UML): InGameState <-> PortState <-> QuestionAnswerState.

### Object creation
- `GameCore` is the composition root: it builds the ship list (one `UserShip`
  plus `levels` enemy ships via factories), the port list, and hands them to
  a new `Map`. `GameManager.CreateGameCore()` guards that this only happens
  in a start state.
- `IShipCreator` / `UserShipCreator` / `EnemyShipCreator` implement the
  Factory Method pattern, so the enemy-creation loop in `GameCore` can later
  produce different enemy types per round without changing calling code.

### World model
- `MapEntity` is the abstract base for anything placed on the map: position
  (`positionX`, `positionY`) and a `collisionRadius`.
- `IShip` (abstract, despite the `I` prefix - see ROADMAP) adds maritime
  movement: `direction` (0-359 degrees with wrap-around in the setter),
  `speed` clamped to `_maxspeed` in the setter, and
  `CalculateNextMove()`/`GetVectorEnd()` that convert course + speed into the
  next (x, y) using trigonometry.
- `UserShip` (max speed 5) adds `SetSteeringParams(ConsoleKey)`: arrows change
  course, `W`/`S` change speed. `EnemyShip` is a placeholder for AI.
- `Port` is a static `MapEntity` with planned `SaveGame()`/`GetHint()`.
- `Map` owns all entities in one `List<MapEntity>` and exposes type-filtered
  read-only views via LINQ (`ships`, `UserShips`, `EnemyShips`). It validates
  movement against map boundaries (`InMapBoundaries`, map centered on origin)
  in `TryMoveObject()` and drives per-tick movement in `UpdateMapPositions()`.

### Static helpers
- `InputManager` - static utility for all input: safe `uint` parsing,
  blocking "press any key", and a thread-based non-blocking key listener
  (`StartListening` / `TryGetLastKey` / `StopListening`) protected by a lock.
  Used by `InGameState` so the game loop never blocks on input.
- `Printer` - colored console output helpers (currently unused; still in the
  old `homework6` namespace).

## Key design decisions

- **State pattern over a flag-driven loop.** Each game phase is its own class;
  `GameManager` only holds shared data and delegates `PerformAction()`. Adding
  a phase means adding a class, not editing a switch.
- **Abstract base state instead of a pure interface.** The original UML used an
  `IState` interface; it was changed to an abstract class so common behavior
  (`SetContext`, default `PerformAction`, console clearing) is implemented once
  and only overridden where needed (works with C# 8.0+ without default
  interface methods). The name `IState` was kept for UML traceability.
- **State-guarded setters.** `GameManager.levels`, `roundCounter` and `mapSize`
  throw `InvalidOperationException` unless the current state is allowed to
  modify them - encapsulation enforces the state diagram at runtime.
- **Factory Method for ships** to keep `GameCore` open for new enemy types.
- **Threaded input** decouples key polling from the 100 ms game tick.
- **Debug state as a subclass** (`StartDebugState`) instead of `if (debug)`
  branches inside `StartState` - polymorphism instead of conditionals.

## Diagrams

The original UML class and state diagrams were created during the design phase
(see `timeStatistics.txt`; ~9 h of design work). They are not stored in the
repository - add them to `docs/uml/` if recovered.
