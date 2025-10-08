namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.BasicTutorialPushMission.xml
/// Définit et exécute automatiquement les missions Push pour le tutoriel
/// </summary>
public class BasicTutorialPushMission : AImission_bt
{
    private MissionPush_EvaluatorClass _evaluator;
    private MissionPush_LogicClass _logic;

    public BasicTutorialPushMission()
    {
        _evaluator = new MissionPush_EvaluatorClass();
        _logic = new MissionPush_LogicClass();
    }

    /// <summary>
    /// Exécute la solution BasicTutorialPushMission (équivalent aux BTInstance du XML)
    /// </summary>
    public bool Execute()
    {
        try
        {
            // Équivalent à BTInstance MissionPush_Evaluator
            bool evaluatorResult = _evaluator.MissionPush_Evaluator();
            if (!evaluatorResult)
            {
                return false;
            }

            // Équivalent à BTInstance MissionPush_Logic
            bool logicResult = _logic.MissionPush_Logic();
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
    /// Exécute la mission BasicTutorialPushMission pour un squad spécifique
    /// </summary>
    public bool ExecuteForSquad(AISquadClass squad, AIMissionClass mission)
    {
        try
        {
            // Configurer la mission pour ce squad
            // _logic.ThisMission = mission;

            // Exécuter la logique
            return _logic.MissionPush_Logic();
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}