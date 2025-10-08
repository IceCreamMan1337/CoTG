using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Fiddlesticks_AttackTargetClass : AI_Characters 
{
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();
    public bool Fiddlesticks_AttackTarget(
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
            (
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
                        // Sequence name :KillMinion
                        (
                              TargetType == MINION_UNIT &&
                              // Sequence name :CastSpellsOrLastHit
                              (
                                    // Sequence name :Spells
                                    (
                                          GetUnitHealthRatio(
                                                out TargetHealthRatio,
                                                ToAttack) &&
                                          // Sequence name :CastSpells
                                          (
                                                // Sequence name :CastW
                                                (
                                                      GreaterFloat(
                                                            TargetHealthRatio,
                                                            0.7f) &&
                                                      GetUnitHealthRatio(
                                                            out SelfHealthRatio,
                                                            Self) &&
                                                      LessFloat(
                                                            SelfHealthRatio,
                                                            0.4f) &&
                                                      canCastChampionAbilityClass.CanCastChampionAbility(
                                                            Self,
                                                            1,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold,
                                                            PreviousSpellCastTarget,
                                                            ToAttack,
                                                            PreviousSpellCast,
                                                            false,
                                                            false) &&
                                                     castTargetAbility.CastTargetAbility(
                                                            out CurrentSpellCast,
                                                            out CurrentSpellCastTarget,
                                                            out PreviousSpellCastTime,
                                                            out CastSpellTimeThreshold,
                                                            Self,
                                                            ToAttack,
                                                            1,
                                                            1,
                                                            PreviousSpellCast,
                                                            PreviousSpellCastTarget,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold, default
                                                            ,
                                                            false) &&
                                                      SetVarBool(
                                                            out SpellStall,
                                                            true)
                                                ) ||
                                                // Sequence name :CastE
                                                (
                                                      LessFloat(
                                                            TargetHealthRatio,
                                                            0.3f) &&
                                                      GetUnitPARRatio(
                                                            out SelfPAR_Ratio,
                                                            Self,
                                                             PrimaryAbilityResourceType.MANA) &&
                                                      GreaterFloat(
                                                            SelfPAR_Ratio,
                                                            0.5f) &&
                                                      canCastChampionAbilityClass.CanCastChampionAbility(
                                                            Self,
                                                            2,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold,
                                                            PreviousSpellCastTarget,
                                                            ToAttack,
                                                            PreviousSpellCast,
                                                            false,
                                                            false) &&
                                                     castTargetAbility.CastTargetAbility(
                                                            out CurrentSpellCast,
                                                            out CurrentSpellCastTarget,
                                                            out PreviousSpellCastTime,
                                                            out CastSpellTimeThreshold,
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
                                          )
                                    ) ||
                                 lastHit.LastHit(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          ToAttack,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget,
                                          0)
                              )
                        ) ||
                        // Sequence name :HarassChampion
                        (
                              TargetType == HERO_UNIT &&
                              // Sequence name :CastSpellsOrAttack
                              (
                                    // Sequence name :Spells
                                    (
                                          GetUnitPARRatio(
                                                out SelfPAR_Ratio,
                                                Self,
                                                 PrimaryAbilityResourceType.MANA) &&
                                          GreaterFloat(
                                                SelfPAR_Ratio,
                                                0.5f) &&
                                          // Sequence name :CastSpells
                                          (
                                                // Sequence name :CastW
                                                (
                                                      GetDistanceBetweenUnits(
                                                            out DistanceToTarget,
                                                            Self,
                                                            ToAttack) &&
                                                      LessFloat(
                                                            DistanceToTarget,
                                                            400) &&
                                                      // Sequence name :CheckForEnemyTower
                                                      (
                                                            GetUnitPosition(
                                                                  out DrainTowerCheckPosition,
                                                                  Self) &&
                                                            CountUnitsInTargetArea(
                                                                  out DrainTowerCheckCount,
                                                                  Self,
                                                                  DrainTowerCheckPosition,
                                                                  900,
                                                                  AffectEnemies | AffectTurrets,
                                                                  "") &&
                                                            DrainTowerCheckCount == 0
                                                      ) &&
                                                      canCastChampionAbilityClass.CanCastChampionAbility(
                                                            Self,
                                                            1,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold,
                                                            PreviousSpellCastTarget,
                                                            ToAttack,
                                                            PreviousSpellCast,
                                                            false,
                                                            false) &&
                                                     castTargetAbility.CastTargetAbility(
                                                            out CurrentSpellCast,
                                                            out CurrentSpellCastTarget,
                                                            out PreviousSpellCastTime,
                                                            out CastSpellTimeThreshold,
                                                            Self,
                                                            ToAttack,
                                                            1,
                                                            0.9f,
                                                            PreviousSpellCast,
                                                            PreviousSpellCastTarget,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold, default
                                                            ,
                                                            false) &&
                                                      SetVarBool(
                                                            out SpellStall,
                                                            true)
                                                ) ||
                                                // Sequence name :CastE
                                                (
                                                      canCastChampionAbilityClass.CanCastChampionAbility(
                                                            Self,
                                                            2,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold,
                                                            PreviousSpellCastTarget,
                                                            ToAttack,
                                                            PreviousSpellCast,
                                                            false,
                                                            false) &&
                                                     castTargetAbility.CastTargetAbility(
                                                            out CurrentSpellCast,
                                                            out CurrentSpellCastTarget,
                                                            out PreviousSpellCastTime,
                                                            out CastSpellTimeThreshold,
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
                                          )
                                    ) ||
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          ToAttack,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              )
                        ) ||
                        // Sequence name :NotAMinion
                        (
                              NotEqualUnitType(
                                    TargetType,
                                    MINION_UNIT) &&
                               autoAttack.AutoAttack(
                                    out IssuedAttack,
                                    out IssuedAttackTarget,
                                    ToAttack,
                                    Self,
                                    IssuedAttack,
                                    IssuedAttackTarget)

                        )
                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __IssuedAttackTarget = _IssuedAttackTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttack = _IssuedAttack;
        return result;


    }
}

