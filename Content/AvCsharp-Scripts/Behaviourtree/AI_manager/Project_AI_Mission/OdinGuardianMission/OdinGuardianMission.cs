using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.OdinGuardianMission.xml
/// Définit et exécute automatiquement les missions de gardien Odin
/// </summary>
public class OdinGuardianMission : AImission_bt
{
    private OdinGuardianMissionEvaluationClass _evaluator;
    private OdinGuardianMissionClass _logic;
    
    public OdinGuardianMission()
    {

    }
    public override void InitOwner(AIMissionClass owner)
    {
        base.InitOwner(owner);

        _evaluator = new OdinGuardianMissionEvaluationClass();
        _logic = new OdinGuardianMissionClass();
        _evaluator.missionowner = this.missionowner;
        _logic.missionowner = this.missionowner;
    }
    /// <summary>
    /// Exécute la solution OdinGuardianMission (équivalent aux BTInstance du XML)
    /// </summary>
    public override void Update()
    {
        base.Update();
        _evaluator.OdinGuardianMissionEvaluation();
        _logic.OdinGuardianMission();
    }
} 