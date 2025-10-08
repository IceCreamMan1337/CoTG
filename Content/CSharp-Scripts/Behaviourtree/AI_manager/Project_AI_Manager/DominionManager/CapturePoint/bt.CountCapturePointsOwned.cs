namespace BehaviourTrees;


class CountCapturePointsOwnedClass : bt_OdinManager
{


    public bool CountCapturePointsOwned(
     out int NumCapturePoints_Enemy,
     out int NumCapturePoints_Ally,
     out int NumCapturePoints_Neutral,
   AttackableUnit CapturePointA,
   AttackableUnit CapturePointB,
   AttackableUnit CapturePointC,
   AttackableUnit CapturePointD,
   AttackableUnit CapturePointE,
   TeamId ReferenceTeam

         )
    {

        int _NumCapturePoints_Enemy = default;
        int _NumCapturePoints_Ally = default;
        int _NumCapturePoints_Neutral = default;


        bool result =
                          // Sequence name :CPInfo

                          SetVarInt(
                                out _NumCapturePoints_Ally,
                                0) &&
                          SetVarInt(
                                out _NumCapturePoints_Enemy,
                                0) &&
                          SetVarInt(
                                out _NumCapturePoints_Neutral,
                                0) &&
                                      // Sequence name :GetCPCount

                                      // Sequence name :CapturePointA

                                      GetUnitTeam(
                                            out CPTeam,
                                            CapturePointA) &&
                                      // Sequence name :Ally_Neutral_Enemy
                                      (
                                            // Sequence name :Ally
                                            (
                                                  CPTeam == ReferenceTeam &&
                                                  AddInt(
                                                        out _NumCapturePoints_Ally,
                                                        _NumCapturePoints_Ally,
                                                        1)
                                            ) ||
                                            // Sequence name :Enemy
                                            (
                                                  NotEqualUnitTeam(
                                                        CPTeam,
                                                        TeamId.TEAM_NEUTRAL) &&
                                                  AddInt(
                                                        out _NumCapturePoints_Enemy,
                                                        _NumCapturePoints_Enemy,
                                                        1)
                                            ) ||
                                            AddInt(
                                                  out _NumCapturePoints_Neutral,
                                                  _NumCapturePoints_Neutral,
                                                  1)
                                      )
                                 &&
                                      // Sequence name :CapturePointB

                                      GetUnitTeam(
                                            out CPTeam,
                                            CapturePointB) &&
                                      // Sequence name :Ally_Neutral_Enemy
                                      (
                                            // Sequence name :Ally
                                            (
                                                  CPTeam == ReferenceTeam &&
                                                  AddInt(
                                                        out _NumCapturePoints_Ally,
                                                        _NumCapturePoints_Ally,
                                                        1)
                                            ) ||
                                            // Sequence name :Enemy
                                            (
                                                  NotEqualUnitTeam(
                                                        CPTeam,
                                                        TeamId.TEAM_NEUTRAL) &&
                                                  AddInt(
                                                        out _NumCapturePoints_Enemy,
                                                        _NumCapturePoints_Enemy,
                                                        1)
                                            ) ||
                                            AddInt(
                                                  out _NumCapturePoints_Neutral,
                                                  _NumCapturePoints_Neutral,
                                                  1)
                                      )
                                 &&
                                      // Sequence name :CapturePointC

                                      GetUnitTeam(
                                            out CPTeam,
                                            CapturePointC) &&
                                      // Sequence name :Ally_Neutral_Enemy
                                      (
                                            // Sequence name :Ally
                                            (
                                                  CPTeam == ReferenceTeam &&
                                                  AddInt(
                                                        out _NumCapturePoints_Ally,
                                                        _NumCapturePoints_Ally,
                                                        1)
                                            ) ||
                                            // Sequence name :Enemy
                                            (
                                                  NotEqualUnitTeam(
                                                        CPTeam,
                                                        TeamId.TEAM_NEUTRAL) &&
                                                  AddInt(
                                                        out _NumCapturePoints_Enemy,
                                                        _NumCapturePoints_Enemy,
                                                        1)
                                            ) ||
                                            AddInt(
                                                  out _NumCapturePoints_Neutral,
                                                  _NumCapturePoints_Neutral,
                                                  1)
                                      )
                                 &&
                                      // Sequence name :CapturePointD

                                      GetUnitTeam(
                                            out CPTeam,
                                            CapturePointD) &&
                                      // Sequence name :Ally_Neutral_Enemy
                                      (
                                            // Sequence name :Ally
                                            (
                                                  CPTeam == ReferenceTeam &&
                                                  AddInt(
                                                        out _NumCapturePoints_Ally,
                                                        _NumCapturePoints_Ally,
                                                        1)
                                            ) ||
                                            // Sequence name :Enemy
                                            (
                                                  NotEqualUnitTeam(
                                                        CPTeam,
                                                        TeamId.TEAM_NEUTRAL) &&
                                                  AddInt(
                                                        out _NumCapturePoints_Enemy,
                                                        _NumCapturePoints_Enemy,
                                                        1)
                                            ) ||
                                            AddInt(
                                                  out _NumCapturePoints_Neutral,
                                                  _NumCapturePoints_Neutral,
                                                  1)
                                      )
                                 &&
                                      // Sequence name :CapturePointE

                                      GetUnitTeam(
                                            out CPTeam,
                                            CapturePointE) &&
                                      // Sequence name :Ally_Neutral_Enemy
                                      (
                                            // Sequence name :Ally
                                            (
                                                  CPTeam == ReferenceTeam &&
                                                  AddInt(
                                                        out _NumCapturePoints_Ally,
                                                        _NumCapturePoints_Ally,
                                                        1)
                                            ) ||
                                            // Sequence name :Enemy
                                            (
                                                  NotEqualUnitTeam(
                                                        CPTeam,
                                                        TeamId.TEAM_NEUTRAL) &&
                                                  AddInt(
                                                        out _NumCapturePoints_Enemy,
                                                        _NumCapturePoints_Enemy,
                                                        1)
                                            ) ||
                                            AddInt(
                                                  out _NumCapturePoints_Neutral,
                                                  _NumCapturePoints_Neutral,
                                                  1)

                                      )


                    ;

        NumCapturePoints_Ally = _NumCapturePoints_Ally;
        NumCapturePoints_Enemy = _NumCapturePoints_Enemy;
        NumCapturePoints_Neutral = _NumCapturePoints_Neutral;
        return result;
    }
}

