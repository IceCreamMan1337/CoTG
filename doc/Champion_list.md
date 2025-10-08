# Working Champions:  
- Akali
- Alistar
- Amumu
- Annie
- Blitzcrank
- Brand
- Caitlyn
- Cassiopeia
- Corki
- Ezreal
- Fiddlesticks
- Galio
- Gangplank
- Garen
- Irelia
- Janna
- Karma
- Kassadin
- Kayle
- Kennen
- Kog'Maw
- Leona
- Lux
- Malzahar
- Maokai
- MissFortune
- Mordekaiser
- Morgana
- Nunu
- Renekton
- Ryze
- Shen
- Singed
- Sion
- Sona
- Soraka
- Swain
- Talon
- Taric
- Tristana
- Trundle
- Tryndamere
- Udyr
- Vladimir
- Warwick
- Xin Zhao 
- Yorick
- Zilean

# Playable Champion: (minor issues, but still playable)
* Anivia
	- Q seems to have some lag when recasting
* Ashe
	- Her hawk doesn't provide vision during flight, only at the final position
* Dr.Mundo
	- Q can pass through targets (client-side only) 
* Evelynn
	- Breaking stealth can sometimes take 0.25sec 
* Gragas
	- E collision works but is uggly
* Heimerdinger 
	- Turret doesn't seem to have full HP at spawn
* Jarvan IV 
	- R wall is not beautiful
* Karthus 
	- When khartus dies, the forcedead adds +1 kill, but doesn't give gold
* LeeSin 
	- R seems to knockback too far away
* Malphite 
	- When malphite spawns, his shield is invisible, but he has one
* Master Yi 
	- Q takes too much time
* Nasus 
	- Q can miss the animation
* Nidalee 
	- The model can be broken when nidalee is out of a bush
* Nocturne 
	- AA + passive can be casted sometimes (AOE AA) but not everytime
* Olaf 
	- Q axe always faces the same direction
* Pantheon 
	- E particle is uggly 
* Rammus 
	- Q knockbacks too far
* Rumble 
	- Too many R particles
* Skarner 
	- Sometimes it's hard to AA 
* Teemo 
	- Damage from minions and jungle monsters remove the W passive
* TwistedFate 
	- PickaCard is reverted to 106, seems to work fine, but more test is needed
* Twitch 
	- His R seems to pass through, but only with certain angle, need more investigation about it, the rest works fine
* Urgot 
	- Q missile can pass through (client-side only) 
* Vayne 
	- Q stealth has a hack, 1 second was too short, so i put 2
* Veigar 
	- Dashing into his wall doesn't stun
* Xerath 
	- R particle is not the correct one 

# Needs Bugfixes: 
* Cho'Gath : 
	- Castrange of R is messed up, he need to be go in the unit for "cancast" the spell (Didn't I fix this already?)
	- R has some replication issue with size and health
* Katarina : 
	- Q of katarina may bounce on the same target
* Leblanc : 
	- W dash is overriden by R (or the other way around)
	- Passive clone doesn't work properly, so it was replaced with a minion
* Orianna : 
	- R seem not knockup to the ball
* Poppy : 
	- E of poppy works almost fine, she just doesn't follow the entity, it works kinda like Alistar's W
* Riven : 
	- Q / R cooldown is not shown client-side
	- Q knockup is too high
* Shaco : 
	-  Casting a spell or AA when in stealth will cause a lag spike
* Wukong : 
	- W creates the pet properly, but the clone cant hit all the map randomly, for the moment a hack "fix" was implemented to get him "playable"

# Champion issues due to .131 (dodge) : 
* Sivir : 
	- Passive need have dodge method not implemented 
	- Q doesn't come Back correctly 
* Jax : 
	- E is an instant cast , due to the non implemented dodge 

# Champions that can be playable but need to be modded in due to being released after: 
* Graves
* Shyvana
* Fizz
* Volibear
* Ahri
* Viktor
* Sejuani