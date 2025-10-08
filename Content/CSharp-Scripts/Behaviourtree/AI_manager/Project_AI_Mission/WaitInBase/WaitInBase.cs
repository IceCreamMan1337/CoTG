namespace BehaviourTrees;

/// <summary>
/// Solution equivalent to sn.WaitInBase.xml
/// Defines and automatically executes base waiting missions
/// </summary>
public class WaitInBase : AImission_bt
{
    private WaitInBase_EvaluatorClass _evaluator;
    private WaitInBase_LogicClass _logic;

    public WaitInBase()
    {

    }
    public override void InitOwner(AIMissionClass owner)
    {
        base.InitOwner(owner);

        _evaluator = new WaitInBase_EvaluatorClass();
        _evaluator.missionowner = owner;
        _logic = new WaitInBase_LogicClass();
        _logic.missionowner = owner;
    }
    /// <summary>
    /// Executes the WaitInBase solution (equivalent to XML BTInstance)
    /// </summary>
    public override void Update()
    {
        base.Update();
        _evaluator.WaitInBase_Evaluator();
        _logic.WaitInBase_Logic();
    }
}