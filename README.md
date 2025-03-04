1. Game Overview
Snowboard Rush is a 2D arcade-style game built in Unity where the player controls a snowboarder skiing down a snowy mountain. The game features obstacle avoidance, trickery, and a coin collection system that rewards exploration and skill.

2. Gameplay Elements
2.1 Skater
Main Character - Default Equipment - Full Stamina

Controls:
- Move left and right with A/D or Left/Right arrow keys
- Jump with spacebar
- Perform tricks using specific key combinations while jumping

Features:
- Smooth movement with animations
- Stamina system that affects performance
- Limited screen boundaries
- Particle effects for ski tracks and collisions
- Coin tracking

2.2 Obstacles

Attributes:
- Obstacles that make it difficult for the player
- Collision with an obstacle will end the game
- Randomly appear on the path

Features:
- Higher trick difficulty will bring more points
- Land correctly to avoid being wiped out
- Tricks combination system to get bonus points

2.3 Monster Boost
Speed ​​Up Zone:
- Found along the track
- Temporary speed boost

2.4 Coin collection system
- Each level contains a maximum of 10 coins
- Coins are scattered throughout the track
- Players must explore and navigate skillfully to collect them all
- No penalty for missing coins, but full collection will be rewarded

3. Game flow
3.1 Main menu
- Level selection system
- Shows coin collection progress

3.2 Gameplay
- Difficulty increases as the track goes down
- Pause system (ESC key)
- Pause menu options:
- Resume game
- Return to main menu
- Exit game

3.3 End game
- Shows total coins collected
- Tracks best coin collection for each level
- Options to:
- Retry run
- Return to main menu
- Exit game

3.4 Coin tracking system (CoinManager)
Features Features:
- Continuous highscore system using PlayerPrefs
- Track total coins collected per level
- Update coin count in real time

4. Technical Features
- Continuous coin tracking system using PlayerPrefs
- Gradual difficulty based on time and terrain
- Smooth character movement and animations
- Screen boundary detection for all objects
- Particle effects for snow spray and wipe
- Audio feedback for tricks and collisions

5. Control Summary
- Move: A/D or Left/Right arrow keys
- Jump: Spacebar
- Pause: ESC
