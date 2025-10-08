namespace BehaviourTrees;


class MinionSpawnManagerClass : OdinLayout

{
    float lastTimeExecuted_EP_updatemutator;

    public bool MinionSpawnManager(
        out int __MutationIndex,
        out float __PreviousMinionSpawnTime_AB,
        out float __PreviousMinionSpawnTime_AE,
        out float __PreviousMinionSpawnTime_BA,
        out float __PreviousMinionSpawnTime_BC,
        out float __PreviousMinionSpawnTime_CB,
        out float __PreviousMinionSpawnTime_CD,
        out float __PreviousMinionSpawnTime_DC,
        out float __PreviousMinionSpawnTime_DE,
        out float __PreviousMinionSpawnTime_EA,
        out float __PreviousMinionSpawnTime_ED,
        int MutationIndex,
        int WaveEncounterID,
        string Definition_Ranged,
        string Definition_Melee,
        TeamId CapturePointOwnerA,
        TeamId CapturePointOwnerB,
        TeamId CapturePointOwnerC,
        TeamId CapturePointOwnerD,
        TeamId CapturePointOwnerE,
        float PreviousMinionSpawnTime_AB,
        float PreviousMinionSpawnTime_AE,
        float PreviousMinionSpawnTime_BA,
        float PreviousMinionSpawnTime_BC,
        float PreviousMinionSpawnTime_CB,
        float PreviousMinionSpawnTime_CD,
        float PreviousMinionSpawnTime_DC,
        float PreviousMinionSpawnTime_DE,
        float PreviousMinionSpawnTime_EA,
        float PreviousMinionSpawnTime_ED,
        float SpawnRate_Seconds,
        string Definition_Superminion,
        AttackableUnit CapturePointGuardian0,
        AttackableUnit CapturePointGuardian1,
        AttackableUnit CapturePointGuardian2,
        AttackableUnit CapturePointGuardian3,
        AttackableUnit CapturePointGuardian4,
        Vector3 MinionSpawnPoint_A1,
        Vector3 MinionSpawnPoint_A2,
        Vector3 MinionSpawnPoint_B1,
        Vector3 MinionSpawnPoint_B2,
        Vector3 MinionSpawnPoint_C1,
        Vector3 MinionSpawnPoint_C2,
        Vector3 MinionSpawnPoint_D1,
        Vector3 MinionSpawnPoint_D2,
        Vector3 MinionSpawnPoint_E1,
        Vector3 MinionSpawnPoint_E2,
        int MinionSpawnPortalParticleEncounterID)
    {

        int _MutationIndex = MutationIndex;
        float _PreviousMinionSpawnTime_AB = PreviousMinionSpawnTime_AB;
        float _PreviousMinionSpawnTime_AE = PreviousMinionSpawnTime_AE;
        float _PreviousMinionSpawnTime_BA = PreviousMinionSpawnTime_BA;
        float _PreviousMinionSpawnTime_BC = PreviousMinionSpawnTime_BC;
        float _PreviousMinionSpawnTime_CB = PreviousMinionSpawnTime_CB;
        float _PreviousMinionSpawnTime_CD = PreviousMinionSpawnTime_CD;
        float _PreviousMinionSpawnTime_DC = PreviousMinionSpawnTime_DC;
        float _PreviousMinionSpawnTime_DE = PreviousMinionSpawnTime_DE;
        float _PreviousMinionSpawnTime_EA = PreviousMinionSpawnTime_EA;
        float _PreviousMinionSpawnTime_ED = PreviousMinionSpawnTime_ED;

        var countPoints = new CountPointsClass();
        var spawnMinionWave = new SpawnMinionWaveClass();

        List<Func<bool>> EP_updatemutator = new()
        { () =>
        {
            return
                                    UpdateMutator(
                                          WaveEncounterID,
                                          Definition_Melee,
                                          MutationIndex) &&
                                    UpdateMutator(
                                          WaveEncounterID,
                                          Definition_Superminion,
                                          MutationIndex) &&
                                    AddInt(
                                          out MutationIndex,
                                          MutationIndex,
                                          1)

            ;
        } };

        bool result =
                  // Sequence name :MinionsManager

                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Spawn All Waves
                        (
                              SetVarBool(
                                    out Run,
                                    true) &&
                              Run == true &&
                              countPoints.CountPoints(
                                    out NumChaosPoints,
                                    CapturePointOwnerA,
                                    CapturePointOwnerB,
                                    CapturePointOwnerC,
                                    CapturePointOwnerD,
                                    CapturePointOwnerE,
                                    TeamId.TEAM_CHAOS) &&
                              countPoints.CountPoints(
                                    out NumOrderPoints,
                                    CapturePointOwnerA,
                                    CapturePointOwnerB,
                                    CapturePointOwnerC,
                                    CapturePointOwnerD,
                                    CapturePointOwnerE,
                                    TeamId.TEAM_ORDER) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :PointA
                                    (
                                          TestUnitHasBuff(
                                                CapturePointGuardian0,
                                                null,
                                                "OdinGuardianSuppression", false) &&
                                          spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_AE,
                                                MinionSpawnPoint_A2,
                                                CapturePointOwnerA,
                                                WaveEncounterID,
                                                7,
                                                true,
                                                "AE",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerE,
                                                PreviousMinionSpawnTime_AE,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID) &&
                                           spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_AB,
                                                MinionSpawnPoint_A1,
                                                CapturePointOwnerA,
                                                WaveEncounterID,
                                                3,
                                                false,
                                                "AB",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerB,
                                                PreviousMinionSpawnTime_AB,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID)
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :PointB
                                    (
                                          TestUnitHasBuff(
                                                CapturePointGuardian1,
                                                null,
                                                "OdinGuardianSuppression", false) &&
                                          spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_BA,
                                                MinionSpawnPoint_B2,
                                                CapturePointOwnerB,
                                                WaveEncounterID,
                                                8,
                                                true,
                                                "BA",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerA,
                                                PreviousMinionSpawnTime_BA,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID) &&
                                          spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_BC,
                                                MinionSpawnPoint_B1,
                                                CapturePointOwnerB,
                                                WaveEncounterID,
                                                4,
                                                false,
                                                "BC",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerC,
                                                PreviousMinionSpawnTime_BC,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID)
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :PointC
                                    (
                                          TestUnitHasBuff(
                                                CapturePointGuardian2,
                                                null,
                                                "OdinGuardianSuppression", false) &&
                                          spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_CB,
                                                MinionSpawnPoint_C2,
                                                CapturePointOwnerC,
                                                WaveEncounterID,
                                                9,
                                                true,
                                                "CB",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerB,
                                                PreviousMinionSpawnTime_CB,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID) &&
                                          spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_CD,
                                                MinionSpawnPoint_C1,
                                                CapturePointOwnerC,
                                                WaveEncounterID,
                                                0,
                                                false,
                                                "CD",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerD,
                                                PreviousMinionSpawnTime_CD,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID)
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :PointD
                                    (
                                          TestUnitHasBuff(
                                                CapturePointGuardian3,
                                                null,
                                                "OdinGuardianSuppression", false) &&
                                           spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_DC,
                                                MinionSpawnPoint_D2,
                                                CapturePointOwnerD,
                                                WaveEncounterID,
                                                5,
                                                true,
                                                "DC",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerC,
                                                PreviousMinionSpawnTime_DC,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID) &&
                                         spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_DE,
                                                MinionSpawnPoint_D1,
                                                CapturePointOwnerD,
                                                WaveEncounterID,
                                                1,
                                                false,
                                                "DE",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerE,
                                                PreviousMinionSpawnTime_DE,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID)
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :PointE
                                    (
                                          TestUnitHasBuff(
                                                CapturePointGuardian4,
                                                null,
                                                "OdinGuardianSuppression", false) &&
                                          spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_ED,
                                                MinionSpawnPoint_E2,
                                                CapturePointOwnerE,
                                                WaveEncounterID,
                                                6,
                                                true,
                                                "ED",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerD,
                                                PreviousMinionSpawnTime_ED,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID) &&
                                           spawnMinionWave.SpawnMinionWave(
                                                out _PreviousMinionSpawnTime_EA,
                                                MinionSpawnPoint_E1,
                                                CapturePointOwnerE,
                                                WaveEncounterID,
                                                2,
                                                false,
                                                "EA",
                                                NumOrderPoints,
                                                NumChaosPoints,
                                                CapturePointOwnerA,
                                                PreviousMinionSpawnTime_EA,
                                                SpawnRate_Seconds,
                                                MinionSpawnPortalParticleEncounterID)
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              )
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                        // Sequence name :MaskFailure

                        ExecutePeriodically(
                            ref lastTimeExecuted_EP_updatemutator,
                            EP_updatemutator,
                              true,
                              60)
            // Sequence name :Sequence



            ;
        __MutationIndex = _MutationIndex;
        __PreviousMinionSpawnTime_AB = _PreviousMinionSpawnTime_AB;
        __PreviousMinionSpawnTime_AE = _PreviousMinionSpawnTime_AE;
        __PreviousMinionSpawnTime_BA = _PreviousMinionSpawnTime_BA;
        __PreviousMinionSpawnTime_BC = _PreviousMinionSpawnTime_BC;
        __PreviousMinionSpawnTime_CB = _PreviousMinionSpawnTime_CB;
        __PreviousMinionSpawnTime_CD = _PreviousMinionSpawnTime_CD;
        __PreviousMinionSpawnTime_DC = _PreviousMinionSpawnTime_DC;
        __PreviousMinionSpawnTime_DE = _PreviousMinionSpawnTime_DE;
        __PreviousMinionSpawnTime_EA = _PreviousMinionSpawnTime_EA;
        __PreviousMinionSpawnTime_ED = _PreviousMinionSpawnTime_ED;

        return result;
    }
}

