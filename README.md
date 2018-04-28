# FPSCyberAttack
First Person Multiplayer Shooter Designed in Unity as Final Year Project in Honors Software Development by Karle Sleith

## Description
With the releases of the new “b33lz3bub” virus, millions of computers across the world have been taken hostage, with the intent of extorting their users, 
but one script kiddy had discovered that that its possible to erase the virus, but must transfer his consciousness to infected computers to do so.

[![Screencast](https://github.com/karlesleith/FPSCyberAttack/blob/master/Screenshots/Screencast.PNG)](https://www.youtube.com/watch?v=RuGs9rvlAo0)

## How to Run
Clone the repository to your Desktop, Access the build Folder, and run the latest Build of the Game.

## Set Up
Requires : MySQL, Unity


## How to Play
This game is compatible with Windows Desktop devices and intended to be played with a Mouse and Keyboard,When the game starts the User is able to navigate the Menu to make a Account and Start up a New Game. The objective of the game is to destroy the Virus Spawner. As it Starts the player spawns in a Randomlly Generated Maze, You should explore the Maze and find the pink Sphere, (The enemy spawner) and destroy it to end the game.  

	W = Forward
	S = Back
	A = Left
	D = Right
	Mouse = Move Cursor,
	Left Mouse Button = Fire
	
## Development
During the process of development I learned how to develop a 3D Game in Unity. The sceneraio of a randomlly Generated Maze, database login and Networking has been built from scratch. I have created a low poly world and objects using a combination of Unity and Blender, All the scripts have been writen from scratch to achieve the feel I wanted, (Eg. Locking the camera rotation on the Y - Axis), I am leaving this game open for futher development as I want to have a release on Itch.io, and further senarios can be implemented with a more complex architechure. I also belive with some effort, this game will able to be ported to Mobile and WebGL. 

### Software Architecture

![arch](https://github.com/karlesleith/FPSCyberAttack/blob/master/Screenshots/Architecture.png)

There are three main components to 1337 :
* Game Engine 
	* Scenes 
	* Scripts
	* Maze Generation ect.
* MySQL Database 
	* User Login
	* Score Manager
* Network Manager
	* FourLeafInteractive.dll
	* UnityNetwork Manager


## Conclusion

To ﬁnish up this project, Over the last 9 months I have created a Multiplayer FPS(First Person Shooter), with a procedually generated world, and functionality to connect to a MySQL Server. I have learned alot about Maze building algorithms and the Unity Engine and its limitations. It was my intention to make a game that I would have enjoyed growing up based on my interests as a child, but also balance being able to show my skills in programming, Algorithmic eﬃciency and Object Orientation, and I believe that I have achieve that. 

## Future Development

Although we are happy with how we developed the game,there is still room to improve, from a gameplay standpoint the World is very bland, although is is due to design and simplicity,given more time,we would’ve added ”GamePlay Modiﬁers” that would have changed the behavior of weapons and enemies. wewould’veaddedalargervarietyofenemytypes,usingarandomgenerator that will choose enemies types from a ”pool” to place in the maze, Weapon variety would be the top of the list, with diﬀerent ﬁre speeds, damage outputs and cosmetic variations. At the moment the world is not very scalable, and that most of the resources go into generating a Maze, I am unable to conclusively determine thatwhetherornotitisduetohardwarelimitationsordotowiththe”Hunt and Kill” Algorithim I have currently in place, While I have Initially wanted to have HUGE dungeon designs between 64 Cells(4096 Cells) Squared and 256 Cells Squared(65536 Cells), I learned very fast it was not feasible, both from gameplay (With the map just being to large to be fun to explore) and from memory limitations. The SQL Database currently only takes in User name and nothing else, we would’ve liked to have been able to track diﬀerent player statistics. While we have started the creating custom network packages for the game, I have fallen back on the Unity Network Manager, Given more time I feel I would’ve been able to complete this feature. Finally the players are unable to communicate with each other, I would’ve like to added a messaging system between players to help with communication facing the boss.


## Built with 
	Unity 5.6
	Blender
	MySQL.
	

