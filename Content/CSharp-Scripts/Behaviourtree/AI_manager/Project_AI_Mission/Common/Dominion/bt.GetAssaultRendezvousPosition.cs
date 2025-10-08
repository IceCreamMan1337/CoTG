namespace BehaviourTrees;


class GetAssaultRendezvousPositionClass : AImission_bt
{

    public bool GetAssaultRendezvousPosition(
         out Vector3 RendezvousPosition,
   int AssaultPointIndex,
   TeamId AssaultingTeam
)

    {

        Vector3 _RendezvousPosition = default(Vector3);


        bool result =
                  // Sequence name :Selector

                  // Sequence name :Order
                  (
                        AssaultingTeam == TeamId.TEAM_ORDER &&
                        // Sequence name :Selector
                        (
                              // Sequence name :PointA
                              (
                                    AssaultPointIndex == 0 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          1714,
                                          -170,
                                          3266)
                              ) ||
                              // Sequence name :PointB
                              (
                                    AssaultPointIndex == 1 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          921,
                                          -155,
                                          5541)
                              ) ||
                              // Sequence name :PointC
                              (
                                    AssaultPointIndex == 2 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          6310,
                                          -188,
                                          9446)
                              ) ||
                              // Sequence name :PointD
                              (
                                    AssaultPointIndex == 3 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          9658,
                                          -188,
                                          8348)
                              ) ||
                              // Sequence name :PointE
                              (
                                    AssaultPointIndex == 4 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          7801,
                                          -188,
                                          3455)
                              )
                        )
                  ) ||
                  // Sequence name :Chaos
                  (
                        AssaultingTeam == TeamId.TEAM_CHAOS &&
                        // Sequence name :Selector
                        (
                              // Sequence name :PointA
                              (
                                    AssaultPointIndex == 0 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          6160,
                                          -188,
                                          3634)
                              ) ||
                              // Sequence name :PointB
                              (
                                    AssaultPointIndex == 1 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          4214,
                                          -188,
                                          8432)
                              ) ||
                              // Sequence name :PointC
                              (
                                    AssaultPointIndex == 2 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          7555,
                                          -188,
                                          9349)
                              ) ||
                              // Sequence name :PointD
                              (
                                    AssaultPointIndex == 3 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          12903,
                                          -163,
                                          5568)
                              ) ||
                              // Sequence name :PointE
                              (
                                    AssaultPointIndex == 4 &&
                                    MakeVector(
                                          out _RendezvousPosition,
                                          11258,
                                          -167,
                                          3162)

                              )
                        )
                  )
            ;

        RendezvousPosition = _RendezvousPosition;
        return result;
    }

}

