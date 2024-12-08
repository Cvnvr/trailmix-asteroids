# trailmix-asteroids
Clone of the class arcade game Asteroids (1979) for the Trailmix take-home assignment.

### Controls:
**Movement**
- 'W' / 'Up arrow' -> forward thrust
- 'A' / 'Left arrow' -> rotate left
- 'D' / 'Right arrow' -> rotate Right
- 'S' / 'Down arrow' -> hyperspace

**Firing**
- 'Spacebar' / 'LMB' -> fire projectile

### Implementation details

#### Core mechanics:
All core game mechanics have been implemented, such as:
- Player ship and all functionality (movement, projectile firing, 'hyperspace' travel)
- Asteroids and all functionality (three sizes, movement, splitting after being destroyed)
- UFOs and all functionality (two sizes, dynamic movement, projectile firing)

#### Additional feature:

**Power-ups!**
- After destroying a UFO, there is a chance that they will drop a power-up at their position.
- Driving into this with your ship will then apply that power-up, changing your active weapon's behaviour (e.g. instead of the default fire, you will be able to fire rapidly instead).
- Implemented power-ups: rapid fire, shotgun

#### Other work:
- Extensive content tests covering all data objects (Window > General > Test Runner)
- Score (rewarded for destroying asteroids + UFOs which is reflected in the UI)

#### Plugins used:
- **Zenject** -> third-party dependency injection framework (implementation)
- Unity’s **Input System** -> used for movement, shooting, etc.
- Unity’s **TextMeshPro** -> used for UI
- Unity’s **Testing Framework** -> to enable Editor tests