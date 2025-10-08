namespace BehaviourTrees;


class GetGuardianHasReferenceClass : OdinLayout
{


    public bool GetGuardianHasReference(
          out AttackableUnit Guardian,
    int GuardianIndex,
    AttackableUnit CapturePointGuardian0,
    AttackableUnit CapturePointGuardian1,
    AttackableUnit CapturePointGuardian2,
    AttackableUnit CapturePointGuardian3,
    AttackableUnit CapturePointGuardian4)
    {
        AttackableUnit _Guardian = default;

        bool result =
                    // Sequence name :Selector

                    // Sequence name :Sequence
                    (
                          GuardianIndex == 0 &&
                          SetVarAttackableUnit(
                                out _Guardian,
                                CapturePointGuardian0)
                    ) ||
                    // Sequence name :Sequence
                    (
                          GuardianIndex == 1 &&
                          SetVarAttackableUnit(
                                out _Guardian,
                                CapturePointGuardian1)
                    ) ||
                    // Sequence name :Sequence
                    (
                          GuardianIndex == 2 &&
                          SetVarAttackableUnit(
                                out _Guardian,
                                CapturePointGuardian2)
                    ) ||
                    // Sequence name :Sequence
                    (
                          GuardianIndex == 3 &&
                          SetVarAttackableUnit(
                                out _Guardian,
                                CapturePointGuardian3)
                    ) ||
                    // Sequence name :Sequence
                    (
                          GuardianIndex == 4 &&
                          SetVarAttackableUnit(
                                out _Guardian,
                                CapturePointGuardian4)

                    )
              ;

        Guardian = _Guardian;
        return result;
    }
}

