using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class SpawnPointGuardClass : OdinLayout 
{


      public bool SpawnPointGuard(

                out int SquadId,
    Vector3 Position,
      TeamId Team
          )
      {

        int _SquadId = default;
bool result = 
            // Sequence name :Sequence
            (
                  CreateEncounterFromDefinition(
                        out EncounterId,
                        "PointGuard", 
                        1
                        ) &&
                  DeleteEncounter(
                        EncounterId)

            );
        SquadId = _SquadId;
        return result;
      }
}

