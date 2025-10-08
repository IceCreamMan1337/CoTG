using BehaviourTrees.Map8;

namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.KillChampionMission.xml
/// Définit et exécute automatiquement les missions de kill de champions
/// </summary>
public class KillChampionMission : AImission_bt
{
    private KillChampionMission_EvaluatorClass _evaluator;
    private KillChampionMission_LogicClass _logic;

    public KillChampionMission()
    {

    }
    public override void InitOwner(AIMissionClass owner)
    {
        base.InitOwner(owner);
        _evaluator = new KillChampionMission_EvaluatorClass();
        _logic = new KillChampionMission_LogicClass();
        _evaluator.missionowner = this.missionowner;
        _logic.missionowner = this.missionowner;
    }
    /// <summary>
    /// Exécute la solution KillChampionMission (équivalent aux BTInstance du XML)
    /// </summary>
    public override void Update()
    {
        base.Update();
        _evaluator.KillChampionMission_Evaluator();
        _logic.KillChampionMission_Logic();
    }
}