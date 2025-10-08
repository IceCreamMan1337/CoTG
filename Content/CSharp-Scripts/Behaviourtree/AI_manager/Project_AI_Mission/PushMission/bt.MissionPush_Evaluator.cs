namespace BehaviourTrees;


class MissionPush_EvaluatorClass : AImission_bt
{

    public bool MissionPush_Evaluator()
    {
        return
                    // Sequence name :Sequence

                    SetVarBool(
                          out Run,
                          true)

              ;
    }
}

