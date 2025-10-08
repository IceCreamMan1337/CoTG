using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Fiddlesticks_KillChampionScoreModifierClass : AI_Characters 
{
   
     public bool Fiddlesticks_KillChampionScoreModifier(
            out int __KillChampionScore,
      AttackableUnit Self,
      AttackableUnit TempTarget,
      int KillChampionScore
         )
    {

        int _KillChampionScore = KillChampionScore;
        bool result =
              // Sequence name :CrowstormModifier
              (
                    TestUnitHasBuff(
                          Self,
                           default
                          ,
                          "Crowstorm",
                          true) &&
                    SetVarInt(
                          out KillChampionScore,
                          10)

              );

        __KillChampionScore = _KillChampionScore;
        return result;
      }
}

