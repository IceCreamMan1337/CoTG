namespace BehaviourTrees;


class ObeliskCapturePointManagerClass : OdinLayout
{


    public bool ObeliskCapturePointManager(
        out TeamId __CapturePointOwnerA,
        out TeamId __CapturePointOwnerB,
        out TeamId __CapturePointOwnerC,
        out TeamId __CapturePointOwnerD,
        out TeamId __CapturePointOwnerE,
        out float __MinionSpawnTime_AB,
        out float __MinionSpawnTime_AE,
        out float __MinionSpawnTime_BA,
        out float __MinionSpawnTime_BC,
        out float __MinionSpawnTime_CB,
        out float __MinionSpawnTime_CD,
        out float __MinionSpawnTime_DC,
        out float __MinionSpawnTime_DE,
        out float __MinionSpawnTime_ED,
        out float __MinionSpawnTime_EA,
        out float __ChaosScore,
        out float __OrderScore,
        out TeamId __PreviousOwner_A,
        out TeamId __PreviousOwner_B,
        out TeamId __PreviousOwner_C,
        out TeamId __PreviousOwner_D,
        out TeamId __PreviousOwner_E,
        TeamId CapturePointOwnerA,
        TeamId CapturePointOwnerB,
        TeamId CapturePointOwnerC,
        TeamId CapturePointOwnerD,
        TeamId CapturePointOwnerE,
        float MinionSpawnTime_AB,
        float MinionSpawnTime_AE,
        float MinionSpawnTime_BA,
        float MinionSpawnTime_BC,
        float MinionSpawnTime_CB,
        float MinionSpawnTime_CD,
        float MinionSpawnTime_DC,
        float MinionSpawnTime_DE,
        float MinionSpawnTime_ED,
        float MinionSpawnTime_EA,
        float ChaosScore,
        float OrderScore,
        TeamId PreviousOwner_A,
        TeamId PreviousOwner_B,
        TeamId PreviousOwner_C,
        TeamId PreviousOwner_D,
        TeamId PreviousOwner_E,
        float ScoringFloor,
        float Score_PointCapture,
        float Score_PointNeutralized,
        float Score_PointAssist,
        float Score_Strategist,
        float Score_Opportunist,
        float Score_Vanguard,
        AttackableUnit CapturePointGuardian0,
        AttackableUnit CapturePointGuardian1,
        AttackableUnit CapturePointGuardian2,
        AttackableUnit CapturePointGuardian3,
        AttackableUnit CapturePointGuardian4,
        float MinionSpawnRate_Seconds,
        bool EnableSecondaryCallout)


