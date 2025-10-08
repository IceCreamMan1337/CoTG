namespace BehaviourTrees;


class LaneDistributionClass : AI_LaneDistribution
{


    public bool LaneDistribution(
    float EnemyStrength_TopLane,
    float EnemyStrength_MidLane,
    float EnemyStrength_BotLane,
    float FriendlyStrength_TopLane,
    float FriendlyStrength_MidLane,
    float FriendlyStrength_BotLane,
    IEnumerable<AttackableUnit> EntitiesToAssign,
    AISquadClass SquadTop,
    AISquadClass SquadMid,
    AISquadClass SquadBot,
    float ChampionPointValue
    )
    {


        var lanePresence = new LanePresenceClass();
        var lanePriorityCalculation = new LanePriorityCalculationClass();
        var lanePriorityPairingCalculation = new LanePriorityPairingCalculationClass();
        var assignToLane = new AssignToLaneClass();
        return

                  // Sequence name :LaneDistribution

                  ForEach(EntitiesToAssign, Entity =>
                              // Sequence name :Evaluation

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
                                          assignToLane.AssignToLane(
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
                                          assignToLane.AssignToLane(
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
                                          assignToLane.AssignToLane(
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

                                                ||
                               DebugAction("MaskFailure")
                                          ) &&
                                          // Sequence name :MaskFailure
                                          (
                                                      // Sequence name :IncreaseLanePresence

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

                                                ||
                               DebugAction("MaskFailure")
                                          )
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              )


            );
    }
}

