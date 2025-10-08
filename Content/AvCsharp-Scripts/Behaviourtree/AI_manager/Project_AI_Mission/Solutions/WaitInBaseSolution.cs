using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

namespace BehaviourTrees;

/// <summary>
    /// Solution equivalent to sn.WaitInBase.xml
    /// Defines and automatically executes WaitInBase missions
/// </summary>
public class WaitInBaseSolution : AImission_bt
{
    private WaitInBase_EvaluatorClass _evaluator;
    private WaitInBase_LogicClass _logic;
    
    public WaitInBaseSolution()
    {
        _evaluator = new WaitInBase_EvaluatorClass();
        _logic = new WaitInBase_LogicClass();
    }
    
    /// <summary>
    /// Executes the WaitInBase solution (equivalent to XML BTInstance)
    /// </summary>
    public bool Execute()
    {
        try
        {            
            // Equivalent to BTInstance WaitInBase_Evaluator
            bool evaluatorResult = _evaluator.WaitInBase_Evaluator();
            if (!evaluatorResult)
            {
                return false;
            }
            
            // Equivalent to BTInstance WaitInBase_Logic
            bool logicResult = _logic.WaitInBase_Logic();
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
    /// Executes the WaitInBase mission for a specific squad
    /// </summary>
    public bool ExecuteForSquad(AISquadClass squad, AIMissionClass mission)
    {
        try
        {            
            // Configure the mission for this squad
          //  _logic.ThisMission = mission;
            
            // Execute the logic
            return _logic.WaitInBase_Logic();
        }
        catch (Exception ex)
        {
            return false;
        }
    }
} 