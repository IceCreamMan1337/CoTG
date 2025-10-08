using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Zilean_HealClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();

      

     public bool Zilean_Heal(
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
                  // Sequence name :HealFriendlies
                  (
                        GetUnitPosition(
                              out ZileanPosition,
                              Self) &&
                        GetUnitAILowestHPTargetInArea(
                              out SelectedFriendlyTarget,
                              Self, default
                              ,
                              true,
                              ZileanPosition,
                              1000,
                              0.6f,
                              false,
                              true,
                              AffectFriends | AffectHeroes | NotAffectSelf) &&
                        GetUnitPosition(
                              out SelectedFriendlyTargetPosition,
                              SelectedFriendlyTarget) &&
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
                        // Sequence name :Selector
                        (
                              // Sequence name :CastEOnAlly
                              (
                                    LessFloat(
                                          HP_Ratio,
                                          0.4f) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelectedFriendlyTargetPosition,
                                          550,
                                          AffectEnemies | AffectHeroes) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          2,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          SelectedFriendlyTarget,
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
                                          2,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastEOnEnemy
                              (
                                    TestUnitHasBuff(
                                          SelectedFriendlyTarget, default
                                          ,
                                          "TimeWarp",
                                          true) &&
                                    LessFloat(
                                          HP_Ratio,
                                          0.4f) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelectedFriendlyTargetPosition,
                                          550,
                                          AffectEnemies | AffectHeroes) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          2,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          NearestEnemyToAttack,
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
                                          2,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastW
                              (
                                    LessFloat(
                                          HP_Ratio,
                                          0.4f) &&
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastR
                              (
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelectedFriendlyTargetPosition,
                                          550,
                                          AffectEnemies | AffectHeroes) &&
                                    LessFloat(
                                          HP_Ratio,
                                          0.2f) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          3,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          SelectedFriendlyTarget,
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
                                          CastSpellTimeThreshold, default
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

