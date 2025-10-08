using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.MissionJungling.xml
/// Définit et exécute automatiquement les missions de jungling
/// </summary>
public class MissionJungling : AImission_bt
{
    private MissionJungling_EvaluatorClass _evaluator;
    private MissionJungling_LogicClass _logic;
    
    public MissionJungling()
    {

    }
    public override void InitOwner(AIMissionClass owner)
    {
        base.InitOwner(owner);

        _evaluator = new MissionJungling_EvaluatorClass();
        _logic = new MissionJungling_LogicClass();
        _evaluator.missionowner = this.missionowner;
        _logic.missionowner = this.missionowner;
    }
    /// <summary>
    /// Exécute la solution MissionJungling (équivalent aux BTInstance du XML)
    /// </summary>
    public override void Update()
    {
        base.Update();
        _evaluator.MissionJungling_Evaluator();
        _logic.MissionJungling_Logic();
    }
} 