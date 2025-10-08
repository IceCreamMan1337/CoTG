using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Renekton_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();

     public bool Renekton_AttackTarget(
        out bool __IssuedAttack,
      out AttackableUnit __IssuedAttackTarget,
      out int CurrentSpellCast,
      out AttackableUnit CurrentSpellCastTarget,
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
        int _CurrentSpellCast = default;
        AttackableUnit _CurrentSpellCastTarget = default;
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
                              GetUnitHealthRatio(
                                    out TargetHealthRatio, 
                                    ToAttack) &&
                              LessFloat(
                                    TargetHealthRatio, 
                                    0.3f) &&
                              GetUnitPARType(
                                    out RenektonPARType, 
                                    Self) &&
                              GetUnitPARRatio(
                                    out SelfPAR_Ratio, 
                                    Self, 
                                    RenektonPARType) &&
                              LessFloat(
                                    SelfPAR_Ratio, 
                                    0.5f) &&
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
                                    CastSpellTimeThreshold, 
                                   default , 
                                    false)
                        ) ||
                        // Sequence name :HarassChampion
                        (
                              TargetType == HERO_UNIT &&
                              // Sequence name :CastSpells
                              (
                                    // Sequence name :CastE
                                    (
                                          GetDistanceBetweenUnits(
                                                out DistanceToTarget, 
                                                ToAttack, 
                                                Self) &&
                                          LessFloat(
                                                DistanceToTarget, 
                                                300) &&
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
                                                0.9f, 
                                                PreviousSpellCast, 
                                                PreviousSpellCastTarget, 
                                                PreviousSpellCastTime, 
                                                CastSpellTimeThreshold, default
                                                , 
                                                false)
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
                         autoAttack.AutoAttack(
                                out _IssuedAttack,
                                out _IssuedAttackTarget,
                                ToAttack,
                                Self,
                                IssuedAttack,
                                IssuedAttackTarget)

                    )
              );
        CurrentSpellCast = _CurrentSpellCast;
        CurrentSpellCastTarget = _CurrentSpellCastTarget;
        __IssuedAttack = _IssuedAttack;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;
      }
}

