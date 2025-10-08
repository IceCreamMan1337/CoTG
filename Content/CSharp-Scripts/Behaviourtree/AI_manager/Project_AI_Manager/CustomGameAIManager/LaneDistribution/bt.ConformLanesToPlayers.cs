namespace BehaviourTrees.Map8;


class ConformLanesToPlayersClass : CommonAI
{


    public bool ConformLanesToPlayers(
           out int _Bot1Lane,
      out int _Bot2Lane,
      out int _Bot3Lane,
      out int _Bot4Lane,
    IEnumerable<AttackableUnit> AllEntities,
    TeamId ReferenceUnitTeam
          )
    {
        int Bot1Lane = default;
        int Bot2Lane = default;
        int Bot3Lane = default;
        int Bot4Lane = default;

        bool result =
                  // Sequence name :ConformLanesToPlayers

                  SetVarInt(
                        out BotIndex,
                        0) &&
                  GetChampionCollection(
                        out ChampCollection) &&
                  SetVarInt(
                        out Lane0Count,
                        0) &&
                  SetVarInt(
                        out Lane1Count,
                        0) &&
                  SetVarInt(
                        out Lane2Count,
                        0) &&
                  ForEach(ChampCollection, Champ =>
                              // Sequence name :CountFriendlyChampsInLane

                              GetUnitTeam(
                                    out ChampTeam,
                                    Champ) &&
                              ChampTeam == ReferenceUnitTeam &&
                              GetUnitAIClosestLaneID(
                                    out ClosestLaneID,
                                    Champ) &&
                              GetUnitAIClosestLanePoint(
                                    out ClosestLanePoint,
                                    Champ,
                                    ClosestLaneID) &&
                              DistanceBetweenObjectAndPoint(
                                    out DistanceToClosestLanePoint,
                                    Champ,
                                    ClosestLanePoint) &&
                              LessFloat(
                                    DistanceToClosestLanePoint,
                                    800) &&
                              // Sequence name :IncrementLaneCount
                              (
                                    // Sequence name :BotLane
                                    (
                                          ClosestLaneID == 0 &&
                                          AddInt(
                                                out Lane0Count,
                                                Lane0Count,
                                                1)
                                    ) ||
                                    // Sequence name :MidLane
                                    (
                                          ClosestLaneID == 1 &&
                                          AddInt(
                                                out Lane1Count,
                                                Lane1Count,
                                                1)
                                    ) ||
                                    // Sequence name :TopLane
                                    (
                                          ClosestLaneID == 2 &&
                                          AddInt(
                                                out Lane2Count,
                                                Lane2Count,
                                                1)
                                    )
                              )

                  ) &&
                  ForEach(AllEntities, Entity =>
                              // Sequence name :AssignLane

                              AddInt(
                                    out BotIndex,
                                    BotIndex,
                                    1) &&
                              // Sequence name :GetCurrentLaneAssignment
                              (
                                    // Sequence name :Bot1
                                    (
                                          BotIndex == 1 &&
                                          SetVarInt(
                                                out LaneToAssign,
                                                Bot1Lane)
                                    ) ||
                                    // Sequence name :Bot2
                                    (
                                          BotIndex == 2 &&
                                          SetVarInt(
                                                out LaneToAssign,
                                                Bot2Lane)
                                    ) ||
                                    // Sequence name :Bot3
                                    (
                                          BotIndex == 3 &&
                                          SetVarInt(
                                                out LaneToAssign,
                                                Bot3Lane)
                                    ) ||
                                    // Sequence name :Bot4
                                    (
                                          BotIndex == 4 &&
                                          SetVarInt(
                                                out LaneToAssign,
                                                Bot4Lane)
                                    )
                              ) &&
                                    // Sequence name :MaskFailure

                                    // Sequence name :MoveLaneIfNecessary
                                    (
                                          // Sequence name :BotLane
                                          (
                                                LaneToAssign == 0 &&
                                                GreaterEqualInt(
                                                      Lane0Count,
                                                      2) &&
                                                // Sequence name :MoveToAnotherLane
                                                (
                                                      // Sequence name :MidLane
                                                      (
                                                            LessInt(
                                                                  Lane1Count,
                                                                  1) &&
                                                            SetVarInt(
                                                                  out LaneToAssign,
                                                                  1)
                                                      ) ||
                                                            // Sequence name :ElseTopLane

                                                            SetVarInt(
                                                                  out LaneToAssign,
                                                                  2)

                                                )
                                          ) ||
                                          // Sequence name :MidLane
                                          (
                                                LaneToAssign == 1 &&
                                                GreaterEqualInt(
                                                      Lane1Count,
                                                      1) &&
                                                // Sequence name :MoveToAnotherLane
                                                (
                                                      // Sequence name :BotLane
                                                      (
                                                            LessInt(
                                                                  Lane0Count,
                                                                  2) &&
                                                            SetVarInt(
                                                                  out LaneToAssign,
                                                                  0)
                                                      ) ||
                                                            // Sequence name :ElseTopLane

                                                            SetVarInt(
                                                                  out LaneToAssign,
                                                                  2)

                                                )
                                          ) ||
                                          // Sequence name :TopLane
                                          (
                                                LaneToAssign == 2 &&
                                                GreaterEqualInt(
                                                      Lane2Count,
                                                      2) &&
                                                // Sequence name :MoveToAnotherLane
                                                (
                                                      // Sequence name :MidLane
                                                      (
                                                            LessInt(
                                                                  Lane1Count,
                                                                  1) &&
                                                            SetVarInt(
                                                                  out LaneToAssign,
                                                                  1)
                                                      ) ||
                                                            // Sequence name :ElseBotLane

                                                            SetVarInt(
                                                                  out LaneToAssign,
                                                                  0)

                                                )
                                          )
                                    )
                               &&
                              // Sequence name :SetLaneAssignment
                              (
                                    // Sequence name :Bot1
                                    (
                                          BotIndex == 1 &&
                                          SetVarInt(
                                                out Bot1Lane,
                                                LaneToAssign)
                                    ) ||
                                    // Sequence name :Bot2
                                    (
                                          BotIndex == 2 &&
                                          SetVarInt(
                                                out Bot2Lane,
                                                LaneToAssign)
                                    ) ||
                                    // Sequence name :Bot3
                                    (
                                          BotIndex == 3 &&
                                          SetVarInt(
                                                out Bot3Lane,
                                                LaneToAssign)
                                    ) ||
                                    // Sequence name :Bot4
                                    (
                                          BotIndex == 4 &&
                                          SetVarInt(
                                                out Bot4Lane,
                                                LaneToAssign)
                                    )
                              ) &&
                              // Sequence name :IncrementLaneCount
                              (
                                    // Sequence name :BotLane
                                    (
                                          LaneToAssign == 0 &&
                                          AddInt(
                                                out Lane0Count,
                                                Lane0Count,
                                                1)
                                    ) ||
                                    // Sequence name :MidLane
                                    (
                                          LaneToAssign == 1 &&
                                          AddInt(
                                                out Lane1Count,
                                                Lane1Count,
                                                1)
                                    ) ||
                                    // Sequence name :TopLane
                                    (
                                          LaneToAssign == 2 &&
                                          AddInt(
                                                out Lane2Count,
                                                Lane2Count,
                                                1)

                                    )
                              )

                  )
            ;

        _Bot1Lane = Bot1Lane;
        _Bot2Lane = Bot2Lane;
        _Bot3Lane = Bot3Lane;
        _Bot4Lane = Bot4Lane;

        return result;
    }
}

