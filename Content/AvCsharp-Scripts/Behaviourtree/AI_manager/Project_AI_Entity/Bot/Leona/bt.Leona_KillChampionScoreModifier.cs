using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Leona_KillChampionScoreModifierClass : AI_Characters 
{
    

     public bool Leona_KillChampionScoreModifier(
           out int __KillChampionScore,
      AttackableUnit Self,
      AttackableUnit TempTarget,
      int KillChampionScore
         )
      {

        int _KillChampionScore = KillChampionScore;
        bool result =
              // Sequence name :ModifyKillScoreForThreeTalonStrike
              (
                    TestUnitHasBuff(
                          Self,
                          default,
                          "LeonaShieldOfDaybreak",
                          true) &&
                    GetDistanceBetweenUnits(
                          out DistanceToTarget,
                          TempTarget,
                          Self) &&
                    LessEqualFloat(
                          DistanceToTarget,
                          250) &&
                    SetVarInt(
                          out KillChampionScore,
                          10)

              );


        __KillChampionScore = _KillChampionScore;
        return result;

      }
}

