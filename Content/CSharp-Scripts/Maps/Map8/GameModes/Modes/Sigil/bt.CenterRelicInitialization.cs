namespace BehaviourTrees;


class CenterRelicInitializationClass : OdinLayout
{


    public bool CenterRelicInitialization(
          out float CenterRelicSpawnTime_A,
     out float CenterRelicSpawnTime_B,
     out int CenterRelicSquad_A,
     out int CenterRelicSquad_B,
     out int RelicEncounterCenter,
     out int RelicEncounterCenter2,
     out Vector3 CenterRelicPositionB,
     out Vector3 CenterRelicPositionA,
     out bool CenterRelicParticleAttached1,
     out bool CenterRelicParticleAttached2)
    {

        float _CenterRelicSpawnTime_A = default;
        float _CenterRelicSpawnTime_B = default;
        int _CenterRelicSquad_A = default;
        int _CenterRelicSquad_B = default;
        int _RelicEncounterCenter = default;
        int _RelicEncounterCenter2 = default;
        Vector3 _CenterRelicPositionB = default;
        Vector3 _CenterRelicPositionA = default;
        bool _CenterRelicParticleAttached1 = default;
        bool _CenterRelicParticleAttached2 = default;

        bool result =
                          // Sequence name :Sequence

                          GetGameTime(
                                out CurrentGameTime) &&
                          GenerateRandomFloat(
                                out RandomTimeDiff,
                                180,
                                180) &&
                          AddFloat(
                                out _CenterRelicSpawnTime_A,
                                CurrentGameTime,
                                RandomTimeDiff) &&
                          AddFloat(
                                out _CenterRelicSpawnTime_B,
                                CurrentGameTime,
                                RandomTimeDiff) &&
                          CreateEncounterFromDefinition(
                                out _RelicEncounterCenter,
                                "OdinCenterRelic_Left",
                                1
                                ) &&
                          CreateEncounterFromDefinition(
                                out _RelicEncounterCenter2,
                                "OdinCenterRelic_Right",
                                1
                                ) &&
                          SetVarInt(
                                out _CenterRelicSquad_A,
                                -1) &&
                          SetVarInt(
                                out _CenterRelicSquad_B,
                                -1) &&
                          MakeColor(
                                out ChaosColor,
                                100,
                                0,
                                100,
                                150) &&
                          MakeColor(
                                out OrderColor,
                                10,
                                100,
                                10,
                                150) &&
                          GetWorldLocationByName(
                                out _CenterRelicPositionB,
                                "LargeRelic1") &&
                          GetWorldLocationByName(
                                out _CenterRelicPositionA,
                                "LargeRelic2") &&
                          SetVarBool(
                                out _CenterRelicParticleAttached1,
                                false) &&
                          SetVarBool(
                                out _CenterRelicParticleAttached2,
                                false)

                    ;
        CenterRelicSpawnTime_A = _CenterRelicSpawnTime_A;
        CenterRelicSpawnTime_B = _CenterRelicSpawnTime_B;
        CenterRelicSquad_A = _CenterRelicSquad_A;
        CenterRelicSquad_B = _CenterRelicSquad_B;
        RelicEncounterCenter = _RelicEncounterCenter;
        RelicEncounterCenter2 = _RelicEncounterCenter2;
        CenterRelicPositionB = _CenterRelicPositionB;
        CenterRelicPositionA = _CenterRelicPositionA;
        CenterRelicParticleAttached1 = _CenterRelicParticleAttached1;
        CenterRelicParticleAttached2 = _CenterRelicParticleAttached2;

        return result;
    }
}

