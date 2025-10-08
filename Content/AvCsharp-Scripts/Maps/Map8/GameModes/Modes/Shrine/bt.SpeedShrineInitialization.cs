using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class SpeedShrineInitializationClass : OdinLayout 
{

    public bool SpeedShrineInitialization()
      {


         return    // Sequence name :Init_the_speed_shrines
            (
                  GetWorldLocationByName(
                        out Shrine1Position,
                        "SpeedShrine01") &&
                  GetWorldLocationByName(
                        out Shrine2Position,
                        "SpeedShrine02") &&
                  GetWorldLocationByName(
                        out Shrine5Position,
                        "SigilB") &&
                  CreateEncounterFromDefinition(
                        out SpeedShrineID,
                        "SpeedShrine"

                        ) &&
                  SpawnNeutralCampFromEncounter(
                        out Shrine1SquadID, 
                        SpeedShrineID, 
                        Shrine1Position,
                        "Shrine", 
                        102) &&
                  SpawnNeutralCampFromEncounter(
                        out Shrine1SquadID, 
                        SpeedShrineID, 
                        Shrine2Position,
                        "Shrine",
                        103) &&
                  SpawnNeutralCampFromEncounter(
                        out Shrine1SquadID, 
                        SpeedShrineID, 
                        Shrine5Position,
                        "Shrine",
                        104)

            );
      }
}

