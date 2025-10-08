using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Nidalee_HealClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();



    public bool Nidalee_Heal(
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

                        // Sequence name :NidaleeHeal

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
                              out NidaleePosition,
                              Self) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :CastE&lt,.3
                              (
                                    LessFloat(
                                          HP_Ratio,
                                          0.3f) &&
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "AspectOfTheCougar",
                                          false) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          2,
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
                                          2,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :HealFriendlies&lt,.3
                              (
                                    GetUnitPosition(
                                          out NidaleePosition,
                                          Self) &&
                                    GetUnitAILowestHPTargetInArea(
                                          out SelectedFriendlyTarget,
                                          Self, default
                                          ,
                                          true,
                                          NidaleePosition,
                                          1000,
                                          0.3f,
                                          false,
                                          true,
                                          AffectFriends | AffectHeroes | NotAffectSelf) &&
                                    GetUnitPosition(
                                          out SelectedFriendlyTargetPosition,
                                          SelectedFriendlyTarget) &&
                                                // Sequence name :Selector

                                                // Sequence name :CastE

                                                TestUnitHasBuff(
                                                      Self, default
                                                      ,
                                                      "AspectOfTheCougar",
                                                      false) &&
                                                canCastChampionAbilityClass.CanCastChampionAbility(
                                                      Self,
                                                      2,
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Self,
                                                      2,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold, default
                                                      ,
                                                      false)


                              ) ||
                              // Sequence name :CastE&lt,.7
                              (
                                    LessFloat(
                                          HP_Ratio,
                                          0.7f) &&
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "AspectOfTheCougar",
                                          false) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          2,
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
                                          2,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :HealFriendlies&lt,.7
                              (
                                    GetUnitPosition(
                                          out NidaleePosition,
                                          Self) &&
                                    GetUnitAILowestHPTargetInArea(
                                          out SelectedFriendlyTarget,
                                          Self, default
                                          ,
                                          true,
                                          NidaleePosition,
                                          1000,
                                          0.7f,
                                          false,
                                          true,
                                          AffectFriends | AffectHeroes | NotAffectSelf) &&
                                    GetUnitPosition(
                                          out SelectedFriendlyTargetPosition,
                                          SelectedFriendlyTarget) &&
                                                // Sequence name :Selector

                                                // Sequence name :CastE

                                                TestUnitHasBuff(
                                                      Self, default
                                                      ,
                                                      "AspectOfTheCougar",
                                                      false) &&
                                                canCastChampionAbilityClass.CanCastChampionAbility(
                                                      Self,
                                                      2,
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Self,
                                                      2,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold, default
                                                      ,
                                                      false)


                              ) ||
                              // Sequence name :IsCougarAndFarAway
                              (
                                    LessFloat(
                                          HP_Ratio,
                                          0.6f) &&
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "AspectOfTheCougar",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out CurrentClosestTarget,
                                          Self, default
                                          ,
                                          true,
                                          NidaleePosition,
                                          800,
                                          AffectEnemies | AffectHeroes) &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          CurrentClosestTarget,
                                          Self) &&
                                    GreaterEqualFloat(
                                          DistanceToTarget,
                                          400) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          2,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          false) &&
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

