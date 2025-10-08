namespace BehaviourTrees.all;


class BlitzcrankPostActionBehaviorClass : AI_Characters
{


    public bool BlitzcrankPostActionBehavior(
         string ActionPerformed,
     AttackableUnit Self
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

