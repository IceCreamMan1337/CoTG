using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class MasterYi_HealClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();


     public bool MasterYi_Heal(
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
            // Sequence name :Selector
            (
                  // Sequence name :MasterYiHeal
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
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        CountUnitsInTargetArea(
                              out EnemyCount,
                              Self,
                              SelfPosition,
                              1100,
                              AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                        CountUnitsInTargetArea(
                              out FriendlyCount,
                              Self,
                              SelfPosition,
                              650,
                              AffectFriends | AffectHeroes | AffectTurrets | NotAffectSelf,
                              "") &&
                        // Sequence name :CastW
                        (
                              // Sequence name :CastWIf&lt,30%AndNearAllies
                              (
                                    LessFloat(
                                          HP_Ratio,
                                          0.3f) &&
                                    GreaterEqualInt(
                                          FriendlyCount,
                                          1) &&
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastWIfHP&lt,70%AndMana&gt,70%
                              (
                                    LessFloat(
                                          HP_Ratio,
                                          0.7f) &&
                                    LessEqualInt(
                                          EnemyCount,
                                          0) &&
                                    GetUnitPARRatio(
                                          out SelfPARRatio,
                                          Self,
                                           PrimaryAbilityResourceType.MANA) &&
                                    GreaterFloat(
                                          SelfPARRatio,
                                          0.7f) &&
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

