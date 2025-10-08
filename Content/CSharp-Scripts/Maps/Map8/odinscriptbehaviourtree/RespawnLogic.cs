namespace BehaviourTrees;


class RespawnLogicClass : OdinLayout
{


    public bool RespawnLogic(
              TeamId PointOwner,
   int RespawnUIPoint,
   int GraveyardID
        )
    {
        return
                    // Sequence name :Selector

                    // Sequence name :Sequence
                    (
                          PointOwner == TeamId.TEAM_ORDER &&
                          GetChampionCollection(
                                out ChampionListing) &&
                          ForEach(ChampionListing, IndividualChampion =>
                                      // Sequence name :Sequence

                                      AllowTeamRespawnPoint(
                                            GraveyardID,
                                            TeamId.TEAM_ORDER) &&
                                      DisallowTeamRespawnPoint(
                                            GraveyardID,
                                            TeamId.TEAM_CHAOS) &&
                                      GetUnitTeam(
                                            out RespawnTeam,
                                            IndividualChampion) &&
                                      // Sequence name :Selector
                                      (
                                            // Sequence name :Sequence
                                            (
                                                  RespawnTeam == TeamId.TEAM_ORDER
                                            ) ||
                                            // Sequence name :Sequence
                                            (
                                                  RespawnTeam == TeamId.TEAM_CHAOS
                                            )
                                      )

                          )
                    ) ||
                    // Sequence name :Sequence
                    (
                          PointOwner == TeamId.TEAM_CHAOS &&
                          GetChampionCollection(
                                out ChampionListing) &&
                          ForEach(ChampionListing, IndividualChampion =>
                                      // Sequence name :Sequence

                                      AllowTeamRespawnPoint(
                                            GraveyardID,
                                            TeamId.TEAM_CHAOS) &&
                                      DisallowTeamRespawnPoint(
                                            GraveyardID,
                                            TeamId.TEAM_ORDER) &&
                                      GetUnitTeam(
                                            out RespawnTeam,
                                            IndividualChampion) &&
                                      // Sequence name :Selector
                                      (
                                            // Sequence name :Sequence
                                            (
                                                  RespawnTeam == TeamId.TEAM_CHAOS
                                            ) ||
                                            // Sequence name :Sequence
                                            (
                                                  RespawnTeam == TeamId.TEAM_ORDER
                                            )
                                      )

                          )
                    ) ||
                    // Sequence name :Sequence
                    (
                          GetChampionCollection(
                                out ChampionListing) &&
                          DisallowTeamRespawnPoint(
                                GraveyardID,
                                TeamId.TEAM_ORDER) &&
                          DisallowTeamRespawnPoint(
                                GraveyardID,
                                TeamId.TEAM_CHAOS) &&
                          ForEach(ChampionListing, IndividualChampion =>
                                Sequence()


                    )
              );
    }
}

