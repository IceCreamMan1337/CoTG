using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class Nidalee_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    public bool Nidalee_AttackTarget(
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
                              // Sequence name :CastSpellsOrLastHit
                              (
                                    // Sequence name :Spells
                                    (
                                          GetUnitHealthRatio(
                                                out TargetHealthRatio,
                                                ToAttack) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.2f) &&
                                          // Sequence name :CastSpells
                                          (
                                                // Sequence name :TurnIntoCougarIfNoNearbyEnemyChampions
                                                (
                                                      GetUnitPosition(
                                                            out SelfPosition,
                                                            Self) &&
                                                      CountUnitsInTargetArea(
                                                            out CollectionCount,
                                                            Self,
                                                            SelfPosition,
                                                            1000,
                                                            AffectEnemies | AffectHeroes,
                                                            "") &&
                                                      // Sequence name :Selector
                                                      (
                                                            // Sequence name :Sequence
                                                            (
                                                                  CollectionCount == 0 &&
                                                                  TestUnitHasBuff(
                                                                        Self,
                                                                        default,
                                                                        "AspectOfTheCougar",
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
                                                                        CastSpellTimeThreshold,
                                                                        default,
                                                                        false)
                                                            ) ||
                                                            // Sequence name :Sequence
                                                            (
                                                                  GreaterInt(
                                                                        CollectionCount,
                                                                        0) &&
                                                                  TestUnitHasBuff(
                                                                        Self,
                                                                        default,
                                                                        "AspectOfTheCougar",
                                                                        true) &&
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
                                                                        CastSpellTimeThreshold,
                                                                       default,
                                                                        false)
                                                            )
                                                      )
                                                ) ||
                                                // Sequence name :Cast Q
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            true) &&
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
                                                            Self,
                                                            0,
                                                            1,
                                                            PreviousSpellCast,
                                                            PreviousSpellCastTarget,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold,
                                                            default,
                                                            false)
                                                ) ||
                                                // Sequence name :Cast E
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            true) &&
                                                      GetDistanceBetweenUnits(
                                                            out DistanceToMinion,
                                                            ToAttack,
                                                            Self) &&
                                                      LessEqualFloat(
                                                            DistanceToMinion,
                                                            225) &&
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
                                                            CastSpellTimeThreshold,
                                                           default,
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
                                    // Sequence name :CastSpells
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
                                                // Sequence name :TurnIntoHuman
                                                (
                                                      GetUnitHealthRatio(
                                                            out TargetHealthRatio,
                                                            ToAttack) &&
                                                      GetDistanceBetweenUnits(
                                                            out DistanceToTarget,
                                                            ToAttack,
                                                            Self) &&
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            true) &&
                                                      // Sequence name :HumanConditions
                                                      (
                                                            GreaterFloat(
                                                                  DistanceToTarget,
                                                                  325) ||
                                                                  GreaterFloat(
                                                                  TargetHealthRatio,
                                                                  0.5f)
                                                      ) &&
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
                                                            CastSpellTimeThreshold,
                                                            default,
                                                            false)
                                                ) ||
                                                // Sequence name :CastQ
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            false) &&
                                                      TestLineMissilePathIsClear(
                                                            Self,
                                                            ToAttack,
                                                            60,
                                                            AffectEnemies | AffectHeroes | AffectMinions) &&
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
                                                            ToAttack,
                                                            0,
                                                            0.9f,
                                                            PreviousSpellCast,
                                                            PreviousSpellCastTarget,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold,
                                                            default,
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

