namespace BehaviourTrees.Map8;


class DisconnectAdjustmentManagerClass : DisconnectAdjustments
{


    public bool DisconnectAdjustmentManager(
    out int __DisconnectAdjustmentEntityID,
    AttackableUnit ReferenceUnit,
    bool DisconnectAdjustmentEnabled,
    int DisconnectAdjustmentEntityID,
    AISquadClass Squad_WaitAtBase,
    IEnumerable<AttackableUnit> MyEntities)
    {
        var assignBotToSquad = new AssignBotToSquadClass();

        int _DisconnectAdjustmentEntityID = DisconnectAdjustmentEntityID;


        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :Sequence
                  (
                        DisconnectAdjustmentEnabled == true &&
                        GetUnitTeam(
                              out ReferenceTeam,
                              ReferenceUnit) &&
                        GetCollectionCount(
                              out BotCount,
                              MyEntities) &&
                        // Sequence name :GetEnemyTeam
                        (
                              // Sequence name :IsOrder
                              (
                                    ReferenceTeam == TeamId.TEAM_ORDER &&
                                    SetVarUnitTeam(
                                          out EnemyTeam,
                                          TeamId.TEAM_CHAOS)
                              ) ||
                              // Sequence name :IsChaos
                              (
                                    ReferenceTeam == TeamId.TEAM_CHAOS &&
                                    SetVarUnitTeam(
                                          out EnemyTeam,
                                          TeamId.TEAM_ORDER)
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
                                          BotCount) &&
                                    LessInt(
                                          DisconnectAdjustmentEntityID,
                                          0) &&
                                    SetVarInt(
                                          out _DisconnectAdjustmentEntityID,
                                          1) &&
                                          DebugAction("AssignBotToSquad") &&
                                    assignBotToSquad.AssignBotToSquad(
                                          MyEntities,
                                          _DisconnectAdjustmentEntityID,
                                          Squad_WaitAtBase)
                                    && DebugAction("FINALLY ASIGNED")
                              ) ||
                              // Sequence name :NoDisconnectedPlayer_Despawn
                              (
                                    GreaterEqualInt(
                                          ConnectedPlayersOnEnemyTeam,
                                          BotCount) &&
                                    GreaterEqualInt(
                                          DisconnectAdjustmentEntityID,
                                          0) &&
                                    SetVarInt(
                                          out _DisconnectAdjustmentEntityID,
                                          -1)

                              )
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            ;


        __DisconnectAdjustmentEntityID = _DisconnectAdjustmentEntityID;

        return result;
    }
}

