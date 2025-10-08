Each champion / Item / Spell has their own ini/inibin data files 

These inibins can be "unhashed" for be readable for humans with tools like lolpytool(By moonshadow565), and the client support these too, although some hashes are missing (Although we haven't ran into issues with that either)
So you can technically convert all binary inis to ini and still have your gameclient functional.

We have integrated both methods to read them, Binary and Text inis.
The Binary reading was reversed engineered from the GameClient, although a couple changes were made.
Namely, we chose to prioritize the Text Inis over the Binary ones, due to how much easier they are to modify, we believed it would've been better for modding.

their ini can be look like an file of configuration of the entity 
for example  , an champion/minion/structure : 

# Characters

	* Data
		This section contains all information about stats of the character(Armor/BaseHP/BaseMP/AD)
		It also contains all spell names of the champion from the character's kit (Spell1/Spell2/Spell3/Spell4), as well as their extra spells.
		There's also some other less relevant data such as descriptions, search tags...
		Usually there's also a bunch of irrelevant descriptions.
		We assume that's because, when creating a new character, the developers would copy some other character's file and use it as a template. But then didn't delete the stuff left over

	* Info
		This was used just for the icons in the left bottom and the icons on minimap 


	* MeshSkin
		This part is for the 3D Model of the champion 

	* Sounds
		Deprecated in .126

# Items

	* Build
		The recipe of the item. 

	* Categories
		This is more used in client side 

	* Data
		Here we can see the stats given by the item and descriptions.

# Spell

	* SpellData
		Spelldata is like the DATA for an character, there you will find stats necessary for the spell, such as CastRange, ManaCosts, MissileSpeed, Flags, etc.
		Most of the stuff here should be pretty self-explanatory, the stuff to take note is to following:

		- TargettingType
			This will represents how the spell is cast 
			It's enum is as follow:
			```
			Self = 0x0,
			Target = 0x1,
			Area = 0x2,
			Cone = 0x3,
			SelfAOE = 0x4,
			TargetOrLocation = 0x5,
			Location = 0x6,
			Direction = 0x7,
			DragDirection = 0x8,
			```
			When you cast the spell, you need take in account this to understand how the spell is casted.
			For example:
			Nunu's R TargettingType is 0, so he casts the spell at himself.

		- TextFlags
			This determinates who can be targeted/hit by the spell.
			AffectFriends = Affects Team equal to yours.
			AffectEnnemies = Affects Team opposite to yours.
			AffectNeutral = Affects Team_Neutral.
			AffectMinions = Affects Minions who are in team [AffectFriends|AffectNeutral|AffectEnnemies].
			AffectHeroes =  Affects Champions who are in team [AffectFriends|AffectNeutral|AffectEnnemies].
			AlwaysSelf = Can be particular, sometimes an effect can be applied on the "Caster" like an heal or whatever.