# Changelog


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