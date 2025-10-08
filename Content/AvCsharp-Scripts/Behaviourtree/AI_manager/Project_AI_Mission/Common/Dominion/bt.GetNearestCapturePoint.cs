using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetNearestCapturePointClass : AImission_bt
{


     public bool GetNearestCapturePoint(
                out int ClosestCapturePoint,
      out Vector3 ClosestCapturePointPosition,
      out float ClosestDistance,
    AttackableUnit Unit
          )
    {
      int _ClosestCapturePoint = default;
      Vector3 _ClosestCapturePointPosition = default;
      float _ClosestDistance = default;
        var getCapturePointPositions = new GetCapturePointPositionsClass();

bool result = 
            // Sequence name :Sequence
            (
                  getCapturePointPositions.GetCapturePointPositions(
                        out CapturePointPositionA, 
                        out CapturePointPositionB, 
                        out CapturePointPositionC, 
                        out CapturePointPositionD, 
                        out CapturePointPositionE
                       ) &&
                  SetVarFloat(
                        out _ClosestDistance, 
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
                                    _ClosestDistance) &&
                              SetVarInt(
                                    out _ClosestCapturePoint, 
                                    0) &&
                              SetVarFloat(
                                    out _ClosestDistance, 
                                    CurrentDistance) &&
                              SetVarVector(
                                    out _ClosestCapturePointPosition, 
                                    CapturePointPositionA)
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
                                    _ClosestDistance) &&
                              SetVarInt(
                                    out _ClosestCapturePoint, 
                                    1) &&
                              SetVarFloat(
                                    out _ClosestDistance, 
                                    CurrentDistance) &&
                              SetVarVector(
                                    out _ClosestCapturePointPosition, 
                                    CapturePointPositionB)
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
                                    _ClosestDistance) &&
                              SetVarInt(
                                    out _ClosestCapturePoint, 
                                    2) &&
                              SetVarFloat(
                                    out _ClosestDistance, 
                                    CurrentDistance) &&
                              SetVarVector(
                                    out _ClosestCapturePointPosition, 
                                    CapturePointPositionC)
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
                                    _ClosestDistance) &&
                              SetVarInt(
                                    out _ClosestCapturePoint, 
                                    3) &&
                              SetVarFloat(
                                    out _ClosestDistance, 
                                    CurrentDistance) &&
                              SetVarVector(
                                    out _ClosestCapturePointPosition, 
                                    CapturePointPositionD)
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
                                    _ClosestDistance) &&
                              SetVarInt(
                                    out _ClosestCapturePoint, 
                                    4) &&
                              SetVarFloat(
                                    out _ClosestDistance, 
                                    CurrentDistance) &&
                              SetVarVector(
                                    out _ClosestCapturePointPosition, 
                                    CapturePointPositionE)

                        )
                        ||
                               DebugAction("MaskFailure")
                  )
            );
        ClosestCapturePoint = _ClosestCapturePoint;
        ClosestDistance = _ClosestDistance;
        ClosestCapturePointPosition = _ClosestCapturePointPosition;
        return result;
      }
}

