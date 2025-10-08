using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Jax_DominionAttackMinionClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
 

     public bool Jax_DominionAttackMinion(
         out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out float __CastSpellTimeThreshold,
      AttackableUnit Self,
      AttackableUnit Target,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime
         )
    {

        int CurrentSpellCast = default;
      AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
       float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        bool result =
            // Sequence name :SpellKillMinion
            (
                  GetUnitHealthRatio(
                        out TargetHealthRatio, 
                        Target) &&
                  LessFloat(
                        TargetHealthRatio, 
                        0.3f) &&
                  canCastChampionAbilityClass.CanCastChampionAbility(
                        Self, 
                        1, 
                        PreviousSpellCastTime, 
                        CastSpellTimeThreshold, 
                        PreviousSpellCastTarget, 
                        Self, 
                        PreviousSpellCast, 
                        true, 
                        false) &&
                 castTargetAbility.CastTargetAbility(
                        out CurrentSpellCast, 
                        out CurrentSpellCastTarget, 
                        out PreviousSpellCastTime, 
                        out CastSpellTimeThreshold, 
                        Self, 
                        Self, 
                        1, 
                        0.9f, 
                                        PreviousSpellCast,
                PreviousSpellCastTarget,
                PreviousSpellCastTime,
                CastSpellTimeThreshold,
                default,
                false)

    );
 _CurrentSpellCast = CurrentSpellCast;
 _CurrentSpellCastTarget = CurrentSpellCastTarget;
 __PreviousSpellCastTime = _PreviousSpellCastTime;
 __CastSpellTimeThreshold = _CastSpellTimeThreshold;

return result;
      }
}

