For DoomBots, we have made an easy system 

take an existing champ ( you can found example in folder Content\Lua-Converted\Characters\Morgana\DoomBotSpell )

Create Spell like "DoomBot_{`nameofspell`}.cs" 
The `nameofspell` needs to be exactly like the original spell. 

If you have a good exeperience you can edit the Behaviourtree and add a new difficulty like `Uber` for your doombot, to be more agressive, for moment we just use BehaviourTree of s1.

And to test your DoomBot, create a new player, go in `GameInfo.json` set it's `playerId` to -1 and set the `useDoomSpells` to `true`.

Actual players can't use these Doom spells, since the GameClient wouldn't know what to load.
A workaround for this could be to copy a Character's folder both in the GameClient and GameServer and rename all mentions of the Character to something like "Doom`Character_Name`".
That would make so the game thinks it is an entire different character, and from there, you can mod the spells to your hearts content.
Just be careful and make sure you properly add the preload calls to the spells and particles, otherwise it can cause huge lag spikes mid-game as the GameClient loads stuff it didn't loaded during the loadscreen