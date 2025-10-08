namespace BehaviourTrees;


class GetCapturePointDeltaClass : OdinLayout
{


    public bool GetCapturePointDelta(
               out int CCDelta,
   TeamId CapturePointAOwner,
   TeamId CapturePointBOwner,
   TeamId CapturePointCOwner,
   TeamId CapturePointDOwner,
   TeamId CapturePointEOwner

         )
    {

        int _CCDelta = default;

        bool result =
                          // Sequence name :Sequence

                          SetVarInt(
                                out _CCDelta,
                                0) &&
                                // Sequence name :Sequence

                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :PointA

                                            // Sequence name :Sequence
                                            (
                                                  CapturePointAOwner == TeamId.TEAM_ORDER &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        1)
                                            ) ||
                                            // Sequence name :Sequence
                                            (
                                                  CapturePointAOwner == TeamId.TEAM_CHAOS &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        -1)
                                            )

                                      ||
                                       DebugAction("MaskFailure")
                                ) &&
                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :PointB

                                            // Sequence name :Sequence
                                            (
                                                  CapturePointBOwner == TeamId.TEAM_ORDER &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        1)
                                            ) ||
                                            // Sequence name :Sequence
                                            (
                                                  CapturePointBOwner == TeamId.TEAM_CHAOS &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        -1)
                                            )

                                      ||
                                       DebugAction("MaskFailure")
                                ) &&
                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :PointC

                                            // Sequence name :Sequence
                                            (
                                                  CapturePointCOwner == TeamId.TEAM_ORDER &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        1)
                                            ) ||
                                            // Sequence name :Sequence
                                            (
                                                  CapturePointCOwner == TeamId.TEAM_CHAOS &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        -1)
                                            )

                                      ||
                                       DebugAction("MaskFailure")
                                ) &&
                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :PointD

                                            // Sequence name :Sequence
                                            (
                                                  CapturePointDOwner == TeamId.TEAM_ORDER &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        1)
                                            ) ||
                                            // Sequence name :Sequence
                                            (
                                                  CapturePointDOwner == TeamId.TEAM_CHAOS &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        -1)
                                            )

                                      ||
                                       DebugAction("MaskFailure")
                                ) &&
                                // Sequence name :MaskFailure
                                (
                                            // Sequence name :PointE

                                            // Sequence name :Sequence
                                            (
                                                  CapturePointEOwner == TeamId.TEAM_ORDER &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        1)
                                            ) ||
                                            // Sequence name :Sequence
                                            (
                                                  CapturePointEOwner == TeamId.TEAM_CHAOS &&
                                                  AddInt(
                                                        out _CCDelta,
                                                        _CCDelta,
                                                        -1)

                                            )

                                      ||
                                       DebugAction("MaskFailure")
                                )

                    ;

        CCDelta = _CCDelta;
        return result;
    }
}

