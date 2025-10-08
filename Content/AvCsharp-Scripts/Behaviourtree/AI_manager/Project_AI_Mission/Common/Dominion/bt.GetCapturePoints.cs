using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetCapturePointsClass : AImission_bt
{


     public bool GetCapturePoints(
                out AttackableUnit CapturePointA,
      out AttackableUnit CapturePointB,
      out AttackableUnit CapturePointC,
      out AttackableUnit CapturePointD,
      out AttackableUnit CapturePointE,
    AttackableUnit ReferenceUnit)
      {

        var getCapturePoint = new GetCapturePointClass();


        var getCapturePointPositions = new GetCapturePointPositionsClass();
      AttackableUnit _CapturePointA = default;
      AttackableUnit _CapturePointB = default;
      AttackableUnit _CapturePointC = default;
      AttackableUnit _CapturePointD = default;
      AttackableUnit _CapturePointE = default;
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
                  getCapturePoint.GetCapturePoint(
                        out _CapturePointA, 
                        ReferenceUnit, 
                        CapturePointPositionA) &&
                  getCapturePoint.GetCapturePoint(
                        out _CapturePointB, 
                        ReferenceUnit, 
                        CapturePointPositionB) &&
                  getCapturePoint.GetCapturePoint(
                        out _CapturePointC, 
                        ReferenceUnit, 
                        CapturePointPositionC) &&
                  getCapturePoint.GetCapturePoint(
                        out _CapturePointD, 
                        ReferenceUnit, 
                        CapturePointPositionD) &&
                  getCapturePoint.GetCapturePoint(
                        out _CapturePointE, 
                        ReferenceUnit, 
                        CapturePointPositionE)

            );
        CapturePointA = _CapturePointA;
        CapturePointB = _CapturePointB;
        CapturePointC = _CapturePointC;
        CapturePointD = _CapturePointD;
        CapturePointE = _CapturePointE;
        
        return result;
      }
}

