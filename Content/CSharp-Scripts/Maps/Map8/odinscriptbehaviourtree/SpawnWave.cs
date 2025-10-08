namespace BehaviourTrees;


class SpawnWaveClass : OdinLayout
{


    public bool SpawnWave(
               Vector3 SpawnLoc,
   int LaneId,
   bool ReverseLane,
   TeamId Team,
   int encounter
         )
    {
        return
                    // Sequence name :SpawnWave

                    SpawnSquadFromEncounter(
                          out squadID,
                          encounter,
                          SpawnLoc,
                          Team,
                          "waveSpawn") &&
                    SquadPushLane(
                          squadID,
                          LaneId,
                          ReverseLane
                          )

              ;
    }
}

