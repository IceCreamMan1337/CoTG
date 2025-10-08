On this part , we will enter in detail about "how work an map" in league of legend 


First all Map are in folder : 
LEVELS\ 
and are enumerated like Map1/Map2 etc .. 

in that folder , we can found so much file , i will describe one by one behind : 


# AIPath.aimesh
This Contains a mesh of the walkable map.
It's a basic mesh that is not really used by the client (you can remove, there won't be a noticeable difference)
We assume this is used server-side for minion pathing, although we haven't implemented that.  

# AIPath.aimesh_ngrid

The Aimesh_ngrid contain multiple "layers" in them 

- VisionPathing : League of legend doesn't use an navmesh for the pathfinding , but use an Navgrid , we can found cell in them , these cell can be flagged by multiple flags like : 
- nothing  
- Wall 
- Grass 
- Can'tpassthought but give vision 
ETC 

-HeightSamples : League of legend is an "3d" game , this one will permit edit the height of the map , but it use corner of cell for the calculation , not the cell itself 


is these one we will edit/read in case we want get it worked ( or for modding )

The entire Pathfinding and colision is maintened with the Navgrid 

official Pathfinding is an A* little modified ( ours is an double A* )

For the collision avoidance , league use list contained in each cell (actor_list)
An entire method is dedicated in gameclient about the collision avoidance with help of Actor method and actor list 
the collision avoidance is calculated with an "bigger" cell who contain approx 4 cell 


The aimesh_ngrid file contain other "map" but not included in .126 like the NearestLane who is used with newest behaviourtree (s4)


# Announcement.dat
This are the announcements based on the MapId and the Gamemode

# Atmosphere.dat
This appears to be entirely client-side. IIRC its something to do with DirectX. 

# Constants.var
This contains very important variables that get adjusted for the map. 

# DeathTimes.ini
The Death Times per level of the map 

# env.ini
Just to change the color of the environement (like a light)

# ExpCurve.ini
Cumulative EXP for each level.
IE. If the exp necessary for level 2 is `100`, and from level 2 to 3 is `200`, then the exp requirement for level 2 is `100`, but for 3 is `300`(100 from level 1 + the desired 200)

# Gamma.ini
The Gamma setup of the map (like we can find in other games).

# Graphics.ini
This one is based on the gamesetting.
It will be applied for the NVR (SimpleEnvironment) and saved here .

# Items.ini
This contains a list of items allowed, and a list item banned.

# Light.dat
Contain all lights present on the map 
It's pattern is as follows

355 292 10865 127 68 0 652

X   Y    Z     R   G  B  A 

Don't lose your time editing this, just edit them directly them with ab_client and it's dev tools

# Locations.dat
This file in "classic" map will not really be interesting for the most part 
But, on special Maps like Dominion, it will contain all spawn of entity/perceptionbuble/navpoint 

This allows a Name to be associate to SCO/SCB object.

SpawnPointAlpha			__NAV_C02 

So if you search `SpawnPointAlpha`, it will be at the position of the `__NAV_C02.sco/SCB` object

# Minions.dat
Found on some versions, haven't found any use for them at the moment 

# Mission.ini
This is used based on the gamemode associated, there we can find:
	- Behaviourtree necessary for server 
	- Experience split percent
	- Loadscreen for the client (there exists only two in 1.0.0.126, default or tutorial)
	- Some other unknown values Server-Side 

# Particle.dat
Contain a list of all particle and their position on the map 

Data/Particles/FireTorch_Blue.troy -404.008 183.582 6023.15 -2147483648
    Particlename+location             X        Y       Z        unk 

Same as the lights, you can also edit them with the ab_client.exe 

# ShadowSettings.ini
To edit shadows' fade min and max 

# soundmap.ini
Ambient sounds of the map , i don't know if really used 

# sun.ini

this one is most used than the Atmosphere.dat for the color environement 
here you can setup the sky color , position of the main light , sky position etc ... 
Can be edited directly in ab_client too 


# INFO Folder
Contains the Minimap 

#Scene Folder
This folder will contain all objects, 3D model of the map, Textures and more.

