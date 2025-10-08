using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class MinionGraveyardInitializationClass : OdinLayout 
{


      public bool MinionGraveyardInitialization(
                out Vector3 MinionGraveyardA,
      out Vector3 MinionGraveyardB,
      out Vector3 MinionGraveyardC,
      out Vector3 MinionGraveyardD,
      out Vector3 MinionGraveyardE,
      out int MinionGraveyardPortalEncounter,
      out int MinionGraveyardPortalID_A,
      out int MinionGraveyardPortalID_B,
      out int MinionGraveyardPortalID_C,
      out int MinionGraveyardPortalID_D,
      out int MinionGraveyardPortalID_E,
      out float MinionGraveyardPortalStartTime_A,
      out float MinionGraveyardPortalStartTime_B,
      out float MinionGraveyardPortalStartTime_C,
      out float MinionGraveyardPortalStartTime_D,
      out float MinionGraveyardPortalStartTime_E)
      {

        Vector3 _MinionGraveyardA = default(Vector3);
        Vector3 _MinionGraveyardB = default(Vector3);
        Vector3 _MinionGraveyardC = default(Vector3);
        Vector3 _MinionGraveyardD = default(Vector3);
        Vector3 _MinionGraveyardE = default(Vector3);
        int _MinionGraveyardPortalEncounter = default(int);
        int _MinionGraveyardPortalID_A = default(int);
        int _MinionGraveyardPortalID_B = default(int);
        int _MinionGraveyardPortalID_C = default(int);
        int _MinionGraveyardPortalID_D = default(int);
        int _MinionGraveyardPortalID_E = default(int);
        float _MinionGraveyardPortalStartTime_A = default(float);
        float _MinionGraveyardPortalStartTime_B = default(float);
        float _MinionGraveyardPortalStartTime_C = default(float);
        float _MinionGraveyardPortalStartTime_D = default(float);
        float _MinionGraveyardPortalStartTime_E = default(float);
        bool result = 
            // Sequence name :Sequence
            (
                  GetWorldLocationByName(
                        out _MinionGraveyardA,
                        "MinionGraveyardA") &&
                  GetWorldLocationByName(
                        out _MinionGraveyardB,
                        "MinionGraveyardB") &&
                  GetWorldLocationByName(
                        out _MinionGraveyardC,
                        "MinionGraveyardC") &&
                  GetWorldLocationByName(
                        out _MinionGraveyardD,
                        "MinionGraveyardD") &&
                  GetWorldLocationByName(
                        out _MinionGraveyardE,
                        "MinionGraveyardE") &&
                  CreateEncounterFromDefinition(
                        out _MinionGraveyardPortalEncounter,
                        "OdinMinionGraveyardPortal", 
                        1
                        ) &&
                  SetVarInt(
                        out _MinionGraveyardPortalID_A, 
                        -1) &&
                  SetVarInt(
                        out _MinionGraveyardPortalID_B, 
                        -1) &&
                  SetVarInt(
                        out _MinionGraveyardPortalID_C, 
                        -1) &&
                  SetVarInt(
                        out _MinionGraveyardPortalID_D, 
                        -1) &&
                  SetVarInt(
                        out _MinionGraveyardPortalID_E, 
                        -1) &&
                  SetVarFloat(
                        out _MinionGraveyardPortalStartTime_A, 
                        -1) &&
                  SetVarFloat(
                        out _MinionGraveyardPortalStartTime_B, 
                        -1) &&
                  SetVarFloat(
                        out _MinionGraveyardPortalStartTime_C, 
                        -1) &&
                  SetVarFloat(
                        out _MinionGraveyardPortalStartTime_D, 
                        -1) &&
                  SetVarFloat(
                        out _MinionGraveyardPortalStartTime_E, 
                        -1)

            );
        MinionGraveyardA = _MinionGraveyardA;
        MinionGraveyardB = _MinionGraveyardB;
        MinionGraveyardC = _MinionGraveyardC;
        MinionGraveyardD = _MinionGraveyardD;
        MinionGraveyardE = _MinionGraveyardE;
        MinionGraveyardPortalEncounter = _MinionGraveyardPortalEncounter;
        MinionGraveyardPortalID_A = _MinionGraveyardPortalID_A;
        MinionGraveyardPortalID_B = _MinionGraveyardPortalID_B;
        MinionGraveyardPortalID_C = _MinionGraveyardPortalID_C;
        MinionGraveyardPortalID_D = _MinionGraveyardPortalID_D;
        MinionGraveyardPortalID_E = _MinionGraveyardPortalID_E;
        MinionGraveyardPortalStartTime_A = _MinionGraveyardPortalStartTime_A;
        MinionGraveyardPortalStartTime_B = _MinionGraveyardPortalStartTime_B;
        MinionGraveyardPortalStartTime_C = _MinionGraveyardPortalStartTime_C;
        MinionGraveyardPortalStartTime_D = _MinionGraveyardPortalStartTime_D;
        MinionGraveyardPortalStartTime_E = _MinionGraveyardPortalStartTime_E;

        return result;
      }
}

