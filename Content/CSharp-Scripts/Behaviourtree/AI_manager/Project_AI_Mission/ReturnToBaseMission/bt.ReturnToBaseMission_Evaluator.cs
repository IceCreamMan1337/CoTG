namespace BehaviourTrees;


class ReturnToBaseMission_EvaluatorClass : AImission_bt
{

    public bool ReturnToBaseMission_Evaluator()
    {
        return
                  // Sequence name :Sequence

                  SetVarBool(
                        out Run,
                        true)

            ;
    }
}

