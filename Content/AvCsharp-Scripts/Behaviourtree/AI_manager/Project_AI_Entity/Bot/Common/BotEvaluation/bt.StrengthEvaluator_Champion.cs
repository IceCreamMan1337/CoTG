using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class StrengthEvaluator_ChampionClass : AI_Characters 
{
     

     public bool StrengthEvaluator_Champion(
          out float _ModifiedChampionValue,
      AttackableUnit ChampionUnit,
      AttackableUnit Self,
      float ChampionValue,
      int FriendlyChampCount,
      int EnemyChampCount
         )
      {
        float ModifiedChampionValue = default;

        bool result =
              // Sequence name :Sequence
              (
                    GetUnitLevel(
                          out SelfLevel,
                          Self) &&
                    GetUnitLevel(
                          out ChampionLevel,
                          ChampionUnit) &&
                    SubtractInt(
                          out LevelDiff,
                          ChampionLevel,
                          SelfLevel) &&
                    MinInt(
                          out LevelDiff,
                          LevelDiff,
                          5) &&
                    MaxInt(
                          out LevelDiff,
                          LevelDiff,
                          -3) &&
                    MultiplyFloat(
                          out Modifier,
                          LevelDiff,
                          0.3f) &&
                    AddFloat(
                          out Modifier,
                          Modifier,
                          1) &&
                    GetUnitMaxHealth(
                          out ChampionMaxHealth,
                          ChampionUnit) &&
                    GetUnitCurrentHealth(
                          out ChampionCurrentHealth,
                          ChampionUnit) &&
                    DivideFloat(
                          out ChampionHealthRatio,
                          ChampionCurrentHealth,
                          ChampionMaxHealth) &&
                    InterpolateLine(
                          out HealthModifier,
                          0,
                          0.4f,
                          0.5f,
                          1,
                          0,
                          1,
                          ChampionHealthRatio) &&
                    MultiplyFloat(
                          out Modifier,
                          Modifier,
                          HealthModifier) &&
                    MultiplyFloat(
                          out ModifiedChampionValue,
                          Modifier,
                          ChampionValue) &&
                    GetUnitTeam(
                          out SelfTeam,
                          Self) &&
                    GetUnitTeam(
                          out ChampionTeam,
                          ChampionUnit) &&
                    // Sequence name :ChampRatioModifier
                    (
                          // Sequence name :SameTeam
                          (
                                SelfTeam == ChampionTeam &&
                                MaxInt(
                                      out EnemyChampCount,
                                      EnemyChampCount,
                                      1) &&
                                DivideFloat(
                                      out ChampRatioModifier,
                                      FriendlyChampCount,
                                      EnemyChampCount) &&
                                MaxFloat(
                                      out ChampRatioModifier,
                                      ChampRatioModifier,
                                      1)
                          ) ||
                          // Sequence name :DifferentTeam
                          (
                                SetVarFloat(
                                      out ChampRatioModifier,
                                      1)
                          )
                    ) &&
                    MultiplyFloat(
                          out ModifiedChampionValue,
                          ModifiedChampionValue,
                          ChampRatioModifier)

              );
        _ModifiedChampionValue = ModifiedChampionValue;
        return result;
      }
}