* .SCB/.SCO
`SCB` is a binary file, its variant is `SCO` which is readable.
We haven't implemented support to reading `SCB` at this hour.

For the most part, GameClients can work fine with just `SCO`s, which are simpler for modding/reading.

`SCO`s have a bunch of stuff: 

```
[ObjectBegin]
Name= Info_PointA
CentralPoint= 4342.5029 -146.3385 2515.9644
Verts= 4
4316.3271 -146.3385 2542.1404
4368.6792 -146.3385 2542.1404
4316.3271 -146.3385 2489.7883
4368.6792 -146.3385 2489.7883
Faces= 2
3	   1    3    0	lambert1            	0.625000000000 0.000000000000 0.625000000000 0.250000000000 0.375000000000 0.000000000000
3	   0    3    2	lambert1            	0.375000000000 0.000000000000 0.625000000000 0.250000000000 0.375000000000 0.250000000000
[ObjectEnd]
```

# room.dsc
This contains a list of all map objects in the map. 

# room.mat
This is all materials/textures necessary for the SCO/SCB objects.

# room.nvr
The 3D Model of the map. 

```
An nvr file contain thing like : 

4E 56 52 00 nvr
 09 00 
 01 00  magicnumber

materialsCount
 vertexBufferCount
 indexBufferCount
 meshCount
 nodesCount

 and after it will be like : 
   for (int i = 0; i < materialsCount; i++)
 materiel name
 color 

 ( you can repeat that for vertex / index / mesh / node ) etc ... 
```
~No idea what's is written above.~

# room.WGEO
This is only for the Season 4. `WGEO` is the newer format the map model in League of Legends.
It follows approximatelly the same style as the `NVR`, but with more capacity and less useless information.

# AuxTextures Folder
Contains textures of the river / water that is animated. 

#cfg Folder
Contains configuration for Gameclients and Server for MapObjects 

In them we can find, for example , Health/Armor/Selection Radius/Perception Bubble for MapObjects

#Textures Folder
This one just contains a bunch of .DDS files that are the textures listed in .Mat / NVR files

# Scripts Folder
	* CreateLevelProps.lua
	This to spawn the Props across the map.
	But that's kind of a bad definition.
	In reality, this script most of the times just spawn turrets. 
	Although there were instances, where they did spawn LevelProps.

	* LevelScript.lua
	This is the script of the map itself and how it behaves. 
	It contains tables of LaneMinions, with their stats and how they are to be spawned, how Turret tagetability is handled, Win condition etc ... 

	* NeutralMinionSpawn.lua
	This script handles the spawn of Neutral Minions, most frequently know as monsters.
	Here you'll find the jungle timers, conditions, camps and stats

# AiTaskDataFile.dat
This permits to associate the correct .dat AITask to the team 

# TeamChaosAiTask.dat
AITask associated with the Chaos Team

# AiMissionDataFile.dat
This allows to associate the correct .dat AIMission to the team 

# Mutators
Although Mutators weren't yet implemented in s1, we decided to keep them.
It works alongside the LevelScript to handle smaller and modular modifications to the match, which can easily swap in an out.
Think of URF for example (The first versions of it at least). It was mostly not more than just a buff applied to all characters.

# Map8
Map8 is somewhat of a different breed of maps in League.
It's Levelscript is for the most part useless when compared to what really goes on there.
That's because `ODIN`, the gamemode played on Map8, is governed almost entirely by the Map's BehaviourTree
There are some very interesting files in there. 
	* AiEntityDataFile.dat
	List of Entities necessary for the MapScript's BehaviourTree 

	* LevelScriptDataFileServer
	All file "levelscriptXXX.dat" is surely used by Riot for their server 

	* OdinScript.xml/OdinScriptBehaviorTrees.xml/OdinTower.xml
	These are mentioned in the Mission.ini, but they are not really used on the levelscript of ODIN Gamemode,
	The only theory we got, is that an it was a special gamemode when league did their spotlight on the Dominion map.

	* Folder Encounters MAP8
	So in this folder, you can find all special entities needed for the BehaviourTree 
	On map8 it contains stuff like HealthPacks, Turrets, SpeedShrines, QuestIcon...