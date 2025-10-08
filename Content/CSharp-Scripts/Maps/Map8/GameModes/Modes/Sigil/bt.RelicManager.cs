namespace BehaviourTrees;


class RelicManagerClass : OdinLayout
{

    private RelicHelperClass relicHelper = new();
    public bool RelicManager(
            out float __RelicSpawnTime_A,
      out float __RelicSpawnTime_B,
      out float __RelicSpawnTime_C,
      out int __RelicSquadID_A,
      out int __RelicSquadID_B,
      out int __RelicSquadID_C,
      out float __RelicSpawnTime_D,
      out float __RelicSpawnTime_E,
      out int __RelicSquadID_D,
      out int __RelicSquadID_E,
      out float __RelicSpawnTime_F,
      out float __RelicSpawnTime_G,
      out float __RelicSpawnTime_H,
      out int __RelicSquadID_F,
      out int __RelicSquadID_G,
      out int __RelicSquadID_H,
      out int __RelicSquadID_I,
      out int __RelicSquadID_J,
      out float __RelicSpawnTime_I,
      out float __RelicSpawnTime_J,
    int RelicSquadID_A,
    int RelicSquadID_B,
    int RelicSquadID_C,
    float RelicSpawnTime_A,
    float RelicSpawnTime_B,
    float RelicSpawnTime_C,
    float RelicSpawnTime_D,
    float RelicSpawnTime_E,
    int RelicSquadID_D,
    int RelicSquadID_E,
    int RelicEncounterShield,
    float RelicSpawnTime_F,
    float RelicSpawnTime_G,
    float RelicSpawnTime_H,
    int RelicSquadID_F,
    int RelicSquadID_G,
    int RelicSquadID_H,
    int RelicSquadID_I,
    int RelicSquadID_J,
    float RelicSpawnTime_I,
    float RelicSpawnTime_J,
    Vector3 RelicPositionA,
      Vector3 RelicPositionB,
      Vector3 RelicPositionC,
      Vector3 RelicPositionD,
      Vector3 RelicPositionE,
      Vector3 RelicPositionF,
      Vector3 RelicPositionG,
      Vector3 RelicPositionH,
      Vector3 RelicPositionI,
      Vector3 RelicPositionJ
          )
    {
        float _RelicSpawnTime_A = RelicSpawnTime_A;
        float _RelicSpawnTime_B = RelicSpawnTime_B;
        float _RelicSpawnTime_C = RelicSpawnTime_C;
        int _RelicSquadID_A = RelicSquadID_A;
        int _RelicSquadID_B = RelicSquadID_B;
        int _RelicSquadID_C = RelicSquadID_C;
        float _RelicSpawnTime_D = RelicSpawnTime_D;
        float _RelicSpawnTime_E = RelicSpawnTime_E;
        int _RelicSquadID_D = RelicSquadID_D;
        int _RelicSquadID_E = RelicSquadID_E;
        float _RelicSpawnTime_F = RelicSpawnTime_F;
        float _RelicSpawnTime_G = RelicSpawnTime_G;
        float _RelicSpawnTime_H = RelicSpawnTime_H;
        int _RelicSquadID_F = RelicSquadID_F;
        int _RelicSquadID_G = RelicSquadID_G;
        int _RelicSquadID_H = RelicSquadID_H;
        int _RelicSquadID_I = RelicSquadID_I;
        int _RelicSquadID_J = RelicSquadID_J;
        float _RelicSpawnTime_I = RelicSpawnTime_I;
        float _RelicSpawnTime_J = RelicSpawnTime_J;


        bool result =
                 // Sequence name :Sequence

                 (relicHelper.RelicHelper(
                        out _RelicSpawnTime_A,
                        out _RelicSquadID_A,
                        RelicSquadID_A,
                        RelicSpawnTime_A,
                        100,
                        RelicEncounterShield,
                        RelicPositionA) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_B,
                        out _RelicSquadID_B,
                        RelicSquadID_B,
                        RelicSpawnTime_B,
                        101,
                        RelicEncounterShield,
                        RelicPositionB) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_C,
                        out _RelicSquadID_C,
                        RelicSquadID_C,
                        RelicSpawnTime_C,
                        112,
                        RelicEncounterShield,
                        RelicPositionC) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_D,
                        out _RelicSquadID_D,
                        RelicSquadID_D,
                        RelicSpawnTime_D,
                        108,
                        RelicEncounterShield,
                        RelicPositionD) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_E,
                        out _RelicSquadID_E,
                        RelicSquadID_E,
                        RelicSpawnTime_E,
                        109,
                        RelicEncounterShield,
                        RelicPositionE) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_F,
                        out _RelicSquadID_F,
                        _RelicSquadID_F,
                        _RelicSpawnTime_F,
                        105,
                        RelicEncounterShield,
                        RelicPositionF) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_G,
                        out _RelicSquadID_G,
                        RelicSquadID_G,
                        RelicSpawnTime_G,
                        106,
                        RelicEncounterShield,
                        RelicPositionG) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_H,
                        out _RelicSquadID_H,
                        RelicSquadID_H,
                        RelicSpawnTime_H,
                        107,
                        RelicEncounterShield,
                        RelicPositionH) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_I,
                        out _RelicSquadID_I,
                        RelicSquadID_I,
                        RelicSpawnTime_I,
                        110,
                        RelicEncounterShield,
                        RelicPositionI) &&
                  relicHelper.RelicHelper(
                        out _RelicSpawnTime_J,
                        out _RelicSquadID_J,
                        RelicSquadID_J,
                        RelicSpawnTime_J,
                        111,
                        RelicEncounterShield,
                        RelicPositionJ))

                  ||
                               DebugAction("MaskFailure")


            ;
        __RelicSpawnTime_A = _RelicSpawnTime_A;
        __RelicSpawnTime_B = _RelicSpawnTime_B;
        __RelicSpawnTime_C = _RelicSpawnTime_C;
        __RelicSquadID_A = _RelicSquadID_A;
        __RelicSquadID_B = _RelicSquadID_B;
        __RelicSquadID_C = _RelicSquadID_C;
        __RelicSpawnTime_D = _RelicSpawnTime_D;
        __RelicSpawnTime_E = _RelicSpawnTime_E;
        __RelicSquadID_D = _RelicSquadID_D;
        __RelicSquadID_E = _RelicSquadID_E;
        __RelicSpawnTime_F = _RelicSpawnTime_F;
        __RelicSpawnTime_G = _RelicSpawnTime_G;
        __RelicSpawnTime_H = _RelicSpawnTime_H;
        __RelicSquadID_F = _RelicSquadID_F;
        __RelicSquadID_G = _RelicSquadID_G;
        __RelicSquadID_H = _RelicSquadID_H;
        __RelicSquadID_I = _RelicSquadID_I;
        __RelicSquadID_J = _RelicSquadID_J;
        __RelicSpawnTime_I = _RelicSpawnTime_I;
        __RelicSpawnTime_J = _RelicSpawnTime_J;



        return result;
    }
}

