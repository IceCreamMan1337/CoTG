using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetCapturePointClass : AImission_bt
{


     public bool GetCapturePoint(
                out AttackableUnit CapturePoint,
    AttackableUnit ReferenceUnit,
    Vector3 Position
          )
    {

        AttackableUnit _CapturePoint = default;
        bool result =
            // Sequence name :Sequence
            (
                  GetUnitsInTargetArea(
                        out UseableUnits, 
                        ReferenceUnit, 
                        Position, 
                        200, 
                        AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable) &&
                  ForEach(UseableUnits,Unit => (
                        // Sequence name :Sequence
                        (
                              GetUnitBuffCount(
                                    out Count, 
                                    Unit,
                                    "OdinGuardianStatsByLevel") &&
                              GreaterInt(
                                    Count, 
                                    0) &&
                              SetVarAttackableUnit(
                                    out _CapturePoint, 
                                    Unit)

                        )
                  ))
            );

        CapturePoint = _CapturePoint;
        return result;
      }
}

