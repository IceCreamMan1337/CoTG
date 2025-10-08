using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class EndOfGameCeremonyClass_forscript : OdinLayout 
{


     public bool EndOfGameCeremony(
          out int __EndGameState,
    TeamId WinningTeam,
    uint NexusCrystalParticle_L1, //  DWORD ??? 
    uint NexusCrystalParticle_L2,
    uint NexusCrystalParticle_L3,
    uint NexusCrystalParticle_L4,
    uint NexusCrystalParticle_R1,
    uint NexusCrystalParticle_R2,
    uint NexusCrystalParticle_R3,
    uint NexusCrystalParticle_R4,
    int EndGameState,
    float ScoreOrder,
    float ScoreChaos,
    uint NexusCrystalGlow_1,
    uint NexusCrystalGlow_2,
    uint NexusCrystalGlow_3,
    uint NexusCrystalGlow_4,
    uint NexusCrystal,
    uint NexusCrystalParticleRed_L1,
    uint NexusCrystalParticleRed_L2,
    uint NexusCrystalParticleRed_L3,
    uint NexusCrystalParticleRed_L4,
    uint NexusCrystalParticleRed_R1,
    uint NexusCrystalParticleRed_R2,
    uint NexusCrystalParticleRed_R3,
    uint NexusCrystalParticleRed_R4
          
          )
      {
       int _EndGameState = EndGameState;
bool result =
            // Sequence name :Sequence
            (
                  GetGameTime(
                        out CurrentGameTime) &&
                  // Sequence name :Selector
                  (
                        // Sequence name :Init_Stage1
                        (
                              __IsFirstRun == true &&
                              // Sequence name :OrderOrChaos
                              (
                                    // Sequence name :OrderWon
                                    (
                                          WinningTeam == TeamId.TEAM_ORDER &&
                                          MakeVector(
                                                out NexusDeathPosition, 
                                                12049.1f, 
                                                -194.162f, 
                                                4287.78f) &&
                                          GetPropByName(
                                                out StairsProp,
                                                "LevelProp_Odin_SoG_Chaos") &&
                                          GetPropByName(
                                                out CrystalProp,
                                                "LevelProp_Odin_SOG_Chaos_Crystal")
                                    ) ||
                                    // Sequence name :ChaosWon
                                    (
                                          WinningTeam == TeamId.TEAM_CHAOS &&
                                          MakeVector(
                                                out NexusDeathPosition, 
                                                1689.68f, 
                                                -81.599f, 
                                                4414.49f) &&
                                          GetPropByName(
                                                out StairsProp,
                                                "LevelProp_Odin_SoG_Order") &&
                                          GetPropByName(
                                                out CrystalProp,
                                                "LevelProp_Odin_SOG_Order_Crystal")
                                    )
                              ) &&
                              RemoveParticle(
                                    NexusCrystal) &&
                              PlayAnimationOnProp(
                                    StairsProp,
                                    "Death", 
                                   false 
                                    ) &&
                              PlayAnimationOnProp(
                                    CrystalProp,
                                    "Death", 
                                    false, 
                                    4.05f) &&
                              AddPositionPerceptionBubble(
                                    out OrderPerc, 
                                    NexusDeathPosition, 
                                    1200, 
                                    300, 
                                    TeamId.TEAM_ORDER, 
                                    false, 
                                    null, 
                                    null) &&
                              AddPositionPerceptionBubble(
                                    out ChaosPerc, 
                                    NexusDeathPosition, 
                                    1200, 
                                    300, 
                                    TeamId.TEAM_CHAOS, 
                                    false,
                                    null,
                                    null) &&
                              RemoveParticle(
                                    NexusCrystalParticle_L1) &&
                              RemoveParticle(
                                    NexusCrystalParticle_L2) &&
                              RemoveParticle(
                                    NexusCrystalParticle_L3) &&
                              RemoveParticle(
                                    NexusCrystalParticle_L4) &&
                              RemoveParticle(
                                    NexusCrystalParticle_R1) &&
                              RemoveParticle(
                                    NexusCrystalParticle_R2) &&
                              RemoveParticle(
                                    NexusCrystalParticle_R3) &&
                              RemoveParticle(
                                    NexusCrystalParticle_R4) &&
                              RemoveParticle(
                                    NexusCrystalParticleRed_L1) &&
                              RemoveParticle(
                                    NexusCrystalParticleRed_L2) &&
                              RemoveParticle(
                                    NexusCrystalParticleRed_L3) &&
                              RemoveParticle(
                                    NexusCrystalParticleRed_L4) &&
                              RemoveParticle(
                                    NexusCrystalParticleRed_R1) &&
                              RemoveParticle(
                                    NexusCrystalParticleRed_R2) &&
                              RemoveParticle(
                                    NexusCrystalParticleRed_R3) &&
                              RemoveParticle(
                                    NexusCrystalParticleRed_R4) &&
                              RemoveParticle(
                                    NexusCrystalGlow_1) &&
                              RemoveParticle(
                                    NexusCrystalGlow_2) &&
                              RemoveParticle(
                                    NexusCrystalGlow_3) &&
                              RemoveParticle(
                                    NexusCrystalGlow_4) &&
                              SetVarFloat(
                                    out EoGStartTime, 
                                    CurrentGameTime) &&
                              SetGreyscaleEnabledWhenDead(
                                    false) &&
                              ToggleSpecificUserInput(
                                    "ABILTIES", 
                                    false) &&
                              ToggleSpecificUserInput(
                                    "CAMERAMOVEMENT", 
                                    false) &&
                              ToggleSpecificUserInput(
                                    "CAMERLOCKING", 
                                    false) &&
                              ToggleSpecificUserInput(
                                    "MINIMAPMOVEMENT", 
                                    false) &&
                              ToggleSpecificUserInput(
                                    "MOVEMENT", 
                                    false) &&
                              ToggleSpecificUserInput(
                                    "SHOP", 
                                    false) &&
                              ToggleSpecificUserInput(
                                    "SUMMONERSPELLS", 
                                    false) &&
                              HaltAllUnits() &&
                              DisableHUDForEndOfGame() &&
                              PanCameraFromCurrentPositionToPointAllHeroes(
                                    NexusDeathPosition, 
                                    2) &&
                              SetVarInt(
                                    out _EndGameState, 
                                    0)
                        ) ||
                        // Sequence name :Stage2
                        (
                              _EndGameState == 0 &&
                              SubtractFloat(
                                    out TimePassed, 
                                    CurrentGameTime, 
                                    EoGStartTime) &&
                              GreaterFloat(
                                    TimePassed, 
                                    4) &&
                              PlayAnimationOnProp(
                                    CrystalProp,
                                    "Explode", 
                                    false, 
                                    2) &&
                              SetVarInt(
                                    out _EndGameState, 
                                    1)
                        ) ||
                        // Sequence name :Stage3
                        (
                              GreaterEqualInt(
                                    _EndGameState, 
                                    0) &&
                              SubtractFloat(
                                    out TimePassed, 
                                    CurrentGameTime, 
                                    EoGStartTime) &&
                              GreaterFloat(
                                    TimePassed, 
                                    6) &&
                              ToggleSpecificUserInput(
                                    "CHAT", 
                                    false) &&
                              EndGame(
                                    WinningTeam, 
                                    ScoreChaos, 
                                    ScoreOrder)

                        )
                  )
            );

        __EndGameState = _EndGameState;
        return result;
      }
}

