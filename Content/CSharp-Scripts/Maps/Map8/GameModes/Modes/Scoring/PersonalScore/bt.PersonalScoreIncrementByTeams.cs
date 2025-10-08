using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class PersonalScoreIncrementByTeamsClass : OdinLayout
{


    bool PersonalScoreIncrementByTeams(

              TeamId TeamToAwardPoints,
  Vector3 ReferencePosition,
    float ScoreValue,
  ScoreCategory ScoreCategory,
  ScoreEvent ScoreEvent,
  float Radius,
  bool IncStat,
  StatEvent StatEvent,
  bool CheckBuff,
  float ScoreWithBuff,
  String BuffToCheck,
  bool IncStatWithBuff,
  StatEvent StatWithBuff
        )
    {
        return
                    // Sequence name :Sequence

                    // Sequence name :Selector
                    (
                          // Sequence name :TeamIsOrder
                          (
                                TeamToAwardPoints == TeamId.TEAM_ORDER &&
                                GetTurret(
                                      out ReferenceUnit,
                                      TeamId.TEAM_ORDER,
                                      0,
                                      1)
                          ) ||
                          // Sequence name :TeamIsChaos
                          (
                                TeamToAwardPoints == TeamId.TEAM_CHAOS &&
                                GetTurret(
                                      out ReferenceUnit,
                                      TeamId.TEAM_CHAOS,
                                      0,
                                      1)
                          )
                    ) &&
                    GetUnitsInTargetArea(
                          out ToGivePoints,
                          ReferenceUnit,
                          ReferencePosition,
                          Radius,
                          AffectEnemies | AffectFriends | AffectHeroes | AffectNeutral) &&
                    ForEach(ToGivePoints, IndividualPointUnit =>
                                // Sequence name :Sequence

                                GetUnitTeam(
                                      out UnitTeam,
                                      IndividualPointUnit) &&
                                UnitTeam == TeamToAwardPoints &&
                                // Sequence name :Selector
                                (
                                      // Sequence name :TestBuff
                                      (
                                            CheckBuff == true &&
                                            TestUnitHasBuff(
                                                  IndividualPointUnit,
                                                  null,
                                                  "BuffToCheck",
                                                  true) &&
                                            IncrementPlayerScore(
                                                  IndividualPointUnit,
                                                  ScoreCategory,
                                                  ScoreEvent,
                                                  ScoreWithBuff
                                                  ) &&
                                            // Sequence name :MaskFailure
                                            (
                                                  // Sequence name :Sequence
                                                  (
                                                        IncStatWithBuff == true &&
                                                        IncrementPlayerStat(
                                                              IndividualPointUnit,
                                                              StatWithBuff)
                                                  )
                                                  ||
                                 DebugAction("MaskFailure")
                                            )
                                      ) ||
                                      // Sequence name :Sequence
                                      (
                                            GreaterFloat(
                                                  ScoreValue,
                                                  0) &&
                                            IncrementPlayerScore(
                                                  IndividualPointUnit,
                                                  ScoreCategory,
                                                  ScoreEvent,
                                                 ScoreValue
                                                  ) &&
                                            // Sequence name :MaskFailure
                                            (
                                                  // Sequence name :Sequence
                                                  (
                                                        IncStat == true &&
                                                        IncrementPlayerStat(
                                                              IndividualPointUnit,
                                                              StatEvent)

                                                  )
                                                  ||
                                 DebugAction("MaskFailure")
                                            )
                                      )
                                )

                    )
              ;
    }
}

