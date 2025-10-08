using BehaviourTrees.Map8;

namespace BehaviourTrees;


class CapturePointManagerClass : OdinLayout
{

    float lastTimeExecuted_EP_ObeliskCapturePointHelper;
    float lastTimeExecuted_EP_minionGraveyardHelper;
    float lastTimeExecuted_EP_manager1;
    float lastTimeExecuted_EP_manager2;
    float lastTimeExecuted_EP_ScoreManager;
    float lastTimeExecuted_EP;


    private MinionSpawnInitializationClass minionSpawnInitialization = new();
    private CapturePointInitializationClass capturePointInitialization = new();
    private GuardianCapturePointInitializationClass guardianCapturePointInitialization = new();
    private RelicInitializationClass relicInitialization = new();
    private CenterRelicInitializationClass centerRelicInitialization = new();
    private RespawnInitializationClass respawnInitialization = new();
    private PersonalScore_InitializationClass personalScore_Initialization = new();
    private SpeedShrineInitializationClass speedShrineInitialization = new();
    private MinionGraveyardInitializationClass minionGraveyardInitialization = new();
    private QuestInitializationClass questInitialization = new();
    private NexusParticleInitializationClass nexusParticleInitialization = new();
    private MinionGraveyardHelperClass minionGraveyardHelper = new();
    private MinionSpawnManagerClass MinionSpawnManager = new();
    private ObeliskCapturePointHelperClass obeliskCapturePointHelper = new();
    private PersonalScore_ManagerClass personalScore_Manager = new();
    private RespawnManagerClass respawnManager = new();
    private QuestManagerClass questManager = new();
    private RelicManagerClass relicManager = new();
    private CenterRelicManagerClass centerRelicManager = new();
    private WinConditionCheckerClass winConditionChecker = new();
    private ScoreManagerClass scoreManager = new();
    private GetGuardianClass getGuardian = new();
    private InitializeGuardianParticlesClass initializeGuardianParticles = new();


