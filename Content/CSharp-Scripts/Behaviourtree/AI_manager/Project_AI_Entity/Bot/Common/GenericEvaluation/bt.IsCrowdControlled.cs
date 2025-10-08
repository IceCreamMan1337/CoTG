namespace BehaviourTrees.all;


class IsCrowdControlledClass : AI_Characters
{


    public bool IsCrowdControlled(
         out bool __IsCrowdControlled,
     AttackableUnit Target
        )
    {
        bool _IsCrowdControlled = default;
        bool result =
                    // Sequence name : ChecksForCC

                    (SetVarBool(
                          out _IsCrowdControlled,
                          false) &&
                                // Sequence name :MaskFailure

                                // Sequence name :Sequence

                                // Sequence name :CC_Checks
                                (
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                           BuffType.BLIND,
                                            true)
                                      ||
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                         BuffType.FEAR,
                                            true)
                                      ||
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                      BuffType.CHARM,
                                            true)
                                      ||
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                     BuffType.SILENCE,
                                            true)
                                      ||
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                   BuffType.SLEEP,
                                            true)
                                      ||
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                    BuffType.SLOW,
                                            true)
                                      ||
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                     BuffType.STUN,
                                            true)
                                      ||
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                         BuffType.SUPPRESSION,
                                            true)
                                      ||
                                      TestUnitHasAnyBuffOfType(
                                            Target,
                                           BuffType.TAUNT,
                                            true)
                                ) &&
                                SetVarBool(
                                      out _IsCrowdControlled,
                                      true))


                     || MaskFailure()
              ;
        __IsCrowdControlled = _IsCrowdControlled;
        return result;
    }
}

