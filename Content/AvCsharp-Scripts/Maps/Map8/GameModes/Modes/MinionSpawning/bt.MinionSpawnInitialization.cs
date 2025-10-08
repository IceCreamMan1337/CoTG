using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class MinionSpawnInitializationClass : OdinLayout 
{


    public bool MinionSpawnInitialization(
  out float PreviousMinionSpawnTime_AB,
  out float PreviousMinionSpawnTime_AE,
  out float PreviousMinionSpawnTime_BA,
  out float PreviousMinionSpawnTime_BC,
  out float PreviousMinionSpawnTime_CB,
  out float PreviousMinionSpawnTime_CD,
  out float PreviousMinionSpawnTime_DC,
  out float PreviousMinionSpawnTime_DE,
  out float PreviousMinionSpawnTime_EA,
  out float PreviousMinionSpawnTime_ED,
  out int MinionWaveEncounterID,
  out int MutationProgressionIndex,
  out float MinionSpawnRate_Seconds,
  out Vector3 MinionSpawnPoint_A1,
  out Vector3 MinionSpawnPoint_A2,
  out Vector3 MinionSpawnPoint_B1,
  out Vector3 MinionSpawnPoint_B2,
  out Vector3 MinionSpawnPoint_C1,
  out Vector3 MinionSpawnPoint_C2,
  out Vector3 MinionSpawnPoint_D1,
  out Vector3 MinionSpawnPoint_D2,
  out Vector3 MinionSpawnPoint_E1,
  out Vector3 MinionSpawnPoint_E2,
  out int MinionSpawnPortalParticleEncounterID)
    {
        float _PreviousMinionSpawnTime_AB = default;
        float _PreviousMinionSpawnTime_AE = default;
        float _PreviousMinionSpawnTime_BA = default;
        float _PreviousMinionSpawnTime_BC = default;
        float _PreviousMinionSpawnTime_CB = default;
        float _PreviousMinionSpawnTime_CD = default;
        float _PreviousMinionSpawnTime_DC = default;
        float _PreviousMinionSpawnTime_DE = default;
        float _PreviousMinionSpawnTime_EA = default;
        float _PreviousMinionSpawnTime_ED = default;
        int _MinionWaveEncounterID = default;
        int _MutationProgressionIndex = default;
        float _MinionSpawnRate_Seconds = default;
        Vector3 _MinionSpawnPoint_A1 = default(Vector3);
        Vector3 _MinionSpawnPoint_A2 = default(Vector3);
        Vector3 _MinionSpawnPoint_B1 = default(Vector3);
        Vector3 _MinionSpawnPoint_B2 = default(Vector3);
        Vector3 _MinionSpawnPoint_C1 = default(Vector3);
        Vector3 _MinionSpawnPoint_C2 = default(Vector3);
        Vector3 _MinionSpawnPoint_D1 = default(Vector3);
        Vector3 _MinionSpawnPoint_D2 = default(Vector3);
        Vector3 _MinionSpawnPoint_E1 = default(Vector3);
        Vector3 _MinionSpawnPoint_E2 = default(Vector3);
        int _MinionSpawnPortalParticleEncounterID = default;


        bool result = 
            // Sequence name :Sequence
            (
                  GetGameTime(
                        out CurrentGameTime) &&
                  AddFloat(
                        out TimeInFuture, 
                        CurrentGameTime, 
                        25000) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_AB, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_AE, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_BA, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_BC, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_CB, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_CD, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_DC, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_DE, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_EA, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _PreviousMinionSpawnTime_ED, 
                        TimeInFuture) &&
                  SetVarFloat(
                        out _MinionSpawnRate_Seconds, 
                        14) &&
                  // Sequence name :Encounter_Initialization
                  (
                        CreateEncounterFromDefinition(
                              out _MinionWaveEncounterID,
                              "SmallWave" 
                               
                              ) &&
                        AddMutatorToEncounter(
                              _MinionWaveEncounterID,
                              "MeleeWaveBase",
                              "Melee", 
                              0) &&
                        AddMutatorToEncounter(
                              _MinionWaveEncounterID,
                              "MeleeWaveProgression",
                              "Melee", 
                              0) &&
                        AddMutatorToEncounter(
                              _MinionWaveEncounterID,
                              "CapturePointMutator",
                              "Melee;SuperMinion", 
                              0) &&
                        AddMutatorToEncounter(
                              _MinionWaveEncounterID,
                              "SuperminionBase",
                              "SuperMinion", 
                              0) &&
                        AddMutatorToEncounter(
                              _MinionWaveEncounterID,
                              "SuperminionProgression",
                              "SuperMinion", 
                              0) &&
                        SetVarInt(
                              out _MutationProgressionIndex, 
                              0) &&
                        CreateEncounterFromDefinition(
                              out _MinionSpawnPortalParticleEncounterID,
                              "OdinMinionSpawnPortal", 
                              1
                              )
                  ) &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_A1,
                        "Info_MinionSpawnA1") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_A2,
                        "Info_MinionSpawnA2") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_B1,
                        "Info_MinionSpawnB1") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_B2,
                        "Info_MinionSpawnB2") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_C1,
                        "Info_MinionSpawnC1") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_C2,
                        "Info_MinionSpawnC2") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_D1,
                        "Info_MinionSpawnD1") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_D2,
                        "Info_MinionSpawnD2") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_E1,
                        "Info_MinionSpawnE1") &&
                  GetWorldLocationByName(
                        out _MinionSpawnPoint_E2,
                        "Info_MinionSpawnE2")

            );
        PreviousMinionSpawnTime_AB = _PreviousMinionSpawnTime_AB;
        PreviousMinionSpawnTime_AE = _PreviousMinionSpawnTime_AE;
        PreviousMinionSpawnTime_BA = _PreviousMinionSpawnTime_BA;
        PreviousMinionSpawnTime_BC = _PreviousMinionSpawnTime_BC;
        PreviousMinionSpawnTime_CB = _PreviousMinionSpawnTime_CB;
        PreviousMinionSpawnTime_CD = _PreviousMinionSpawnTime_CD;
        PreviousMinionSpawnTime_DC = _PreviousMinionSpawnTime_DC;
        PreviousMinionSpawnTime_DE = _PreviousMinionSpawnTime_DE;
        PreviousMinionSpawnTime_EA = _PreviousMinionSpawnTime_EA;
        PreviousMinionSpawnTime_ED = _PreviousMinionSpawnTime_ED;
        MinionWaveEncounterID = _MinionWaveEncounterID;
        MutationProgressionIndex = _MutationProgressionIndex;
        MinionSpawnRate_Seconds = _MinionSpawnRate_Seconds;
        MinionSpawnPoint_A1 = _MinionSpawnPoint_A1;
        MinionSpawnPoint_A2 = _MinionSpawnPoint_A2;
        MinionSpawnPoint_B1 = _MinionSpawnPoint_B1;
        MinionSpawnPoint_B2 = _MinionSpawnPoint_B2;
        MinionSpawnPoint_C1 = _MinionSpawnPoint_C1;
        MinionSpawnPoint_C2 = _MinionSpawnPoint_C2;
        MinionSpawnPoint_D1 = _MinionSpawnPoint_D1;
        MinionSpawnPoint_D2 = _MinionSpawnPoint_D2;
        MinionSpawnPoint_E1 = _MinionSpawnPoint_E1;
        MinionSpawnPoint_E2 = _MinionSpawnPoint_E2;
        MinionSpawnPortalParticleEncounterID = _MinionSpawnPortalParticleEncounterID;


        return result;
      }
}

