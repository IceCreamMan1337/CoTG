namespace BehaviourTrees.all;


class PostActionBehaviorClass : AI_Characters
{


    public bool PostActionBehavior(
        AttackableUnit Self,
     string ActionPerformed
        )
    {
        return
                    // Sequence name :ReturnFailure

                    SetVarBool(
                          out Run,
                          false) &&
                    Run == true

              ;
    }
}

