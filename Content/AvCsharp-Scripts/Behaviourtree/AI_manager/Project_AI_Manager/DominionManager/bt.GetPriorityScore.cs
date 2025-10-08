using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetPriorityScoreClass : CommonAI 
{


     public bool GetPriorityScore(
                out float PriorityScore,
    float FriendlyStrength,
    float EnemyStrength,
    float ChampionPointValue,
    AttackableUnit CapturePoint,
    TeamId ReferenceTeam,
    int DifficultyIndex
          )
      {

        float _PriorityScore = default;


      bool result =             // Sequence name :GetPriorityScore
            (
                  // Sequence name :Beginner
                  (
                        DifficultyIndex == 0 &&
                        GetUnitTeam(
                              out CPTeam, 
                              CapturePoint) &&
                        SubtractFloat(
                              out Temp1, 
                              EnemyStrength, 
                              FriendlyStrength) &&
                        MultiplyFloat(
                              out Temp2, 
                              ChampionPointValue, 
                              4) &&
                        DivideFloat(
                              out ThreatScore, 
                              Temp1, 
                              Temp2) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :Sequence
                              (
                                    CPTeam == ReferenceTeam &&
                                    LessFloat(
                                          EnemyStrength, 
                                          ChampionPointValue) &&
                                    SetVarFloat(
                                          out _PriorityScore, 
                                          0.1f)
                              ) ||
                              // Sequence name :Sequence
                              (
                                    AbsFloat(
                                          out ThreatScore, 
                                          ThreatScore) &&
                                    InterpolateLine(
                                          out _PriorityScore, 
                                          0, 
                                          1, 
                                          1, 
                                          0, 
                                          0, 
                                          1, 
                                          ThreatScore)
                              )
                        )
                  ) ||
                  // Sequence name :NotBeginner
                  (
                        NotEqualInt(
                              DifficultyIndex, 
                              0) &&
                        GetUnitTeam(
                              out CPTeam, 
                              CapturePoint) &&
                        SubtractFloat(
                              out Temp1, 
                              EnemyStrength, 
                              FriendlyStrength) &&
                        MultiplyFloat(
                              out Temp2, 
                              ChampionPointValue, 
                              6) &&
                        DivideFloat(
                              out ThreatScore, 
                              Temp1, 
                              Temp2) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :Sequence
                              (
                                    CPTeam == ReferenceTeam &&
                                    LessFloat(
                                          EnemyStrength, 
                                          ChampionPointValue) &&
                                    SetVarFloat(
                                          out _PriorityScore, 
                                          0.1f)
                              ) ||
                              // Sequence name :Sequence
                              (
                                    AbsFloat(
                                          out ThreatScore, 
                                          ThreatScore) &&
                                    InterpolateLine(
                                          out _PriorityScore, 
                                          0, 
                                          1, 
                                          1.5f, 
                                          0, 
                                          0, 
                                          1, 
                                          ThreatScore)

                              )
                        )
                  )
            );

        PriorityScore = _PriorityScore;
        return result;
      }
}

