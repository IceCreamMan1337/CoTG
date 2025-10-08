namespace BehaviourTrees;


class CapturePointIndexToNameClass : OdinLayout
{


    public bool CapturePointIndexToName(
               out string CapturePointName,
   int CapturePointIndex
         )
    {

        string _CapturePointName = "";
        bool result =
                          // Sequence name :Selector

                          // Sequence name :Sequence
                          (
                                CapturePointIndex == 0 &&
                                SetVarString(
                                      out _CapturePointName,
                                      "Quarry")
                          ) ||
                          // Sequence name :Sequence
                          (
                                CapturePointIndex == 1 &&
                                SetVarString(
                                      out _CapturePointName,
                                      "Refinery")
                          ) ||
                          // Sequence name :Sequence
                          (
                                CapturePointIndex == 2 &&
                                SetVarString(
                                      out _CapturePointName,
                                      "Windmill")
                          ) ||
                          // Sequence name :Sequence
                          (
                                CapturePointIndex == 3 &&
                                SetVarString(
                                      out _CapturePointName,
                                      "Drill")
                          ) ||
                          // Sequence name :Sequence
                          (
                                CapturePointIndex == 4 &&
                                SetVarString(
                                      out _CapturePointName,
                                      "Boneyard")

                          )
                    ;
        CapturePointName = _CapturePointName;
        return result;
    }
}

