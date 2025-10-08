For starters, you'll need two copies of the GameClient. One to actually play, and another for the GameServer read data from.
The placement for the copy for the GameServer is at `Content/GameClient`

Settings for the match can be found at the build directory, once the server has been ran at least once to generate the Settings file

Start the server by running `ServerConsole` Executable

In order to connect to the server, you have to launch League's executable with launch arguments.
You may do this using a batch file or creating a shortcut.

If using the batch file, you may create a `.bat` file in the same directory as league's executable, then paste the following snippet:
`start "" "League of Legends.exe" "" "" "" "127.0.0.1 5119 17BLOhi6KZsTtldTsizvHg== 1" `

If using the shortcut, at the shortcut's target, you need to add a space at the end and paste the following:
`"" "" "" "127.0.0.1 5119 17BLOhi6KZsTtldTsizvHg== 1"`

# Lauch Arguments

* GameClient
	`"" "" "" "<IP of gameserver> <Port> <encryptionKey> <PlayerID>"`

* GameServer
	The GameServer's `Port` can be changed with a launch argument(`--port <Port>`), while the EncryptionKey and PlayerIDs are setup in the Gameinfo.json 

Going into detail :

For example this is the command used to start spectator mode 

`start "deploy" "League of Legends.exe" "8394" "LoLLauncher.exe" "" "spectator spectator.na.lol.riotgames.com:80 <encryptionKey> <gameID> NA1" `

"deploy" = folder ( because normally old league interract with folder who look like this : rads\solutions\lol_game_client_sln\releases\0.0.0.51\deploy )

"League of Legends.exe" = the executable ( gameclient )

"8394" = ??? 

"LoLLauncher.exe" = associated to the launcher 

"" 

"spectator ip:port <encryptionKey> <gameID> Server" = this one is for start the spectator mode on an specific ip 



# Setup the Gameinfo.json

* `gameId`
	- Like in League, the gameid is use for Replay and for History/Endgame stats.

