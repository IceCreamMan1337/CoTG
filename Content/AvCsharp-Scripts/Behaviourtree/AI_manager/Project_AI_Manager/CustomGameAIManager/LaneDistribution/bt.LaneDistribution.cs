/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class LaneDistribution : BehaviourTree 
{
      float EnemyStrength_TopLane;
      float EnemyStrength_MidLane;
      float EnemyStrength_BotLane;
      float FriendlyStrength_TopLane;
      float FriendlyStrength_MidLane;
      float FriendlyStrength_BotLane;
      AttackableUnitCollection EntitiesToAssign;
      AISquad SquadTop;
      AISquad SquadMid;
      AISquad SquadBot;
      float ChampionPointValue;

      bool LaneDistribution()
      {
      return
            // Sequence name :LaneDistribution
            (
                  EntitiesToAssign.ForEach( Entity => (
                        // Sequence name :Evaluation
                        (
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
                              GetUnitAIClosestLaneID(
                                    out CurrentLane, 
                                    Entity) &&
                              SetVarInt(
                                    out LaneAssigned, 
                                    -1) &&
                              // Sequence name :AssignToLane
                              (
                                    // Sequence name :AssignToTop
                                    (
                                          GreaterEqualFloat(
                                                ScoreTop, 
                                                ScoreMid) &&
                                          GreaterEqualFloat(
                                                ScoreTop, 
                                                ScoreBot) &&
                                          AssignToLane(
                                                SquadTop, 
                                                SquadMid, 
                                                SquadBot, 
                                                Entity, 
                                                2) &&
                                          SetVarInt(
                                                out LaneAssigned, 
                                                2)
                                    ) ||
                                    // Sequence name :AssignToMid
                                    (
                                          GreaterEqualFloat(
                                                ScoreMid, 
                                                ScoreTop) &&
                                          GreaterEqualFloat(
                                                ScoreMid, 
                                                ScoreBot) &&
                                          AssignToLane(
                                                SquadTop, 
                                                SquadMid, 
                                                SquadBot, 
                                                Entity, 
                                                1) &&
                                          SetVarInt(
                                                out LaneAssigned, 
                                                1)
                                    ) ||
                                    // Sequence name :AssignToBot
                                    (
                                          GreaterEqualFloat(
                                                ScoreBot, 
                                                ScoreTop) &&
                                          GreaterEqualFloat(
                                                ScoreBot, 
                                                ScoreMid) &&
                                          AssignToLane(
                                                SquadTop, 
                                                SquadMid, 
                                                SquadBot, 
                                                Entity, 
                                                0) &&
                                          SetVarInt(
                                                out LaneAssigned, 
                                                0)
                                    )
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :Check_Lane
                                    (
                                          NotEqualInt(
                                                LaneAssigned, 
                                                CurrentLane) &&
                                          // Sequence name :MaskFailure
                                          (
                                                // Sequence name :ReduceLanePresence
                                                (
                                                      // Sequence name :CurrentlyInTopLane
                                                      (
                                                            CurrentLane == 2 &&
                                                            SubtractFloat(
                                                                  out FriendlyStrength_TopLane, 
                                                                  FriendlyStrength_TopLane, 
                                                                  ChampionPointValue)
                                                      ) ||
                                                      // Sequence name :CurrentlyInMidLane
                                                      (
                                                            CurrentLane == 1 &&
                                                            SubtractFloat(
                                                                  out FriendlyStrength_MidLane, 
                                                                  FriendlyStrength_MidLane, 
                                                                  ChampionPointValue)
                                                      ) ||
                                                      // Sequence name :CurrentlyInBotLane
                                                      (
                                                            CurrentLane == 0 &&
                                                            SubtractFloat(
                                                                  out FriendlyStrength_BotLane, 
                                                                  FriendlyStrength_BotLane, 
                                                                  ChampionPointValue)
                                                      )
                                                )
                                          ) &&
                                          // Sequence name :MaskFailure
                                          (
                                                // Sequence name :IncreaseLanePresence
                                                (
                                                      // Sequence name :AssignedToTop
                                                      (
                                                            LaneAssigned == 2 &&
                                                            AddFloat(
                                                                  out FriendlyStrength_TopLane, 
                                                                  FriendlyStrength_TopLane, 
                                                                  ChampionPointValue)
                                                      ) ||
                                                      // Sequence name :AssignedToMid
                                                      (
                                                            LaneAssigned == 1 &&
                                                            AddFloat(
                                                                  out FriendlyStrength_MidLane, 
                                                                  FriendlyStrength_MidLane, 
                                                                  ChampionPointValue)
                                                      ) ||
                                                      // Sequence name :AssignedToBot
                                                      (
                                                            LaneAssigned == 0 &&
                                                            AddFloat(
                                                                  out FriendlyStrength_BotLane, 
                                                                  FriendlyStrength_BotLane, 
                                                                  ChampionPointValue)

                                                      )
                                                )
                                          )
                                    )
                              )
                        )
                  )
            );
      }
}

*/