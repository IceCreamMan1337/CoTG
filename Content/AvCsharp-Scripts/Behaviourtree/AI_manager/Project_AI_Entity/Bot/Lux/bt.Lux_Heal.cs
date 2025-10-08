using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Lux_HealClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();


     public bool Lux_Heal(
          out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime
         )
    {
        int CurrentSpellCast = default;
       AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;

        bool result =
            // Sequence name :ShieldFriendlies
            (
                  GetUnitPosition(
                        out LuxPosition,
                        Self) &&
                  GetUnitAILowestHPTargetInArea(
                        out SelectedFriendlyTarget,
                        Self, default
                        ,
                        true,
                        LuxPosition,
                        1000,
                        0.6f,
                        false,
                        true,
                        AffectFriends | AffectHeroes | NotAffectSelf) &&
                  GetUnitPosition(
                        out TargetPosition,
                        SelectedFriendlyTarget) &&
                  GetUnitHealthRatio(
                        out HP_Ratio,
                        SelectedFriendlyTarget) &&
                  LessFloat(
                        HP_Ratio,
                        0.2f) &&
                  // Sequence name :Shield
                  (
                        canCastChampionAbilityClass.CanCastChampionAbility(
                              Self,
                              1,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold,
                              PreviousSpellCastTarget,
                              SelectedFriendlyTarget,
                              PreviousSpellCast,
                              false,
                              false) &&
                        PredictLineMissileCastPosition(
                              out SkillShotCastPosition,
                              SelectedFriendlyTarget,
                              LuxPosition,
                              1300,
                              0.25f) &&
                        GetRandomPositionBetweenTwoPoints(
                              out SkillShotCastPosition,
                              TargetPosition,
                              SkillShotCastPosition, default
                              ) &&
                       castTargetAbility.CastTargetAbility(
                              out CurrentSpellCast,
                              out CurrentSpellCastTarget,
                              out PreviousSpellCastTime,
                              out CastSpellTimeThreshold,
                              Self,
                              SelectedFriendlyTarget,
                              1,
                              1,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold, default
                              ,
                              false)

                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
      
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
       
        return result;

    }
}

