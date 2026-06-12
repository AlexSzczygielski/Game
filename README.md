# Naval Game - OOP Design Project

A console naval game written in C# / .NET 6, built as the final project for
the university course "Designing and creating high-level object-oriented
applications" (3rd year, Electrical Engineering).

The emphasis of the project is software design rather than gameplay: the
architecture was modeled in UML (class and state diagrams) before any code was
written, and the implementation follows that design. Of the ~28 hours logged,
roughly a third went into design work.

## What it demonstrates

- **State pattern** - each game phase (start, gameplay, end, port, quiz) is a
  separate `IState` subclass; `GameManager` acts as the context and runs the
  main loop. Adding a game phase requires no changes to existing states.
- **Factory Method pattern** - `IShipCreator` with `UserShipCreator` /
  `EnemyShipCreator` decouples ship construction from `GameCore`, leaving the
  door open for new enemy types per round.
- **Inheritance and polymorphism** - `MapEntity` -> `IShip` -> `UserShip` /
  `EnemyShip`; a debug mode implemented as a subclass
  (`StartDebugState : StartState`) overriding input gathering, instead of
  `if (debug)` branches.
- **Encapsulation as invariant enforcement** - properties such as
  `GameManager.levels` and `mapSize` throw `InvalidOperationException` when
  modified outside the states allowed by the state diagram; ship `direction`
  and `speed` setters wrap and clamp their values.
- **Concurrency** - a static `InputManager` runs a background listener thread
  with lock-protected, non-blocking key retrieval, so the 100 ms game tick is
  never blocked by input.
- **LINQ-based views** - `Map` stores all entities in one collection and
  exposes read-only, type-filtered views (`UserShips`, `EnemyShips`) via
  `OfType<>`.

## Gameplay

You command a ship on a 2D map using maritime navigation: a course in degrees
(0 = north) and a speed. Each 100 ms tick, every ship's next position is
computed from its course and speed vector and validated against the map
boundaries. Planned mechanics: enemy ships per level, collisions, and ports
where the player can dock to save the game and answer quiz questions for
hints.

Controls (in game): Left/Right arrows - change course by 1 degree,
`W`/`S` - increase/decrease speed, `Q` - quit.

## Build and run

Requires the .NET 6 SDK.

```
dotnet run --project finalSzczygielski.csproj
```

By default the game starts in debug mode (fixed setup: 3 levels, map size
2000, no input prompts). To go through the interactive setup, set
`debug = false` in `Program.cs`.

## Project structure

```
Program.cs                          entry point
GameManager.cs                      game loop, State-pattern context
IState.cs, StartState.cs,           game states
  StartDebugState.cs, InGameState.cs,
  EndState.cs, PortState.cs,
  QuestionAnswerState.cs
GameCore.cs                         composition root for game objects
Map.cs, MapEntity.cs                world model and positioning
IShip.cs, UserShip.cs, EnemyShip.cs ship hierarchy
IShipCreator.cs, UserShipCreator.cs,
  EnemyShipCreator.cs               Factory Method
Port.cs                             dockable map object
InputManager.cs, Printer.cs         static helpers
docs/                               documentation
```

## Documentation

- [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) - layers, design decisions,
  state machine
- [docs/CLASS_REFERENCE.md](docs/CLASS_REFERENCE.md) - per-class reference
- [docs/ROADMAP.md](docs/ROADMAP.md) - known issues and planned work

## Status

Work in progress. The state machine, game loop, threaded input, steering, and
movement are functional; collisions, enemy AI, object placement, ports, and
the quiz mechanic are designed (present in the UML and as stubs) but not yet
implemented. See [docs/ROADMAP.md](docs/ROADMAP.md) for an honest list of
gaps, including known bugs found in code review.

## Author

Olek Szczygielski
