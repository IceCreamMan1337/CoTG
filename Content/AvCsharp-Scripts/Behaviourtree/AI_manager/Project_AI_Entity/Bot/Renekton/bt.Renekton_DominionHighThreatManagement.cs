using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Renekton_DominionHighThreatManagementClass : AI_Characters 
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();
    private IsInFrontClass isInFrontClass = new IsInFrontClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();

    public bool Renekton_DominionHighThreatManagement(
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
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
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
                                          Self, default,
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
                           //isTargetInFront.   IsTargetInFront(
                           isInFrontClass.IsInFront(
                                    out IsTargetInFront,
                                    Self,
                                    CurrentClosestTarget) &&
                              IsTargetInFront == false &&
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
                              ClearUnitAISpellPosition() &&
                              ComputeUnitAISpellPosition(
                                    Self,
                                    CurrentClosestTarget,
                                    600,
                                    false) &&
                              GetUnitAISpellPosition(
                                    out TauntPosition) &&
                              SetAIUnitSpellTargetLocation(
                                    TauntPosition,
                                    SPELLBOOK_CHAMPION,
                                    2) &&
                              ClearUnitAISpellPosition() &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out PreviousSpellCastTime,
                                    Self,
                                    CurrentClosestTarget,
                                    2,
                                    0.8f,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    TauntPosition,
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

