namespace BehaviourTrees;


class EndOfGameCeremonyClass : OdinLayout
{


    public bool EndOfGameCeremony(
               out bool __EndCeremonyBool,
     out float __GameEndTime,
     out int __EndOfGameState,
   float ChaosVictoryPointTotal,
   float OrderVictoryPointTotal,
   TeamId WinningTeam,
   bool EndCeremonyBool,
   float GameEndTime,
   int EndOfGameState

         )
    {
        bool _EndCeremonyBool = EndCeremonyBool;
        float _GameEndTime = GameEndTime;
        int _EndOfGameState = EndOfGameState;


        bool result =
                                // Sequence name :MaskFailure

                                // Sequence name :Order_or_Chaos_winning?

                                // Sequence name :Order_Sequence?
                                (
                                      WinningTeam == TeamId.TEAM_ORDER &&
                                      GetChampionCollection(
                                            out ChampCollection) &&
                                      MakeVector(
                                            out ChaosDeathLoc,
                                            12049.1f,
                                            -194.162f,
                                            4287.78f) &&
                                      MakeVector(
                                            out OrderDeathLoc,
                                            1689.68f,
                                            -81.599f,
                                            4414.49f) &&
                                      AddPositionPerceptionBubble(
                                            out OrderPerc,
                                            ChaosDeathLoc,
                                            1200,
                                            300,
                                            TeamId.TEAM_ORDER,
                                            false,
                                            null,
                                            null) &&
                                      AddPositionPerceptionBubble(
                                            out ChaosPerc,
                                            ChaosDeathLoc,
                                            1200,
                                            300,
                                            TeamId.TEAM_CHAOS,
                                            false,
                                            null,
                                            null) &&
                                      AddPositionPerceptionBubble(
                                            out OrderPerc2,
                                            OrderDeathLoc,
                                            1200,
                                            300,
                                            TeamId.TEAM_ORDER,
                                            false,
                                            null,
                                            null) &&
                                      AddPositionPerceptionBubble(
                                            out ChaosPerc2,
                                            OrderDeathLoc,
                                            1200,
                                            300,
                                            TeamId.TEAM_CHAOS,
                                            false,
                                            null,
                                            null) &&
                                      // Sequence name :Selector
                                      (
                                            // Sequence name :First_Sequence
                                            (
                                                  _EndOfGameState == -1 &&
                                                  HaltAllUnits() &&
                                                  DisableHUDForEndOfGame() &&
                                                  CreatePositionParticle(
                                                        out OrderWinParticle,
                                                        ChaosDeathLoc,
                                                        "NexusDestroyedExplosionFinal_Chaos.troy",
                                                        null,
                                                        "",
                                                        default(Vector3),
                                                        default(Vector3),
                                                        null,
                                                        TeamId.TEAM_ALL,
                                                        0,
                                                        TeamId.TEAM_ALL,
                                                        false) &&
                                                  PanCameraFromCurrentPositionToPointAllHeroes(
                                                        ChaosDeathLoc,
                                                        2) &&
                                                  GetGameTime(
                                                        out _GameEndTime) &&
                                                  SetVarBool(
                                                        out _EndCeremonyBool,
                                                        false) &&
                                                  SetVarInt(
                                                        out _EndOfGameState,
                                                        0)
                                            ) ||
                                            // Sequence name :Second_Sequence
                                            (
                                                  _EndOfGameState == 0 &&
                                                  GetGameTime(
                                                        out CurrentGameTime) &&
                                                  SubtractFloat(
                                                        out TimeDiff,
                                                        CurrentGameTime,
                                                        _GameEndTime) &&
                                                  GreaterEqualFloat(
                                                        TimeDiff,
                                                        4) &&
                                                  EndGame(
                                                        WinningTeam,
                                                        ChaosVictoryPointTotal,
                                                        OrderVictoryPointTotal)
                                            )
                                      )
                                ) ||
                                // Sequence name :Order_Sequence?
                                (
                                      WinningTeam == TeamId.TEAM_CHAOS &&
                                      GetChampionCollection(
                                            out ChampCollection) &&
                                      MakeVector(
                                            out ChaosDeathLoc,
                                            12049.1f,
                                            -194.162f,
                                            4287.78f) &&
                                      MakeVector(
                                            out OrderDeathLoc,
                                            1689.68f,
                                            -81.599f,
                                            4414.49f) &&
                                      // Sequence name :Selector
                                      (
                                            // Sequence name :StageOne_HaltUnits
                                            (
                                                  _EndOfGameState == -1 &&
                                                  AddPositionPerceptionBubble(
                                                        out OrderPerc,
                                                        ChaosDeathLoc,
                                                        1200,
                                                        300,
                                                        TeamId.TEAM_ORDER,
                                                        false,
                                                        null,
                                                        null) &&
                                                  AddPositionPerceptionBubble(
                                                        out ChaosPerc,
                                                        ChaosDeathLoc,
                                                        1200,
                                                        300,
                                                        TeamId.TEAM_CHAOS,
                                                        false,
                                                        null,
                                                        null) &&
                                                  AddPositionPerceptionBubble(
                                                        out OrderPerc2,
                                                        OrderDeathLoc,
                                                        1200,
                                                        300,
                                                        TeamId.TEAM_ORDER,
                                                        false,
                                                        null,
                                                        null) &&
                                                  AddPositionPerceptionBubble(
                                                        out ChaosPerc2,
                                                        OrderDeathLoc,
                                                        1200,
                                                        300,
                                                        TeamId.TEAM_CHAOS,
                                                        false,
                                                        null,
                                                        null) &&
                                                  GetPropByName(
                                                        out OrderStairsProp,
                                                        "LevelProp_Odin_SoG_Order") &&
                                                  PlayAnimationOnProp(
                                                        OrderStairsProp,
                                                        "Death",
                                                        false,
                                                        6) &&
                                                  GetPropByName(
                                                        out CrystalProp,
                                                        "LevelProp_Odin_SOG_Order_Crystal") &&
                                                  PlayAnimationOnProp(
                                                        CrystalProp,
                                                        "Death",
                                                        false,
                                                        4) &&
                                                  GetGameTime(
                                                        out _GameEndTime) &&
                                                  SetVarInt(
                                                        out _EndOfGameState,
                                                        0) &&
                                                  DebugAction(

                                                        "Part1_Done")
                                            ) ||
                                            // Sequence name :Stage2_PlayExplosion
                                            (
                                                  _EndOfGameState == 0 &&
                                                  GetGameTime(
                                                        out CurrentGameTime) &&
                                                  SubtractFloat(
                                                        out TimeDiff,
                                                        CurrentGameTime,
                                                        _GameEndTime) &&
                                                  GreaterEqualFloat(
                                                        TimeDiff,
                                                        4) &&
                                                  GetPropByName(
                                                        out CrystalProp,
                                                        "LevelProp_Odin_SOG_Order_Crystal") &&
                                                  PlayAnimationOnProp(
                                                        CrystalProp,
                                                        "Explode",
                                                        false,
                                                        2) &&
                                                  SetVarInt(
                                                        out _EndOfGameState,
                                                        1) &&
                                                  DebugAction(

                                                        "PArt2_Done")
                                            ) ||
                                            // Sequence name :Stage3_EndGame
                                            (
                                                  _EndOfGameState == 1 &&
                                                  GetGameTime(
                                                        out CurrentGameTime) &&
                                                  SubtractFloat(
                                                        out TimeDiff,
                                                        CurrentGameTime,
                                                        GameEndTime) &&
                                                  GreaterEqualFloat(
                                                        TimeDiff,
                                                        6) &&
                                                  DebugAction(

                                                        "PArt3_Done")
                                            ) ||
                                            // Sequence name :Sequence
                                            (
                                                  SetVarBool(
                                                        out Run,
                                                        false) &&
                                                  Run == true &&
                                                  RemoveParticle(
                                                        NexusParticle_Order) &&
                                                  CreateUnitParticle(
                                                        out NexusParticle_Order,
                                                        OrderStairsProp as AttackableUnit,
                                                        "center_crystal",
                                                        "Odin_Crystal_blue.troy",
                                                        null,
                                                        "",
                                                        default(Vector3),
                                                        default(Vector3),
                                                        null,
                                                        TeamId.TEAM_ALL,
                                                        0,
                                                        TeamId.TEAM_ALL,

                                                        false) &&
                                                  DisableHUDForEndOfGame() &&
                                                  HaltAllUnits() &&
                                                  PanCameraFromCurrentPositionToPointAllHeroes(
                                                        OrderDeathLoc,
                                                        2) &&
                                                  EndGame(
                                                        WinningTeam,
                                                        ChaosVictoryPointTotal,
                                                        OrderVictoryPointTotal)

                                            )
                                      )
                                )

                          ||
                                       DebugAction("MaskFailure")
                    ;

        __EndCeremonyBool = _EndCeremonyBool;
        __GameEndTime = _GameEndTime;
        __EndOfGameState = _EndOfGameState;
        return result;
    }
}

