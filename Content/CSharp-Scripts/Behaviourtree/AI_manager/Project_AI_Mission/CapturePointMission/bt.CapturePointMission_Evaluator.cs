namespace BehaviourTrees;


class CapturePointMission_EvaluatorClass : AImission_bt
{

    public bool CapturePointMission_Evaluator()
    {
        return

                  // Sequence name :Sequence

                  SetVarBool(
                        out Run,
                        true)

            ;
    }
}

