using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class MinionGraveyardHelperClass : OdinLayout 
{
     

     public bool MinionGraveyardHelper( 

           TeamId LaneOwner,
    Vector3 LaneGraveyardPosition,
    float KillRadius,
    string SquadNameToIgnore_A,
    string SquadNameToIgnore_B,
    AttackableUnit OrderShrineTurret)
      {
      return
            // Sequence name :Sequence
            (
                  GetUnitsInTargetArea(
                        out AllMinions, 
                        OrderShrineTurret, 
                        LaneGraveyardPosition, 
                        KillRadius, 
                        AffectEnemies | AffectFriends | AffectMinions | AffectNotPet 
                        ) &&
                         DebugAction($"///////////////// AllMinions {AllMinions.Count()}") &&
                  AddString(
                        out SquadNameToIgnoreChaos_A, 
                        SquadNameToIgnore_A,
                        "_CHAOS") &&
                  AddString(
                        out SquadNameToIgnoreOrder_A, 
                        SquadNameToIgnore_A,
                        "_ORDER") &&
                  AddString(
                        out SquadNameToIgnoreChaos_B, 
                        SquadNameToIgnore_B,
                        "_CHAOS") &&
                  AddString(
                        out SquadNameToIgnoreOrder_B, 
                        SquadNameToIgnore_B,
                        "_ORDER") &&
                  ForEach(AllMinions,Minion => (           
                  // Sequence name :Sequence
                        (
                              GetSquadNameOfUnit(
                                    out SquadName, 
                                    Minion) &&
                                    DebugAction($"///////////////// SquadName {SquadName}") &&
                                    //this has fucking no sens , i all case minion will not get killed ? rito ? 
                           /*   NotEqualString(
                                    SquadName, 
                                    "") &&
                                    DebugAction($"///////////////// NotEqualString {SquadNameToIgnoreChaos_A}") &&
                              NotEqualString(
                                   SquadName, 
                                    SquadNameToIgnoreChaos_A) &&
                                     DebugAction($"///////////////// NotEqualString {SquadNameToIgnoreChaos_B}") &&
                              NotEqualString(
                                    SquadName, 
                                    SquadNameToIgnoreChaos_B) &&
                              NotEqualString(
                                    SquadName, 
                                    SquadNameToIgnoreOrder_A) &&
                              NotEqualString(
                                    SquadName, 
                                    SquadNameToIgnoreOrder_B) &&*/
                              GetUnitTeam(
                                    out MinionTeam, 
                                    Minion) &&
                                    DebugAction($"///////////////// MinionTeam {MinionTeam}") &&
                                    DebugAction($"///////////////// LaneOwner {LaneOwner}") &&
                              // Sequence name :Either_Chaos_Order
                              (
                                    // Sequence name :OwnerIsOrder
                                    (
                                          LaneOwner == TeamId.TEAM_ORDER &&
                                          MinionTeam != TeamId.TEAM_ORDER
                                    ) ||
                                    // Sequence name :OwnerIsChaos
                                    (
                                          LaneOwner == TeamId.TEAM_CHAOS &&
                                          MinionTeam != TeamId.TEAM_CHAOS
                                    )
                              ) &&
                              DebugAction($"/////////////////kill em with kindness") &&
                              KillUnit(
                                    Minion, 
                                    Minion)
                        ))
                  ) &&
                  GetGameTime(
                        out CurrentTime)

            );
      }
}

