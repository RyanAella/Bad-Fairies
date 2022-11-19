# Changelog


## [0.14.0] - 2022-11-19
## Added
- Standard Assets Package
- Level
  - Build
  - Color
  - NavMesh
- Stats
  - integrated Armor calculation
- Player
  - can use Bombs
    - Damage
    - Force
- Bot
  - can choose from three different attacking animations


## [0.13.0] - 2022-11-17
## Added
- Bot
  - detects Buildables
  - makes damage
  - Hit Animation
- Player
  - Takes Damage
  - Dies
- Buildables
  - no instantiation if hit location has Tag "Environment" 


## [0.12.0] - 2022-11-16
### Added
- UI
  - Shows Key to enter/exit Build Mode
  - Shows current selected buildable
  - Shows current player life
- Player
  - 3 different modes: Default, Build, Die
  - Default: Walk, Run, Jump, Shoot
  - Build: Walk, Jump, Build
- Buildings
  - Select and deselect Buildables
- Player Weapon
  - Can destroy Buildables


## [0.11.0] - 2022-11-15
### Added
- Main Menu
- Spawn Location
- NavMesh to Level

### Changed
- Level Design


## [0.10.0] - 2022-11-12
### Added
- Buildables are children if they are connected
- Buildables have healthpoints

### Changed
- Bugfixes
  - wrong rotation and position if buildable was set alone


## [0.9.0] - 2022-11-11
### Added
- Crosshair
- Buildable Indicators (Containers) and Prefabs (Frames)
  - Floor

### Changed
- Walls, ramps and floor connect together
- Buildables not moving to mouse anymore
 
### Removed
- Rotation of Buildables
- Buildables snap to grid


## [0.8.0] - 2022-11-10
### Changed
- Level Design
- BotMovement
  - minor Bugfixes
- Building
  - Before building the final Buildable, transparent blueprints are visible
  - Rotation with mouse wheel (90°)
  - Blueprint tries to move with mouse (not working right quite yet)


## [0.7.0] - 2022-11-08
### Added
- Buildable Indicators and Prefabs
  - Wall
  - Ramp
  - Stairs
- Grid
- Buildables snap on grid


## [0.6.1] - 2022-11-07
### Changed
- Bot started walking when current location != destination


## [0.6.0] - 2022-11-06
### Changed
- Bot-Prefab gets destroyed after death animation
- Bot moves to player destination if old destination is not reached or if it gets a new destination


## [0.5.0] - 2022-11-05
### Added
- Player
  - Movement
    - Jump
    - Run
  - Attacking
### Changed
- Player can move in camera direction
- Bot turns in direction of player


## [0.4.0] - 2022-11-04
### Added
- Started building Level


## [0.3.0] - 2022-11-03
### Added
- Player
  - Movement
    - Walk
  - Camera-Movement


## [0.2.0] - 2022-11-02
### Added
- Bot-Prefab
  - Animation
    - Die
  - BotLife
  - Taking Damage
- Player
  - PlayerLife


## [0.1.0] - 2022-11-01
### Added
- Bot-Prefab
  - Grafikmodell
  - Animation
    - Idle
    - Walk
    - Run
    - Attack (3x)
  - States
    - Idle
    - Searching
    - Chasing
    - Attacking
    - Dying