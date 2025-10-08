using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Kayle_HealClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();



     public bool Kayle_Heal(
          out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
            bool IssuedAttack,
      AttackableUnit IssuedAttackTarget
      )
    {
        int CurrentSpellCast = default;
       AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;

        bool result =
            // Sequence name :Selector
            (
                  // Sequence name :KayleHeal
                  (
                        GetUnitCurrentHealth(
                              out Health,
                              Self) &&
                        GetUnitMaxHealth(
                              out MaxHealth,
                              Self) &&
                        DivideFloat(
                              out HP_Ratio,
                              Health,
                              MaxHealth) &&
                        LessFloat(
                              HP_Ratio,
                              0.55f) &&
                        GetUnitPosition(
                              out KaylePosition,
                              Self) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :CastW
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          1,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          false) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Self,
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastR
                              (
                                    GetUnitCurrentHealth(
                                          out Health,
                                          Self) &&
                                    GetUnitMaxHealth(
                                          out MaxHealth,
                                          Self) &&
                                    DivideFloat(
                                          out HP_Ratio,
                                          Health,
                                          MaxHealth) &&
                                    LessFloat(
                                          HP_Ratio,
                                          0.3f) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          3,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          false) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Self,
                                          3,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,default
                                          ,
                                          false)
                              )
                        )
                  ) ||
                  // Sequence name :HealFriendlies
                  (
                        GetUnitPosition(
                              out KaylePosition,
                              Self) &&
                        GetUnitAILowestHPTargetInArea(
                              out SelectedFriendlyTarget,
                              Self,default
                              ,
                              true,
                              KaylePosition,
                              1000,
                              0.6f,
                              false,
                              true,
                              AffectFriends | AffectHeroes | NotAffectSelf) &&
                        GetUnitPosition(
                              out SelectedFriendlyTargetPosition,
                              SelectedFriendlyTarget) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :CastW
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          1,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          false) &&
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
                                          CastSpellTimeThreshold,default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastQ
                              (
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self,default
                                          ,
                                          true,
                                          SelectedFriendlyTargetPosition,
                                          400,
                                          AffectEnemies | AffectHeroes) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          false) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          NearestEnemyToAttack,
                                          0,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastR
                              (
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self,default
                                          ,
                                          true,
                                          SelectedFriendlyTargetPosition,
                                          450,
                                          AffectEnemies | AffectHeroes) &&
                                    GetUnitCurrentHealth(
                                          out Health,
                                          SelectedFriendlyTarget) &&
                                    GetUnitMaxHealth(
                                          out MaxHealth,
                                          SelectedFriendlyTarget) &&
                                    DivideFloat(
                                          out HP_Ratio,
                                          Health,
                                          MaxHealth) &&
                                    LessFloat(
                                          HP_Ratio,
                                          0.25f) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          3,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          false) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          SelectedFriendlyTarget,
                                          3,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,default
                                          ,
                                          false)

                              )
                        )
                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
      
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
  
        return result;
    }
}

