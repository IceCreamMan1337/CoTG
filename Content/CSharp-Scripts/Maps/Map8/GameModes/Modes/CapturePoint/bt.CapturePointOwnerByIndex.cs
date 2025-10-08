namespace BehaviourTrees;


class CapturePointOwnerByIndexClass : OdinLayout
{


    public bool CapturePointOwnerByIndex(
               out TeamId CapturePointOwner,
     out TeamId CapturePoint_PreviousOwner,
   int CapturePointIndex,
   TeamId CapturePointOwnerA,
   TeamId CapturePointOwnerB,
   TeamId CapturePointOwnerC,
   TeamId CapturePointOwnerD,
   TeamId CapturePointOwnerE,
   TeamId PreviousOwner_A,
   TeamId PreviousOwner_B,
   TeamId PreviousOwner_C,
   TeamId PreviousOwner_D,
   TeamId PreviousOwner_E

         )
    {

        TeamId _CapturePointOwner = default;
        TeamId _CapturePoint_PreviousOwner = default;
        bool result =
                          // Sequence name :Selector

                          // Sequence name :A
                          (
                                CapturePointIndex == 0 &&
                                SetVarUnitTeam(
                                      out _CapturePointOwner,
                                      CapturePointOwnerA) &&
                                SetVarUnitTeam(
                                      out _CapturePoint_PreviousOwner,
                                      PreviousOwner_A)
                          ) ||
                          // Sequence name :B
                          (
                                CapturePointIndex == 1 &&
                                SetVarUnitTeam(
                                      out _CapturePointOwner,
                                      CapturePointOwnerB) &&
                                SetVarUnitTeam(
                                      out _CapturePoint_PreviousOwner,
                                      PreviousOwner_B)
                          ) ||
                          // Sequence name :C
                          (
                                CapturePointIndex == 2 &&
                                SetVarUnitTeam(
                                      out _CapturePointOwner,
                                      CapturePointOwnerC) &&
                                SetVarUnitTeam(
                                      out _CapturePoint_PreviousOwner,
                                      PreviousOwner_C)
                          ) ||
                          // Sequence name :D
                          (
                                CapturePointIndex == 3 &&
                                SetVarUnitTeam(
                                      out _CapturePointOwner,
                                      CapturePointOwnerD) &&
                                SetVarUnitTeam(
                                      out _CapturePoint_PreviousOwner,
                                      PreviousOwner_D)
                          ) ||
                          // Sequence name :E
                          (
                                CapturePointIndex == 4 &&
                                SetVarUnitTeam(
                                      out _CapturePointOwner,
                                      CapturePointOwnerE) &&
                                SetVarUnitTeam(
                                      out _CapturePoint_PreviousOwner,
                                      PreviousOwner_E)

                          )
                    ;

        CapturePointOwner = _CapturePointOwner;
        CapturePoint_PreviousOwner = _CapturePoint_PreviousOwner;
        return result;


    }
}

