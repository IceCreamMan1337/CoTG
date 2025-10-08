using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

namespace BehaviourTrees;

/// <summary>
/// Solution équivalente à sn.KillChampionMission.xml
/// Définit et exécute automatiquement les missions de kill de champions
/// </summary>
public class KillChampionMissionSolution : AImission_bt
{
    private KillChampionMission_EvaluatorClass _evaluator;
 //   private KillChampionMission_LogicClass _logic;
    
    public KillChampionMissionSolution()
    {
        _evaluator = new KillChampionMission_EvaluatorClass();
    //    _logic = new KillChampionMission_LogicClass();
    }
    
    /// <summary>
    /// Exécute la solution KillChampionMission (équivalent aux BTInstance du XML)
    /// </summary>
    public bool Execute()
    {
        try
        {            
            // Équivalent à BTInstance KillChampionMission_Evaluator
            bool evaluatorResult = _evaluator.KillChampionMission_Evaluator();
            if (!evaluatorResult)
            {
                return false;
            }
            
            // Équivalent à BTInstance KillChampionMission_Logic
          //  bool logicResult = _logic.KillChampionMission_Logic();
          /*  if (!logicResult)
            {
                return false;
            }
            
            return true;*/
        }
        catch (Exception ex)
        {
            return false;
        }
        return false;
    }
    
    /// <summary>
    /// Exécute la mission KillChampion pour un squad spécifique
    /// </summary>
    public bool ExecuteForSquad(AISquadClass squad, AIMissionClass mission)
    {
        try
        {            
            // Configurer la mission pour ce squad
            //_logic.ThisMission = mission;
            
            // Exécuter la logique
         //   return _logic.KillChampionMission_Logic();
        }
        catch (Exception ex)
        {
            return false;
        }
        return false;
    }
} 