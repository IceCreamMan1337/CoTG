using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class SpawnMinionWaveClass : OdinLayout 
{


     public bool SpawnMinionWave(
               out float __PreviousSpawnTime,
    Vector3 StartingPosition,
      TeamId ControllingTeam,
    int EncounterID,
    int LaneID,
    bool ReverseMinionPath,
    string SquadName,
    int NumOrderPoints,
    int NumChaosPoints,
    TeamId OpposingPointController,
    float PreviousSpawnTime,
    float SpawnRate_Seconds,
    int MinionSpawnPortalParticleEncounterID
         
         )
      {

        float _PreviousSpawnTime = PreviousSpawnTime;


bool result = 
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        NotEqualUnitTeam(
                              ControllingTeam, 
                              TeamId.TEAM_NEUTRAL) &&
                        NotEqualUnitTeam(
                              OpposingPointController, 
                              TeamId.TEAM_NEUTRAL) &&
                        NotEqualUnitTeam(
                              OpposingPointController, 
                              ControllingTeam) &&
                        GetGameTime(
                              out CurrentGameTime) &&
                        SubtractFloat(
                              out TimePassed, 
                              CurrentGameTime,
                              _PreviousSpawnTime) &&
                             
                        // Sequence name :FirstTimeSpawning_Or_RegularSpawning
                        (
                              // Sequence name :RegularSpawn
                              (
                                    GreaterEqualFloat(
                                          TimePassed, 
                                          SpawnRate_Seconds)
                              ) ||
                              // Sequence name :FreshSpawn
                              (
                                    LessFloat(
                                          TimePassed, 
                                          0) &&
                                    MultiplyFloat(
                                          out NegativeSpawnRate, 
                                          SpawnRate_Seconds, 
                                          -1) &&
                                    GreaterEqualFloat(
                                          TimePassed, 
                                          NegativeSpawnRate)
                              )
                        ) &&
                 
                        // Sequence name :MutatorCalculations_CP_Variance
                        (
                              // Sequence name :TeamCalculations
                              (
                                    // Sequence name :Order
                                    (
                                          ControllingTeam == TeamId.TEAM_ORDER &&
                                          SubtractInt(
                                                out PointDiff, 
                                                NumChaosPoints, 
                                                NumOrderPoints)
                                    ) ||
                                    // Sequence name :Chaos
                                    (
                                          ControllingTeam == TeamId.TEAM_CHAOS &&
                                          SubtractInt(
                                                out PointDiff, 
                                                NumOrderPoints, 
                                                NumChaosPoints)
                                    )
                              ) &&
                              MaxInt(
                                    out CapturePointMutator, 
                                    PointDiff, 
                                    0) &&
                              UpdateMutator(
                                    EncounterID,
                                    "CapturePointMutator", 
                                    CapturePointMutator)
                        ) &&
                  
                        // Sequence name :UpdateLaneID_Name_With_Team
                        (
                              // Sequence name :Sequence
                              (
                                    ControllingTeam == TeamId.TEAM_ORDER &&
                                    AddString(
                                          out SquadName_Team, 
                                          SquadName,
                                          "_ORDER")
                              ) ||
                              // Sequence name :Sequence
                              (
                                    ControllingTeam == TeamId.TEAM_CHAOS &&
                                    AddString(
                                          out SquadName_Team, 
                                          SquadName,
                                          "_CHAOS")
                              )
                        ) &&
                        SpawnSquadFromEncounter(
                              out SquadId3, 
                              EncounterID, 
                              StartingPosition, 
                              ControllingTeam, 
                              SquadName_Team
                              ) &&
                        SquadPushLane(
                              SquadId3, 
                              LaneID, 
                              ReverseMinionPath, 
                              true) &&
                        SetVarFloat(
                              out _PreviousSpawnTime, 
                              CurrentGameTime)

                  )
                  ||
                               DebugAction("MaskFailure")
            );

        __PreviousSpawnTime = _PreviousSpawnTime;
        return result;
    }
}

