This project is the first tutorial project from the Udemy course [The Ultimate Guide to Game Development with Unity (Official)](https://www.udemy.com/course/the-ultimate-guide-to-game-development-with-unity/)

I'll add notes here on any problems I had with this project... Over 97,000 students have taken the course, so there shouldn't be anything new to see here other than my bumbling mistakes and general confusion on becoming a game developer at my age.

### First Commit
I've made it to the first code challenge. At this point, we have a 3d cube representing the Player object and a script attached to it. I have Unity configured to open Rider as my IDE, but "advanced integration" is not working. I opened a support ticket with JetBrains, but all the basic functionality works, including setting breakpoints to debug the code. 

My Player script supports moving the player with the arrow or WASD keys. The tutorial gives an example of setting bounds on the movement in the Y direction and then gives a challenge to add the edge boundaries for the X direction. Their bonus challenge is to wrap the Player's movement to enter from the opposite edge. Their example uses hardcoded values for the boundaries pulled from inspecting the Player transform position. 

I coded my script to get the Player's screen position and compare the position with the screen size, thus removing the hardcoded values and allowing dynamic changes to the screen dimensions. I worked on the bonus to have the Player wrap around like in an Asteroids game. I learned how to do this from this [YouTube video](https://youtu.be/zWy29yeFNX8?si=w6MnF94Hjvr6Sl4T), but I got stuck for a while trying to make it work. The fix not mentioned in the video (but perhaps in a prior video) was to switch the Camera from perspective to orthographic projection.

### Linux Build
I've built the game, v1, for Linux desktop and converted it into a deb file for easy installation. I struggled a bit to get UnityPlayer.so included in the package at the right location. I had never built a deb file until now.

This commit also added a WebGL build but that is broken at the moment, and not deployed to my new website which is, of course, under construction. However, my new mantra is build the basic functionality quick and add features later. As evidenced by version 1 of this game.

### WebGL Build
To get the WebGL build to work on Debian 12, which does not ship python2 and is missing a synonym for "python", I had to install python-is-python3, python3-distutils, and libtinfo5. I copied the resulting build folder up to my new website and it works like a charm. Play it [here](https://themc.games/SpaceShooter-C001/).

### Commit C002
We completed the second round of code challenges by implementing a laser fire system.
