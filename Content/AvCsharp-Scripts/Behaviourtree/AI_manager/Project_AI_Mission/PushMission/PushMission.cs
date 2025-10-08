using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.PushMission.xml
/// Définit et exécute automatiquement les missions Push
/// </summary>
public class PushMission : AImission_bt
{
    private MissionPush_EvaluatorClass _evaluator;
    private MissionPush_LogicClass _logic;

    public PushMission()
    {
        _evaluator = new MissionPush_EvaluatorClass();
        _logic = new MissionPush_LogicClass();

        _evaluator.missionowner = this.missionowner;
        _logic.missionowner = this.missionowner;    
    }

    public override void InitOwner(AIMissionClass owner)
    {
        base.InitOwner(owner);

        _evaluator = new MissionPush_EvaluatorClass();
        _evaluator.missionowner = owner;

        _logic = new MissionPush_LogicClass();
        _logic.missionowner = owner;
    }
    /// <summary>
    /// Exécute la solution PushMission (équivalent aux BTInstance du XML)
    /// </summary>
    public override void Update()
    {

        base.Update();
        _evaluator.MissionPush_Evaluator();
        _logic.MissionPush_Logic();
      
    }
       
    
   
} 