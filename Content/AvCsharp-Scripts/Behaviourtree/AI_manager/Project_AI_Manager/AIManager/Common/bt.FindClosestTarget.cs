using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;

/// <summary>
/// Possibly exist another findclosestTarget with help of an buff 
/// we can found on odinscript : in
/// ClosestTarget.FindClosestTarget(
/// out ClosestEnemyGuardian, 
/// Self, 
///  UnitsToSearch, 
///  "", 
///    false, 
///  false, 
///   SelfPosition, 
///  true,
///   OdinGuardianBuff, 
///   true) 
/// </summary>
class FindClosestTargetClass : CommonAI
{
     public bool FindClosestTarget(
    out AttackableUnit ClosestTarget,
    IEnumerable<AttackableUnit> UnitsToSearch,
    AttackableUnit ReferenceUnit,
    bool UseVisibilityChecks,
    AttackableUnit IgnoreUnit,
    bool IgnoreUnitFlag)
      {

     AttackableUnit _ClosestTarget = default;
        bool result =
              // Sequence name :ClosestTargetFinder
              (
                    SetVarBool(
                          out ValueChanged,
                          false) &&
                    SetVarFloat(
                          out CurrentClosestDistance,
                          1E+09f) &&
                    GetUnitPosition(
                          out UnitPosition,
                          ReferenceUnit) &&
                    ForEach(UnitsToSearch ,Unit => (
                          // Sequence name :CheckClosestTarget
                          (
                                // Sequence name :VisibilityChecks
                                (
                                      UseVisibilityChecks == false &&
                                      // Sequence name :Visible
                                      (
                                            UseVisibilityChecks == true &&
                                            TestUnitIsVisibleToTeam(
                                                  ReferenceUnit,
                                                  Unit,
                                                  true)
                                      )
                                ) &&
                                // Sequence name :IgnoreUnitChecks
                                (
                                      IgnoreUnitFlag == false &&
                                      // Sequence name :CheckAgainstUnitToIgnore
                                      (
                                            IgnoreUnitFlag == true &&
                                            NotEqualUnit(
                                                  Unit,
                                                  IgnoreUnit)
                                      )
                                ) &&
                                DistanceBetweenObjectAndPoint(
                                      out Distance,
                                      Unit,
                                      UnitPosition) &&
                                LessFloat(
                                      Distance,
                                      CurrentClosestDistance) &&
                                SetVarFloat(
                                      out CurrentClosestDistance,
                                      Distance) &&
                                SetVarAttackableUnit(
                                      out _ClosestTarget,
                                      Unit) &&
                                SetVarBool(
                                      out ValueChanged,
                                      true)
                          ))
                    ) &&
                    ValueChanged == true

              );
                  ClosestTarget = _ClosestTarget;
                  return result;
      }
}

