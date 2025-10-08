using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DistanceHealthScoreClass : AI_Characters
{


    public bool DistanceHealthScore(
        out float _UnitScore,
     float UnitHealth,
     float Distance
        )
    {
        float UnitScore = default;
        bool result =
              // Sequence name :DistanceAndHealthScore
              (
                    SubtractFloat(
                          out DistanceScore,
                          2100,
                          Distance) &&
                    DivideFloat(
                          out DistanceScore,
                          DistanceScore,
                          2100) &&
                    MaxFloat(
                          out DistanceScore,
                          DistanceScore,
                          0) &&
                    MinFloat(
                          out DistanceScore,
                          DistanceScore,
                          1) &&
                    MultiplyFloat(
                          out DistanceScore,
                          DistanceScore,
                          DistanceScore) &&
                    MultiplyFloat(
                          out DistanceScore,
                          DistanceScore,
                          DistanceScore) &&
                    AddFloat(
                          out DistanceScore,
                          DistanceScore,
                          1) &&
                    SubtractFloat(
                          out HealthScore,
                          4500,
                          UnitHealth) &&
                    DivideFloat(
                          out HealthScore,
                          HealthScore,
                          4500) &&
                    MultiplyFloat(
                          out HealthScore,
                          HealthScore,
                          HealthScore) &&
                    MultiplyFloat(
                          out HealthScore,
                          HealthScore,
                          HealthScore) &&
                    MaxFloat(
                          out HealthScore,
                          HealthScore,
                          0.01f) &&
                    MultiplyFloat(
                          out UnitScore,
                          HealthScore,
                          DistanceScore)

              );

        _UnitScore = UnitScore;
        return result;
    }

}

