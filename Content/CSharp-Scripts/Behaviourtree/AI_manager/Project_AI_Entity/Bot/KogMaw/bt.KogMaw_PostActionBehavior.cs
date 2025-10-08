namespace BehaviourTrees.all;


class KogMaw_PostActionBehaviorClass : AI_Characters
{


    public bool KogMaw_PostActionBehavior(
         AttackableUnit Self,
     string ActionPerformed
        )
    {
        return
                    // Sequence name :Sequence

                    NotEqualString(
                          ActionPerformed,
                          "KillChampion")

              ;
    }
}