    public bool CapturePointManager(
        out TeamId __FinalWinTeam,
      out float __RemainingOrderScore,
      out float __RemainingChaosScore,
      out TeamId __CapturePointAOwner,
      out TeamId __CapturePointBOwner,
      out TeamId __CapturePointCOwner,
      out TeamId __CapturePointDOwner,
      out TeamId __CapturePointEOwner,
      out TeamId __CapturePointPreviousOwnerA,
      out TeamId __CapturePointPreviousOwnerB,
      out TeamId __CapturePointPreviousOwnerC,
      out TeamId __CapturePointPreviousOwnerD,
      out TeamId __CapturePointPreviousOwnerE,
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
      out int __CCDelta,
      out bool __OrderRespawnBonus,
      out bool __ChaosRespawnBonus,
      out int __BombQuestID_Order,
      out int __BombQuestID_Chaos,
      out float __ScoringFloor,
      out bool __IsFirstBlood,
      float RemainingOrderScore,
      float RemainingChaosScore,
      TeamId CapturePointAOwner,
      TeamId CapturePointBOwner,
      TeamId CapturePointCOwner,
      TeamId CapturePointDOwner,
      TeamId CapturePointEOwner,
      TeamId CapturePointPreviousOwnerA,
      TeamId CapturePointPreviousOwnerB,
      TeamId CapturePointPreviousOwnerC,
      TeamId CapturePointPreviousOwnerD,
      TeamId CapturePointPreviousOwnerE,
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
      bool ChaosRespawnBonus,
      bool OrderRespawnBonus,
      int BombQuestID_Order,
      int BombQuestID_Chaos,
      bool IsFirstBlood)
    {

        float _RemainingOrderScore = RemainingOrderScore;
        float _RemainingChaosScore = RemainingChaosScore;
        TeamId _CapturePointAOwner = CapturePointAOwner;
        TeamId _CapturePointBOwner = CapturePointBOwner;
        TeamId _CapturePointCOwner = CapturePointCOwner;
        TeamId _CapturePointDOwner = CapturePointDOwner;
        TeamId _CapturePointEOwner = CapturePointEOwner;
        TeamId _CapturePointPreviousOwnerA = CapturePointPreviousOwnerA;
        TeamId _CapturePointPreviousOwnerB = CapturePointPreviousOwnerB;
        TeamId _CapturePointPreviousOwnerC = CapturePointPreviousOwnerC;
        TeamId _CapturePointPreviousOwnerD = CapturePointPreviousOwnerD;
        TeamId _CapturePointPreviousOwnerE = CapturePointPreviousOwnerE;
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
        int _CCDelta = default;
        bool _OrderRespawnBonus = OrderRespawnBonus;
        bool _ChaosRespawnBonus = ChaosRespawnBonus;
        int _BombQuestID_Order = BombQuestID_Order;
        int _BombQuestID_Chaos = BombQuestID_Chaos;
        float _ScoringFloor = default;
        bool _IsFirstBlood = IsFirstBlood;




        List<Func<bool>> EP_ScoreManager = new()
        { () =>
        {

            return  DebugAction("//////0000000000000///// ScoreManager") &&
            DebugAction($"{CapturePointAOwner}")&&
            scoreManager.ScoreManager(
                                    out Scoring_PreviousUpdateTime,
                                    out _RemainingOrderScore,
                                    out _RemainingChaosScore,
                                    out _CCDelta,
                                    out OverTimeLimitWarning,
                                    out NexusParticleEnabledOrder,
                                    out NexusParticleEnabledChaos,
                                    out NexusParticleOrder_1,
                                    out NexusParticleOrder_2,
                                    out NexusParticleChaos_1,
                                    out NexusParticleChaos_2,
                                    CapturePointAOwner,
                                    CapturePointBOwner,
                                    CapturePointCOwner,
                                    CapturePointDOwner,
                                    CapturePointEOwner,
                                    _RemainingChaosScore,
                                    _RemainingOrderScore,
                                    OverTimeLimitWarning,
                                    NexusParticleEnabledOrder,
                                    NexusParticleEnabledChaos,
                                    NexusParticleOrder_1,
                                    NexusParticleOrder_2,
                                    NexusParticleChaos_1,
                                    NexusParticleChaos_2,
                                    SoGLevelProp_Order,
                                    SoGLevelProp_Chaos)
            ;


        } };
        List<Func<bool>> EP_ObeliskCapturePointHelper = new()
        {
       () =>
        {
            return
                obeliskCapturePointHelper.ObeliskCapturePointHelper(
                    out _CapturePointAOwner,
                    out _PreviousMinionSpawnTime_AB,
                    out _PreviousMinionSpawnTime_AE,
                    out _PreviousMinionSpawnTime_BA,
                    out _PreviousMinionSpawnTime_EA,
                    out _RemainingChaosScore,
                    out _RemainingOrderScore,
                    out _CapturePointPreviousOwnerA,
                    _CapturePointAOwner,
                    0,
                    _CapturePointBOwner,
                    _CapturePointEOwner,
                    _PreviousMinionSpawnTime_AB,
                    _PreviousMinionSpawnTime_AE,
                    _PreviousMinionSpawnTime_BA,
                    _PreviousMinionSpawnTime_EA,
                    RemainingChaosScore,
                    RemainingOrderScore,
                    _CapturePointPreviousOwnerA,
                    ScoringFloor,
                    40,
                    40,
                    25,
                    5,
                    5,
                    Guardian0,
                    MinionSpawnRate_Seconds,
                    false,
                    ChaosShrineTurret,
                    OrderShrineTurret)
                ;
             },
             () =>
            {
        return
                obeliskCapturePointHelper.ObeliskCapturePointHelper(
                    out _CapturePointBOwner,
                    out _PreviousMinionSpawnTime_BC,
                    out _PreviousMinionSpawnTime_BA,
                    out _PreviousMinionSpawnTime_CB,
                    out _PreviousMinionSpawnTime_AB,
                    out _RemainingChaosScore,
                    out _RemainingOrderScore,
                    out _CapturePointPreviousOwnerB,
                    _CapturePointBOwner,
                    1,
                    _CapturePointCOwner,
                    _CapturePointAOwner,
                    _PreviousMinionSpawnTime_BC,
                    _PreviousMinionSpawnTime_BA,
                    _PreviousMinionSpawnTime_CB,
                    _PreviousMinionSpawnTime_AB,
                    _RemainingChaosScore,
                    _RemainingOrderScore,
                    _CapturePointPreviousOwnerB,
                    ScoringFloor,
                    40,
                    40,
                    25,
                    5,
                    5,
                    Guardian1,
                    MinionSpawnRate_Seconds,
                    false,
                    ChaosShrineTurret,
                    OrderShrineTurret)                 ;
             },
             () =>
            {
        return
                obeliskCapturePointHelper.ObeliskCapturePointHelper(
                    out _CapturePointCOwner,
                    out _PreviousMinionSpawnTime_CD,
                    out _PreviousMinionSpawnTime_CB,
                    out _PreviousMinionSpawnTime_DC,
                    out _PreviousMinionSpawnTime_BC,
                    out _RemainingChaosScore,
                    out _RemainingOrderScore,
                    out _CapturePointPreviousOwnerC,
                    _CapturePointCOwner,
                    2,
                    _CapturePointDOwner,
                    _CapturePointBOwner,
                    _PreviousMinionSpawnTime_CD,
                    _PreviousMinionSpawnTime_CB,
                    _PreviousMinionSpawnTime_DC,
                    _PreviousMinionSpawnTime_BC,
                    _RemainingChaosScore,
                    _RemainingOrderScore,
                    _CapturePointPreviousOwnerC,
                    ScoringFloor,
                    40,
                    40,
                    25,
                    5,
                    5,
                    Guardian2,
                    MinionSpawnRate_Seconds,
                    false,
                    ChaosShrineTurret,
                    OrderShrineTurret)

                                ;
             },
             () =>
            {
        return
                obeliskCapturePointHelper.ObeliskCapturePointHelper(
                    out _CapturePointDOwner,
                    out _PreviousMinionSpawnTime_DE,
                    out _PreviousMinionSpawnTime_DC,
                    out _PreviousMinionSpawnTime_ED,
                    out _PreviousMinionSpawnTime_CD,
                    out _RemainingChaosScore,
                    out _RemainingOrderScore,
                    out _CapturePointPreviousOwnerD,
                    _CapturePointDOwner,
                    3,
                    _CapturePointEOwner,
                    _CapturePointCOwner,
                    _PreviousMinionSpawnTime_DE,
                    _PreviousMinionSpawnTime_DC,
                    _PreviousMinionSpawnTime_ED,
                    _PreviousMinionSpawnTime_CD,
                    _RemainingChaosScore,
                    _RemainingOrderScore,
                    _CapturePointPreviousOwnerD,
                    ScoringFloor,
                    40,
                    40,
                    25,
                    5,
                    5,
                    Guardian3,
                    MinionSpawnRate_Seconds,
                    false,
                    ChaosShrineTurret,
                    OrderShrineTurret)
                                ;
             },
             () =>
            {
        return
                obeliskCapturePointHelper.ObeliskCapturePointHelper(
                    out _CapturePointEOwner,
                    out _PreviousMinionSpawnTime_EA,
                    out _PreviousMinionSpawnTime_ED,
                    out _PreviousMinionSpawnTime_AE,
                    out _PreviousMinionSpawnTime_DE,
                    out _RemainingChaosScore,
                    out _RemainingOrderScore,
                    out _CapturePointPreviousOwnerE,
                    _CapturePointEOwner,
                    4,
                    _CapturePointAOwner,
                    _CapturePointDOwner,
                    _PreviousMinionSpawnTime_EA,
                    _PreviousMinionSpawnTime_ED,
                    _PreviousMinionSpawnTime_AE,
                    _PreviousMinionSpawnTime_DE,
                    _RemainingChaosScore,
                    _RemainingOrderScore,
                    _CapturePointPreviousOwnerE,
                    ScoringFloor,
                    40,
                    40,
                    25,
                    5,
                    5,
                    Guardian4,
                    MinionSpawnRate_Seconds,
                    false,
                    ChaosShrineTurret,
                    OrderShrineTurret)
                        ;
             }
        };
        List<Func<bool>> EP_manager1 = new()
        {
        () =>
        {
            return
             DebugAction("///////////////// EP_manager1") &&
                                MinionSpawnManager.MinionSpawnManager(
                                         out MutationProgressionIndex,
                                         out _PreviousMinionSpawnTime_AB,
                                         out _PreviousMinionSpawnTime_AE,
                                         out _PreviousMinionSpawnTime_BA,
                                         out _PreviousMinionSpawnTime_BC,
                                         out _PreviousMinionSpawnTime_CB,
                                         out _PreviousMinionSpawnTime_CD,
                                         out _PreviousMinionSpawnTime_DC,
                                         out _PreviousMinionSpawnTime_DE,
                                         out _PreviousMinionSpawnTime_EA,
                                         out _PreviousMinionSpawnTime_ED,
                                         MutationProgressionIndex,
                                         WaveEncounterID,
                                         "RangedWaveProgression",
                                         "MeleeWaveProgression",
                                         _CapturePointAOwner,
                                         _CapturePointBOwner,
                                         _CapturePointCOwner,
                                         _CapturePointDOwner,
                                         _CapturePointEOwner,
                                         _PreviousMinionSpawnTime_AB,
                                         _PreviousMinionSpawnTime_AE,
                                         _PreviousMinionSpawnTime_BA,
                                         _PreviousMinionSpawnTime_BC,
                                         _PreviousMinionSpawnTime_CB,
                                         _PreviousMinionSpawnTime_CD,
                                         _PreviousMinionSpawnTime_DC,
                                         _PreviousMinionSpawnTime_DE,
                                         _PreviousMinionSpawnTime_EA,
                                         _PreviousMinionSpawnTime_ED,
                                         MinionSpawnRate_Seconds,
                                         "SuperminionProgression",
                                         Guardian0,
                                         Guardian1,
                                         Guardian2,
                                         Guardian3,
                                         Guardian4,
                                         MinionSpawnPoint_A1,
                                         MinionSpawnPoint_A2,
                                         MinionSpawnPoint_B1,
                                         MinionSpawnPoint_B2,
                                         MinionSpawnPoint_C1,
                                         MinionSpawnPoint_C2,
                                         MinionSpawnPoint_D1,
                                         MinionSpawnPoint_D2,
                                         MinionSpawnPoint_E1,
                                         MinionSpawnPoint_E2,
                                         MinionSpawnPortalParticleEncounterID)
                                ;
        }
        ,
             () =>
            {
            return
                                       personalScore_Manager.PersonalScore_Manager(
                                         _CapturePointAOwner,
                                         _CapturePointBOwner,
                                         _CapturePointCOwner,
                                         _CapturePointDOwner,
                                         _CapturePointEOwner,
                                         2,
                                         3,
                                         5,
                                         2,
                                         1,
                                         2,
                                         false) &&

                                       /*  (
                                   respawnManager.RespawnManager(
                                         out OrderRespawnMutator,
                                         out ChaosRespawnMutator,
                                         out RespawnWindowAdjustment_Chaos,
                                         out RespawnWindowAdjustment_Order,
                                         OrderRespawnMutator,
                                         ChaosRespawnMutator,
                                         _RemainingChaosScore,
                                         _RemainingOrderScore,
                                         RespawnWindowAdjustment_Chaos,
                                         RespawnWindowAdjustment_Order)
                                   || DebugAction("force true because respawn manager are not implemented")
                                   )&&*/
                                   questManager.QuestManager(
                                         out QuestID_Chaos,
                                         out QuestID_Order,
                                         out PreviousQuestCompleteTime,
                                         out QuestObjective_Order,
                                         out QuestObjective_Chaos,
                                         out _RemainingOrderScore,
                                         out _RemainingChaosScore,
                                         out QuestIconSquadID_Order,
                                         out QuestIconSquadID_Chaos,
                                         out QuestRewardsID_Chaos,
                                         out QuestRewardsID_Order,
                                         _CapturePointAOwner,
                                         _CapturePointBOwner,
                                         _CapturePointCOwner,
                                         _CapturePointDOwner,
                                         _CapturePointEOwner,
                                         QuestID_Chaos,
                                         QuestID_Order,
                                         PreviousQuestCompleteTime,
                                         QuestObjective_Order,
                                         QuestObjective_Chaos,
                                         Guardian0,
                                         Guardian1,
                                         Guardian2,
                                         Guardian3,
                                         Guardian4,
                                         _RemainingOrderScore,
                                         _RemainingChaosScore,
                                         QuestGiver_EncounterID,
                                         QuestIconEncounterID,
                                         QuestIconSquadID_Order,
                                         QuestObjective_Chaos,
                                         QuestIconEncounterID2,
                                         QuestRewardsID_Chaos,
                                         QuestRewardsID_Order)

                                ;
        } };
        List<Func<bool>> EP_manager2 = new()
    {
        () =>
        {
            return
             DebugAction("///////////////// EP_manager2") &&
                                    relicManager.RelicManager(
                                          out RelicSpawnTime_A,
                                          out RelicSpawnTime_B,
                                          out RelicSpawnTime_C,
                                          out RelicSquad_A,
                                          out RelicSquad_B,
                                          out RelicSquad_C,
                                          out RelicSpawnTime_D,
                                          out RelicSpawnTime_E,
                                          out RelicSquad_D,
                                          out RelicSquad_E,
                                          out RelicSpawnTime_F,
                                          out RelicSpawnTime_G,
                                          out RelicSpawnTime_H,
                                          out RelicSquad_F,
                                          out RelicSquad_G,
                                          out RelicSquad_H,
                                          out RelicSquad_I,
                                          out RelicSquad_J,
                                          out RelicSpawnTime_I,
                                          out RelicSpawnTime_J,
                                          RelicSquad_A,
                                          RelicSquad_B,
                                          RelicSquad_C,
                                          RelicSpawnTime_A,
                                          RelicSpawnTime_B,
                                          RelicSpawnTime_C,
                                          RelicSpawnTime_D,
                                          RelicSpawnTime_E,
                                          RelicSquad_D,
                                          RelicSquad_E,
                                          RelicEncounterShield,
                                          RelicSpawnTime_F,
                                          RelicSpawnTime_G,
                                          RelicSpawnTime_H,
                                          RelicSquad_F,
                                          RelicSquad_G,
                                          RelicSquad_H,
                                          RelicSquad_I,
                                          RelicSquad_J,
                                          RelicSpawnTime_I,
                                          RelicSpawnTime_J,
                                          RelicPositionA,
                                          RelicPositionB,
                                          RelicPositionC,
                                          RelicPositionD,
                                          RelicPositionE,
                                          RelicPositionF,
                                          RelicPositionG,
                                          RelicPositionH,
                                          RelicPositionI,
                                          RelicPositionJ)
                                ;
        }
    ,
             () =>
            {
        return
                                    centerRelicManager.CenterRelicManager(
                                          out CenterRelicSpawnTime_A,
                                          out CenterRelicSpawnTime_B,
                                          out CenterRelicSquad_A,
                                          out CenterRelicSquad_B,
                                          out CenterRelicParticleAttached1,
                                          out CenterRelicParticleAttached2,
                                          CenterRelicSpawnTime_A,
                                          CenterRelicSpawnTime_B,
                                          CenterRelicSquad_A,
                                          CenterRelicSquad_B,
                                          RelicEncounterCenter,
                                          RelicEncounterCenter2,
                                          CenterRelicPositionA,
                                          CenterRelicPositionB,
                                          CenterRelicParticleAttached1,
                                          CenterRelicParticleAttached2)
                                ;
             },
             () =>
            {
        return
                                    winConditionChecker.WinConditionChecker(
                                          out FinalWinTeam,
                                          _RemainingOrderScore,
                                          _RemainingChaosScore,
                                          FinalWinTeam)

                                ;
             }};
        List<Func<bool>> EP_minionGraveyardHelper = new()
                 { () =>
        {
            return
             DebugAction($"///////////////// EP_minionGraveyardHelper {MinionGraveyardA.X} {MinionGraveyardA.Y} {MinionGraveyardA.Z}") &&
                       minionGraveyardHelper.MinionGraveyardHelper(
                                          _CapturePointAOwner,
                                          MinionGraveyardA,
                                          150,
                                          "AB",
                                          "AE",
                                          OrderShrineTurret)
                                ;
             },
             () =>
            {
        return
                      minionGraveyardHelper.MinionGraveyardHelper(
                                          _CapturePointBOwner,
                                          MinionGraveyardB,
                                          150,
                                          "BA",
                                          "BC",
                                          OrderShrineTurret)
                                ;
             },
             () =>
            {
        return
                      minionGraveyardHelper.MinionGraveyardHelper(
                                          _CapturePointCOwner,
                                          MinionGraveyardC,
                                          150,
                                          "CB",
                                          "CD",
                                          OrderShrineTurret)
                                ;
             },
             () =>
            {
        return
                      minionGraveyardHelper.MinionGraveyardHelper(
                                          _CapturePointDOwner,
                                          MinionGraveyardD,
                                          150,
                                          "DC",
                                          "DE",
                                          OrderShrineTurret)
                                ;
             },
             () =>
            {
        return
                        minionGraveyardHelper.MinionGraveyardHelper(
                                          _CapturePointEOwner,
                                          MinionGraveyardE,
                                          150,
                                          "EA",
                                          "ED",
                                          OrderShrineTurret)
                                ;
             }
        };


        bool result =
                  // Sequence name :Selector

                  // Sequence name :Initialization_Pre
                  (
                        __IsFirstRun == true &&
                        SetVarBool(
                              out GuardianInit,
                              false) &&
                        SetVarBool(
                              out Initialization,
                              false) &&
                              // Sequence name :Actual_Initialization


                              Initialization == false &&
                              minionSpawnInitialization.MinionSpawnInitialization(
                                    out _PreviousMinionSpawnTime_AB,
                                    out _PreviousMinionSpawnTime_AE,
                                    out _PreviousMinionSpawnTime_BA,
                                    out _PreviousMinionSpawnTime_BC,
                                    out _PreviousMinionSpawnTime_CB,
                                    out _PreviousMinionSpawnTime_CD,
                                    out _PreviousMinionSpawnTime_DC,
                                    out _PreviousMinionSpawnTime_DE,
                                    out _PreviousMinionSpawnTime_EA,
                                    out _PreviousMinionSpawnTime_ED,
                                    out WaveEncounterID,
                                    out MutationProgressionIndex,
                                    out MinionSpawnRate_Seconds,
                                    out MinionSpawnPoint_A1,
                                    out MinionSpawnPoint_A2,
                                    out MinionSpawnPoint_B1,
                                    out MinionSpawnPoint_B2,
                                    out MinionSpawnPoint_C1,
                                    out MinionSpawnPoint_C2,
                                    out MinionSpawnPoint_D1,
                                    out MinionSpawnPoint_D2,
                                    out MinionSpawnPoint_E1,
                                    out MinionSpawnPoint_E2,
                                    out MinionSpawnPortalParticleEncounterID
                                   ) &&
                              capturePointInitialization.CapturePointInitialization(
                                    out _CapturePointPreviousOwnerA,
                                    out _CapturePointPreviousOwnerB,
                                    out _CapturePointPreviousOwnerC,
                                    out _CapturePointPreviousOwnerD,
                                    out _CapturePointPreviousOwnerE,
                                    out _CapturePointAOwner,
                                    out _CapturePointBOwner,
                                    out _CapturePointCOwner,
                                    out _CapturePointDOwner,
                                    out _CapturePointEOwner
                                    ) &&
                                    // Sequence name :Variable_Initialization

                                    GetTurret(
                                          out OrderShrineTurret,
                                          TeamId.TEAM_ORDER,
                                          0,
                                          1) &&
                                    GetTurret(
                                          out ChaosShrineTurret,
                                          TeamId.TEAM_CHAOS,
                                          0,
                                          1) &&
                                    GetGameTime(
                                          out CurrentGameTime) &&
                                          // Sequence name :VictoryPoints

                                          SetVarFloat(
                                                out _RemainingOrderScore,
                                                500) &&
                                          SetVarFloat(
                                                out _RemainingChaosScore,
                                                500) &&
                                                DebugAction("initialization SetGameScore") &&
                                          SetGameScore(
                                                TeamId.TEAM_ORDER,
                                                500) &&
                                          SetGameScore(
                                                TeamId.TEAM_CHAOS,
                                                500) &&
                                                DebugAction("finish SetGameScore") &&
                                          SetVarFloat(
                                                out Scoring_PreviousUpdateTime,
                                                CurrentGameTime) &&
                                          SetVarFloat(
                                                out _ScoringFloor,
                                                100)
                                     &&
                                    SetVarBool(
                                          out GuardianInit,
                                          false) &&
                                    SetVarBool(
                                          out OverTimeLimitWarning,
                                          false)
                               &&
                              guardianCapturePointInitialization.GuardianCapturePointInitialization() &&
                              relicInitialization.RelicInitialization(
                                    out RelicSquad_A,
                                    out RelicSquad_B,
                                    out RelicSquad_C,
                                    out RelicSpawnTime_A,
                                    out RelicSpawnTime_B,
                                    out RelicSpawnTime_C,
                                    out RelicSpawnTime_D,
                                    out RelicSpawnTime_E,
                                    out RelicSquad_D,
                                    out RelicSquad_E,
                                    out RelicEncounterShield,
                                    out RelicSpawnTime_F,
                                    out RelicSpawnTime_G,
                                    out RelicSpawnTime_H,
                                    out RelicSquad_F,
                                    out RelicSquad_G,
                                    out RelicSquad_H,
                                    out RelicSpawnTime_I,
                                    out RelicSpawnTime_J,
                                    out RelicSquad_I,
                                    out RelicSquad_J,
                                    out RelicPositionA,
                                    out RelicPositionB,
                                    out RelicPositionC,
                                    out RelicPositionD,
                                    out RelicPositionE,
                                    out RelicPositionF,
                                    out RelicPositionG,
                                    out RelicPositionI,
                                    out RelicPositionJ,
                                    out RelicPositionH
                                   ) &&
                              centerRelicInitialization.CenterRelicInitialization(
                                    out CenterRelicSpawnTime_A,
                                    out CenterRelicSpawnTime_B,
                                    out CenterRelicSquad_A,
                                    out CenterRelicSquad_B,
                                    out RelicEncounterCenter,
                                    out RelicEncounterCenter2,
                                    out CenterRelicPositionB,
                                    out CenterRelicPositionA,
                                    out CenterRelicParticleAttached1,
                                    out CenterRelicParticleAttached2
                                   ) &&
                              respawnInitialization.RespawnInitialization(
                                    out ChaosRespawnMutator,
                                    out OrderRespawnMutator,
                                    out RespawnWindowAdjustment_Order,
                                    out RespawnWindowAdjustment_Chaos
                                   ) &&
                              personalScore_Initialization.PersonalScore_Initialization(
                                    out _IsFirstBlood) &&
                              speedShrineInitialization.SpeedShrineInitialization() &&
                              minionGraveyardInitialization.MinionGraveyardInitialization(
                                    out MinionGraveyardA,
                                    out MinionGraveyardB,
                                    out MinionGraveyardC,
                                    out MinionGraveyardD,
                                    out MinionGraveyardE,
                                    out MinionGraveyardPortalEncounter,
                                    out MinionGraveyardPortalID_A,
                                    out MinionGraveyardPortalID_B,
                                    out MinionGraveyardPortalID_C,
                                    out MinionGraveyardPortalID_D,
                                    out MinionGraveyardPortalID_E,
                                    out MinionGraveyardPortalStartTime_A,
                                    out MinionGraveyardPortalStartTime_B,
                                    out MinionGraveyardPortalStartTime_C,
                                    out MinionGraveyardPortalStartTime_D,
                                    out MinionGraveyardPortalStartTime_E) &&
                              questInitialization.QuestInitialization(
                                    out PreviousQuestCompleteTime,
                                    out QuestID_Order,
                                    out QuestID_Chaos,
                                    out QuestObjective_Order,
                                    out QuestObjective_Chaos,
                                    out QuestGiver_EncounterID,
                                    out QuestIconEncounterID,
                                    out QuestIconSquadID_Order,
                                    out QuestIconSquadID_Chaos,
                                    out QuestIconEncounterID2,
                                    out QuestRewardsID_Order,
                                    out QuestRewardsID_Chaos
                                    ) &&
                                    // Sequence name :EndOfGameInit

                                    SetVarFloat(
                                          out GameEndTime,
                                          0) &&
                                    SetVarBool(
                                          out EndCeremonyBool,
                                          true) &&
                                    SetVarInt(
                                          out EndOfGameState,
                                          -1) &&
                                    SetVarUnitTeam(
                                          out FinalWinTeam,
                                          TeamId.TEAM_NEUTRAL)
                               &&
                              SetVarBool(
                                    out Initialization,
                                    true) &&
                              nexusParticleInitialization.NexusParticleInitialization(
                                    out NexusParticleEnabledOrder,
                                    out NexusParticleEnabledChaos,
                                    out NexusParticleOrder_1,
                                    out NexusParticleOrder_2,
                                    out NexusParticleChaos_1,
                                    out NexusParticleChaos_2,
                                    out SoGLevelProp_Order,
                                    out SoGLevelProp_Chaos)

                  ) ||
                  // Sequence name :Point_Logic
                  (

                        GuardianInit == true &&
                        GetGameTime(
                              out CurrentGameTime)
                        &&
                        ExecutePeriodically(
                            ref lastTimeExecuted_EP_ObeliskCapturePointHelper,
                            EP_ObeliskCapturePointHelper,
                              true,
                              0.2f)
                        // Sequence name :Sequence
                        &&

                          ExecutePeriodically(
                              ref lastTimeExecuted_EP_minionGraveyardHelper,
                              EP_minionGraveyardHelper,
                                true,
                                0.4f)
                          &&
                          // Sequence name :Sequence

                          ExecutePeriodically(
                              ref lastTimeExecuted_EP_manager1,
                              EP_manager1,
                                true,
                                1.1f)                              // Sequence name :Sequence
                          &&

                          ExecutePeriodically(
                              ref lastTimeExecuted_EP_manager2,
                              EP_manager2,
                                true,
                                1.3f)                              // Sequence name :Sequence
                          &&
                        ExecutePeriodically(
                            ref lastTimeExecuted_EP_ScoreManager,
                            EP_ScoreManager,
                              true,
                              1.85f)
                  ) ||
                  // Sequence name :Initialize_Guardians
                  (
                        GuardianInit == false &&
                        getGuardian.GetGuardian(
                              out Guardian0,
                              0) &&
                         getGuardian.GetGuardian(
                              out Guardian1,
                              1) &&
                         getGuardian.GetGuardian(
                              out Guardian2,
                              2) &&
                         getGuardian.GetGuardian(
                              out Guardian3,
                              3) &&
                         getGuardian.GetGuardian(
                              out Guardian4,
                              4) &&
                        initializeGuardianParticles.InitializeGuardianParticles() &&
                        SetVarBool(
                              out GuardianInit,
                              true)

                  )
            ;

        __RemainingOrderScore = _RemainingOrderScore;
        __RemainingChaosScore = _RemainingChaosScore;
        __CapturePointAOwner = _CapturePointAOwner;
        __CapturePointBOwner = _CapturePointBOwner;
        __CapturePointCOwner = _CapturePointCOwner;
        __CapturePointDOwner = _CapturePointDOwner;
        __CapturePointEOwner = _CapturePointEOwner;
        __CapturePointPreviousOwnerA = _CapturePointPreviousOwnerA;
        __CapturePointPreviousOwnerB = _CapturePointPreviousOwnerB;
        __CapturePointPreviousOwnerC = _CapturePointPreviousOwnerC;
        __CapturePointPreviousOwnerD = _CapturePointPreviousOwnerD;
        __CapturePointPreviousOwnerE = _CapturePointPreviousOwnerE;
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
        __CCDelta = _CCDelta;
        __OrderRespawnBonus = _OrderRespawnBonus;
        __ChaosRespawnBonus = _ChaosRespawnBonus;
        __BombQuestID_Order = _BombQuestID_Order;
        __BombQuestID_Chaos = _BombQuestID_Chaos;
        __ScoringFloor = _ScoringFloor;
        __IsFirstBlood = _IsFirstBlood;
        __FinalWinTeam = FinalWinTeam;

        return result;
    }
}

