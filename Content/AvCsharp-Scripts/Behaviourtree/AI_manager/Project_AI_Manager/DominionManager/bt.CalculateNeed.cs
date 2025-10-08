using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class CalculateNeedClass : CommonAI 
{


      bool CalculateNeed(

                out int Need,
    AttackableUnit CapturePoint,
    float StrDiff,
    float ChampionPointValue,
    TeamId ReferenceTeam,
    int OwnedCPCount,
    int EnemyCPCount
          )
      {
        int _Need = default;
        var numberToSend = new NumberToSendClass();

        bool result =
            // Sequence name :CalculateNeed
            (
                  GetUnitTeam(
                        out CPTeam, 
                        CapturePoint) &&
                  DivideFloat(
                        out TempNeed, 
                        StrDiff, 
                        ChampionPointValue) &&
                  // Sequence name :Selector
                  (
                        // Sequence name :MatchIfNeutral
                        (
                              CPTeam == TeamId.TEAM_NEUTRAL &&
                              numberToSend.NumberToSend(
                                    out _Need, 
                                    TempNeed) &&
                              AddInt(
                                    out _Need,
                                    _Need, 
                                    1)
                        ) ||
                        // Sequence name :AssessIfEnemy
                        (
                              NotEqualUnitTeam(
                                    CPTeam, 
                                    ReferenceTeam) &&
                              // Sequence name :Assess
                              (
                                    // Sequence name :Undefended
                                    (
                                          LessFloat(
                                                StrDiff, 
                                                ChampionPointValue) &&
                                          SetVarInt(
                                                out _Need, 
                                                2)
                                    ) ||
                                    // Sequence name :Losing
                                    (
                                          LessInt(
                                                OwnedCPCount, 
                                                EnemyCPCount) &&
                                          numberToSend.NumberToSend(
                                                out _Need, 
                                                TempNeed) &&
                                          AddInt(
                                                out _Need,
                                                _Need, 
                                                1)
                                    )
                              )
                        ) ||
                        // Sequence name :DefendIfOwnedAndThreatened
                        (
                              // Sequence name :Threatened
                              (
                                    GreaterFloat(
                                          StrDiff, 
                                          0) &&
                                    numberToSend.NumberToSend(
                                          out _Need, 
                                          TempNeed) &&
                                    AddInt(
                                          out _Need,
                                          _Need, 
                                          1)
                              ) ||
                              SetVarInt(
                                    out _Need, 
                                    0)

                        )
                  )
            );

        Need = _Need;
        return result;
      }
}

