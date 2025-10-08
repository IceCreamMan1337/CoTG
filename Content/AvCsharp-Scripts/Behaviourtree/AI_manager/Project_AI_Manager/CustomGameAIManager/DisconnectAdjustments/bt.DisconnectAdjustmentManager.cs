/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class DisconnectAdjustmentManager : BehaviourTree 
{
      out int DisconnectAdjustmentEntityID;
      AttackableUnit ReferenceUnit;
      bool DisconnectAdjustmentEnabled;
      int DisconnectAdjustmentEntityID;
      AISquad Squad_WaitAtBase;
      AttackableUnitCollection MyEntities;

      bool DisconnectAdjustmentManager()
      {
      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        DisconnectAdjustmentEnabled == true &&
                        GetUnitTeam(
                              out ReferenceTeam, 
                              ReferenceUnit) &&
                        // Sequence name :GetEnemyTeam
                        (
                              // Sequence name :IsOrder
                              (
                                    ReferenceTeam == TeamId.TEAM_BLUE &&
                                    SetVarUnitTeam(
                                          out EnemyTeam, 
                                          TeamId.TEAM_PURPLE)
                              ) ||
                              // Sequence name :IsChaos
                              (
                                    ReferenceTeam == TeamId.TEAM_PURPLE &&
                                    SetVarUnitTeam(
                                          out EnemyTeam, 
                                          TeamId.TEAM_BLUE)
                              )
                        ) &&
                        GetNumberOfConnectedPlayers(
                              out ConnectedPlayersOnEnemyTeam, 
                              EnemyTeam) &&
                        // Sequence name :DisconnectAdjustments
                        (
                              // Sequence name :DisconnectedPlayers_Spawn
                              (
                                    LessInt(
                                          ConnectedPlayersOnEnemyTeam, 
                                          5) &&
                                    LessInt(
                                          DisconnectAdjustmentEntityID, 
                                          0) &&
                                    SetVarInt(
                                          out DisconnectAdjustmentEntityID, 
                                          4) &&
                                    AssignBotToSquad(
                                          MyEntities, 
                                          DisconnectAdjustmentEntityID, 
                                          Squad_WaitAtBase)
                              ) ||
                              // Sequence name :NoDisconnectedPlayer_Despawn
                              (
                                    GreaterEqualInt(
                                          ConnectedPlayersOnEnemyTeam, 
                                          5) &&
                                    GreaterEqualInt(
                                          DisconnectAdjustmentEntityID, 
                                          0) &&
                                    SetVarInt(
                                          out DisconnectAdjustmentEntityID, 
                                          -1)

                              )
                        )
                  )
            );
      }
}

*/