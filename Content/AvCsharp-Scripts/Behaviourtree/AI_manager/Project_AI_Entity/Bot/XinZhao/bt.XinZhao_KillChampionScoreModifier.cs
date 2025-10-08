using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class XinZhao_KillChampionScoreModifierClass  : AI_Characters 
{
   

     public bool XinZhao_KillChampionScoreModifier(
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
                        Self, default
                        ,
                        "XenZhaoComboAutoFinish",
                        true) &&
                  GetDistanceBetweenUnits(
                        out DistanceToTarget,
                        TempTarget,
                        Self) &&
                  LessEqualFloat(
                        DistanceToTarget,
                        300) &&
                  SetVarInt(
                        out KillChampionScore,
                        10)

            );


        __KillChampionScore = _KillChampionScore;
        return result;

    }
}

