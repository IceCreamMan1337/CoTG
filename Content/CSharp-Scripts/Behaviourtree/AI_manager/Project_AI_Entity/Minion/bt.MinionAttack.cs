namespace BehaviourTrees.all;


class MinionAttackClass : AI_Characters
{
    private MinionAcquireTargetClass minionAcquireTarget = new();
    private MinionAttackTargetClass minionAttackTarget = new();
    public bool MinionAttack(
         AttackableUnit Self,
      TeamId SelfTeam,
      Vector3 SelfPosition,
      Vector3 AssistPosition
         )
    {
        return
                 // Sequence name :MinionAttack

                 minionAcquireTarget.MinionAcquireTarget(
                          Self,
                          AssistPosition,
                          SelfPosition) &&
                minionAttackTarget.MinionAttackTarget(
                          SelfTeam,
                          Self)

              ;
    }
}

