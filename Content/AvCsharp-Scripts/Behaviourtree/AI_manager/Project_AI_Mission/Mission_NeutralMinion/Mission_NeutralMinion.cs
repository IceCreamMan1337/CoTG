using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.Mission_NeutralMinion.xml
/// Définit et exécute automatiquement les missions de kill de boss neutres
/// </summary>
public class Mission_NeutralMinion : AImission_bt
{
    private MissionKillNeutralBoss_EvaluatorClass _evaluator;
    private MissionKillNeutralBoss_LogicClass _logic;
    
    public Mission_NeutralMinion()
    {
    
    }
    public override void InitOwner(AIMissionClass owner)
    {
        base.InitOwner(owner);
        _evaluator = new MissionKillNeutralBoss_EvaluatorClass();
        _logic = new MissionKillNeutralBoss_LogicClass();
        _evaluator.missionowner = this.missionowner;
        _logic.missionowner = this.missionowner;
    }
    /// <summary>
    /// Exécute la solution Mission_NeutralMinion (équivalent aux BTInstance du XML)
    /// </summary>
    public override void Update()
    {
        base.Update();
        _evaluator.MissionKillNeutralBoss_Evaluator();
        _logic.MissionKillNeutralBoss_Logic();
    }
} 