using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class CapturePointIndexToStringClass : AImission_bt
{
      

     public bool CapturePointIndexToString(
          out string CapturePointStringName,
    int CapturePointIndex)
      {

        string _CapturePointStringName = default;
bool result = 
            // Sequence name :Selector
            (
                  // Sequence name :CapturePointIndex=0
                  (
                        CapturePointIndex == 0 &&
                        SetVarString(
                              out _CapturePointStringName, 
                              "A")
                  ) ||
                  // Sequence name :CapturePointIndex=1
                  (
                        CapturePointIndex == 1 &&
                        SetVarString(
                              out _CapturePointStringName, 
                              "B")
                  ) ||
                  // Sequence name :CapturePointIndex=2
                  (
                        CapturePointIndex == 2 &&
                        SetVarString(
                              out _CapturePointStringName, 
                              "C")
                  ) ||
                  // Sequence name :CapturePointIndex=3
                  (
                        CapturePointIndex == 3 &&
                        SetVarString(
                              out _CapturePointStringName, 
                              "D")
                  ) ||
                  // Sequence name :CapturePointIndex=4
                  (
                        CapturePointIndex == 4 &&
                        SetVarString(
                              out _CapturePointStringName, 
                              "E")

                  )
            );
        CapturePointStringName = _CapturePointStringName;
        return result;
      }
}

