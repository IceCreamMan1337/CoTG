using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Malphite_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttack_MeleeClass autoAttack_Melee = new AutoAttack_MeleeClass();

     public bool Malphite_AttackTarget(
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
                                    0.6f) &&
                              // Sequence name :UseQ
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
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              )
                        ) ||
                        // Sequence name :HarassChampion
                        (
                              TargetType == HERO_UNIT &&
                              GetUnitPARRatio(
                                    out SelfPAR_Ratio,
                                    Self,
                                     PrimaryAbilityResourceType.MANA) &&
                              GreaterFloat(
                                    SelfPAR_Ratio,
                                    0.4f) &&
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
                        ) ||
                      autoAttack_Melee.AutoAttack_Melee(
                              out IssuedAttack,
                              out IssuedAttackTarget,
                              ToAttack,
                              Self,
                              IssuedAttack,
                              IssuedAttackTarget)

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

