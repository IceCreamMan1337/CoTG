Behaviourtree is used for: 
AI (Bot)
Mission/Quest(Khazix vs Rengar Quest / Dominion Quest)
"Ceremony" (For example Map11, New summoner Rift , all prop / jungle animation was handled with them)
"Gamemode" (Majority of Gamemode was created with help of Behaviourtree : Dominion/1v1-2v2/Ascension...)
Tutorial (all tutorial scene, tips, ai, item was handled in behaviourtree)
Other (Plate of NewTT / some aram buffs...)

How does it work ? 
Behaviourtree is an XML file (we suppose created with behaviourtreecpp but edited by rito)\
These XML files allow the creation of a tree of decision, we believe at 95% the BehaviourTree was created with an tool like the Blueprints of Unreal engine too.

A BehaviourTree's function looks like:

```
<?xml version="1.0" encoding="utf-8"?>
<BehaviorTrees>
  <BehaviorTree Name="PushMission.FindFurthestUnitOrTurretInLane">
    <Parameters>
      <Parameter Name="EntitiesToSearch" VariableType="Reference" ParameterType="AttackableUnitCollection" Default="" Optional="false">
        <Description>Units that we want to search</Description>
```

these BehaviourTree weren't planned to be shipped with the Gameclient, as they're only used in the Server. 
you can find some mentions of them in the maps folder. For example in the Mission.ini file mentions this one ( for example map8 )

``` unkB69F8016="OdinScriptBehaviorTrees.xml" ```

and sometimes in a special file that were leaked in some versions: 
DATA\Levels\LevelScripts.sproject

```
 <Namespace QualifiedName="Layouts.OdinLayout">
      <Solutions>
        <Solution Name="OdinScript" SolutionType="Quest" />
      </Solutions>
      <BehaviorTrees>
        <BehaviorTree QualifiedName="Layouts.OdinLayout.BaseTeleporters" />
        <BehaviorTree QualifiedName="Layouts.OdinLayout.CapturePointManager" />
        <BehaviorTree QualifiedName="Layouts.OdinLayout.LevelPropAnimations" />
      </BehaviorTrees>
      <Models />
      <ModelInstances />
      <CollectionInstances />
      <DictionaryInstances />
      <EnumInstances />
      <FlagsInstances />
    </Namespace>
```

This helped us to find out how some featyres from the New Summoners Rift or New Twisted treeline were handled 
```
        <BehaviorTree QualifiedName="Layouts.S4Spawn.S4OpeningGame" />
        <BehaviorTree QualifiedName="Modes.SummonersRift.Buildings.B_EV_TurretDeath" />
        <Solution Name="CharacterQuests" SolutionType="Quest" />
        ...
        <BehaviorTree QualifiedName="Modes.Classic.TTOpeningCeremony" />
        <BehaviorTree QualifiedName="Modes.Classic.TTAltarParticleSupport" />
        ...
        <BehaviorTree QualifiedName="Modes.BattleTraining.MoveToLane" />
        ...
        <Namespace QualifiedName="Modes.NightmareBots">

```

Unfortunatelly none of those were ever leaked

BUT 

2 Versions leaked behaviourtrees

(0.0.0.0) has some behaviourtree , their first implementation of bots

(1.0.0.142) live version / (0.0.0.17 tmnt equivalent .124) etc ...
This version is really interesting , because approx 70/80% of behaviourtree was leaked 
but not only that, they give the manual of all function 

in the folder DATA\Summoner

they have let : SummonerBlockLibrary.xml 
Which has litterally the definition of all blocks 

```
  <Block Name="LessEqualUnsignedInt" Category="Test\Integer" SubCategory="" CanHaveChildren="false" NumberOfChildren="-1">
    <Description>Returns SUCCESS if LeftHandSide is less than or equal to RightHandSide, and FAILURE if it is not. This version is for Unsigned int References.</Description>
    <Parameters>
      <Parameter Name="LeftHandSide" Type="UnsignedInt" Default="0" VariableType="Reference">
        <Description>LeftHandSide Reference of the comparison</Description>
      </Parameter>
      <Parameter Name="RightHandSide" Type="UnsignedInt" Default ="0" VariableType="Reference,Value">
        <Description>RightHandSide Reference of the comparison</Description>
      </Parameter>
    </Parameters>
  </Block>
  ```

