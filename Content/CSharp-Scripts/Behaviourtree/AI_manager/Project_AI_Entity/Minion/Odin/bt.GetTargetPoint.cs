namespace BehaviourTrees.Map8;


class GetTargetPointClass : IODIN_MinionAIBT
{


    public bool GetTargetPoint(
        out int TargetIndex, ObjAIBase self)
    {
        Self = self;
        var getCapturePointOwner = new GetCapturePointOwnerClass();
        var isEnemyTeam = new IsEnemyTeamClass();
        int _TargetIndex = default;
        bool result =
                    // Sequence name :Sequence

                    /*GetUnitAISelf(
                          out Self) &&*/
                    SetVarInt(
                          out _TargetIndex,
                          -1) &&
                    GetUnitTeam(
                          out MyTeam,
                          Self) &&
                          /* origin is that but we can simplify it 
                    getCapturePointOwner.GetCapturePointOwner(
                          out CapturePointOwner_A,
                          0,
                          self) &&
                    getCapturePointOwner.GetCapturePointOwner(
                          out CapturePointOwner_B,
                          1,
                          self) &&
                    getCapturePointOwner.GetCapturePointOwner(
                          out CapturePointOwner_C,
                          2,
                          self) &&
                    getCapturePointOwner.GetCapturePointOwner(
                          out CapturePointOwner_D,
                          3,
                          self) &&
                    getCapturePointOwner.GetCapturePointOwner(
                          out CapturePointOwner_E,
                          4,
                          self) &&*/

                          GetCapturePointOwnerBT(
                          out CapturePointOwner_A,
                          0) &&
                    GetCapturePointOwnerBT(
                          out CapturePointOwner_B,
                          1) &&
                    GetCapturePointOwnerBT(
                          out CapturePointOwner_C,
                          2) &&
                    GetCapturePointOwnerBT(
                          out CapturePointOwner_D,
                          3) &&
                   GetCapturePointOwnerBT(
                          out CapturePointOwner_E,
                          4) &&





                    // Sequence name :Order_Or_Chaos
                    (
                          // Sequence name :Order
                          (
                                MyTeam == TeamId.TEAM_ORDER &&
                                // Sequence name :EDCBA
                                (
                                            // Sequence name :EDCBA_Enemy

                                            // Sequence name :Test_E_is_eneny
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_E) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        4)
                                            ) ||
                                            // Sequence name :Test_D_is_eneny
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_D) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        3)
                                            ) ||
                                            // Sequence name :Test_C_is_eneny
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_C) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        2)
                                            ) ||
                                            // Sequence name :Test_B_is_enemy
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_B) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        1)
                                            ) ||
                                            // Sequence name :Test_A_is_enemy
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_A) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        0)
                                            )
                                       ||
                                            // Sequence name :EDCBA_Neutral

                                            // Sequence name :Test_E_is_Neutral
                                            (
                                                  CapturePointOwner_E == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        4)
                                            ) ||
                                            // Sequence name :Test_D_is_eneny
                                            (
                                                  CapturePointOwner_D == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        3)
                                            ) ||
                                            // Sequence name :Test_C_is_eneny
                                            (
                                                  CapturePointOwner_C == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        2)
                                            ) ||
                                            // Sequence name :Test_B_is_enemy
                                            (
                                                  CapturePointOwner_B == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        1)
                                            ) ||
                                            // Sequence name :Test_A_is_enemy
                                            (
                                                  CapturePointOwner_A == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        0)
                                            )
                                       ||
                                      SetVarInt(
                                            out _TargetIndex,
                                            4)
                                )
                          ) ||
                          // Sequence name :Chaos
                          (
                                MyTeam == TeamId.TEAM_CHAOS &&
                                // Sequence name :ABCDE
                                (
                                            // Sequence name :EDCBA_Enemy

                                            // Sequence name :Test_A_is_enemy
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_A) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        0)
                                            ) ||
                                            // Sequence name :Test_B_is_enemy
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_B) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        1)
                                            ) ||
                                            // Sequence name :Test_C_is_eneny
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_C) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        2)
                                            ) ||
                                            // Sequence name :Test_D_is_eneny
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_D) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        3)
                                            ) ||
                                            // Sequence name :Test_E_is_eneny
                                            (
                                                  isEnemyTeam.IsEnemyTeam(
                                                        MyTeam,
                                                        CapturePointOwner_E) &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        4)
                                            )
                                       ||
                                            // Sequence name :EDCBA_Neutral

                                            // Sequence name :Test_A_is_enemy
                                            (
                                                  CapturePointOwner_A == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        0)
                                            ) ||
                                            // Sequence name :Test_B_is_enemy
                                            (
                                                  CapturePointOwner_B == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        1)
                                            ) ||
                                            // Sequence name :Test_C_is_eneny
                                            (
                                                  CapturePointOwner_C == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        2)
                                            ) ||
                                            // Sequence name :Test_D_is_eneny
                                            (
                                                  CapturePointOwner_D == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        3)
                                            ) ||
                                            // Sequence name :Test_E_is_Neutral
                                            (
                                                  CapturePointOwner_E == TeamId.TEAM_NEUTRAL &&
                                                  SetVarInt(
                                                        out _TargetIndex,
                                                        4)
                                            )
                                       ||
                                      SetVarInt(
                                            out _TargetIndex,
                                            -1)

                                )
                          )
                    )
              ;
        TargetIndex = _TargetIndex;
        return result;
    }
}