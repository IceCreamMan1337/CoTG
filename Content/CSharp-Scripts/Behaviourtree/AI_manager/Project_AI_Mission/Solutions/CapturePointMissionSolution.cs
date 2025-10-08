namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.CapturePointMission.xml
/// Définit et exécute automatiquement les missions de capture de points
/// </summary>
public class CapturePointMissionSolution : AImission_bt
{
    private CapturePointMission_EvaluatorClass _evaluator;
    private CapturePointMission_LogicClass _logic;

    public CapturePointMissionSolution()
    {
        _evaluator = new CapturePointMission_EvaluatorClass();
        _logic = new CapturePointMission_LogicClass();
    }

    /// <summary>
    /// Exécute la solution CapturePointMission (équivalent aux BTInstance du XML)
    /// </summary>
    public bool Execute()
    {
        try
        {
            // Équivalent à BTInstance CapturePointMission_Evaluator
            bool evaluatorResult = _evaluator.CapturePointMission_Evaluator();
            if (!evaluatorResult)
            {
                return false;
            }

            // Équivalent à BTInstance CapturePointMission_Logic
            bool logicResult = _logic.CapturePointMission_Logic();
            if (!logicResult)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Exécute la mission CapturePoint pour un squad spécifique
    /// </summary>
    public bool ExecuteForSquad(AISquadClass squad, AIMissionClass mission)
    {
        try
        {
            // Configurer la mission pour ce squad
            //  _logic.ThisMission = mission;

            // Exécuter la logique
            return _logic.CapturePointMission_Logic();
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}