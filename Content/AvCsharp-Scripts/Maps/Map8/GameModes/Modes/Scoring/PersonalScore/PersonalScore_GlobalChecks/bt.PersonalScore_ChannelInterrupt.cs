using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_ChannelInterruptClass : OdinLayout 
{


     public bool PersonalScore_ChannelInterrupt(
                TeamId CapturePointOwnerA,
    TeamId CapturePointOwnerB,
    TeamId CapturePointOwnerC,
    TeamId CapturePointOwnerD,
    TeamId CapturePointOwnerE,
    TeamId PreviousOwnerA,
    TeamId PreviousOwnerB,
    TeamId PreviousOwnerC,
    TeamId PreviousOwnerD,
    TeamId PreviousOwnerE
          )
      {

        var findClosestCapturePointByPosition = new FindClosestCapturePointByPositionClass();
        var capturePointOwnerByIndex = new CapturePointOwnerByIndexClass();
      return
            // Sequence name :Sequence
            (
                  GetChampionCollection(
                        out AllChampions) &&
                  ForEach(AllChampions, Champ => (
                        // Sequence name :Sequence
                        (
                              GetUnitBuffCount(
                                    out Count, 
                                    Champ,
                                    "OdinCaptureInterrupt") &&
                              GreaterInt(
                                    Count, 
                                    0) &&
                              GetUnitBuffCaster(
                                    out ChampWhoInterrupted, 
                                    Champ,
                                    "OdinCaptureInterrupt") &&
                              findClosestCapturePointByPosition.FindClosestCapturePointByPosition(
                                    out ClosestCapturePoint, 
                                    ChampWhoInterrupted) &&
                              capturePointOwnerByIndex.CapturePointOwnerByIndex(
                                    out CapturePointOwner, 
                                    out CapturePoint_PreviousOwner, 
                                    ClosestCapturePoint, 
                                    CapturePointOwnerA, 
                                    CapturePointOwnerB, 
                                    CapturePointOwnerC, 
                                    CapturePointOwnerD, 
                                    CapturePointOwnerE, 
                                    PreviousOwnerA, 
                                    PreviousOwnerB, 
                                    PreviousOwnerC, 
                                    PreviousOwnerD, 
                                    PreviousOwnerE) &&
                              GetUnitTeam(
                                    out InterrupterTeam, 
                                    ChampWhoInterrupted) &&
                              // Sequence name :OffenseOrDefensive
                              (
                                    // Sequence name :Offensive
                                    (
                                          // Sequence name :NeutralOrSameTeam
                                          (
                                                // Sequence name :DiffTeam
                                                (
                                                      NotEqualUnitTeam(
                                                            CapturePointOwner, 
                                                            TeamId.TEAM_NEUTRAL) &&
                                                      NotEqualUnitTeam(
                                                            CapturePointOwner, 
                                                            InterrupterTeam)
                                                ) ||
                                                // Sequence name :OldOwnerNotTheSame
                                                (
                                                      CapturePointOwner == TeamId.TEAM_NEUTRAL &&
                                                      NotEqualUnitTeam(
                                                            CapturePoint_PreviousOwner, 
                                                            InterrupterTeam)
                                                )
                                          ) &&
                                          IncrementPlayerScore(
                                                ChampWhoInterrupted, 
                                               ScoreCategory.Objective, 
                                                ScoreEvent.Counter, 
                                                5 
                                                )
                                    ) ||
                                    // Sequence name :Defensive
                                    (
                                          // Sequence name :NeutralOrSameTeam
                                          (
                                                // Sequence name :SameTeam
                                                (
                                                      NotEqualUnitTeam(
                                                            CapturePointOwner, 
                                                            TeamId.TEAM_NEUTRAL) &&
                                                      CapturePointOwner == InterrupterTeam
                                                ) ||
                                                // Sequence name :OldOwnerIsTheSame
                                                (
                                                      CapturePointOwner == TeamId.TEAM_NEUTRAL &&
                                                      CapturePoint_PreviousOwner == InterrupterTeam
                                                )
                                          ) &&
                                          IncrementPlayerScore(
                                                ChampWhoInterrupted,
                                                ScoreCategory.Objective,
                                                ScoreEvent.Counter,
                                                5
                                                )
                                    )
                              ) &&
                              SpellBuffClear(
                                    Champ,
                                    "OdinCaptureInterrupt")

                        ))
                  )
            );
      }
}

