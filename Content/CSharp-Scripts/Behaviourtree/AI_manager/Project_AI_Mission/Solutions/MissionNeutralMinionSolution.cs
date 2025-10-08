namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.Mission_NeutralMinion.xml
/// Définit et exécute automatiquement les missions de kill de boss neutres
/// </summary>
public class MissionNeutralMinionSolution : AImission_bt
{
    private MissionKillNeutralBoss_EvaluatorClass _evaluator;
    private MissionKillNeutralBoss_LogicClass _logic;

    public MissionNeutralMinionSolution()
    {
        _evaluator = new MissionKillNeutralBoss_EvaluatorClass();
        _logic = new MissionKillNeutralBoss_LogicClass();
    }

    /// <summary>
    /// Exécute la solution MissionNeutralMinion (équivalent aux BTInstance du XML)
    /// </summary>
    public bool Execute()
    {
        try
        {
            // Équivalent à BTInstance MissionKillNeutralBoss_Evaluator
            bool evaluatorResult = _evaluator.MissionKillNeutralBoss_Evaluator();
            if (!evaluatorResult)
            {
                return false;
            }

            // Équivalent à BTInstance MissionKillNeutralBoss_Logic
            bool logicResult = _logic.MissionKillNeutralBoss_Logic();
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
    /// Exécute la mission NeutralMinion pour un squad spécifique
    /// </summary>
    public bool ExecuteForSquad(AISquadClass squad, AIMissionClass mission)
    {
        try
        {
            // Configurer la mission pour ce squad
            // _logic.ThisMission = mission;

            // Exécuter la logique
            return _logic.MissionKillNeutralBoss_Logic();
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}