using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class OverallScoreMathClass : OdinLayout 
{


     public bool OverallScoreMath(
                out float __WinningTeamScore,
    int CapturePointDelta,
    float WinningTeamScore
          )
      {

        float _WinningTeamScore = WinningTeamScore;
bool result= 
            // Sequence name :Selector
            (
                  // Sequence name :1 ahead
                  (
                        CapturePointDelta == 1 &&
                        AddFloat(
                              out _WinningTeamScore, 
                              3, 
                              _WinningTeamScore)
                  ) ||
                  // Sequence name :2 ahead
                  (
                        CapturePointDelta == 2 &&
                        AddFloat(
                              out _WinningTeamScore, 
                              5, 
                              _WinningTeamScore)
                  ) ||
                  // Sequence name :3 ahead
                  (
                        CapturePointDelta == 3 &&
                        AddFloat(
                              out _WinningTeamScore, 
                              8, 
                              _WinningTeamScore)
                  ) ||
                  // Sequence name :4 ahead
                  (
                        CapturePointDelta == 4 &&
                        AddFloat(
                              out _WinningTeamScore, 
                              13, 
                              _WinningTeamScore)
                  ) ||
                  // Sequence name :Rofl stomp
                  (
                        AddFloat(
                              out _WinningTeamScore, 
                              21, 
                              _WinningTeamScore)

                  )
            );

        
        __WinningTeamScore = _WinningTeamScore;
        return result;
    }
}

