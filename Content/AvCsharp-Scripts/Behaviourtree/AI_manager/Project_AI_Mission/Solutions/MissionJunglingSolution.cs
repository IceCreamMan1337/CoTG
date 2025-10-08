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
public class MissionJunglingSolution : AImission_bt
{
    private MissionJungling_EvaluatorClass _evaluator;
    private MissionJungling_LogicClass _logic;

    public MissionJunglingSolution()
    {
        _evaluator = new MissionJungling_EvaluatorClass();
        _logic = new MissionJungling_LogicClass();
    }

    /// <summary>
    /// Exécute la solution MissionJungling (équivalent aux BTInstance du XML)
    /// </summary>
    public bool Execute()
    {
        try
        {
            // Équivalent à BTInstance MissionJungling_Evaluator
            bool evaluatorResult = _evaluator.MissionJungling_Evaluator();
            if (!evaluatorResult)
            {
                return false;
            }

            // Équivalent à BTInstance MissionJungling_Logic
            bool logicResult = _logic.MissionJungling_Logic();
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
    /// Exécute la mission Jungling pour un squad spécifique
    /// </summary>
    public bool ExecuteForSquad(AISquadClass squad, AIMissionClass mission)
    {
        try
        {
            // Configurer la mission pour ce squad
            //  _logic.ThisMission = mission;

            // Exécuter la logique
            return _logic.MissionJungling_Logic();
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}