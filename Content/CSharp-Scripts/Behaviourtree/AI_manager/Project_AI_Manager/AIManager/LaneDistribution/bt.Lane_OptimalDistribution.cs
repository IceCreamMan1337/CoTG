namespace BehaviourTrees;


class Lane_OptimalDistributionClass : AI_LaneDistribution
{


    public bool Lane_OptimalDistribution(
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
        float ChampionPointValue,
        int DisconnectAdjustmentEntityID
          )
    {

        var assignToLane = new AssignToLaneClass();
        var lane_GetOptimalAssignment = new Lane_GetOptimalAssignmentClass();

        return
                    // Sequence name :MaskFailure

                    // Sequence name :OptimalLaneDistribution
                    (
                                // Sequence name :InitVariables

                                SetVarFloat(
                                      out TempFriendlyStrengthTop,
                                      FriendlyStrength_TopLane) &&
                                SetVarFloat(
                                      out TempFriendlyStrengthBot,
                                      FriendlyStrength_BotLane) &&
                                SetVarFloat(
                                      out TempFriendlyStrengthMid,
                                      FriendlyStrength_MidLane) &&
                                SetVarBool(
                                      out AssignedEntity0,
                                      false) &&
                                SetVarBool(
                                      out AssignedEntity1,
                                      false) &&
                                SetVarBool(
                                      out AssignedEntity2,
                                      false) &&
                                SetVarBool(
                                      out AssignedEntity3,
                                      false) &&
                                SetVarBool(
                                      out AssignedEntity4,
                                      false) &&
                                SetVarInt(
                                      out TotalEntitiesAssigned,
                                      0) &&
                                SetVarInt(
                                      out EntitiesAlive,
                                      0) &&
                                // Sequence name :HandleDisconnectsAdjustment
                                (
                                      (LessInt(
                                            DisconnectAdjustmentEntityID,
                                            0) &&


                                            // Sequence name :DC

                                            DisconnectAdjustmentEntityID == 0 &&
                                            SetVarBool(
                                                  out AssignedEntity0,
                                                  true))
                                       ||
                                      // Sequence name :DC
                                      (
                                            DisconnectAdjustmentEntityID == 1 &&
                                            SetVarBool(
                                                  out AssignedEntity1,
                                                  true)
                                      ) ||
                                      // Sequence name :DC
                                      (
                                            DisconnectAdjustmentEntityID == 2 &&
                                            SetVarBool(
                                                  out AssignedEntity2,
                                                  true)
                                      ) ||
                                      // Sequence name :DC
                                      (
                                            DisconnectAdjustmentEntityID == 3 &&
                                            SetVarBool(
                                                  out AssignedEntity3,
                                                  true)
                                      ) ||
                                      // Sequence name :DC
                                      (
                                            DisconnectAdjustmentEntityID == 4 &&
                                            SetVarBool(
                                                  out AssignedEntity4,
                                                  true)
                                      )
                                )
                           &&
                          ForEach(EntitiesToAssign, Temp =>
                                      // Sequence name :FindAndAssignOptimalAssignment

                                      TestAIEntityHasTask(
                                            Temp,
                                            AITaskTopicType.ASSIST,
                                            null,
                                            default,
                                            default,
                                            false) &&
                                      TestUnitCondition(
                                            Temp) &&
                                      AddInt(
                                            out EntitiesAlive,
                                            EntitiesAlive,
                                            1) &&
                                      lane_GetOptimalAssignment.Lane_GetOptimalAssignment(
                                            out BestScore_Entity,
                                            out BestScore_EntityIndex,
                                            out BestScore_Lane,
                                            EnemyStrength_TopLane,
                                            EnemyStrength_MidLane,
                                            EnemyStrength_BotLane,
                                            TempFriendlyStrengthTop,
                                            TempFriendlyStrengthMid,
                                            TempFriendlyStrengthBot,
                                            EntitiesToAssign,
                                            AssignedEntity0,
                                            AssignedEntity1,
                                            AssignedEntity2,
                                            AssignedEntity3,
                                            AssignedEntity4) &&
                                      assignToLane.AssignToLane(
                                            SquadTop,
                                            SquadMid,
                                            SquadBot,
                                            BestScore_Entity,
                                            BestScore_Lane) &&
                                      GetUnitAIClosestLaneID(
                                            out CurrentLane,
                                            BestScore_Entity) &&
                                      // Sequence name :MaskFailure
                                      (
                                            // Sequence name :Check_Lane
                                            (
                                                  NotEqualInt(
                                                        BestScore_Lane,
                                                        CurrentLane) &&
                                                  // Sequence name :MaskFailure
                                                  (
                                                              // Sequence name :ReduceLanePresence

                                                              // Sequence name :CurrentlyInTopLane
                                                              (
                                                                    CurrentLane == 2 &&
                                                                    SubtractFloat(
                                                                          out TempFriendlyStrengthTop,
                                                                          TempFriendlyStrengthTop,
                                                                          400)
                                                              ) ||
                                                              // Sequence name :CurrentlyInMidLane
                                                              (
                                                                    CurrentLane == 1 &&
                                                                    SubtractFloat(
                                                                          out TempFriendlyStrengthMid,
                                                                          TempFriendlyStrengthMid,
                                                                          400)
                                                              ) ||
                                                              // Sequence name :CurrentlyInBotLane
                                                              (
                                                                    CurrentLane == 0 &&
                                                                    SubtractFloat(
                                                                          out TempFriendlyStrengthBot,
                                                                          TempFriendlyStrengthBot,
                                                                          400)
                                                              )

                                                        ||
                                 DebugAction("MaskFailure")
                                                  ) &&
                                                  // Sequence name :MaskFailure
                                                  (
                                                              // Sequence name :IncreaseLanePresence

                                                              // Sequence name :AssignedToTop
                                                              (
                                                                    BestScore_Lane == 2 &&
                                                                    AddFloat(
                                                                          out TempFriendlyStrengthTop,
                                                                          TempFriendlyStrengthTop,
                                                                          300)
                                                              ) ||
                                                              // Sequence name :AssignedToMid
                                                              (
                                                                    BestScore_Lane == 1 &&
                                                                    AddFloat(
                                                                          out TempFriendlyStrengthMid,
                                                                          TempFriendlyStrengthMid,
                                                                          300)
                                                              ) ||
                                                              // Sequence name :AssignedToBot
                                                              (
                                                                    BestScore_Lane == 0 &&
                                                                    AddFloat(
                                                                          out TempFriendlyStrengthBot,
                                                                          TempFriendlyStrengthBot,
                                                                          300)
                                                              )

                                                        ||
                                 DebugAction("MaskFailure")
                                                  )
                                            )
                                            ||
                                 DebugAction("MaskFailure")
                                      ) &&
                                      // Sequence name :SetAssignmentFlag
                                      (
                                            // Sequence name :Index==0
                                            (
                                                  BestScore_EntityIndex == 0 &&
                                                  SetVarBool(
                                                        out AssignedEntity0,
                                                        true)
                                            ) ||
                                            // Sequence name :Index==1
                                            (
                                                  BestScore_EntityIndex == 1 &&
                                                  SetVarBool(
                                                        out AssignedEntity1,
                                                        true)
                                            ) ||
                                            // Sequence name :Index==2
                                            (
                                                  BestScore_EntityIndex == 2 &&
                                                  SetVarBool(
                                                        out AssignedEntity2,
                                                        true)
                                            ) ||
                                            // Sequence name :Index==3
                                            (
                                                  BestScore_EntityIndex == 3 &&
                                                  SetVarBool(
                                                        out AssignedEntity3,
                                                        true)
                                            ) ||
                                            // Sequence name :Index==4
                                            (
                                                  BestScore_EntityIndex == 4 &&
                                                  SetVarBool(
                                                        out AssignedEntity4,
                                                        true)
                                            )
                                      ) &&
                                      AddInt(
                                            out TotalEntitiesAssigned,
                                            TotalEntitiesAssigned,
                                            1)


                          )
                    )
                    ||
                                 DebugAction("MaskFailure")
              ;
    }
}

