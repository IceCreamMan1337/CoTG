using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class Garen_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttack_MeleeClass autoAttack_Melee = new();

    public bool Garen_AttackTarget(
       out bool __IssuedAttack,
     out AttackableUnit __IssuedAttackTarget,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out float __CastSpellTimeThreshold,
     out float __PreviousSpellCastTime,
   AttackableUnit ToAttack,
   AttackableUnit Self,
   bool IssuedAttack,
   AttackableUnit IssuedAttackTarget,
   int PreviousSpellCast,
   AttackableUnit PreviousSpellCastTarget,
   float CastSpellTimeThreshold,
   float PreviousSpellCastTime
       )
    {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;


        bool result =
                  // Sequence name :AttackTarget

                  GetUnitTeam(
                        out TargetTeam,
                        ToAttack) &&
                  GetUnitTeam(
                        out SelfTeam,
                        Self) &&
                  NotEqualUnitTeam(
                        TargetTeam,
                        SelfTeam) &&
                  GetUnitType(
                        out TargetType,
                        ToAttack) &&
                  // Sequence name :ChampMinion
                  (
                        // Sequence name :SpellKillMinion
                        (
                              TargetType == MINION_UNIT &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    ToAttack) &&
                              LessFloat(
                                    TargetHealthRatio,
                                    0.3f) &&
                              // Sequence name :CastSpells
                              (
                                          // Sequence name :Bladestorm

                                          // Sequence name :BladestormOn
                                          (
                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
                                                      "GarenBladestorm",
                                                      true) &&
                                             autoAttack_Melee.AutoAttack_Melee(
                                                      out IssuedAttack,
                                                      out IssuedAttackTarget,
                                                      ToAttack,
                                                      Self,
                                                      IssuedAttack,
                                                      IssuedAttackTarget)
                                          ) ||
                                          // Sequence name :CastBladestorm
                                          (
                                                canCastChampionAbilityClass.CanCastChampionAbility(
                                                      Self,
                                                      2,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      PreviousSpellCastTarget,
                                                      ToAttack,
                                                      PreviousSpellCast,
                                                      true,
                                                      false) &&
                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
                                                      "GarenBladestorm",
                                                      false) &&
                                                GetUnitPosition(
                                                      out SelfPosition,
                                                      Self) &&
                                                CountUnitsInTargetArea(
                                                      out NumEnemyChamps,
                                                      Self,
                                                      SelfPosition,
                                                      1500,
                                                      AffectEnemies | AffectHeroes,
                                                      "") &&
                                                NumEnemyChamps == 0 &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      ToAttack,
                                                      2,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold, default
                                                      ,
                                                      false)
                                          )
                                     ||
                                    // Sequence name :CastQ
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                0,
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
                                                0,
                                                0.9f,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    )
                              )
                        ) ||
                        // Sequence name :HarassChampion
                        (
                              TargetType == HERO_UNIT &&
                              // Sequence name :CastSpells
                              (
                                    // Sequence name :GarenBladestorm
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "GarenBladestorm",
                                                true) &&
                                   autoAttack_Melee.AutoAttack_Melee(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                ToAttack,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)
                                    ) ||
                                    // Sequence name :CastQ
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                0,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                ToAttack,
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
                                                0,
                                                0.9f,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    )
                              )
                        ) ||
                  autoAttack_Melee.AutoAttack_Melee(
                              out IssuedAttack,
                              out IssuedAttackTarget,
                              ToAttack,
                              Self,
                              IssuedAttack,
                              IssuedAttackTarget)

                  )
            ;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __IssuedAttackTarget = _IssuedAttackTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttack = _IssuedAttack;
        return result;


    }
}