* `game` 
	- This block will permit to edit Gamemode / Map.
	
	* `map`
		- This part need correspond to the map you want launch present in your Gameclient and in your script folder 
		Example of map already integrated (doesn't require mods):\
		1 : SummonerRift\
		4 : Old Twisted Treeline\
		8 : Crystal Scar (Dominion) : Be aware ! Setup this map on ODIN gamemode\

		By modding the Gameclient you may also launch:\
		2 : HarrowingRift\
		3 : Providing Ground (Was just an Tutorial map , need to reverse behaviourtree for it)\
		6 : WinterRift Christmas map\
		10 : New Twisted Treeline - the map can be backported but there are some little things like particle to fix\ 
		19 : Substructure 43 - This map was backported from patch 7.23 as a proof of concept, but not really playable and ugly \
		24 : Providing Ground "Custom" : this one has Lua script custom for be like the aram map (s2) , this one is used too for the Summoners Games (for Test summonerGames , set ODIN in gamemode)\

	* `gameMode`
		- Here, most of the time you can let it as `CLASSIC`. If you want to play dominion, it has to be `ODIN` instead.
		This can be used to handle custom gamemode scripts 
 	
	* `mutators`
		- Mutators were officially only introduced in S3. This is leftover functionality from a project migration. 
		This should still be functional though, and you may use it to create custom mutators.
		Mutators are for things like URF, which it doesn't inherintly change the gamemode, it's something extra on the side.

* `gameInfo`
	- This block permits to editing many properties about the gameserver. 
	
	* `CLIENT_VERSION`
		- Here you can choose the version of the GameClient expected to connect 
		While we focused on version 1.0.0.126, the server can technically support 1.0.0.106 / 1.0.0.132 if setup correctly.

	* `FORCE_START_TIMER`
		- Amount in seconds of time the server will wait for everyone to load. If this limit is reached, the match will start regardless.

	* `MANACOSTS_ENABLED`
		- Enables or disables mana costs. 

	* `COOLDOWNS_ENABLED`
		- Enables or disables cooldowns.
		Warning: This may break many spells that weren't designed to be spammed.

	* `CHEATS_ENABLED`
		- Enables or disables chat cheats/commands (prefix for commands is `!`).

	* `MINION_SPAWNS_ENABLED`
		- Enables or disables Lane Minion spawns (May also be set mid-match with the `!spawnstate 1|0` command).

	* `CONTENT_PATH`
		- Directory where the GameClient with the data to be used by the GameServer is.

	* `IS_DAMAGE_TEXT_GLOBAL`
		- Enables or disables the broadcasting of damage text to all clients. 

	* `ENDGAME_HTTP_POST_ADDRESS`
		- This is only to be used if you want make a launcher or recover stats. At the end of the game the server will post all stats, such as damage dealt, minions killed, items bought to this address.

	* `APIKEYDROPBOX`
		- This one wasone was more for fun. At the end of the game, if you have enabled replays, you can create a download link with just a dropbox APIkey.

	* `USERNAMEOFREPLAYMAN`
		- This is used in case you got already a database for your launcher. The replayman is an "admin" who will associate the replay to the gameid (was used with a basic java server with jwt).

	* `PASSWORDOFREPLAYMAN`
		- Same as the previous one, but the password.

	* `PASSWORDOFREPLAYMAN`
		- Same as the previous one, but the password.
	
	* `ENABLE_LAUNCHER`*
		- This sends a heartbeat to your launcher to say the GameServer is ready to accept connections.

	* `LAUNCHER_ADRESS_AND_PORT`
		- Ip:Port of a LauncherServer to send EndGame Stats, heart beats...

	* `SUPRESS_SCRIPT_NOT_FOUND_LOGS`
		- Whether or not to Log if a script couldn't be found.

	* `ENABLE_LOG_AND_CONSOLEWRITELINE` | `ENABLE_LOG_BehaviourTree` | `ENABLE_LOG_PKT`
		- All these are for debugging, it will spam the hell of the console.

	* `ENABLE_REPLAY`
		- This one is more an POC not finished, this one will write an file .rep (official format of replay/spectator league), you just need drag and drop this file on your leagueoflegend.exe and it will run replay.

	* `ENABLE_ALLOCATION_TRACKER`
		- Warning : Only enable this for tracking performance. Works better on windows, this can be used along with Visual Studio's profiler.

	* `SCRIPT_ASSEMBLIES` 
		- Which assemblies to load scripts from, this was added so script assemblie can be shared.
		Here you can add an assembly file with scripts that someone else compiled.
		Alternativelly, you can add a directory with uncompiled/raw C# scripts, the server will then automatically compile them during runtime and use them.
		Scripts at the bottom of the list take priority over the ones at the top. For example:
		```
		"SCRIPT_ASSEMBLIES": [
			"X",
			"Y"
		]```
		Let's say you have a `ChildrenOfTheGrave` script in both `X` and `Y`, the script in `X` will be overriden by the one in `Y` because `Y` came after `X`.
	
* `players`
	- This block permits to edit players or bot.

	* `blowfishKey`
		- This is the Blowfish key, necessary to pass the keycheck.
		
	* `playerId`
		- The player id of a given player. This should be unique. If you want a bot, then use negative numbers.
	
	* `name`
		- Name of the player.
		
	* `champion`
		- Name of the Champion (Case Sensitive) being played by the player.
	
	* `skin`
		- ID of the skin of the Champion.
	
	* `AIDifficulty`
		- If player is Human , keep it at 0.\
		This is used for choosing the difficulty of bots on Summoner Rift (map4 has only basic bots).\
		Although Riot had only implement newbie/intermediate difficulties they have let some placeholders for other difficulty that can be implemented.\
		0 = DIFFICULTY_NEWBIE\
		1 = DIFFICULTY_INTERMEDIATE\
		2 = DIFFICULTY_ADVANCED\
		3 = DIFFICULTY_UBER\

	* `useDoomSpells` 
		- Whether or not to use "DoomBot" versions of the SpellScript instead of the normal one.
		- This was added goofing around. The GameClient doesn't really have support for this kind of stuff, so it is limited to bots.

	* `team`
		- Team of the Player (BLUE/ORDER or PURPLE/CHAOS).

	* `summoner1` and `summoner2` 
		- Spells to be put in the SummonerSpellSlots (D and F).

	* `icon` 
		- Icon displayed on launcher/loading screen.

	* `runes`
		- List of runes used by the player. Runes are essentially items, so they can be found in DATA/Items in the GameClient.

	* `talent`
		- List of Talents, or "Masteries", as some people call them.
		A KeyValuePair of TalentID and its Level.

Some example of Gameinfo.json will be included in the folder GameserverConsole/Config/Gameinfo.json.template .