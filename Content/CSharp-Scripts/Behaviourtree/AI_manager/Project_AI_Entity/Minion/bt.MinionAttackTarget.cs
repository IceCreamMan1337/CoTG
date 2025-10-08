namespace BehaviourTrees.all;


class MinionAttackTargetClass : AI_Characters
{

    private MinionAutoAttackTargetClass minionAutoAttackTarget = new();


    public bool MinionAttackTarget(
        TeamId SelfTeam,
     AttackableUnit Self
        )
    {
        return
                    // Sequence name :MinionAttackTarget

                    GetUnitAIAttackTarget(
                          out Target) &&
                    GetUnitTeam(
                          out TargetTeam,
                          Target) &&
                    NotEqualUnitTeam(
                          TargetTeam,
                          SelfTeam) &&
                  minionAutoAttackTarget.MinionAutoAttackTarget(
                          Target,
                          Self)

              ;
    }
}

