using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetGuardianClass : OdinLayout 
{

    private modesGetCapturePointsClass getCapturePoints = new modesGetCapturePointsClass();
    public bool GetGuardian(
                out AttackableUnit Guardian,
    int GuardianIndexToFind)
      {

   
        AttackableUnit _Guardian = default;
bool result =
            // Sequence name :Sequence
            (    
                  getCapturePoints.GetCapturePoints(
                        out CapturePointPositionA, 
                        out CapturePointPositionB, 
                        out CapturePointPositionC, 
                        out CapturePointPositionD, 
                        out CapturePointPositionE) &&
                  GreaterEqualInt(
                        GuardianIndexToFind, 
                        0) &&
                  LessEqualInt(
                        GuardianIndexToFind, 
                        4) &&
                 /* GetTurret(
                        out OrderShrineTurret, 
                        TeamId.TEAM_ORDER, 
                        0, 
                        1) && */
                  SetVarBool(
                        out FoundGuardian, 
                        false) &&
                  // Sequence name :FindGuardianAtPoint
                  (
                        // Sequence name :Point0
                        (
                              GuardianIndexToFind == 0 &&
                              GetUnitsBySquadName(
                                    out MinionsInArea,
                                    "CaptureGuardian0") &&
                                    
                              ForEach(MinionsInArea,minion => (
                                    // Sequence name :Sequence
                                    (
                                          GetUnitSkinName(
                                                out MinionName,
                                                minion) &&
                                          // Sequence name :DeadUnit_Is_Guardian
                                          (
                                                MinionName == "OdinNeutralGuardian" ||          
                                                MinionName == "OdinOrderGuardian" ||
                                                MinionName == "OdinChaosGuardian"
                                          ) &&
                                          
                                          SetVarAttackableUnit(
                                                out _Guardian, 
                                                minion) &&
                                               
                                          SetVarBool(
                                                out FoundGuardian, 
                                                true)
                                    )
                              ))
                        ) ||
                        // Sequence name :Point1
                        (
                              GuardianIndexToFind == 1 &&
                              GetUnitsBySquadName(
                                    out MinionsInArea,
                                    "CaptureGuardian1") &&
                              ForEach(MinionsInArea,minion => (
                                    // Sequence name :Sequence
                                    (
                                          GetUnitSkinName(
                                                out MinionName, 
                                                minion) &&
                                          // Sequence name :DeadUnit_Is_Guardian
                                          (
                                                MinionName == "OdinNeutralGuardian" ||
                                                MinionName == "OdinOrderGuardian" ||
                                                MinionName == "OdinChaosGuardian"
                                          ) &&
                                          SetVarAttackableUnit(
                                                out _Guardian, 
                                                minion) &&
                                          SetVarBool(
                                                out FoundGuardian, 
                                                true)
                                    )
                              ))
                        ) ||
                        // Sequence name :Point2
                        (
                              GuardianIndexToFind == 2 &&
                              GetUnitsBySquadName(
                                    out MinionsInArea,
                                    "CaptureGuardian2") &&
                              ForEach(MinionsInArea,minion => (
                                    // Sequence name :Sequence
                                    (
                                          GetUnitSkinName(
                                                out MinionName, 
                                                minion) &&
                                          // Sequence name :DeadUnit_Is_Guardian
                                          (
                                                MinionName == "OdinNeutralGuardian" ||
                                                MinionName == "OdinOrderGuardian" ||
                                                MinionName == "OdinChaosGuardian"
                                          ) &&
                                          SetVarAttackableUnit(
                                                out _Guardian, 
                                                minion) &&
                                          SetVarBool(
                                                out FoundGuardian, 
                                                true)
                                    )
                              ))
                        ) ||
                        // Sequence name :Point3
                        (
                              GuardianIndexToFind == 3 &&
                              GetUnitsBySquadName(
                                    out MinionsInArea,
                                    "CaptureGuardian3") &&
                              ForEach(MinionsInArea,minion => (
                                    // Sequence name :Sequence
                                    (
                                          GetUnitSkinName(
                                                out MinionName, 
                                                minion) &&
                                          // Sequence name :DeadUnit_Is_Guardian
                                          (
                                                MinionName == "OdinNeutralGuardian"   ||
                                                MinionName == "OdinOrderGuardian" ||
                                                MinionName == "OdinChaosGuardian"
                                          ) &&
                                          SetVarAttackableUnit(
                                                out _Guardian, 
                                                minion) &&
                                          SetVarBool(
                                                out FoundGuardian, 
                                                true)
                                    )
                              ))
                        ) ||
                        // Sequence name :Point4
                        (
                              GuardianIndexToFind == 4 &&
                              GetUnitsBySquadName(
                                    out MinionsInArea,
                                    "CaptureGuardian4") &&
                              ForEach(MinionsInArea,minion => (
                                    // Sequence name :Sequence
                                    (
                                          GetUnitSkinName(
                                                out MinionName, 
                                                minion) &&
                                          // Sequence name :DeadUnit_Is_Guardian
                                          (
                                                MinionName == "OdinNeutralGuardian" ||
                                                MinionName == "OdinOrderGuardian" ||
                                                MinionName == "OdinChaosGuardian"
                                          ) &&
                                          SetVarAttackableUnit(
                                                out _Guardian, 
                                                minion) &&
                                          SetVarBool(
                                                out FoundGuardian, 
                                                true)
                                    ))
                         )
                        ) &&
                  FoundGuardian == true
                  ) 

            );

        Guardian = _Guardian;
        return result;
      }
}

