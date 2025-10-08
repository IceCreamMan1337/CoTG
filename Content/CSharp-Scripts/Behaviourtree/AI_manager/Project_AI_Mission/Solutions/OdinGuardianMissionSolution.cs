namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.OdinGuardianMission.xml
/// Définit et exécute automatiquement les missions de gardien Odin
/// </summary>
public class OdinGuardianMissionSolution : AImission_bt
{
    private OdinGuardianMissionEvaluationClass _evaluator;
    private OdinGuardianMissionClass _logic;

    public OdinGuardianMissionSolution()
    {
        _evaluator = new OdinGuardianMissionEvaluationClass();
        _logic = new OdinGuardianMissionClass();
    }

    /// <summary>
    /// Exécute la solution OdinGuardianMission (équivalent aux BTInstance du XML)
    /// </summary>
    public bool Execute()
    {
        try
        {
            // Équivalent à BTInstance OdinGuardianMissionEvaluation
            bool evaluatorResult = _evaluator.OdinGuardianMissionEvaluation();
            if (!evaluatorResult)
            {
                return false;
            }

            // Équivalent à BTInstance OdinGuardianMission
            bool logicResult = _logic.OdinGuardianMission();
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
    /// Exécute la mission OdinGuardian pour un squad spécifique
    /// </summary>
    public bool ExecuteForSquad(AISquadClass squad, AIMissionClass mission)
    {
        try
        {
            // Configurer la mission pour ce squad
            //  _logic.ThisMission = mission;

            // Exécuter la logique
            return _logic.OdinGuardianMission();
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}