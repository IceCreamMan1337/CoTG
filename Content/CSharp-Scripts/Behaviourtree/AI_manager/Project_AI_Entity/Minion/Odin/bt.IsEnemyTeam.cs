namespace BehaviourTrees;


class IsEnemyTeamClass : IODIN_MinionAIBT
{


    public bool IsEnemyTeam(TeamId MyTeam,
  TeamId ComparingTeam)
    {
        return
                    // Sequence name :Selector

                    // Sequence name :Order
                    (
                          MyTeam == TeamId.TEAM_ORDER &&
                          ComparingTeam == TeamId.TEAM_CHAOS
                    ) ||
                    // Sequence name :Chaos
                    (
                          MyTeam == TeamId.TEAM_CHAOS &&
                          ComparingTeam == TeamId.TEAM_ORDER

                    )
              ;
    }
}