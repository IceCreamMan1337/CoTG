using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class Lane_GetOptimalAssignmentClass : AI_LaneDistribution
{


     public bool Lane_GetOptimalAssignment(
      out AttackableUnit BestScore_Entity,
      out int BestScore_EntityIndex,
      out int BestScore_Lane,
      float EnemyStrength_TopLane,
      float EnemyStrength_MidLane,
      float EnemyStrength_BotLane,
      float FriendlyStrength_TopLane,
      float FriendlyStrength_MidLane,
      float FriendlyStrength_BotLane,
      IEnumerable<AttackableUnit> EntitiesToAssign,
      bool AssignedEntity0,
      bool AssignedEntity1,
      bool AssignedEntity2,
      bool AssignedEntity3,
      bool AssignedEntity4)
      {


        var lanePresence = new LanePresenceClass();
        var lanePriorityCalculation = new LanePriorityCalculationClass();
        var lanePriorityPairingCalculation = new LanePriorityPairingCalculationClass();

        AttackableUnit _BestScore_Entity = default;
        int _BestScore_EntityIndex = default;
        int _BestScore_Lane = default;

        bool result =
            // Sequence name :LaneDistribution
            (
                  SetVarInt(
                        out _BestScore_Lane, 
                        -1) &&
                  SetVarFloat(
                        out BestScore, 
                        -1) &&
                  SetVarInt(
                        out _BestScore_EntityIndex, 
                        -1) &&
                  SetVarInt(
                        out CurrentEntityIndex, 
                        -1) &&
                  ForEach(EntitiesToAssign , Entity => (                      
                  // Sequence name :Evaluation
                        (
                              AddInt(
                                    out CurrentEntityIndex, 
                                    CurrentEntityIndex, 
                                    1) &&
                              TestUnitCondition(
                                    Entity) &&
                              // Sequence name :CurrentEntityWasNotAssigned
                              (
                                    // Sequence name :Index==0
                                    (
                                          CurrentEntityIndex == 0 &&
                                          AssignedEntity0 == false
                                    ) ||
                                    // Sequence name :Index==1
                                    (
                                          CurrentEntityIndex == 1 &&
                                          AssignedEntity1 == false
                                    ) ||
                                    // Sequence name :Index==2
                                    (
                                          CurrentEntityIndex == 2 &&
                                          AssignedEntity2 == false
                                    ) ||
                                    // Sequence name :Index==3
                                    (
                                          CurrentEntityIndex == 3 &&
                                          AssignedEntity3 == false
                                    ) ||
                                    // Sequence name :Index==4
                                    (
                                          CurrentEntityIndex == 4 &&
                                          AssignedEntity4 == false
                                    )
                              ) &&
                              lanePresence.LanePresence(
                                    out Temp, 
                                    EnemyStrength_TopLane, 
                                    FriendlyStrength_TopLane) &&
                              lanePriorityCalculation.LanePriorityCalculation(
                                    out PriorityTop, 
                                    Temp) &&
                              lanePresence.LanePresence(
                                    out Temp, 
                                    EnemyStrength_MidLane, 
                                    FriendlyStrength_MidLane) &&
                              lanePriorityCalculation.LanePriorityCalculation(
                                    out PriorityMid, 
                                    Temp) &&
                              lanePresence.LanePresence(
                                    out Temp, 
                                    EnemyStrength_BotLane, 
                                    FriendlyStrength_BotLane) &&
                              lanePriorityCalculation.LanePriorityCalculation(
                                    out PriorityBot, 
                                    Temp) &&
                              lanePriorityPairingCalculation.LanePriorityPairingCalculation(
                                    out ScoreTop, 
                                    out ScoreMid, 
                                    out ScoreBot, 
                                    PriorityTop, 
                                    PriorityMid, 
                                    PriorityBot, 
                                    Entity) &&
                              // Sequence name :GetBestScore
                              (
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :CheckIfTopScoreIsBetter
                                          (
                                                GreaterFloat(
                                                      ScoreTop, 
                                                      BestScore) &&
                                                SetVarInt(
                                                      out _BestScore_Lane, 
                                                      2) &&
                                                SetVarFloat(
                                                      out BestScore, 
                                                      ScoreTop) &&
                                                SetVarAttackableUnit(
                                                      out _BestScore_Entity, 
                                                      Entity) &&
                                                SetVarInt(
                                                      out _BestScore_EntityIndex, 
                                                      CurrentEntityIndex)
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    ) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :CheckIfMidScoreIsBetter
                                          (
                                                GreaterFloat(
                                                      ScoreMid, 
                                                      BestScore) &&
                                                SetVarFloat(
                                                      out BestScore, 
                                                      ScoreMid) &&
                                                SetVarInt(
                                                      out _BestScore_Lane, 
                                                      1) &&
                                                SetVarInt(
                                                      out _BestScore_EntityIndex, 
                                                      CurrentEntityIndex) &&
                                                SetVarAttackableUnit(
                                                      out _BestScore_Entity, 
                                                      Entity)
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    ) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :CheckIfBotScoreIsBetter
                                          (
                                                GreaterFloat(
                                                      ScoreBot, 
                                                      BestScore) &&
                                                SetVarFloat(
                                                      out BestScore, 
                                                      ScoreBot) &&
                                                SetVarInt(
                                                      out _BestScore_Lane, 
                                                      0) &&
                                                SetVarInt(
                                                      out _BestScore_EntityIndex, 
                                                      CurrentEntityIndex) &&
                                                SetVarAttackableUnit(
                                                      out _BestScore_Entity, 
                                                      Entity)
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    )
                              )
                        ))
                  ) &&
                  GreaterEqualInt(
                        _BestScore_Lane, 
                        0)

            );
        BestScore_Entity = _BestScore_Entity;
        BestScore_Lane = _BestScore_Lane;
        BestScore_EntityIndex = _BestScore_EntityIndex;

        return result;
      }
}

