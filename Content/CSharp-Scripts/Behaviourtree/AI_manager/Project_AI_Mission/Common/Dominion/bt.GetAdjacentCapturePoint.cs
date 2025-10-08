namespace BehaviourTrees;


class GetAdjacentCapturePointClass : AImission_bt
{


    public bool GetAdjacentCapturePoint(
               out AttackableUnit AdjacentCapturePoint0,
     out AttackableUnit AdjacentCapturePoint1,
     out int AdjacentCapturePointIndex1,
     out int AdjacentCapturePointIndex0,
   int CapturePointIndex,
   AttackableUnit CapturePointA,
   AttackableUnit CapturePointB,
   AttackableUnit CapturePointC,
   AttackableUnit CapturePointD,
   AttackableUnit CapturePointE

         )
    {

        AttackableUnit _AdjacentCapturePoint0 = default;
        AttackableUnit _AdjacentCapturePoint1 = default;
        int _AdjacentCapturePointIndex1 = default;
        int _AdjacentCapturePointIndex0 = default;


        bool result =
                    // Sequence name :Selector

                    // Sequence name :Index=0
                    (
                          CapturePointIndex == 0 &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint0,
                                CapturePointB) &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint1,
                                CapturePointE) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex0,
                                1) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex1,
                                4)
                    ) ||
                    // Sequence name :Index=1
                    (
                          CapturePointIndex == 1 &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint0,
                                CapturePointC) &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint1,
                                CapturePointA) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex0,
                                2) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex1,
                                0)
                    ) ||
                    // Sequence name :Index=2
                    (
                          CapturePointIndex == 2 &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint0,
                                CapturePointD) &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint1,
                                CapturePointB) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex0,
                                3) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex1,
                                1)
                    ) ||
                    // Sequence name :Index=3
                    (
                          CapturePointIndex == 3 &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint0,
                                CapturePointE) &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint1,
                                CapturePointC) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex0,
                                4) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex1,
                                2)
                    ) ||
                    // Sequence name :Index=4
                    (
                          CapturePointIndex == 4 &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint0,
                                CapturePointA) &&
                          SetVarAttackableUnit(
                                out _AdjacentCapturePoint1,
                                CapturePointD) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex0,
                                0) &&
                          SetVarInt(
                                out _AdjacentCapturePointIndex1,
                                3)

                    )
              ;

        AdjacentCapturePoint0 = _AdjacentCapturePoint0;
        AdjacentCapturePoint1 = _AdjacentCapturePoint1;
        AdjacentCapturePointIndex0 = _AdjacentCapturePointIndex0;
        AdjacentCapturePointIndex1 = _AdjacentCapturePointIndex1;
        return result;
    }
}

