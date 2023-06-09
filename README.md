# StarKnight

[![Build and Deploy of StarKnight](https://github.com/StampedeStudios/StartKnight/actions/workflows/build-and-deploy.yml/badge.svg)](https://github.com/StampedeStudios/StartKnight/actions/workflows/build-and-deploy.yml)


This repository contains the Unity project for a 2D spaceship shooter game built using pixel art assets. The game features a spaceship that fights enemies in a top-down view. The project is configured to be deployed on itch.io using GitHub Actions.

## Gameplay

In this game, you control a spaceship and your objective is to destroy waves of enemy spaceships. The player can move the spaceship using the arrow keys and shoot projectiles at the enemies using the space bar. The game ends if the player's spaceship is destroyed or after defeating all the enemy waves.

## Getting Started

To run and modify the game locally, follow these steps:

1. **Clone** or **download** this repository to your local machine.
2. **Open** the Unity game project using Unity Editor (version 2021.3.25f1 or later).
3. **Explore** the project files and assets in the Unity Editor.
4. **Play** the game in the Unity Editor by clicking the "Play" button.

## Deployment

The game is configured to be deployed on itch.io using GitHub Actions. The deployment process is automated through the following steps:

1. **Build** - A GitHub Actions workflow is triggered whenever changes are pushed to the `master` branch. The workflow builds the game as WebGL using Unity's build pipeline.
2. **Release** - After a successful build, the workflow creates a new release on GitHub and attaches the game build as an artifact.
3. **Deploy** - Finally, the workflow deploys the game to itch.io by uploading the artifact to a specific game project.

To set up the deployment for your own game, you need to configure the necessary secrets and adjust the workflow file located at `.github/workflows/build-and-deploy.yml` to match your project's needs. Refer to the official documentation of GitHub Actions and itch.io for more information.

## Next Steps
- [ ] Make the game more accessible.
- [ ] Adding a more visible reload hint.
- [ ] Adding levels and a level selection screen.
- [ ] Handling save and load of game progress.
- [ ] Improving the spaceship movement.
- [ ] Adding more enemy types.
- [ ] Adding health packs and power-ups.

## License
This project has not been licensed yet, the goal is to make it open source and free to use for everyone. Every asset and code can freely be used for any purpose, exception made for explosion sound effects. If you want to use them, you need to purchase a license from the original authors:

- BigExplosion made by Sound-Ideas.
- AlienExpl1 and AlienExpl2 made by prosoundfx.
