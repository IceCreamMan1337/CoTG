using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using BehaviourTrees.Map8;

namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.CapturePointMission.xml
/// Définit et exécute automatiquement les missions de capture de points
/// </summary>
public class CapturePointMission : AImission_bt
{
    private CapturePointMission_EvaluatorClass _evaluator;
    private CapturePointMission_LogicClass _logic;
    
    public CapturePointMission()
    {
      
    }
    public override void InitOwner(AIMissionClass owner)
    {
        base.InitOwner(owner);
        _evaluator = new CapturePointMission_EvaluatorClass();
        _logic = new CapturePointMission_LogicClass();
        _evaluator.missionowner = this.missionowner;
        _logic.missionowner = this.missionowner;
    }
    /// <summary>
    /// Exécute la solution CapturePointMission (équivalent aux BTInstance du XML)
    /// </summary>
    public override void Update()
    {
        base.Update();
        _evaluator.CapturePointMission_Evaluator();
        _logic.CapturePointMission_Logic();
    }
} 