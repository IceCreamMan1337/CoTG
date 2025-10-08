using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class ValidateAdjacentPointsAndMinionsClass : AImission_bt
{


     public bool ValidateAdjacentPointsAndMinions(
                out bool IsValid_AdjCapturePoint1,
      out bool IsValid_AdjCapturePoint0,
      out bool IsValid_MinionFromPoint0,
      out bool IsValid_MinionFromPoint1,
      out AttackableUnit MinionFromPoint0,
      out AttackableUnit MinionFromPoint1,
    AttackableUnit AdjacentCapturePoint0,
    AttackableUnit AdjacentCapturePoint1,
    int AdjacentCapturePointIndex0,
    int AdjacentCapturePointIndex1,
    AttackableUnit ReferenceUnit,
    AttackableUnit CapturePointTarget,
    int CapturePointTargetIndex
          )
    {
        bool _IsValid_AdjCapturePoint1 = default;
      bool _IsValid_AdjCapturePoint0 = default;
      bool _IsValid_MinionFromPoint0 = default;
    bool _IsValid_MinionFromPoint1 = default;
    AttackableUnit _MinionFromPoint0 = default;
    AttackableUnit _MinionFromPoint1 = default;
        var getMinionSquadName = new GetMinionSquadNameClass();


    bool result = 

            // Sequence name :Sequence
            (
                  GetUnitTeam(
                        out ReferenceTeam, 
                        ReferenceUnit) &&
                  GetUnitTeam(
                        out ToCaptureTeam, 
                        CapturePointTarget) &&
                  SetVarBool(
                        out IsValid_AdjCapturePoint0, 
                        false) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              NotEqualUnitTeam(
                                    ToCaptureTeam, 
                                    TeamId.TEAM_NEUTRAL) &&
                              GetUnitTeam(
                                    out TeamCapturePoint0, 
                                    AdjacentCapturePoint0) &&
                              TeamCapturePoint0 == ReferenceTeam &&
                              SetVarBool(
                                    out _IsValid_AdjCapturePoint0, 
                                    true)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  SetVarBool(
                        out _IsValid_AdjCapturePoint1, 
                        false) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              NotEqualUnitTeam(
                                    ToCaptureTeam, 
                                    TeamId.TEAM_NEUTRAL) &&
                              GetUnitTeam(
                                    out TeamCapturePoint1, 
                                    AdjacentCapturePoint1) &&
                              TeamCapturePoint1 == ReferenceTeam &&
                              SetVarBool(
                                    out _IsValid_AdjCapturePoint1, 
                                    true)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :FindClosestMinionsFromCapturePoint
                  (
                        SetVarBool(
                              out _IsValid_MinionFromPoint0, 
                              false) &&
                        SetVarBool(
                              out _IsValid_MinionFromPoint1, 
                              false) &&
                        getMinionSquadName.GetMinionSquadName(
                              out FromSquadName0, 
                              out FromSquadName1, 
                              CapturePointTargetIndex, 
                              AdjacentCapturePointIndex0, 
                              AdjacentCapturePointIndex1, 
                              ReferenceTeam) &&
                        GetUnitPosition(
                              out CapturePointPosition, 
                              CapturePointTarget) &&
                        GetUnitsInTargetArea(
                              out Minions, 
                              ReferenceUnit, 
                              CapturePointPosition, 
                              10000, 
                              AffectFriends | AffectMinions
                              ) &&
                        SetVarFloat(
                              out ClosestMinionDistance0, 
                              50000) &&
                        SetVarFloat(
                              out ClosestMinionDistance1, 
                              50000) &&
                        ForEach(Minions,Minion => (
                              // Sequence name :Sequence
                              (
                                    GetSquadNameOfUnit(
                                          out MinionSquadName, 
                                          Minion) &&
                                    // Sequence name :Selector
                                    (
                                          // Sequence name :MinionFromCapturePoint0
                                          (
                                                FromSquadName0 == MinionSquadName &&
                                                GetDistanceBetweenUnits(
                                                      out CurrentDistance, 
                                                      Minion, 
                                                      CapturePointTarget) &&
                                                LessFloat(
                                                      CurrentDistance, 
                                                      ClosestMinionDistance0) &&
                                                SetVarAttackableUnit(
                                                      out _MinionFromPoint0, 
                                                      Minion) &&
                                                SetVarFloat(
                                                      out ClosestMinionDistance0, 
                                                      CurrentDistance) &&
                                                SetVarBool(
                                                      out _IsValid_MinionFromPoint0, 
                                                      true)
                                          ) ||
                                          // Sequence name :MinionFromCapturePoint1
                                          (
                                                FromSquadName1 == MinionSquadName &&
                                                GetDistanceBetweenUnits(
                                                      out CurrentDistance, 
                                                      Minion, 
                                                      CapturePointTarget) &&
                                                LessFloat(
                                                      CurrentDistance, 
                                                      ClosestMinionDistance1) &&
                                                SetVarAttackableUnit(
                                                      out _MinionFromPoint1, 
                                                      Minion) &&
                                                SetVarFloat(
                                                      out ClosestMinionDistance1, 
                                                      CurrentDistance) &&
                                                SetVarBool(
                                                      out _IsValid_MinionFromPoint1, 
                                                      true)

                                          )
                                    )
                              ))
                        )
                  )
            );
        IsValid_AdjCapturePoint1 = _IsValid_AdjCapturePoint1;
        IsValid_AdjCapturePoint0 = _IsValid_AdjCapturePoint0;
        IsValid_MinionFromPoint0 = _IsValid_MinionFromPoint0;
        IsValid_MinionFromPoint1 = _IsValid_MinionFromPoint1;
        MinionFromPoint0 = _MinionFromPoint0;
        MinionFromPoint1 = _MinionFromPoint1;
        return result;
      }
}

