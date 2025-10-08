namespace BehaviourTrees;


class KillChampionMission_EvaluatorClass : AImission_bt
{

    public bool KillChampionMission_Evaluator()
    {
        return
                  // Sequence name :Sequence

                  SetVarBool(
                        out Run,
                        true)

            ;
    }
}

