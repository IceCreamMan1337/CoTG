In League of Legends , all champions use lua scripts
These lua scripts use their Building Blocks (BB) 

To avoid repetition in code, and for uniformisation they chose this format which was a cool idea 

The BB functions can be see like legos,
you just need assemble the blocks to create your Spell 

We assume for the creation of league, BB was designed to be used with the help of a tool, like Unreal Engine's Blueprint 
But BB uses Lua Tables, we have reproduced an approximate behaviour with our Convertedscript 
Library\Scripting\Lua\ 
you can find our reproduction of the buidingblock for our convertedScript 

With this we respect how the script work, keeping close to their original, but adapting it better for C#

``` 
    Function = BBSpellEffectCreate,
    Params = {
      BindObjectVar = "Owner",
      EffectName = "CassMiasma_tar_green.troy",
      EffectNameForOtherTeam = "CassMiasma_tar_red.troy",
      Flags = 0,
      EffectIDVar = "Particle2",
      EffectIDVarTable = "InstanceVars",
      EffectID2Var = "Particle",
      EffectID2VarTable = "InstanceVars",
      TargetObjectVar = "Target",
      SpecificUnitOnlyVar = "Nothing",
      SpecificTeamOnly = TEAM_UNKNOWN,
      UseSpecificUnit = false,
      FOWTeam = TEAM_UNKNOWN,
      FOWTeamOverrideVar = "TeamOfOwner",
      FOWVisibilityRadius = 10,
      SendIfOnScreenOrDiscard = false,
      PersistsThroughReconnect = false,
      BindFlexToOwnerPAR = false,
      FollowsGroundTilt = false,
      FacesTarget = false
    }
```

Becomes the following

```
SpellEffectCreate(out particle2, out particle, "CassMiasma_tar_green.troy", "CassMiasma_tar_red.troy", teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
```

Approximatelly 98% of the BB is reversed for .126

There exists multiple types of BB Lua Scripts: 

OnBuffActivateBuildingBlocks 
OnBuffDeactivateBuildingBlocks
BuffOnUpdateActionsBuildingBlocks
SpellOnMissileEndBuildingBlocks
...

First we have separated them into Character/Spell/Buff/Item Scripts .. 

For example.

The sections of a Script that contains `OnBuffActivateBuildingBlocks` will be written inside a `BuffScript`
Or, if a Script contains `SpellOnMissileEndBuildingBlocks` then that will go to a `SpellScript`

OnBuffActivateBuildingBlocks becomes
Onactivate function in a `BuffScript`

SpellOnMissileEndBuildingBlocks becomes
OnMissileEnd function in a `SpellScript`