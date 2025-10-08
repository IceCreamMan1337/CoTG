using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class RelicHelperClass : OdinLayout 
{


     public bool RelicHelper(
                out float __RelicSpawnTime,
      out int __RelicSquadID,
    int RelicSquadID,
    float RelicSpawnTime,
    int RelicCampID,
    int RelicEncounterShield,
    Vector3 RelicPosition)
      {

        int _RelicSquadID = RelicSquadID;
        float _RelicSpawnTime = RelicSpawnTime;

      bool result = 
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        GetGameTime(
                              out CurrentTime) &&
                        // Sequence name :Spawn_Or_CheckSpawn
                        (
                                // Sequence name :Spawn
                              (
                           
                                    LessEqualFloat(
                                          _RelicSpawnTime, 
                                          CurrentTime) &&
                                    AddFloat(
                                          out _RelicSpawnTime, 
                                          CurrentTime, 
                                          50000) &&

                                       /*      SpawnNeutralCampFromEncounter(
                                                   out _RelicSquadID, 
                                                   RelicEncounterShield, 
                                                   RelicPosition,
                                                   "HealthPack", 
                                                   RelicCampID)*/
                                       SpawnSquadFromEncounter(
                                          out _RelicSquadID,
                                          RelicEncounterShield,
                                          RelicPosition,
                                          TeamId.TEAM_NEUTRAL,
                                          "HealthPack"
                                          )

                              ) ||
                              // Sequence name :CheckSpawn
                              (
                                    AddFloat(
                                          out UpperBound, 
                                          CurrentTime, 
                                          25000) &&
                                    GreaterFloat(
                                          _RelicSpawnTime, 
                                          UpperBound) &&
                                    GreaterEqualInt(
                                          _RelicSquadID, 
                                          0) &&
                                    TestSquadIsAlive(
                                          _RelicSquadID, 
                                          false) &&
                                          
                                    GenerateRandomFloat(
                                          out NewRelicSpawnTime, 
                                          30, 
                                          30) &&
                                    AddFloat(
                                          out _RelicSpawnTime, 
                                          NewRelicSpawnTime, 
                                          CurrentTime)

                              )
                        )
                  )
                  ||
                               DebugAction("MaskFailure - RelicHelper")
            );

       __RelicSquadID = _RelicSquadID;
       __RelicSpawnTime = _RelicSpawnTime;
        return result;
      }
}

