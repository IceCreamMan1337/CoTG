namespace BehaviourTrees;


class FindClosestTargetBuffClass : CommonAI
{


    public bool FindClosestTargetBuff(
           out AttackableUnit ClosestTarget,
   AttackableUnit ReferenceUnit,
   IEnumerable<AttackableUnit> UnitsToSearch,
   AttackableUnit IgnoreUnit,
   bool IgnoreUnitFlag,
   bool UseVisibilityChecks,
   Vector3 ReferencePosition,
   bool UseReferencePosition,
   string BuffName,
   bool TestForBuff)
    {

        AttackableUnit _ClosestTarget = default;


        bool result =
                  // Sequence name :ClosestTargetFinder

                  SetVarBool(
                        out ValueChanged,
                        false) &&
                  SetVarFloat(
                        out CurrentClosestDistance,
                        1E+09f) &&
                  // Sequence name :GetReferencePosition
                  (
                        // Sequence name :Sequence
                        (
                              UseReferencePosition == true &&
                              SetVarVector(
                                    out UnitPosition,
                                    ReferencePosition)
                        ) ||
                        GetUnitPosition(
                              out UnitPosition,
                              ReferenceUnit)
                  ) &&
                  ForEach(UnitsToSearch, Unit =>
                                    // Sequence name :CheckClosestTarget

                                    // Sequence name :VisibilityChecks

                                    UseVisibilityChecks == false &&
                                          // Sequence name :Visible

                                          UseVisibilityChecks == true &&
                                          TestUnitIsVisibleToTeam(
                                                ReferenceUnit,
                                                Unit,
                                                true)

                               &&
                                    // Sequence name :IgnoreUnitChecks

                                    IgnoreUnitFlag == false &&
                                          // Sequence name :CheckAgainstUnitToIgnore

                                          IgnoreUnitFlag == true &&
                                          NotEqualUnit(
                                                Unit,
                                                IgnoreUnit)

                               &&
                                    // Sequence name :TestBuffName

                                    TestForBuff == false &&
                                          // Sequence name :CheckAgainstUnitToIgnore

                                          TestForBuff == true &&
                                          TestUnitHasBuff(
                                                Unit,
                                                null,
                                                BuffName,
                                                true)

                               &&
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

                  ) &&
                  ValueChanged == true

            ;

        ClosestTarget = _ClosestTarget;
        return result;

    }
}

