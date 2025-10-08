namespace BehaviourTrees;


class RelicInitializationClass : OdinLayout
{


    public bool RelicInitialization(
               out int RelicSquad_A,
     out int RelicSquad_B,
     out int RelicSquad_C,
     out float RelicSpawnTime_A,
     out float RelicSpawnTime_B,
     out float RelicSpawnTime_C,
     out float RelicSpawnTime_D,
     out float RelicSpawnTime_E,
     out int RelicSquad_D,
     out int RelicSquad_E,
     out int RelicEncounterShield,
     out float RelicSpawnTime_F,
     out float RelicSpawnTime_G,
     out float RelicSpawnTime_H,
     out int RelicSquad_F,
     out int RelicSquad_G,
     out int RelicSquad_H,
     out float RelicSpawnTime_I,
     out float RelicSpawnTime_J,
     out int RelicSquad_I,
     out int RelicSquad_J,
     out Vector3 RelicPositionA,
     out Vector3 RelicPositionB,
     out Vector3 RelicPositionC,
     out Vector3 RelicPositionD,
     out Vector3 RelicPositionE,
     out Vector3 RelicPositionF,
     out Vector3 RelicPositionG,
     out Vector3 RelicPositionI,
     out Vector3 RelicPositionJ,
     out Vector3 RelicPositionH

         )
    {
        int _RelicSquad_A = default;
        int _RelicSquad_B = default;
        int _RelicSquad_C = default;
        float _RelicSpawnTime_A = default;
        float _RelicSpawnTime_B = default;
        float _RelicSpawnTime_C = default;
        float _RelicSpawnTime_D = default;
        float _RelicSpawnTime_E = default;
        int _RelicSquad_D = default;
        int _RelicSquad_E = default;
        int _RelicEncounterShield = default;
        float _RelicSpawnTime_F = default;
        float _RelicSpawnTime_G = default;
        float _RelicSpawnTime_H = default;
        int _RelicSquad_F = default;
        int _RelicSquad_G = default;
        int _RelicSquad_H = default;
        float _RelicSpawnTime_I = default;
        float _RelicSpawnTime_J = default;
        int _RelicSquad_I = default;
        int _RelicSquad_J = default;
        Vector3 _RelicPositionA = default;
        Vector3 _RelicPositionB = default;
        Vector3 _RelicPositionC = default;
        Vector3 _RelicPositionD = default;
        Vector3 _RelicPositionE = default;
        Vector3 _RelicPositionF = default;
        Vector3 _RelicPositionG = default;
        Vector3 _RelicPositionI = default;
        Vector3 _RelicPositionJ = default;
        Vector3 _RelicPositionH = default;


        bool result =
                  // Sequence name :Sequence

                  GetGameTime(
                        out CurrentGameTime) &&
                  GenerateRandomFloat(
                        out RandomTimeDiff,
                        120,
                        120) &&
                  AddFloat(
                        out _RelicSpawnTime_A,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_B,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_C,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_D,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_E,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_F,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_G,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_H,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_I,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  AddFloat(
                        out _RelicSpawnTime_J,
                        CurrentGameTime,
                        RandomTimeDiff) &&
                  CreateEncounterFromDefinition(
                        out _RelicEncounterShield,
                        "OdinShieldRelic"
                       , 1
                        ) &&
                  SetVarInt(
                        out _RelicSquad_A,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_B,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_C,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_D,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_E,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_F,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_G,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_H,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_I,
                        -1) &&
                  SetVarInt(
                        out _RelicSquad_J,
                        -1) &&
                  GetWorldLocationByName(
                        out _RelicPositionA,
                        "RelicA") &&
                  GetWorldLocationByName(
                        out _RelicPositionB,
                        "RelicB") &&
                  GetWorldLocationByName(
                        out _RelicPositionC,
                        "RelicC") &&
                  GetWorldLocationByName(
                        out _RelicPositionD,
                        "LesserRelic1") &&
                  GetWorldLocationByName(
                        out _RelicPositionE,
                        "LesserRelic2") &&
                  GetWorldLocationByName(
                        out _RelicPositionF,
                        "LesserRelic3") &&
                  GetWorldLocationByName(
                        out _RelicPositionG,
                        "LesserRelic4") &&
                  GetWorldLocationByName(
                        out _RelicPositionH,
                        "LesserRelic5") &&
                  GetWorldLocationByName(
                        out _RelicPositionI,
                        "LesserRelic6") &&
                  GetWorldLocationByName(
                        out _RelicPositionJ,
                        "LesserRelic7")

            ;

        RelicSquad_A = _RelicSquad_A;
        RelicSquad_B = _RelicSquad_B;
        RelicSquad_C = _RelicSquad_C;
        RelicSpawnTime_A = _RelicSpawnTime_A;
        RelicSpawnTime_B = _RelicSpawnTime_B;
        RelicSpawnTime_C = _RelicSpawnTime_C;
        RelicSpawnTime_D = _RelicSpawnTime_D;
        RelicSpawnTime_E = _RelicSpawnTime_E;
        RelicSquad_D = _RelicSquad_D;
        RelicSquad_E = _RelicSquad_E;
        RelicEncounterShield = _RelicEncounterShield;
        RelicSpawnTime_F = _RelicSpawnTime_F;
        RelicSpawnTime_G = _RelicSpawnTime_G;
        RelicSpawnTime_H = _RelicSpawnTime_H;
        RelicSquad_F = _RelicSquad_F;
        RelicSquad_G = _RelicSquad_G;
        RelicSquad_H = _RelicSquad_H;
        RelicSpawnTime_I = _RelicSpawnTime_I;
        RelicSpawnTime_J = _RelicSpawnTime_J;
        RelicSquad_I = _RelicSquad_I;
        RelicSquad_J = _RelicSquad_J;
        RelicPositionA = _RelicPositionA;
        RelicPositionB = _RelicPositionB;
        RelicPositionC = _RelicPositionC;
        RelicPositionD = _RelicPositionD;
        RelicPositionE = _RelicPositionE;
        RelicPositionF = _RelicPositionF;
        RelicPositionG = _RelicPositionG;
        RelicPositionI = _RelicPositionI;
        RelicPositionJ = _RelicPositionJ;
        RelicPositionH = _RelicPositionH;

        return result;
    }
}