Riot has let in folder DATA\Summoner\Plugins
ALL library necessary for run these behaviourtree ( and some other dll like p4api )

We haven't used these DLLs, but with a tool like dotPeek, you could export it.
In a nutshell, the dll uses the LevelScripts.sproject to run the BehaviourTree(litterally like an Csproj)

But like Lua , we preferred to convert them to C#
We have "differency" Behaviourtree in function of their hierarchy class we found on xml and with help of their functionnality

All behaviourtree found in 0.0.0.0 were "alpha" behaviourtree, we used it to understand how they work.
These one were associated to map1, we just adapted some functionality to get it to work on map4 

All behaviourtree found in 1.0.0.142 in the folder DATA\Levels\Project_LevelScripts 
were added in our CScript folder associated to the correct map (not hard only two maps have behaviourtree mapscript)

Layout/Modes of odin(Dominion Gamemode) was in that folder , and contain ALL logic for
-Minion Squad 
-Turret 
-Stair (open Ceremony)
-Quest 
-Capture system 
-Point Manager ETC ... 

All entity who will spawn are foundable in an json who got leaked in 1.0.0.142 

LEVELS\Map8\Encounters
```
{"EncounterDefinition": {
"BonusValue": 12,
"SquadAI": "Guard",
"MonsterGroups":
[
	{
		"Monsters":
		[
			{
				"SkinName": "OdinSpeedShrine",
				"Value": 1,
				"Count": 1,
				"Targetable": 0
			}
		]
	}
]
}} 
```
this is the speedshrine with the squad name "Guard", and some variables that are used in BehaviourTree

All other behaviourtree present in : DATA\AI 
were added too in folder Cscript but can be called by all maps (if necessary)
it contains: 
AI_manager : an manager called when the game start , each team has their own aimanager, this is what gives missions to bots / Setup their difficulty / transmit messages

AI_Entity:  The behaviourtree associated with the entity like : 
         - Bot : Each bot has his own behaviourtree, they have their own "roles" / item to buy / be agressive or no, preference to level up some spell , calculation of risk etc 

         - Minion : For the moment we used BehaviourTree of minions only for Dominion, they are spawned like "squad" in behaviourtree dominion, they can have basic aitask 

         - Structure : Same as the minion we used it only in Dominion, is the BehaviourTree of the turrets

AI_mission: Mission given by the AIManager, when a bot is in aa squad (For example, squadpushbot) he got assigned a mission that is another behaviourtree he needs to follow

These mission and behaviourtree are initialized with help of a .dat file which leaked in 1.0.0.142 too
LEVELS\Map1\Scripts


```
#######################################################################################################
   [Team ID Name]              [ Manager project Name ]	[Tag in Project ]
#######################################################################################################

<CLASSIC> # GAME MODE
#_______________________________
{MATCHED_GAME} # GAME TYPE

  TEAM_ORDER                    AI_Manager		Manager
  TEAM_CHAOS                    AI_Manager		Manager

#_______________________________
{CUSTOM_GAME} # GAME TYPE
  TEAM_ORDER                    AI_Manager		CustomGameManager
  TEAM_CHAOS                    AI_Manager		CustomGameManager

#  TEAM_NEUTRAL                 AI_Manager		Manager

<TUTORIAL> # GAME MODE
#_______________________________
{TUTORIAL_GAME,CUSTOM_GAME,MATCHED_GAME} # GAME TYPE

  TEAM_ORDER                    AI_Manager		TutorialManager
  TEAM_CHAOS                    AI_Manager		TutorialManager

#######################################################################################################
```
As you can see here, Based on the gamemode, the aimanager is different, on our Gameserver we have only implemented the basic AImanager ({MATCHED_GAME})

But like we said Not all Behaviourtree have been leaked in this version,
like Some tasks (PushTaskBehaviorTrees.xml, DefendTaskBehaviorTrees.xml), or some 'value/function' that are necessary for the calculation of risk (Fuzzy_NoTower_NoCC.txt, Fuzzy_NoTower_HasCC.txt).

Regardless of that, we got 
-Alpha bot for Map4 
-Dominion working some bugs can happen (first the loading of the map is constrained)
-Bots from 1.0.0.142 work, but can be improved




