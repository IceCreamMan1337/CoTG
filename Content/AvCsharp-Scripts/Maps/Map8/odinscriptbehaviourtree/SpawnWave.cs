using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

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
            (
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

            );
      }
}

