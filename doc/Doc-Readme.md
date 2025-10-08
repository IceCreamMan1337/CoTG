Welcome here brother

This Gameserver was written with the passionate work of countless developers.
Here you will find all information and all our work.

# The Gameserver : 

This Server is an emulator for League of Legends Season 1 
It includes: 
- 75 Fully Playable Champion(8 need small fixes, 9 come from different versions) (Lua/BB scripts were converted to C# scripts for performance)
- Summoner Rift / Old Twisted Treeline / Dominion Are Playable (Lua mapscript)
- Focused mainly on `1.0.0.126`, but the server can start on `1.0.0.132`, `1.0.0.106` and `Beta`, but it wasn't designed for those versions
- Behaviourtrees leaked in `0.0.0.0` and `1.0.0.141` were 80% reversed and adapted for this Gameserver ( xml-BT to C# ) 

The Gameserver supports modding but for custom map/champion, you'll need to also mod the client: 

- Newer maps can be backported, for example, the Providing Grounds ARAM and New Twisted Treeline (Behaviourtree of these maps were never leaked unfortunatelly) 
- You can create Custom Gamemode : `SumonnersGames` inspired by GunGames 
- Custom Spells / Custom Buffs / Custom Champion are supported 
- Custom DoomBot spells, with help of their BehaviourTree, just with one rename 
- Custom maps are supported 

# Extra Tools

All tools we have used we will be in an folder, some other will be here more by nostalgia: 

- Wooxie
	For porting `obj` to `Wgeo` (format of map s4)
- WgeotoNVR
	For converting map `wgeo` to `nvr` format , used/supported in s1 (magic number < 8 )
- UnluaC
	For convert `LuaOBJ` to `Lua`
- LolPyTool
	For convert `Inibin`/`Troybin` to the format readable `ini`/`troy` format
- BuildingBlocktoConvertedScript
	Our original way to convert `Lua` to `ConvertedScript`
- LolNgridConverter-BMP
	This one allows editing an original `aimeshngrid` file with help of an `BMP` (can export lolngrid map to bmp too)
- MOBEditor
	Used only in s4 , this one permit to edit mob file who contain position of turret/inhibitor/grass etc .. 
- Replay parser
	Used for parse `LRF` replay , just .126-.130 replay can have some difficultuies to be parsed 


In the folder doc, you will find all documentation about how work the server , the reverse engineering , file  , some test , how to mods and mods 

# Q&A: 

* Why Release in that state and not when is finished? 
    - The time spent in it could be more useful on other projects, we learned so much with this project and it was very cool, but we need be realistic.
	Without a community, fan projects like this one can't really live, so we took the choice to open the doors to everyone.
    - The risk, as previously seen, twice in fact, Riot will likely not be too keen about the existance of this project.
	And is understandable, as this could be see as a competidor against League in a way.
	We just want to allow people to rediscover League of Legend, let them see how everything started, and also allow the possibility to people to create their own Gamemodes / Champions / Maps 
    - If other teams like ours exist, they could take inspiration on some of our systems. 


* Why 1.0.0.126 ?
    - Debug Symbols from this version were leaked (.pdb) 
    - The Developer build was released for this version. It contains a bunch of Cheats, Mapgridviewer, Refreshcompetence and more. 
    - The nostalgia, is the end of the 1st season (pre-season 2).
	- `1.0.0.126` is pretty close to `1.0.0.131`, the last patch where full Character/Spell scripts were shipped to GameClients (In fact, I believe some of the scripts are from `1.0.0.131`)

* Why not direclty load lua spells? 
Althout we have setup a Lua script engine, with C# scripts we can more easily debug, modify and write custom scripts.

* What is BehaviourTree? 
For Gamemodes / AI / Quests, League of Legends uses BehaviourTrees.
That's why up to this day, as far as we are aware, no team was be able to get Dominion Playable, Official Bots or the New twisted treeline work (At least the plate part).\
We have reversed approximatelly 80% of this.

* Custom map / Custom Champion ? Why ? 
We think we can't just come and purpose the same exact way to play League of Legend.
And what better way to do that than community generated content?

* A message to Rito? 
Thanks and Sorry.
We know your fight these Servers/Fan projects, but it seems you also chose to not create an Vanilla/Classic server.\
On top of that there's Vanguard, an incredibly intrusive anti-cheat, you can't even play Battlefield 6 with with installed.