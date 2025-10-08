namespace BehaviourTrees;


class SpawnMinionWaveClass_forscript : OdinLayout
{


    public bool SpawnMinionWave(
               Vector3 StartingPosition,
   TeamId ControllingTeam,
   TeamId OpposingPointOwner,
   int EncounterID,
   int LaneID,
   bool ReverseMinionPath,
   string SquadName,
   int NumOrderPoints,
   int NumChaosPoints
         )
    {
        return
                    // Sequence name :Sequence

                    DebugAction(

                          "In Spawn Test") &&
                    NotEqualUnitTeam(
                          ControllingTeam,
                          TeamId.TEAM_NEUTRAL) &&
                    DebugAction(

                          "Point is not neutral") &&
                    NotEqualUnitTeam(
                          ControllingTeam,
                          OpposingPointOwner) &&
                    DebugAction(
                         "Point is opposite"
                          ) &&
                    // Sequence name :Update &quot;catchup&quot; mutators
                    (
                          // Sequence name :Sequence
                          (
                                ControllingTeam == TeamId.TEAM_ORDER &&
                                UpdateMutator(
                                      EncounterID,
                                      "CapturePointMutator",
                                      NumChaosPoints)
                          ) ||
                          UpdateMutator(
                                EncounterID,
                                "CapturePointMutator",
                                NumOrderPoints)
                    ) &&
                    SpawnSquadFromEncounter(
                          out SquadId,
                          EncounterID,
                          StartingPosition,
                          ControllingTeam,
                          "SquadName") &&
                    SquadPushLane(
                          SquadId,
                          LaneID,
                          ReverseMinionPath,
                          true) &&
                    DebugAction(

                          "Spawned!")

              ;
    }
}

