using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class KillChampionScoreModifierClass : AI_Characters 
{
     

     public bool KillChampionScoreModifier(
          out int __KillChampionScore,
      AttackableUnit Self,
      AttackableUnit TempTarget,
      int KillChampionScore
         )
      {

        int _KillChampionScore = KillChampionScore;
        bool result =
              // Sequence name :ReturnFailure
              (
                    SetVarBool(
                          out Run,
                          false) &&
                    Run == true

              );
        __KillChampionScore = _KillChampionScore;
        return result;
      }
}

