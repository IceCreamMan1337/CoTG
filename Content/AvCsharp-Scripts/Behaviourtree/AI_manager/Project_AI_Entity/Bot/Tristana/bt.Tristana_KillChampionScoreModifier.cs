using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Tristana_KillChampionScoreModifierClass : AI_Characters 
{
      

     public bool Tristana_KillChampionScoreModifier(
         out int __KillChampionScore,
      AttackableUnit Self,
      AttackableUnit TempTarget,
      int KillChampionScore
         )
      {

        int _KillChampionScore = KillChampionScore;
        bool result =
              // Sequence name :TargetRocketJumped?
              (
                    TestUnitHasBuff(
                          TempTarget, default
                          ,
                          "RocketJumpSlow",
                          true) &&
                    SetVarInt(
                          out KillChampionScore,
                          10)

              );

         __KillChampionScore = _KillChampionScore;
        return result;

      }
}

