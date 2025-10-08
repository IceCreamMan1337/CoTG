namespace BehaviourTrees.all;


class AutoAttack_MeleeClass : AI_Characters
{


    public bool AutoAttack_Melee(
              out bool _IssuedAttack,
     out AttackableUnit _IssuedAttackTarget,
     AttackableUnit Target,
     AttackableUnit Self,
     bool PrevIssuedAttack,
     AttackableUnit PrevIssuedAttackTarget
        )
    {

        bool IssuedAttack = default;
        AttackableUnit IssuedAttackTarget = default;

        bool result =
                    // Sequence name :Sequence

                    SetUnitAIAttackTarget(
                          Target) &&
                    IssueChaseOrder()

              ;
        _IssuedAttack = IssuedAttack;
        _IssuedAttackTarget = IssuedAttackTarget;
        return result;
    }
}

