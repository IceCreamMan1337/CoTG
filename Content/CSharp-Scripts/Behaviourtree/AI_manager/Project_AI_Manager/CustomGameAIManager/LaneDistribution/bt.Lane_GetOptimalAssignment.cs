/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class Lane_GetOptimalAssignment : BehaviourTree 
{
      out AttackableUnit BestScore_Entity;
      out int BestScore_EntityIndex;
      out int BestScore_Lane;
      float EnemyStrength_TopLane;
      float EnemyStrength_MidLane;
      float EnemyStrength_BotLane;
      float FriendlyStrength_TopLane;
      float FriendlyStrength_MidLane;
      float FriendlyStrength_BotLane;
      AttackableUnitCollection EntitiesToAssign;
      bool AssignedEntity0;
      bool AssignedEntity1;
      bool AssignedEntity2;
      bool AssignedEntity3;
      bool AssignedEntity4;

      bool Lane_GetOptimalAssignment()
      {
      return
            // Sequence name :LaneDistribution
            (
                  SetVarInt(
                        out BestScore_Lane, 
                        -1) &&
                  SetVarFloat(
                        out BestScore, 
                        -1) &&
                  SetVarInt(
                        out BestScore_EntityIndex, 
                        -1) &&
                  SetVarInt(
                        out CurrentEntityIndex, 
                        -1) &&
                  EntitiesToAssign.ForEach( Entity => (                        // Sequence name :Evaluation
                        (
                              AddInt(
                                    out CurrentEntityIndex, 
                                    CurrentEntityIndex, 
                                    1) &&
                              TestUnitCondition(
                                    Entity, 
                                    true) &&
                              // Sequence name :CurrentEntityWasNotAssigned
                              (
                                    // Sequence name :Index==0
                                    (
                                          CurrentEntityIndex == 0 &&
                                          AssignedEntity0 == False
                                    ) ||
                                    // Sequence name :Index==1
                                    (
                                          CurrentEntityIndex == 1 &&
                                          AssignedEntity1 == False
                                    ) ||
                                    // Sequence name :Index==2
                                    (
                                          CurrentEntityIndex == 2 &&
                                          AssignedEntity2 == False
                                    ) ||
                                    // Sequence name :Index==3
                                    (
                                          CurrentEntityIndex == 3 &&
                                          AssignedEntity3 == False
                                    ) ||
                                    // Sequence name :Index==4
                                    (
                                          CurrentEntityIndex == 4 &&
                                          AssignedEntity4 == False
                                    )
                              ) &&
                              LanePresence(
                                    out Temp, 
                                    EnemyStrength_TopLane, 
                                    FriendlyStrength_TopLane) &&
                              LanePriorityCalculation(
                                    out PriorityTop, 
                                    Temp) &&
                              LanePresence(
                                    out Temp, 
                                    EnemyStrength_MidLane, 
                                    FriendlyStrength_MidLane) &&
                              LanePriorityCalculation(
                                    out PriorityMid, 
                                    Temp) &&
                              LanePresence(
                                    out Temp, 
                                    EnemyStrength_BotLane, 
                                    FriendlyStrength_BotLane) &&
                              LanePriorityCalculation(
                                    out PriorityBot, 
                                    Temp) &&
                              LanePriorityPairingCalculation(
                                    out Value=, 
                                    out Value=, 
                                    out Value=, 
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
                                                      out BestScore_Lane, 
                                                      2) &&
                                                SetVarFloat(
                                                      out BestScore, 
                                                      ScoreTop) &&
                                                SetVarAttackableUnit(
                                                      out BestScore_Entity, 
                                                      Entity) &&
                                                SetVarInt(
                                                      out BestScore_EntityIndex, 
                                                      CurrentEntityIndex)
                                          )
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
                                                      out BestScore_Lane, 
                                                      1) &&
                                                SetVarInt(
                                                      out BestScore_EntityIndex, 
                                                      CurrentEntityIndex) &&
                                                SetVarAttackableUnit(
                                                      out BestScore_Entity, 
                                                      Entity)
                                          )
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
                                                      out BestScore_Lane, 
                                                      0) &&
                                                SetVarInt(
                                                      out BestScore_EntityIndex, 
                                                      CurrentEntityIndex) &&
                                                SetVarAttackableUnit(
                                                      out BestScore_Entity, 
                                                      Entity)
                                          )
                                    )
                              )
                        )
                  ) &&
                  GreaterEqualInt(
                        BestScore_Lane, 
                        0)

            );
      }
}

*/