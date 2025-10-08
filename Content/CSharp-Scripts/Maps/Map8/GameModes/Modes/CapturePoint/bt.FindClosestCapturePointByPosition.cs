namespace BehaviourTrees;


class FindClosestCapturePointByPositionClass : OdinLayout
{


    public bool FindClosestCapturePointByPosition(
               out int ClosestCapturePoint,
   AttackableUnit Unit

         )


    {

        var getCapturePoints = new modesGetCapturePointsClass();

        int _ClosestCapturePoint = default;

        bool result =
                          // Sequence name :Sequence

                          getCapturePoints.GetCapturePoints(
                                out CapturePointPositionA,
                                out CapturePointPositionB,
                                out CapturePointPositionC,
                                out CapturePointPositionD,
                                out CapturePointPositionE) &&
                          SetVarFloat(
                                out ClosestDistance,
                                9000000) &&
                          SetVarInt(
                                out _ClosestCapturePoint,
                                -1) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Sequence
                                (
                                      DistanceBetweenObjectAndPoint(
                                            out CurrentDistance,
                                            Unit,
                                            CapturePointPositionA) &&
                                      LessFloat(
                                            CurrentDistance,
                                            ClosestDistance) &&
                                      SetVarInt(
                                            out _ClosestCapturePoint,
                                            0) &&
                                      SetVarFloat(
                                            out ClosestDistance,
                                            CurrentDistance)
                                )
                                ||
                                       DebugAction("MaskFailure")
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Sequence
                                (
                                      DistanceBetweenObjectAndPoint(
                                            out CurrentDistance,
                                            Unit,
                                            CapturePointPositionB) &&
                                      LessFloat(
                                            CurrentDistance,
                                            ClosestDistance) &&
                                      SetVarInt(
                                            out _ClosestCapturePoint,
                                            1) &&
                                      SetVarFloat(
                                            out ClosestDistance,
                                            CurrentDistance)
                                )
                                ||
                                       DebugAction("MaskFailure")
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Sequence
                                (
                                      DistanceBetweenObjectAndPoint(
                                            out CurrentDistance,
                                            Unit,
                                            CapturePointPositionC) &&
                                      LessFloat(
                                            CurrentDistance,
                                            ClosestDistance) &&
                                      SetVarInt(
                                            out _ClosestCapturePoint,
                                            2) &&
                                      SetVarFloat(
                                            out ClosestDistance,
                                            CurrentDistance)
                                )
                                ||
                                       DebugAction("MaskFailure")
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Sequence
                                (
                                      DistanceBetweenObjectAndPoint(
                                            out CurrentDistance,
                                            Unit,
                                            CapturePointPositionD) &&
                                      LessFloat(
                                            CurrentDistance,
                                            ClosestDistance) &&
                                      SetVarInt(
                                            out _ClosestCapturePoint,
                                            3) &&
                                      SetVarFloat(
                                            out ClosestDistance,
                                            CurrentDistance)
                                )
                                ||
                                       DebugAction("MaskFailure")
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Sequence
                                (
                                      DistanceBetweenObjectAndPoint(
                                            out CurrentDistance,
                                            Unit,
                                            CapturePointPositionE) &&
                                      LessFloat(
                                            CurrentDistance,
                                            ClosestDistance) &&
                                      SetVarInt(
                                            out _ClosestCapturePoint,
                                            4) &&
                                      SetVarFloat(
                                            out ClosestDistance,
                                            CurrentDistance)

                                )
                                ||
                                       DebugAction("MaskFailure")
                          )
                    ;
        ClosestCapturePoint = _ClosestCapturePoint;
        return result;
    }
}

