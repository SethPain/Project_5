What Works:
-All of the steering behavoirs work reasonably well
-Honors portion (Wander) works reasonably well
-Scene management works well; a pause menu can be accessed by pressing "E" in each scene. This allows you to restart the level, go to 
the main menu, or quit the game
-In seek, offset pursuit, and flocking, the target can be moved by clicking on the terrain
-In offset pursuit and flee the target will wander on its own

What doesn't work:
-Everything works at least reasonably well

Bugs:
-In scenes where the target can wander, it might wander out of bounds. This has been mixed by allowing the target to be moved my mouse
clicks
-Some objects couldn't be called back to spawn, so they attempt to bounce off bounding wall. Some objects don't bounce well, so they
are called back to spawn

Problems Encountered:
-Alignment in flocking doesn't seem very strong unless all boids are seeking. Alignment is not strong enough to force wandering boids
into a flock
-I could not get objects to avoid walls, so they are often called back to the middle of the play area on collision
-Vector operations sometimes yeilded a NaN or infinitely large value (this issues have been avoided in the current state of the game)

