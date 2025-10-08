namespace BehaviourTrees.Map8;


class CapturePointInitializationClass : BehaviourTree
{


    public bool CapturePointInitialization(
               out TeamId CapturePointPreviousOwnerA,
     out TeamId CapturePointPreviousOwnerB,
     out TeamId CapturePointPreviousOwnerC,
     out TeamId CapturePointPreviousOwnerD,
     out TeamId CapturePointPreviousOwnerE,
     out TeamId CapturePointOwnerA,
     out TeamId CapturePointOwnerB,
     out TeamId CapturePointOwnerC,
     out TeamId CapturePointOwnerD,
     out TeamId CapturePointOwnerE

         )
    {
        TeamId _CapturePointPreviousOwnerA = default;
        TeamId _CapturePointPreviousOwnerB = default;
        TeamId _CapturePointPreviousOwnerC = default;
        TeamId _CapturePointPreviousOwnerD = default;
        TeamId _CapturePointPreviousOwnerE = default;
        TeamId _CapturePointOwnerA = default;
        TeamId _CapturePointOwnerB = default;
        TeamId _CapturePointOwnerC = default;
        TeamId _CapturePointOwnerD = default;
        TeamId _CapturePointOwnerE = default;


        bool result =
                          // Sequence name :CapturePointOwner_Initialization

                          SetVarUnitTeam(
                                out _CapturePointOwnerA,
                                TeamId.TEAM_NEUTRAL) && //put chaos for do some test
                          SetVarUnitTeam(
                                out _CapturePointOwnerB,
                                TeamId.TEAM_NEUTRAL) &&
                          SetVarUnitTeam(
                                out _CapturePointOwnerC,
                                TeamId.TEAM_NEUTRAL) &&
                          SetVarUnitTeam(
                                out _CapturePointOwnerD,
                                TeamId.TEAM_NEUTRAL) &&
                          SetVarUnitTeam(
                                out _CapturePointOwnerE,
                                TeamId.TEAM_NEUTRAL) &&
                          SetVarUnitTeam(
                                out _CapturePointPreviousOwnerA,
                                TeamId.TEAM_NEUTRAL) &&
                          SetVarUnitTeam(
                                out _CapturePointPreviousOwnerB,
                                TeamId.TEAM_NEUTRAL) &&
                          SetVarUnitTeam(
                                out _CapturePointPreviousOwnerC,
                                TeamId.TEAM_NEUTRAL) &&
                          SetVarUnitTeam(
                                out _CapturePointPreviousOwnerD,
                                TeamId.TEAM_NEUTRAL) &&
                          SetVarUnitTeam(
                                out _CapturePointPreviousOwnerE,
                                TeamId.TEAM_NEUTRAL)

                    ;
        CapturePointPreviousOwnerA = _CapturePointPreviousOwnerA;
        CapturePointPreviousOwnerB = _CapturePointPreviousOwnerB;
        CapturePointPreviousOwnerC = _CapturePointPreviousOwnerC;
        CapturePointPreviousOwnerD = _CapturePointPreviousOwnerD;
        CapturePointPreviousOwnerE = _CapturePointPreviousOwnerE;
        CapturePointOwnerA = _CapturePointOwnerA;
        CapturePointOwnerB = _CapturePointOwnerB;
        CapturePointOwnerC = _CapturePointOwnerC;
        CapturePointOwnerD = _CapturePointOwnerD;
        CapturePointOwnerE = _CapturePointOwnerE;


        return result;


    }
}

