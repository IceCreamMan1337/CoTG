using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Alistar_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    public bool Alistar_AttackTarget(
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
                                GetUnitPARRatio(
                                      out SelfPAR_Ratio,
                                      Self,
                                     PrimaryAbilityResourceType.MANA) &&
                                GreaterFloat(
                                      SelfPAR_Ratio,
                                      0.6f) &&
                                GetUnitLevel(
                                      out UnitLevel,
                                      Self) &&
                                GreaterInt(
                                      UnitLevel,
                                      9) &&
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
                                      out _CurrentSpellCast,
                                      out _CurrentSpellCastTarget,
                                      out _PreviousSpellCastTime,
                                      out _CastSpellTimeThreshold,
                                      Self,
                                      ToAttack,
                                      0,
                                      1,
                                      PreviousSpellCast,
                                      PreviousSpellCastTarget,
                                      PreviousSpellCastTime,
                                      CastSpellTimeThreshold,
                                      default,//be aware seem an vector3
                                      false)
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
                                            castTargetAbility.CastTargetAbility(
                                                  out _CurrentSpellCast,
                                                  out _CurrentSpellCastTarget,
                                                  out _PreviousSpellCastTime,
                                                  out _CastSpellTimeThreshold,
                                                  Self,
                                                  ToAttack,
                                                  0,
                                                  1,
                                                  PreviousSpellCast,
                                                  PreviousSpellCastTarget,
                                                  PreviousSpellCastTime,
                                                  CastSpellTimeThreshold,
                                                    default //be aware seem an vector3
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

