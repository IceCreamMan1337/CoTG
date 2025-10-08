using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class EvaluateCapturePointForMinionsClass : OdinLayout
{

    public bool EvaluateCapturePointForMinions()
    {
        return
                    // Sequence name :Adjust capture status for minions

                    DebugAction(
                          "Starting Minion Test"
                          ) &&
                    // Sequence name :Poll for the right minions
                    (
                          // Sequence name :Poll for Chaos
                          (
                                CurrentPointOwner == TeamId.TEAM_ORDER &&
                                SetVarUnitTeam(
                                      out AttackingTeam,
                                      TeamId.TEAM_CHAOS) &&
                                GetUnitsInTargetArea(
                                      out LocalMinionCollection,
                                      OrderTurret,
                                      CapturePosition,
                                      300,
                                      AffectEnemies | AffectMinions)
                          ) ||
                          // Sequence name :Poll for order
                          (
                                CurrentPointOwner == TeamId.TEAM_CHAOS &&
                                SetVarUnitTeam(
                                      out AttackingTeam,
                                      TeamId.TEAM_ORDER) &&
                                GetUnitsInTargetArea(
                                      out LocalMinionCollection,
                                      OrderTurret,
                                      CapturePosition,
                                      300,
                                      AffectFriends | AffectMinions)
                          ) ||
                          // Sequence name :Poll for Chaos
                          (
                                SetVarUnitTeam(
                                      out AttackingTeam,
                                      TeamId.TEAM_NEUTRAL) &&
                                GetUnitsInTargetArea(
                                      out LocalMinionCollection,
                                      OrderTurret,
                                      CapturePosition,
                                      300,
                                      AffectEnemies | AffectFriends | AffectMinions) &&
                                ForEach(LocalMinionCollection, LocalMinion =>
                                            // Sequence name :Sequence

                                            GetUnitTeam(
                                                  out LocalMinionTeam,
                                                  LocalMinion) &&
                                            SetVarUnitTeam(
                                                  out AttackingTeam,
                                                  LocalMinionTeam)

                                )
                          )
                    ) &&
                    GetCollectionCount(
                          out MinionCollectionCount,
                          LocalMinionCollection) &&
                    GreaterInt(
                          MinionCollectionCount,
                          0) &&
                    NotEqualUnitTeam(
                          AttackingTeam,
                          TeamId.TEAM_NEUTRAL) &&
                    DebugAction(

                          "We have minions of the opposing team") &&
                    GetUnitsInTargetArea(
                          out ChaosMinionCollection,
                          OrderTurret,
                          CapturePosition,
                          700,
                          AffectEnemies | AffectMinions) &&
                    GetUnitsInTargetArea(
                          out OrderMinionCollection,
                          OrderTurret,
                          CapturePosition,
                          700,
                          AffectFriends | AffectMinions) &&
                    GetCollectionCount(
                          out MinionCollectionCount,
                          ChaosMinionCollection) &&
                    AddString(
                          out DebugString,
                          "Chaos/Order Minions:",
                          $"{MinionCollectionCount}") &&
                    GetCollectionCount(
                          out MinionCollectionCount,
                          OrderMinionCollection) &&
                    AddString(
                          out DebugString,
                          DebugString,
                          "/") &&
                    AddString(
                          out DebugString,
                          DebugString,
                          "MinionCollectionCount") &&
                    DebugAction(

                          DebugString) &&
                    // Sequence name :MaskFailure
                    (
                          ForEach(OrderMinionCollection, LocalMinion =>
                                      // Sequence name :KILL THE MINION

                                      KillUnit(
                                            LocalMinion,
                                            LocalMinion)

                          )
                          ||
                                 DebugAction("MaskFailure")
                    ) &&
                    // Sequence name :MaskFailure
                    (
                          ForEach(ChaosMinionCollection, LocalMinion =>
                                      // Sequence name :KILL THE MINION

                                      KillUnit(
                                            LocalMinion,
                                            LocalMinion)

                          )
                          ||
                                 DebugAction("MaskFailure")
                    ) &&
                    DebugAction(

                          "We have minions of the opposing team") &&
                    // Sequence name :Change point progress
                    (
                          // Sequence name :Sequence
                          (
                                AttackingTeam == TeamId.TEAM_CHAOS &&
                                AddFloat(
                                      out CaptureProgress,
                                      CaptureProgress,
                                      -20) &&
                                MaxFloat(
                                      out CaptureProgress,
                                      CaptureProgress,
                                      -100)
                          ) ||
                          // Sequence name :Sequence
                          (
                                AddFloat(
                                      out CaptureProgress,
                                      CaptureProgress,
                                      20) &&
                                MinFloat(
                                      out CaptureProgress,
                                      CaptureProgress,
                                      100)
                          )
                    ) &&
                    DebugAction(

                          "Incremented score") &&
                          // Sequence name :Count number of defending minions killed

                          // Sequence name :Get the count of the right team's minions
                          (
                                // Sequence name :Check if defenders are Order
                                (
                                      AttackingTeam == TeamId.TEAM_CHAOS &&
                                      GetCollectionCount(
                                            out DeadDefendingMinionCount,
                                            ChaosMinionCollection)
                                ) ||
                                GetCollectionCount(
                                      out DeadDefendingMinionCount,
                                      OrderMinionCollection)
                          )
                     &&
                    MultiplyFloat(
                          out TotalGoldToSplit,
                          DeadDefendingMinionCount,
                          25) &&
                    SetVarFloat(
                          out IndividualBounty,
                          TotalGoldToSplit) &&
                    DebugAction(

                          "Calculating Gold") &&
                    // Sequence name :Split the gold among nearby attacking champions
                    (
                          // Sequence name :Check if attackers are Order
                          (
                                AttackingTeam == TeamId.TEAM_CHAOS &&
                                GetCollectionCount(
                                      out AttackingChampionCount,
                                      OrderMinionCollection) &&
                                GreaterInt(
                                      AttackingChampionCount,
                                      0) &&
                                DivideFloat(
                                      out IndividualBounty,
                                      TotalGoldToSplit,
                                      AttackingChampionCount)
                          ) ||
                          // Sequence name :Chaos attackers
                          (
                                GetCollectionCount(
                                      out AttackingChampionCount,
                                      ChaosMinionCollection) &&
                                GreaterInt(
                                      AttackingChampionCount,
                                      0) &&
                                DivideFloat(
                                      out IndividualBounty,
                                      TotalGoldToSplit,
                                      AttackingChampionCount)
                          )
                    ) &&
                          // Sequence name :Sequence

                          ForEach(LocalChampionCollection, LocalHero =>
                                      // Sequence name :Sequence

                                      GetUnitTeam(
                                            out LocalHeroTeam,
                                            LocalHero) &&
                                      LocalHeroTeam == AttackingTeam &&
                                      GiveChampionGold(
                                            LocalHero,
                                            IndividualBounty)


                          )

              ;
    }
}

