/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class FindClosestTarget : BehaviourTree 
{
      out AttackableUnit ClosestTarget;
      AttackableUnitCollection UnitsToSearch;
      AttackableUnit ReferenceUnit;
      bool UseVisibilityChecks;
      AttackableUnit IgnoreUnit;
      bool IgnoreUnitFlag;

      bool FindClosestTarget()
      {
      return
            // Sequence name :ClosestTargetFinder
            (
                  SetVarBool(
                        out ValueChanged, 
                        False) &&
                  SetVarFloat(
                        out CurrentClosestDistance, 
                        1E+09) &&
                  GetUnitPosition(
                        out UnitPosition, 
                        ReferenceUnit) &&
                  UnitsToSearch.ForEach( Unit => (          
                  // Sequence name :CheckClosestTarget
                        (
                              // Sequence name :VisibilityChecks
                              (
                                    UseVisibilityChecks == False                                    // Sequence name :Visible
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
                                    IgnoreUnitFlag == False                                    // Sequence name :CheckAgainstUnitToIgnore
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
                                    out ClosestTarget, 
                                    Unit) &&
                              SetVarBool(
                                    out ValueChanged, 
                                    true)
                        )
                  ) &&
                  ValueChanged == true

            );
      }
}

*/