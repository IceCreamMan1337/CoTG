using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class Cassiopeia_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    public bool Cassiopeia_AttackTarget(
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
                                                0.4f) &&
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

