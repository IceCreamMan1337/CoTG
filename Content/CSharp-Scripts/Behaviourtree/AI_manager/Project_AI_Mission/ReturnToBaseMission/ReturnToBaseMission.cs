namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.ReturnToBaseMission.xml
/// Définit et exécute automatiquement les missions de retour en base
/// </summary>
public class ReturnToBaseMission : AImission_bt
{
    private ReturnToBaseMission_EvaluatorClass _evaluator;
    private ReturnToBaseMission_LogicClass _logic;

    public ReturnToBaseMission()
    {
    }
    public override void InitOwner(AIMissionClass owner)
    {
        base.InitOwner(owner);

        _evaluator = new ReturnToBaseMission_EvaluatorClass();
        _evaluator.missionowner = owner;

        _logic = new ReturnToBaseMission_LogicClass();
        _logic.missionowner = owner;
    }
    /// <summary>
    /// Exécute la solution ReturnToBaseMission (équivalent aux BTInstance du XML)
    /// </summary>
    public override void Update()
    {
        base.Update();
        _evaluator.ReturnToBaseMission_Evaluator();
        _logic.ReturnToBaseMission_Logic();
    }
}