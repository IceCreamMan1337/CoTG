using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Renekton_HighThreatManagementClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();

     public bool Renekton_HighThreatManagement(
         out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      Vector3 SelfPosition,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      int ExhaustSlot,
      int GhostSlot
         )
      {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
            // Sequence name :UseSpells
            (
                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :CastQ
                        (
                              // Sequence name :Fury&gt,50
                              (
                                    GetUnitCurrentPAR(
                                          out CurrentFury,
                                          Self,
                                          PrimaryAbilityResourceType.Other) &&
                                    GreaterEqualFloat(
                                          CurrentFury,
                                          50) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          true) &&
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :N&gt,=2
                              (
                                    CountUnitsInTargetArea(
                                          out UnitsNearby,
                                          Self,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes | AffectMinions | AffectNeutral,
                                          "") &&
                                    GreaterEqualInt(
                                          UnitsNearby,
                                          2) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          true) &&
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              )
                        ) ||
                        // Sequence name :Cast W
                        (
                              // Sequence name :Attack
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "RenektonPreExecute",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    SetUnitAIAttackTarget(
                                          NearestEnemyToAttack) &&
                                    IssueChaseOrder()
                              ) ||
                              // Sequence name :Cast
                              (
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          1,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          true) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Self,
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              )
                        ) ||
                        // Sequence name :Slice
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.4f) &&
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    450,
                                    AffectEnemies | AffectHeroes) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              GetUnitAIBasePosition(
                                    out BasePosition,
                                    Self) &&
                              DistanceBetweenObjectAndPoint(
                                    out DistanceSelfToBase,
                                    Self,
                                    BasePosition) &&
                              DistanceBetweenObjectAndPoint(
                                    out DistanceTargetToBase,
                                    CurrentClosestTarget,
                                    BasePosition) &&
                              LessFloat(
                                    DistanceSelfToBase,
                                    DistanceTargetToBase) &&
                              ClearUnitAISpellPosition() &&
                              ComputeUnitAISpellPosition(
                                    Self,
                                    CurrentClosestTarget,
                                    800,
                                    false) &&
                              GetUnitAISpellPosition(
                                    out RocketJumpPosition) &&
                              SetAIUnitSpellTargetLocation(
                                    RocketJumpPosition,
                                    SPELLBOOK_CHAMPION,
                                    1) &&
                              ClearUnitAISpellPosition() &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out PreviousSpellCastTime,
                                    Self,
                                    CurrentClosestTarget,
                                    2,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    RocketJumpPosition,
                                    true)
                        ) ||
                        // Sequence name :RetreatAtLowHP
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.2f) &&
                           summonerGhost.SummonerGhost(
                                    Self,
                                    1)

                        )
                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
       
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
       
        return result;



    }
}

