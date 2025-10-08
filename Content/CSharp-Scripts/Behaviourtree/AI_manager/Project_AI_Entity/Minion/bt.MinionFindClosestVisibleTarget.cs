namespace BehaviourTrees.all;


class MinionFindClosestVisibleTargetClass : AI_Characters
{


    public bool MinionFindClosestVisibleTarget(
        out AttackableUnit _CurrentClosestTarget,
     out bool _TargetFound,
     IEnumerable<AttackableUnit> TargetCollection,
     Vector3 SelfPosition,
     AttackableUnit Self
        )
    {
        bool TargetFound = default;
        AttackableUnit CurrentClosestTarget = default;

        bool result =
                    // Sequence name :MinionFindClosestVisibleTarget

                    SetVarBool(
                          out TargetFound,
                          false) &&
                    ForEach(TargetCollection, Attacker =>
                                // Sequence name :Evaluate Target

                                TestUnitIsVisible(
                                      Self,
                                      Attacker,
                                      true) &&
                                DistanceBetweenObjectAndPoint(
                                      out Distance,
                                      Attacker,
                                      SelfPosition) &&
                                LessFloat(
                                      Distance,
                                      CurrentClosestDistance) &&
                                SetVarAttackableUnit(
                                      out CurrentClosestTarget,
                                      Attacker) &&
                                SetVarFloat(
                                      out CurrentClosestDistance,
                                      Distance) &&
                                SetVarBool(
                                      out TargetFound,
                                      true)


                    )
              ;
        _TargetFound = TargetFound;
        _CurrentClosestTarget = CurrentClosestTarget;
        return result;

    }
}

