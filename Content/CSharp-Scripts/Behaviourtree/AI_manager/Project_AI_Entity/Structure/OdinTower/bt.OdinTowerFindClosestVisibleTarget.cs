namespace BehaviourTrees.all;


class OdinTowerFindClosestVisibleTargetClass : AI_Characters
{


    public bool OdinTowerFindClosestVisibleTarget(
        out AttackableUnit _CurrentClosestTarget,
     out bool _TargetFound,
     IEnumerable<AttackableUnit> TargetCollection,
     Vector3 SelfPosition,
     AttackableUnit Self)
    {

        AttackableUnit CurrentClosestTarget = default;
        bool TargetFound = default;
        bool result =
                    // Sequence name :OdinTowerFindClosestVisibleTarget

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
        _CurrentClosestTarget = CurrentClosestTarget;
        _TargetFound = TargetFound;
        return result;
    }
}

