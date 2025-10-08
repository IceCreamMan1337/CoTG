using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Rammus_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();

     public bool Rammus_AttackTarget(
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
                  GetDistanceBetweenUnits(
                        out DistanceToTarget, 
                        Self, 
                        ToAttack) &&
                  // Sequence name :ChampMinionTower
                  (
                        // Sequence name :HarassChampion
                        (
                              TargetType == HERO_UNIT &&
                              GetUnitPARRatio(
                                    out SelfPAR_Ratio, 
                                    Self, 
                                     PrimaryAbilityResourceType.MANA) &&
                              GreaterFloat(
                                    SelfPAR_Ratio, 
                                    0.7f) &&
                              // Sequence name :CastE
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self, 
                                          2, 
                                          PreviousSpellCastTime, 
                                          CastSpellTimeThreshold, 
                                          PreviousSpellCastTarget, 
                                          PreviousSpellCastTarget, 
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
                                          0.9f, 
                                          PreviousSpellCast, 
                                          PreviousSpellCastTarget, 
                                          PreviousSpellCastTime, 
                                          CastSpellTimeThreshold, default
                                          , 
                                          false)
                              )
                        ) ||
                        // Sequence name :AttackTowerWithUlt
                        (
                              TargetType == TURRET_UNIT &&
                              // Sequence name :CountFriendlies
                              (
                                    GetUnitPosition(
                                          out SelfPosition, 
                                          Self) &&
                                    CountUnitsInTargetArea(
                                          out FriendliesCount, 
                                          Self, 
                                          SelfPosition, 
                                          800, 
                                          AffectFriends | AffectHeroes | AffectMinions | NotAffectSelf, 
                                          "")
                              ) &&
                              GreaterInt(
                                    FriendliesCount, 
                                    2) &&
                              LessFloat(
                                    DistanceToTarget, 
                                    300) &&
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
                              CastUnitSpell(
                                    Self, 
                                    SPELLBOOK_CHAMPION, 
                                    3, 
                                   default , default 
                                    )
                        ) ||
                        // Sequence name :PowerballNonBuildingUnit
                        (
                              NotEqualUnitType(
                                    TargetType, 
                                    TURRET_UNIT) &&
                              NotEqualUnitType(
                                    TargetType, 
                                    INHIBITOR_UNIT) &&
                              NotEqualUnitType(
                                    TargetType, 
                                    HQ_UNIT) &&
                              TestUnitHasBuff(
                                    Self, 
                                   default , 
                                    "default", 
                                    true) &&
                              ClearUnitAISpellPosition() &&
                              ComputeUnitAISpellPosition(
                                    ToAttack, 
                                    Self, 
                                    200, 
                                    false) &&
                              GetUnitAISpellPosition(
                                    out PowerballPosition) &&
                              ClearUnitAISpellPosition() &&
                              IssueMoveToPositionOrder(
                                    PowerballPosition)
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

