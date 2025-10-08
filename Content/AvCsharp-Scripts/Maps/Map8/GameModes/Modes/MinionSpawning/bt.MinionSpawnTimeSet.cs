using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class MinionSpawnTimeSetClass : OdinLayout 
{


     public bool MinionSpawnTimeSet(
             out float __LaneSpawnPreviousTime,
    TeamId LaneFromOwner,
    TeamId LaneToOwner,
    float LaneSpawnPreviousTime,
    float SpawnRate_Seconds

         )


    {

        float _LaneSpawnPreviousTime = LaneSpawnPreviousTime;
        bool result = 
            // Sequence name :MaskFailure
            (
                  // Sequence name :Check_Owners_To_Determine_Time_To_Set
                  (
                        GetGameTime(
                              out CurrentTime) &&
                        SubtractFloat(
                              out TimePassed, 
                              CurrentTime, 
                              LaneSpawnPreviousTime) &&
                        MultiplyFloat(
                              out NegativeSpawnRate, 
                              SpawnRate_Seconds, 
                              -1) &&
                        // Sequence name :Check_Owners_To_Determine_Time_To_Set
                        (
                              // Sequence name :FromOwner_is_Neutral_Don'tSpawn
                              (
                                    LaneFromOwner == TeamId.TEAM_NEUTRAL &&
                                    AddFloat(
                                          out _LaneSpawnPreviousTime, 
                                          CurrentTime, 
                                          25000)
                              ) ||
                              // Sequence name :Owners_Are_The_Same_Don'tSpawn
                              (
                                    LaneFromOwner == LaneToOwner &&
                                    AddFloat(
                                          out _LaneSpawnPreviousTime, 
                                          CurrentTime, 
                                          25000)
                              ) ||
                              // Sequence name :ToOwner_is_Neutral_Don'tSpawn
                              (
                                    LaneToOwner == TeamId.TEAM_NEUTRAL &&
                                    AddFloat(
                                          out _LaneSpawnPreviousTime, 
                                          CurrentTime, 
                                          25000)
                              ) ||
                              // Sequence name :Wasn't_Spawning_Before
                              (
                                    LessFloat(
                                          TimePassed, 
                                          NegativeSpawnRate) &&
                                    SubtractFloat(
                                          out _LaneSpawnPreviousTime, 
                                          CurrentTime, 
                                          25000)
                              ) ||
                              // Sequence name :IsAlreadySpawning_Continue
                              (
                                    GreaterEqualFloat(
                                          TimePassed, 
                                          NegativeSpawnRate)

                              )
                        )
                  )||
                               DebugAction("MaskFailure")
            );

        __LaneSpawnPreviousTime = _LaneSpawnPreviousTime;
        return result;
      }
}

