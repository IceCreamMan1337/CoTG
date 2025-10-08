namespace BehaviourTrees;


class MinionGraveyardManagerClass : OdinLayout
{

    public bool MinionGraveyardManager(
   out float __OrderScore,
   out float __ChaosScore,
   out int __MinionGraveyardSquadID_A,
   out int __MinionGraveyardSquadID_B,
   out int __MinionGraveyardSquadID_C,
   out int __MinionGraveyardSquadID_D,
   out int __MinionGraveyardSquadID_E,
   out float __MinionGraveyardPortalStartTime_A,
   out float __MinionGraveyardPortalStartTime_B,
   out float __MinionGraveyardPortalStartTime_C,
   out float __MinionGraveyardPortalStartTime_D,
   out float __MinionGraveyardPortalStartTime_E,
   TeamId CapturePointOwnerA,
   TeamId CapturePointOwnerB,
   TeamId CapturePointOwnerC,
   TeamId CapturePointOwnerD,
   TeamId CapturePointOwnerE,
   float OrderScore,
   float ChaosScore,
   Vector3 MinionGraveyardA,
   Vector3 MinionGraveyardB,
   Vector3 MinionGraveyardC,
   Vector3 MinionGraveyardD,
   Vector3 MinionGraveyardE,
   int MinionGraveyardPortalEncounter,
   int MinionGraveyardSquadID_A,
   int MinionGraveyardSquadID_B,
   int MinionGraveyardSquadID_C,
   int MinionGraveyardSquadID_D,
   int MinionGraveyardSquadID_E,
   float MinionGraveyardPortalStartTime_A,
   float MinionGraveyardPortalStartTime_B,
   float MinionGraveyardPortalStartTime_C,
   float MinionGraveyardPortalStartTime_D,
   float MinionGraveyardPortalStartTime_E
)

    {
        float _OrderScore = OrderScore;
        float _ChaosScore = ChaosScore;
        int _MinionGraveyardSquadID_A = MinionGraveyardSquadID_A;
        int _MinionGraveyardSquadID_B = MinionGraveyardSquadID_B;
        int _MinionGraveyardSquadID_C = MinionGraveyardSquadID_C;
        int _MinionGraveyardSquadID_D = MinionGraveyardSquadID_D;
        int _MinionGraveyardSquadID_E = MinionGraveyardSquadID_E;
        float _MinionGraveyardPortalStartTime_A = MinionGraveyardPortalStartTime_A;
        float _MinionGraveyardPortalStartTime_B = MinionGraveyardPortalStartTime_B;
        float _MinionGraveyardPortalStartTime_C = MinionGraveyardPortalStartTime_C;
        float _MinionGraveyardPortalStartTime_D = MinionGraveyardPortalStartTime_D;
        float _MinionGraveyardPortalStartTime_E = MinionGraveyardPortalStartTime_E;

        var minionGraveyardHelper = new MinionGraveyardHelperClass();

        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :Sequence
                  (
                        minionGraveyardHelper.MinionGraveyardHelper(
                              CapturePointOwnerA,
                              MinionGraveyardA,
                              225,
                              "AB",
                              "AE",
                              null) &&
                        minionGraveyardHelper.MinionGraveyardHelper(
                              CapturePointOwnerB,
                              MinionGraveyardB,
                              225,
                              "BA",
                              "BC",
                              null) &&
                        minionGraveyardHelper.MinionGraveyardHelper(
                              CapturePointOwnerC,
                              MinionGraveyardC,
                              225,
                              "CB",
                              "CD",
                              null) &&
                        minionGraveyardHelper.MinionGraveyardHelper(
                              CapturePointOwnerD,
                              MinionGraveyardD,
                              225,
                              "DC",
                              "DE",
                              null) &&
                        minionGraveyardHelper.MinionGraveyardHelper(
                              CapturePointOwnerE,
                              MinionGraveyardE,
                              225,
                              "EA",
                              "ED",
                              null)

                  )
                  ||
                               DebugAction("MaskFailure")
            ;
        __OrderScore = _OrderScore;
        __ChaosScore = _ChaosScore;
        __MinionGraveyardSquadID_A = _MinionGraveyardSquadID_A;
        __MinionGraveyardSquadID_B = _MinionGraveyardSquadID_B;
        __MinionGraveyardSquadID_C = _MinionGraveyardSquadID_C;
        __MinionGraveyardSquadID_D = _MinionGraveyardSquadID_D;
        __MinionGraveyardSquadID_E = _MinionGraveyardSquadID_E;
        __MinionGraveyardPortalStartTime_A = _MinionGraveyardPortalStartTime_A;
        __MinionGraveyardPortalStartTime_B = _MinionGraveyardPortalStartTime_B;
        __MinionGraveyardPortalStartTime_C = _MinionGraveyardPortalStartTime_C;
        __MinionGraveyardPortalStartTime_D = _MinionGraveyardPortalStartTime_D;
        __MinionGraveyardPortalStartTime_E = _MinionGraveyardPortalStartTime_E;
        return result;
    }
}

