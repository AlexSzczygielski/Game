# Roadmap and known issues

Consolidated from `notes.txt`, code TODOs, and a full code review
(June 2026). Useful as a re-entry point after a break from the project.

## Current state

Working: state machine (Start/Debug -> InGame -> End), game loop at 10 ticks/s,
threaded keyboard input, user ship steering (course/speed), vector-based
movement, map boundary checks, factory-based object creation.

Not working / not started: collisions, enemy AI, object placement, ports,
question/answer mechanic, persistence, rendering beyond debug text.

## Bugs and code smells found in review

1. `Map.CheckCollision()` returns `true` inside the loop on the first entity -
   the method is a half-written skeleton. Finish or remove before relying on it.
2. `Printer` is in namespace `homework6` (copied from an earlier assignment)
   and unused. Move to `finalSzczygielski` and adopt it for game output.
3. `IShip` is an abstract class named like an interface. Rename to `ShipBase`
   (or extract a real `IShip` interface) and update the UML.
4. `Program.debug` is hardcoded. Read it from a command-line arg
   (e.g. `--debug`) or an env var.
5. All entities spawn at (0,0) because `DistributeObjects()` is not
   implemented - ships start stacked on top of each other.
6. `GameManager.Test()` and commented-out fields (`SqlManager`,
   `WindowManager`, weather) are leftovers - remove or implement.
7. `InputManager.KeyListener()` duplicates listener logic - decide and delete
   (already flagged in notes.txt).
8. `IShip.direction` setter wraps 360 to 0 incorrectly for exactly 360
   (value > 359 maps any overshoot to 0, losing the remainder). Use modulo.
9. `lastQuestionId` has a `// FIX` marker - decide its access level once the
   question mechanic exists.

## Short-term plan (from notes.txt ACTION PLAN + ToDo)

- Implement `Map.DistributeObjects()`: random, non-overlapping placement;
  link minimum map size to number of levels.
- Implement collision detection (circle-circle using `collisionRadius`),
  then wire `InGameState.CheckCollisions()` into the loop.
- Render the map (ASCII grid or array-based view) - "Map Array logic" was
  the last planned task.
- Replace hardcoded UI strings with text loaded from files (flagged in both
  StartState and InGameState).

## Medium-term

- Enemy AI: override `CalculateNextMove()` in `EnemyShip`; vary enemy types
  per round through the existing factory loop in `GameCore.CreateIShips()`.
- `PortState`: docking when colliding with a port; `SaveGame()`, `GetHint()`.
- `QuestionAnswerState`: quiz mechanic; uses `deletedAnsCounter` and
  `lastQuestionId` already reserved in `GameManager`.
- Persistence: the commented `SqlManager` suggests SQL-backed save/score
  storage was planned.

## Long-term / nice to have

- Weather system affecting movement (commented `IWeather` in GameCore).
- Proper rendering layer (`RenderWindow()` is a placeholder everywhere;
  commented `WindowManager` hints at this).
- Unit tests - movement math (`GetVectorEnd`), boundary checks, and the
  state-guarded setters are all easily testable.
- Recover and commit the UML diagrams to `docs/uml/`.

## Time log

From `timeStatistics.txt`: ~1.25 h initial docs, ~9 h UML and state diagrams,
~15 h coding across three sessions, ~1.5 h UML revisions, ~1 h collision/map
planning. Roughly 28 h total, with a third spent on design.
