using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Lux_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();

    public bool Lux_AttackTarget(
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
                        // Sequence name :SpellKillMinion
                        (
                              TargetType == MINION_UNIT &&
                              // Sequence name :CastSpellsOrLastHit
                              (
                                    // Sequence name :Spells
                                    (
                                          // Sequence name :CastE
                                          (
                                                // Sequence name :CastProjectile
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "LuxLightstrikeKugel",
                                                            false) &&
                                                      GetUnitHealthRatio(
                                                            out TargetHealthRatio,
                                                            ToAttack) &&
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
                                                ) ||
                                                // Sequence name :Detonate
                                                (
                                                      TestUnitHasBuff(
                                                            Self, default
                                                            ,
                                                            "LuxLightstrikeKugel",
                                                            true) &&
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
                              // Sequence name :CastSpellsOrAutoAttack
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
                                                            false,
                                                            false) &&
                                                      TestLineMissilePathIsClear(
                                                            Self,
                                                            ToAttack,
                                                            50,
                                                            AffectEnemies | AffectHeroes | AffectMinions) &&
                                                     castTargetAbility.CastTargetAbility(
                                                            out CurrentSpellCast,
                                                            out CurrentSpellCastTarget,
                                                            out PreviousSpellCastTime,
                                                            out CastSpellTimeThreshold,
                                                            Self,
                                                            ToAttack,
                                                            0,
                                                            1,
                                                            PreviousSpellCast,
                                                            PreviousSpellCastTarget,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold, default
                                                            ,
                                                            false)
                                                ) ||
                                                // Sequence name :CastE
                                                (
                                                      // Sequence name :CastProjectile
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
                                                                  CastSpellTimeThreshold,default
                                                                  ,
                                                                  false)
                                                      ) ||
                                                      // Sequence name :Detonate
                                                      (
                                                            TestUnitHasBuff(
                                                                  Self, default
                                                                  ,
                                                                  "LuxLightstrikeKugel",
                                                                  true) &&
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
                                                      )
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

