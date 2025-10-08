using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class modesGetCapturePointsClass : OdinLayout  
{


     public bool GetCapturePoints(
      out Vector3 CapturePointPositionA,
      out Vector3 CapturePointPositionB,
      out Vector3 CapturePointPositionC,
      out Vector3 CapturePointPositionD,
      out Vector3 CapturePointPositionE
          )
      {
        Vector3 _CapturePointPositionA = default;
        Vector3 _CapturePointPositionB = default;
        Vector3 _CapturePointPositionC = default;
        Vector3 _CapturePointPositionD = default;
        Vector3 _CapturePointPositionE = default;

bool result =
            // Sequence name :Sequence
            (
                  GetWorldLocationByName(
                        out _CapturePointPositionA,
                        "CapturePointA") &&
                  GetWorldLocationByName(
                        out _CapturePointPositionB,
                        "CapturePointB") &&
                  GetWorldLocationByName(
                        out _CapturePointPositionC,
                        "CapturePointC") &&
                  GetWorldLocationByName(
                        out _CapturePointPositionD,
                        "CapturePointD") &&
                  GetWorldLocationByName(
                        out _CapturePointPositionE,
                        "CapturePointE")

            );

        CapturePointPositionA = _CapturePointPositionA;
        CapturePointPositionB = _CapturePointPositionB;
        CapturePointPositionC = _CapturePointPositionC;
        CapturePointPositionD = _CapturePointPositionD;
        CapturePointPositionE = _CapturePointPositionE; 
        return result;
      }
}

