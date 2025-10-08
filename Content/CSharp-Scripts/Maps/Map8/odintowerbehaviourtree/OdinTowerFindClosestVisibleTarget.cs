namespace BehaviourTrees;


class OdinTowerFindClosestVisibleTargetClass : OdinLayout
{


    public bool OdinTowerFindClosestVisibleTarget(
         out AttackableUnit CurrentClosestTarget,
     out bool TargetFound,
   IEnumerable<AttackableUnit> TargetCollection,
   Vector3 SelfPosition,
     AttackableUnit Self
         )
    {

        AttackableUnit _CurrentClosestTarget = default;

        bool _TargetFound = default;
        bool result =
                  // Sequence name :OdinTowerFindClosestVisibleTarget

                  SetVarBool(
                        out _TargetFound,
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
                                    out _CurrentClosestTarget,
                                    Attacker) &&
                              SetVarFloat(
                                    out CurrentClosestDistance,
                                    Distance) &&
                              SetVarBool(
                                    out _TargetFound,
                                    true)


                  )
            ;
        CurrentClosestTarget = _CurrentClosestTarget;
        TargetFound = _TargetFound;
        return result;
    }
}

