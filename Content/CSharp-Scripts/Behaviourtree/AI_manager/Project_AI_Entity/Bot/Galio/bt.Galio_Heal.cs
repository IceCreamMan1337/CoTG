using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Galio_HealClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();



    public bool Galio_Heal(
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
        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        bool result =
                        // Sequence name :Selector

                        // Sequence name :HealFriendlies

                        GetUnitPosition(
                              out GalioPosition,
                              Self) &&
                        GetUnitAILowestHPTargetInArea(
                              out SelectedFriendlyTarget,
                              Self,
                              default,
                              true,
                              GalioPosition,
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
                              // Sequence name :CastWOnAlly
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
                                          1,
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
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastQOnEnemy
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
                                          0,
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
                                          0,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)

                              )
                        )

            ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;

    }
}

