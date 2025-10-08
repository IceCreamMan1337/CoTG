Some versions of League of Legends were released with an `pdb` file, which contain useful debug symbols.

With help of this file, if you have the correct executable, you can basically decompile the gameclient with GHidra, IDA or Binary Ninja and get an approximate version of all functions in the GameClient in pseudocode.

These debug symbols got leaked a couple times in the story. 
On windows that happened on `beta 0.9.13`, `1.0.0.106`, `1.0.0.126`. And on mac, on version `4.17` 

On some of that versions, there also exists `RiotLOL_Client_AB` executables. These are the dev versions of the game,
It contain many useful tools, like a navgrid visualizer, pathfinding calculation, cheats, collision radius viewer, particle editor / turret etc ... 

We have also found in the Alpha 0.8, a mention of an RiotLOL_Server_AB.exe 
which uses file in DATA/CFG/GamePermanent.cfg 
```
[NetConfig]
enetprofile=0
netlogging=1
chatlogging=1
localaddr =127.0.0.1
; numclients used only by server to determine how many clients he expect to connect
NumClients = 1
; 100 - order side, 200 - chaos side
TeamID = 100
; if not specified will be Annie for order, minotaur for chaos
Skin= Minotaur
CheatsDisabled = 0
batchPackets=1
UserName=defaultname

[ServerPerformanceTweaks]
; Note FPS = 1 / Frames
OverallServerFPSCap = .033 ; 1 / Frames ; Note this doesn't update live
PercentageOfMinionsProccessedPerFrame = .5 ; ie .3 means a minion will get updated every 3 frames
FPSMinonCapBeforeThrottle = .2 ;1 / Frames
PercentageOfHeroesProccessedPerFrame = .1 ; ie .3 means a hero will get updated every 3 frames (TODO: Make this not based on the number of heroes : Joel)
FPSHeroCapBeforeThrottle = .2 ;1 / Frames

LevelProp_FPSCapBeforeThrottle=.5
LevelProp_PercentageProccessedPerFrame=0.15
LevelProp_MaxUnitsProcessedPerFrame=3
```
This is aproximately the same idea with our gameinfo.json 

