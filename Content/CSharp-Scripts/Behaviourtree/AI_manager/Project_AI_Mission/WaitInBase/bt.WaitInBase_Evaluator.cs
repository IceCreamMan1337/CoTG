namespace BehaviourTrees;


class WaitInBase_EvaluatorClass : AImission_bt
{

    public bool WaitInBase_Evaluator()
    {
        return
                     // Sequence name :Sequence

                     SetVarBool(
                           out Run,
                           true)

               ;
    }
}