    {
        TeamId _CapturePointOwnerA = CapturePointOwnerA;
        TeamId _CapturePointOwnerB = CapturePointOwnerB;
        TeamId _CapturePointOwnerC = CapturePointOwnerC;
        TeamId _CapturePointOwnerD = CapturePointOwnerD;
        TeamId _CapturePointOwnerE = CapturePointOwnerE;
        float _MinionSpawnTime_AB = MinionSpawnTime_AB;
        float _MinionSpawnTime_AE = MinionSpawnTime_AE;
        float _MinionSpawnTime_BA = MinionSpawnTime_BA;
        float _MinionSpawnTime_BC = MinionSpawnTime_BC;
        float _MinionSpawnTime_CB = MinionSpawnTime_CB;
        float _MinionSpawnTime_CD = MinionSpawnTime_CD;
        float _MinionSpawnTime_DC = MinionSpawnTime_DC;
        float _MinionSpawnTime_DE = MinionSpawnTime_DE;
        float _MinionSpawnTime_ED = MinionSpawnTime_ED;
        float _MinionSpawnTime_EA = MinionSpawnTime_EA;
        float _ChaosScore = ChaosScore;
        float _OrderScore = OrderScore;
        TeamId _PreviousOwner_A = PreviousOwner_A;
        TeamId _PreviousOwner_B = PreviousOwner_B;
        TeamId _PreviousOwner_C = PreviousOwner_C;
        TeamId _PreviousOwner_D = PreviousOwner_D;
        TeamId _PreviousOwner_E = PreviousOwner_E;

        var getCapturePointDelta = new GetCapturePointDeltaClass();
        var obeliskCapturePointHelper = new ObeliskCapturePointHelperClass();
        bool result =
                  // Sequence name :Sequence

                  getCapturePointDelta.GetCapturePointDelta(
                        out CCDelta,
                        CapturePointOwnerA,
                        CapturePointOwnerB,
                        CapturePointOwnerC,
                        CapturePointOwnerD,
                        CapturePointOwnerE) &&
                  obeliskCapturePointHelper.ObeliskCapturePointHelper(
                        out _CapturePointOwnerA,
                        out _MinionSpawnTime_AB,
                        out _MinionSpawnTime_AE,
                        out _MinionSpawnTime_BA,
                        out _MinionSpawnTime_EA,
                        out _ChaosScore,
                        out _OrderScore,
                        out _PreviousOwner_A,
                        CapturePointOwnerA,
                        0,
                        CapturePointOwnerB,
                        CapturePointOwnerE,
                        MinionSpawnTime_AB,
                        MinionSpawnTime_AE,
                        MinionSpawnTime_BA,
                        MinionSpawnTime_EA,
                        ChaosScore,
                        OrderScore,
                        PreviousOwner_A,
                        ScoringFloor,
                        Score_PointCapture,
                        Score_PointNeutralized,
                        Score_PointAssist,
                        Score_Strategist,
                        Score_Opportunist,
                        CapturePointGuardian0,
                        MinionSpawnRate_Seconds,
                        EnableSecondaryCallout,
                        null,
                        null) &&
                  obeliskCapturePointHelper.ObeliskCapturePointHelper(
                        out _CapturePointOwnerB,
                        out _MinionSpawnTime_BC,
                        out _MinionSpawnTime_BA,
                        out _MinionSpawnTime_CB,
                        out _MinionSpawnTime_AB,
                        out _ChaosScore,
                        out _OrderScore,
                        out _PreviousOwner_B,
                        CapturePointOwnerB,
                        1,
                        CapturePointOwnerC,
                        CapturePointOwnerA,
                        MinionSpawnTime_BC,
                        MinionSpawnTime_BA,
                        MinionSpawnTime_CB,
                        MinionSpawnTime_AB,
                        ChaosScore,
                        OrderScore,
                        PreviousOwner_B,
                        ScoringFloor,
                        Score_PointCapture,
                        Score_PointNeutralized,
                        Score_PointAssist,
                        Score_Strategist,
                        Score_Opportunist,
                        CapturePointGuardian1,
                        MinionSpawnRate_Seconds,
                        EnableSecondaryCallout,
                        null,
                        null) &&
                  obeliskCapturePointHelper.ObeliskCapturePointHelper(
                        out _CapturePointOwnerC,
                        out _MinionSpawnTime_CD,
                        out _MinionSpawnTime_CB,
                        out _MinionSpawnTime_DC,
                        out _MinionSpawnTime_BC,
                        out _ChaosScore,
                        out _OrderScore,
                        out _PreviousOwner_C,
                        CapturePointOwnerC,
                        2,
                        CapturePointOwnerD,
                        CapturePointOwnerB,
                        MinionSpawnTime_CD,
                        MinionSpawnTime_CB,
                        MinionSpawnTime_DC,
                        MinionSpawnTime_BC,
                        ChaosScore,
                        OrderScore,
                        PreviousOwner_C,
                        ScoringFloor,
                        Score_PointCapture,
                        Score_PointNeutralized,
                        Score_PointAssist,
                        Score_Strategist,
                        Score_Opportunist,
                        CapturePointGuardian2,
                        MinionSpawnRate_Seconds,
                        EnableSecondaryCallout,
                        null,
                        null) &&
                  obeliskCapturePointHelper.ObeliskCapturePointHelper(
                        out _CapturePointOwnerD,
                        out _MinionSpawnTime_DE,
                        out _MinionSpawnTime_DC,
                        out _MinionSpawnTime_ED,
                        out _MinionSpawnTime_CD,
                        out _ChaosScore,
                        out _OrderScore,
                        out _PreviousOwner_D,
                        CapturePointOwnerD,
                        3,
                        CapturePointOwnerE,
                        CapturePointOwnerC,
                        MinionSpawnTime_DE,
                        MinionSpawnTime_DC,
                        MinionSpawnTime_ED,
                        MinionSpawnTime_CD,
                        ChaosScore,
                        OrderScore,
                        PreviousOwner_D,
                        ScoringFloor,
                        Score_PointCapture,
                        Score_PointNeutralized,
                        Score_PointAssist,
                        Score_Strategist,
                        Score_Opportunist,
                        CapturePointGuardian3,
                        MinionSpawnRate_Seconds,
                        EnableSecondaryCallout,
                        null,
                        null) &&
                  obeliskCapturePointHelper.ObeliskCapturePointHelper(
                        out _CapturePointOwnerE,
                        out _MinionSpawnTime_EA,
                        out _MinionSpawnTime_ED,
                        out _MinionSpawnTime_AE,
                        out _MinionSpawnTime_DE,
                        out _ChaosScore,
                        out _OrderScore,
                        out _PreviousOwner_E,
                        CapturePointOwnerE,
                        4,
                        CapturePointOwnerA,
                        CapturePointOwnerD,
                        MinionSpawnTime_EA,
                        MinionSpawnTime_ED,
                        MinionSpawnTime_AE,
                        MinionSpawnTime_DE,
                        ChaosScore,
                        OrderScore,
                        PreviousOwner_E,
                        ScoringFloor,
                        Score_PointCapture,
                        Score_PointNeutralized,
                        Score_PointAssist,
                        Score_Strategist,
                        Score_Opportunist,
                        CapturePointGuardian4,
                        MinionSpawnRate_Seconds,
                        EnableSecondaryCallout,
                        null,
                        null)

            ;

        __CapturePointOwnerA = _CapturePointOwnerA;
        __CapturePointOwnerB = _CapturePointOwnerB;
        __CapturePointOwnerC = _CapturePointOwnerC;
        __CapturePointOwnerD = _CapturePointOwnerD;
        __CapturePointOwnerE = _CapturePointOwnerE;
        __MinionSpawnTime_AB = _MinionSpawnTime_AB;
        __MinionSpawnTime_AE = _MinionSpawnTime_AE;
        __MinionSpawnTime_BA = _MinionSpawnTime_BA;
        __MinionSpawnTime_BC = _MinionSpawnTime_BC;
        __MinionSpawnTime_CB = _MinionSpawnTime_CB;
        __MinionSpawnTime_CD = _MinionSpawnTime_CD;
        __MinionSpawnTime_DC = _MinionSpawnTime_DC;
        __MinionSpawnTime_DE = _MinionSpawnTime_DE;
        __MinionSpawnTime_ED = _MinionSpawnTime_ED;
        __MinionSpawnTime_EA = _MinionSpawnTime_EA;
        __ChaosScore = _ChaosScore;
        __OrderScore = _OrderScore;
        __PreviousOwner_A = _PreviousOwner_A;
        __PreviousOwner_B = _PreviousOwner_B;
        __PreviousOwner_C = _PreviousOwner_C;
        __PreviousOwner_D = _PreviousOwner_D;
        __PreviousOwner_E = _PreviousOwner_E;

        return result;
    }
}

