namespace BehaviourTrees;

/// <summary>
/// Solution equivalent to sn.ReturnToBaseMission.xml
/// Defines and automatically executes return to base missions
/// </summary>
public class ReturnToBaseMissionSolution : AImission_bt
{
    private ReturnToBaseMission_EvaluatorClass _evaluator;
    private ReturnToBaseMission_LogicClass _logic;

    public ReturnToBaseMissionSolution()
    {
        _evaluator = new ReturnToBaseMission_EvaluatorClass();
        _logic = new ReturnToBaseMission_LogicClass();
    }

    /// <summary>
    /// Executes the ReturnToBaseMission solution (equivalent to XML BTInstance)
    /// </summary>
    public bool Execute()
    {
        try
        {
            // Equivalent to BTInstance ReturnToBaseMission_Evaluator
            bool evaluatorResult = _evaluator.ReturnToBaseMission_Evaluator();
            if (!evaluatorResult)
            {
                return false;
            }

            // Equivalent to BTInstance ReturnToBaseMission_Logic
            bool logicResult = _logic.ReturnToBaseMission_Logic();
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
    /// Executes the ReturnToBase mission for a specific squad
    /// </summary>
    public bool ExecuteForSquad(AISquadClass squad, AIMissionClass mission)
    {
        try
        {
            // Configure the mission for this squad
            // _logic.ThisMission = mission;

            // Execute the logic
            return _logic.ReturnToBaseMission_Logic();
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}