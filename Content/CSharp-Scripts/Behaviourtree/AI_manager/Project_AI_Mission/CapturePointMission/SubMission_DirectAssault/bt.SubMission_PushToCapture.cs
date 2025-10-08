using BehaviourTrees.Map8;

namespace BehaviourTrees;


class SubMission_PushToCaptureClass : AImission_bt
{


    public bool SubMission_PushToCapture(
   AttackableUnit CapturePointA,
   AttackableUnit CapturePointB,
   AttackableUnit CapturePointC,
   AttackableUnit CapturePointD,
   AttackableUnit CapturePointE,
   int CapturePointIndex,
   IEnumerable<AttackableUnit> SquadMembers,
   AttackableUnit Mission_CapturePoint

         )
    {
        var getAdjacentCapturePoint = new GetAdjacentCapturePointClass();
        var validateAdjacentPointsAndMinions = new ValidateAdjacentPointsAndMinionsClass();
        var assignTaskWithPosition = new AssignTaskWithPositionClass();

        return
                    // Sequence name :Sequence

                    ForEach(SquadMembers, Member =>
                    SetVarAttackableUnit(
                                out ReferenceUnit,
                                Member)
                    ) &&
                         // Sequence name :ValidateAdjacentPointsAndMinions

                         getAdjacentCapturePoint.GetAdjacentCapturePoint(
                                out AdjacentCapturePoint0,
                                out AdjacentCapturePoint1,
                                out AdjacentCapturePointIndex1,
                                out AdjacentCapturePointIndex0,
                                CapturePointIndex,
                                CapturePointA,
                                CapturePointB,
                                CapturePointC,
                                CapturePointD,
                                CapturePointE) &&
                         validateAdjacentPointsAndMinions.ValidateAdjacentPointsAndMinions(
                                out IsValid_AdjCapturePoint1,
                                out IsValid_AdjCapturePoint0,
                                out IsValid_ClosestMinionLane0,
                                out IsValid_ClosestMinionLane1,
                                out MinionFromPoint0,
                                out MinionFromPoint1,
                                AdjacentCapturePoint0,
                                AdjacentCapturePoint1,
                                AdjacentCapturePointIndex0,
                                AdjacentCapturePointIndex1,
                                ReferenceUnit,
                                Mission_CapturePoint,
                                CapturePointIndex) &&
                          // Sequence name :AtLeastOneIsValid
                          (
                                IsValid_AdjCapturePoint0 == true ||
                                IsValid_AdjCapturePoint1 == true ||
                                IsValid_ClosestMinionLane0 == true ||
                                IsValid_ClosestMinionLane1 == true
                          )
                     &&
                    ForEach(SquadMembers, Member =>
                                // Sequence name :Sequence

                                SetVarFloat(
                                      out ClosestDistance,
                                      50000) &&
                                SetVarInt(
                                      out ActionToPick,
                                      -1) &&
                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :Selector

                                            // Sequence name :FromMinion0
                                            (
                                                  IsValid_ClosestMinionLane0 == true &&
                                                  GetDistanceBetweenUnits(
                                                        out Distance,
                                                        Member,
                                                        MinionFromPoint0) &&
                                                  LessFloat(
                                                        Distance,
                                                        ClosestDistance) &&
                                                  SetVarFloat(
                                                        out ClosestDistance,
                                                        Distance) &&
                                                  SetVarInt(
                                                        out ActionToPick,
                                                        0)
                                            ) ||
                                            // Sequence name :FromCapturePoint
                                            (
                                                  IsValid_AdjCapturePoint0 == true &&
                                                  GetDistanceBetweenUnits(
                                                        out Distance,
                                                        Member,
                                                        AdjacentCapturePoint0) &&
                                                  LessFloat(
                                                        Distance,
                                                        ClosestDistance) &&
                                                  SetVarFloat(
                                                        out ClosestDistance,
                                                        Distance) &&
                                                  SetVarInt(
                                                        out ActionToPick,
                                                        1)
                                            )

                                      ||
                                 DebugAction("MaskFailure")
                                ) &&
                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :Selector

                                            // Sequence name :FromMinion1
                                            (
                                                  IsValid_ClosestMinionLane1 == true &&
                                                  GetDistanceBetweenUnits(
                                                        out Distance,
                                                        Member,
                                                        MinionFromPoint1) &&
                                                  LessFloat(
                                                        Distance,
                                                        ClosestDistance) &&
                                                  SetVarFloat(
                                                        out ClosestDistance,
                                                        Distance) &&
                                                  SetVarInt(
                                                        out ActionToPick,
                                                        2)
                                            ) ||
                                            // Sequence name :FromCapturePoint
                                            (
                                                  IsValid_ClosestMinionLane1 == true &&
                                                  GetDistanceBetweenUnits(
                                                        out Distance,
                                                        Member,
                                                        AdjacentCapturePoint1) &&
                                                  LessFloat(
                                                        Distance,
                                                        ClosestDistance) &&
                                                  SetVarFloat(
                                                        out ClosestDistance,
                                                        Distance) &&
                                                  SetVarInt(
                                                        out ActionToPick,
                                                        3)
                                            )

                                      ||
                                 DebugAction("MaskFailure")
                                ) &&
                                // Sequence name :AssigningTaskBasedOnAction
                                (
                                      // Sequence name :Action0
                                      (
                                            ActionToPick == 0 &&
                                            GetUnitPosition(
                                                  out TaskPosition,
                                                  MinionFromPoint0)
                                      ) ||
                                      // Sequence name :Action1
                                      (
                                            ActionToPick == 1 &&
                                            GetUnitPosition(
                                                  out TaskPosition,
                                                  AdjacentCapturePoint0)
                                      ) ||
                                      // Sequence name :Action2
                                      (
                                            ActionToPick == 2 &&
                                            GetUnitPosition(
                                                  out TaskPosition,
                                                  MinionFromPoint1)
                                      ) ||
                                      // Sequence name :Action3
                                      (
                                            ActionToPick == 3 &&
                                            GetUnitPosition(
                                                  out TaskPosition,
                                                  AdjacentCapturePoint1)
                                      )
                                ) &&
                                assignTaskWithPosition.AssignTaskWithPosition(
                                     AITaskTopicType.PUSH_TO_CAPTURE_POINT,
                                      TaskPosition,
                                      Member)


                    )
              ;
    }
}

