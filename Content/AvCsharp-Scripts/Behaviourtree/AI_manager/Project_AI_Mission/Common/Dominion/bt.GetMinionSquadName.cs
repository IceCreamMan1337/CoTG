using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using BehaviourTrees.Map8;

namespace BehaviourTrees;


class GetMinionSquadNameClass : AImission_bt
{


     public bool GetMinionSquadName(
                out string SquadName0,
      out string SquadName1,
    int TargetCapturePointIndex,
    int FromCapturePointIndex0,
    int FromCapturePointIndex1,
    TeamId ReferenceTeam
          )
    {
        var capturePointIndexToString = new CapturePointIndexToStringClass();


        string _SquadName0 = default;
      string _SquadName1 = default;


        bool result = 
            // Sequence name :Sequence
            (
                  SetVarString(
                        out _SquadName0, 
                        "") &&
                  SetVarString(
                        out _SquadName1,
                        "") &&
                  capturePointIndexToString.CapturePointIndexToString(
                        out _SquadName0, 
                        FromCapturePointIndex0) &&
                  capturePointIndexToString.CapturePointIndexToString(
                        out _SquadName1, 
                        FromCapturePointIndex1) &&
                  capturePointIndexToString.CapturePointIndexToString(
                        out TargetStringName, 
                        TargetCapturePointIndex) &&
                  AddString(
                        out _SquadName0, 
                        _SquadName0, 
                        TargetStringName) &&
                  AddString(
                        out _SquadName1, 
                        _SquadName1, 
                        TargetStringName) &&
                  AddString(
                        out _SquadName0, 
                        _SquadName0, 
                        "_") &&
                  AddString(
                        out _SquadName1, 
                        _SquadName1, 
                        "_") &&
                  // Sequence name :CHAOS_OR_ORDER
                  (
                        // Sequence name :ORDER
                        (
                              ReferenceTeam == TeamId.TEAM_ORDER &&
                              AddString(
                                    out _SquadName0, 
                                    _SquadName0,
                                    "ORDER") &&
                              AddString(
                                    out _SquadName1, 
                                    _SquadName1,
                                    "ORDER")
                        ) ||
                        // Sequence name :CHAOS
                        (
                              ReferenceTeam == TeamId.TEAM_CHAOS &&
                              AddString(
                                    out _SquadName0, 
                                    _SquadName0,
                                    "CHAOS") &&
                              AddString(
                                    out _SquadName1, 
                                    _SquadName1,
                                    "CHAOS")

                        )
                  )
            );
        SquadName0 = _SquadName0;
        SquadName1 = _SquadName1;
        return result;
      }
}

