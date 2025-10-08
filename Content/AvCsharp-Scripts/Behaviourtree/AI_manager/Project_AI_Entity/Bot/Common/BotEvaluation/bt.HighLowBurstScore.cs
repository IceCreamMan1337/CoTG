using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class HighLowBurstScoreClass : AI_Characters 
{


     public bool HighLowBurstScore(
               out float _Score,
      bool FindHighOrLowScore,
      float DamageOverTime,
      AttackableUnit Self
         )
      {
        float Score = default;
        bool result =
              // Sequence name :HighLowBurstScore
              (
                    GetUnitMaxHealth(
                          out MaxHealth,
                          Self) &&
                    DivideFloat(
                          out DamageRatio,
                          DamageOverTime,
                          MaxHealth) &&
                    // Sequence name :HighLowBurstScore
                    (
                          // Sequence name :LowScore
                          (
                                FindHighOrLowScore == false &&
                                MultiplyFloat(
                                      out Score,
                                      DamageRatio,
                                      -20) &&
                                AddFloat(
                                      out Score,
                                      Score,
                                      1) &&
                                MinFloat(
                                      out Score,
                                      Score,
                                      1) &&
                                MaxFloat(
                                      out Score,
                                      Score,
                                      0)
                          ) ||
                          // Sequence name :HighScore
                          (
                                FindHighOrLowScore == true &&
                                MultiplyFloat(
                                      out Score,
                                      DamageRatio,
                                      4) &&
                                AddFloat(
                                      out Score,
                                      Score,
                                      0) &&
                                MinFloat(
                                      out Score,
                                      Score,
                                      1) &&
                                MaxFloat(
                                      out Score,
                                      Score,
                                      0)

                          )
                    )
              );
        _Score = Score;
        return result;
      }
}

