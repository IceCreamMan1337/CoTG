using BehaviourTrees;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.Game;


namespace MapScripts.Map8.GameModes;



public class ODIN : DefaultGamemode
{


    bool OdinOpeningCeremony_bool = true;
    private OdinOpeningCeremonyClass_forscript odinOpeningCeremonyClass_forscript = new OdinOpeningCeremonyClass_forscript();
    private OdinOpeningCeremonyClass odinOpeningCeremony = new OdinOpeningCeremonyClass();
    private LevelPropAnimationsClass levelPropAnimations = new LevelPropAnimationsClass();
    private CapturePointManagerClass capturePointManager = new CapturePointManagerClass();
    private ThresholdAnnouncementsClass ThresholdAnnouncements = new ThresholdAnnouncementsClass();
    private EndOfGameCeremonyClass_forscript EndOfGameCeremony = new EndOfGameCeremonyClass_forscript();
    private CapturePointManagerClass_script capturePointManagerClass_script = new CapturePointManagerClass_script();

    float TeamscoreChaos = 500.0f;
    float TeamscoreOrder = 500.0f;

    TeamId Teamwinner = TeamId.TEAM_UNKNOWN;
    uint musiccue = default;
    int EndGameState = default;
    uint OrderNexusBeam_L1 = default;
    uint OrderNexusBeam_L2 = default;
    uint OrderNexusBeam_L3 = default;
    uint OrderNexusBeam_L4 = default;
    uint OrderNexusBeam_R1 = default;
    uint OrderNexusBeam_R2 = default;
    uint OrderNexusBeam_R3 = default;
    uint OrderNexusBeam_R4 = default;
    uint OrderNexusGlow_1 = default;
    uint OrderNexusGlow_2 = default;
    uint OrderNexusGlow_3 = default;
    uint OrderNexusGlow_4 = default;
    uint ChaosNexusBeam_R1 = default;
    uint ChaosNexusBeam_R2 = default;
    uint ChaosNexusBeam_R3 = default;
    uint ChaosNexusBeam_R4 = default;
    uint ChaosNexusBeam_L1 = default;
    uint ChaosNexusBeam_L2 = default;
    uint ChaosNexusBeam_L3 = default;
    uint ChaosNexusBeam_L4 = default;
    uint ChaosNexusGlow_1 = default;
    uint ChaosNexusGlow_2 = default;
    uint ChaosNexusGlow_3 = default;
    uint ChaosNexusGlow_4 = default;
    uint OrderNexusCrystal = default;
    uint ChaosNexusCrystal = default;
    uint OrderNexusBeamRed_L1 = default;
    uint OrderNexusBeamRed_L2 = default;
    uint OrderNexusBeamRed_L3 = default;
    uint OrderNexusBeamRed_L4 = default;
    uint OrderNexusBeamRed_R1 = default;
    uint OrderNexusBeamRed_R2 = default;
    uint OrderNexusBeamRed_R3 = default;
    uint OrderNexusBeamRed_R4 = default;
    uint ChaosNexusBeamRed_L1 = default;
    uint ChaosNexusBeamRed_L2 = default;
    uint ChaosNexusBeamRed_L3 = default;
    uint ChaosNexusBeamRed_L4 = default;
    uint ChaosNexusBeamRed_R1 = default;
    uint ChaosNexusBeamRed_R2 = default;
    uint ChaosNexusBeamRed_R3 = default;
    uint ChaosNexusBeamRed_R4 = default;

    float RemainingOrderScore = default;
    float RemainingChaosScore = default;
    TeamId CapturePointAOwner = default;
    TeamId CapturePointBOwner = default;
    TeamId CapturePointCOwner = default;
    TeamId CapturePointDOwner = default;
    TeamId CapturePointEOwner = default;
    TeamId CapturePointPreviousOwnerA = default;
    TeamId CapturePointPreviousOwnerB = default;
    TeamId CapturePointPreviousOwnerC = default;
    TeamId CapturePointPreviousOwnerD = default;
    TeamId CapturePointPreviousOwnerE = default;
    float PreviousMinionSpawnTime_AB = default;
    float PreviousMinionSpawnTime_AE = default;
    float PreviousMinionSpawnTime_BA = default;
    float PreviousMinionSpawnTime_BC = default;
    float PreviousMinionSpawnTime_CB = default;
    float PreviousMinionSpawnTime_CD = default;
    float PreviousMinionSpawnTime_DC = default;
    float PreviousMinionSpawnTime_DE = default;
    float PreviousMinionSpawnTime_EA = default;
    float PreviousMinionSpawnTime_ED = default;
    bool ChaosRespawnBonus = default;
    bool OrderRespawnBonus = default;
    int BombQuestID_Order = default;
    int BombQuestID_Chaos = default;
    bool IsFirstBlood = default;





    public override MapScriptMetadata MapScriptMetadata { get; } = new()
    {
        MinionSpawnEnabled = false,
        OverrideSpawnPoints = true,
        RecallSpellItemId = 2005,
        InitialLevel = 3,
    };

    //This function is executed in-between Loading the map structures and applying the structure protections. Is the first thing on this script to be executed
    public override void OnLevelLoad(Dictionary<ObjectType, List<MapObject>> mapObjects)
    {


        odinOpeningCeremony.__IsFirstRun = true;
        capturePointManager.__IsFirstRun = true;

        base.OnLevelLoad(mapObjects);



        //TODO: Implement Dynamic Minion spawn mechanics for Map8
        //SpawnEnabled = map.IsMinionSpawnEnabled();
        //AddSurrender(1200000.0f, 300000.0f, 30.0f);

        //  LevelScriptObjects.LoadObjects(mapObjects);


        /*
        // Welcome to the Crystal Scar!
        CreateTimedEvent(() => AnnounceStartGameMessage(3, 8), 30);
        // The battle will begin in 30 seconds!
        CreateTimedEvent(() => AnnounceStartGameMessage(1, 8), 50);
        // The Battle Has Begun!
        CreateTimedEvent(() => AnnounceStartGameMessage(2, 8), 80);
        CreateTimedEvent(AnnouceNexusCrystalStart, 90);

        CreateTimedEvent(() => LevelProps.StairAnimation("Close1", 17.5f), 1);
        //The timing feels off, but from what i've seen from old footage, it looks like it is just like that
        CreateTimedEvent(() => { LevelProps.Particles(4); LevelProps.AddNexusParticles(); }, 16);
        CreateTimedEvent(() => LevelProps.StairAnimation("Close2"), 21);
        CreateTimedEvent(() => LevelProps.Particles(3), 40);
        CreateTimedEvent(() => LevelProps.StairAnimation("Close3"), 41);
        CreateTimedEvent(() => LevelProps.Particles(2), 59.6f);
        CreateTimedEvent(() => LevelProps.StairAnimation("Close4"), 61);
        CreateTimedEvent(() => LevelProps.Particles(1), 80);
        CreateTimedEvent(() => LevelProps.StairAnimation("Raise", 6.7f), 81);
        CreateTimedEvent(LevelProps.StairRaisedIdle, 87.0f);*/
    }

    //readonly Dictionary<TeamId, int> TeamScores = new() { { TeamId.TEAM_ORDER, 500 }, { TeamId.TEAM_CHAOS, 500 } };
    public override void OnMatchStart()
    {


        //LevelScriptObjects.OnMatchStart();

        CreateLevelProps.CreateProps();
        /* foreach (var champion in GetAllPlayers())
         {
             AddBuff("OdinPlayerBuff", 25000, 1, null, champion, null);
         }
        */



        /* foreach (var team in TeamScores.Keys)
         {
            // Console.WriteLine("///////////////////////////////////////////////////////////////////////////////");
             NotifyGameScore(team, TeamScores[team]);
         } */
    }
    private float lastExecutionTime = 0;
    private float interval = 1000;
    bool OdinOpeningCeremonybool = true;
    bool Odinmanagerbool = true;

    bool __OdinOpeningCeremonybool = false;

    bool OdinOpeningCeremonybool2 = true;
    bool __OdinOpeningCeremonybool2 = false;
    public override void OnUpdate()
    {

        TeamscoreOrder = GetTeamScore(TeamId.TEAM_ORDER);
        TeamscoreChaos = GetTeamScore(TeamId.TEAM_CHAOS);

        SetGameScoreCS(TeamId.TEAM_ORDER, (int)TeamscoreOrder);
        SetGameScoreCS(TeamId.TEAM_CHAOS, (int)TeamscoreChaos);

        /*
        in file of .142 , he exist BT script in data/level these one are all correct 
        but in map folder he exist 2 behaviourtree who seem used partially , 
         odinscript.xml and Odintower.xml

        odinscript.xml
          <BTInstance Type="PrimaryQuest" Name="OdinOpeningCeremony"> // seem useless 
          <BTInstance Type="PrimaryQuest" Name="ScoreboardPrototypeManager"> // seem useless 
          <BTInstance Type="PrimaryQuest" Name="CapturePointManager">
        Odinbehaviourtree.xml 
        <BTInstance Type="BehaviourTree" Name="OdinTowerBehavior">

        but these one have some shit , and when we see mission.ini they are called 
        my supposition these one are preload and be overwrite by data/level
        */
        if ((Game.Time.GameTime - lastExecutionTime) >= interval)
        {
            Odin_BT_LEVELSCRIPT_initialisation();
            lastExecutionTime = Game.Time.GameTime;
        }

        Odin_BT_LEVELSCRIPT_manager();
        Odin_BT_LEVELSCRIPT_ThresholdAnnouncements();
        Odin_BT_LEVELSCRIPT_EndOfGameCeremony();


    }
    public bool Odin_BT_LEVELSCRIPT_initialisation()
    {

        if (





            //DATA\Level

            levelPropAnimations.LevelPropAnimations()
            &&
            OdinOpeningCeremonybool
            &&
            odinOpeningCeremony.OdinOpeningCeremony(

                out uint __OrderNexusBeam_L1,
     out uint __OrderNexusBeam_L2,
     out uint __OrderNexusBeam_L3,
     out uint __OrderNexusBeam_L4,
     out uint __OrderNexusBeam_R1,
     out uint __OrderNexusBeam_R2,
     out uint __OrderNexusBeam_R3,
     out uint __OrderNexusBeam_R4,
     out uint __OrderNexusGlow_1,
     out uint __OrderNexusGlow_2,
     out uint __OrderNexusGlow_3,
     out uint __OrderNexusGlow_4,
     out uint __ChaosNexusBeam_R1,
     out uint __ChaosNexusBeam_R2,
     out uint __ChaosNexusBeam_R3,
     out uint __ChaosNexusBeam_R4,
     out uint __ChaosNexusBeam_L1,
     out uint __ChaosNexusBeam_L2,
     out uint __ChaosNexusBeam_L3,
     out uint __ChaosNexusBeam_L4,
     out uint __ChaosNexusGlow_1,
     out uint __ChaosNexusGlow_2,
     out uint __ChaosNexusGlow_3,
     out uint __ChaosNexusGlow_4,
     out uint __OrderNexusCrystal,
     out uint __ChaosNexusCrystal,
     out uint __OrderNexusBeamRed_L1,
     out uint __OrderNexusBeamRed_L2,
     out uint __OrderNexusBeamRed_L3,
     out uint __OrderNexusBeamRed_L4,
     out uint __OrderNexusBeamRed_R1,
     out uint __OrderNexusBeamRed_R2,
     out uint __OrderNexusBeamRed_R3,
     out uint __OrderNexusBeamRed_R4,
     out uint __ChaosNexusBeamRed_L1,
     out uint __ChaosNexusBeamRed_L2,
     out uint __ChaosNexusBeamRed_L3,
     out uint __ChaosNexusBeamRed_L4,
     out uint __ChaosNexusBeamRed_R1,
     out uint __ChaosNexusBeamRed_R2,
     out uint __ChaosNexusBeamRed_R3,
     out uint __ChaosNexusBeamRed_R4,
     out bool __OdinOpeningCeremonybool,
    OrderNexusBeam_L1,
    OrderNexusBeam_L2,
    OrderNexusBeam_L3,
    OrderNexusBeam_L4,
    OrderNexusBeam_R1,
    OrderNexusBeam_R2,
    OrderNexusBeam_R3,
    OrderNexusBeam_R4,
    OrderNexusGlow_1,
    OrderNexusGlow_2,
    OrderNexusGlow_3,
    OrderNexusGlow_4,
    ChaosNexusBeam_R1,
    ChaosNexusBeam_R2,
    ChaosNexusBeam_R3,
    ChaosNexusBeam_R4,
    ChaosNexusBeam_L1,
    ChaosNexusBeam_L2,
    ChaosNexusBeam_L3,
    ChaosNexusBeam_L4,
    ChaosNexusGlow_1,
    ChaosNexusGlow_2,
    ChaosNexusGlow_3,
    ChaosNexusGlow_4,
    OrderNexusCrystal,
    ChaosNexusCrystal,
    OrderNexusBeamRed_L1,
    OrderNexusBeamRed_L2,
    OrderNexusBeamRed_L3,
    OrderNexusBeamRed_L4,
    OrderNexusBeamRed_R1,
    OrderNexusBeamRed_R2,
    OrderNexusBeamRed_R3,
    OrderNexusBeamRed_R4,
    ChaosNexusBeamRed_L1,
    ChaosNexusBeamRed_L2,
    ChaosNexusBeamRed_L3,
    ChaosNexusBeamRed_L4,
    ChaosNexusBeamRed_R1,
    ChaosNexusBeamRed_R2,
    ChaosNexusBeamRed_R3,
    ChaosNexusBeamRed_R4,
    OdinOpeningCeremonybool
                )


            )
        {
            OdinOpeningCeremonybool = __OdinOpeningCeremonybool;
            odinOpeningCeremony.__IsFirstRun = false;

        }
        return OdinOpeningCeremonybool;
    }
    //
    public bool Odin_BT_LEVELSCRIPT_manager()
    {

        if ((Teamwinner == TeamId.TEAM_UNKNOWN || Teamwinner == TeamId.TEAM_NEUTRAL)

            &&
            capturePointManagerClass_script.CapturePointManager_script( //this one seem an debug bt everything is shit 
                                                                        // but for moment only this one have debugradius 
      /*   out float CapturePointEProgress,
         out float CapturePointAProgress,
         out float CapturePointBProgress,
         out float CapturePointCProgress,
         out float CapturePointDProgress,
         out TeamId CapturePointEOwner,
         out TeamId CapturePointAOwner,
         out TeamId CapturePointBOwner,
         out TeamId CapturePointCOwner,
         out TeamId CapturePointDOwner,
         out float TeamOrderScore,
         out float TeamChaosScore, */
      OdinOpeningCeremonybool2
                )
            &&
            capturePointManager.CapturePointManager(
            out TeamId __Teamwinner,
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
        out int CCDelta,
        out bool __OrderRespawnBonus,
        out bool __ChaosRespawnBonus,
        out int __BombQuestID_Order,
        out int __BombQuestID_Chaos,
        out float ScoringFloor,
        out bool __IsFirstBlood,
         RemainingOrderScore,
         RemainingChaosScore,
         CapturePointAOwner,
         CapturePointBOwner,
         CapturePointCOwner,
         CapturePointDOwner,
         CapturePointEOwner,
         CapturePointPreviousOwnerA,
         CapturePointPreviousOwnerB,
         CapturePointPreviousOwnerC,
         CapturePointPreviousOwnerD,
         CapturePointPreviousOwnerE,
         PreviousMinionSpawnTime_AB,
         PreviousMinionSpawnTime_AE,
         PreviousMinionSpawnTime_BA,
         PreviousMinionSpawnTime_BC,
         PreviousMinionSpawnTime_CB,
         PreviousMinionSpawnTime_CD,
         PreviousMinionSpawnTime_DC,
         PreviousMinionSpawnTime_DE,
         PreviousMinionSpawnTime_EA,
         PreviousMinionSpawnTime_ED,
         ChaosRespawnBonus,
         OrderRespawnBonus,
         BombQuestID_Order,
         BombQuestID_Chaos,
         IsFirstBlood)
              )
        {
            Teamwinner = __Teamwinner;
            RemainingOrderScore = __RemainingOrderScore;
            RemainingChaosScore = __RemainingChaosScore;
            CapturePointAOwner = __CapturePointAOwner;
            CapturePointBOwner = __CapturePointBOwner;
            CapturePointCOwner = __CapturePointCOwner;
            CapturePointDOwner = __CapturePointDOwner;
            CapturePointEOwner = __CapturePointEOwner;
            CapturePointPreviousOwnerA = __CapturePointPreviousOwnerA;
            CapturePointPreviousOwnerB = __CapturePointPreviousOwnerB;
            CapturePointPreviousOwnerC = __CapturePointPreviousOwnerC;
            CapturePointPreviousOwnerD = __CapturePointPreviousOwnerD;
            CapturePointPreviousOwnerE = __CapturePointPreviousOwnerE;
            PreviousMinionSpawnTime_AB = __PreviousMinionSpawnTime_AB;
            PreviousMinionSpawnTime_AE = __PreviousMinionSpawnTime_AE;
            PreviousMinionSpawnTime_BA = __PreviousMinionSpawnTime_BA;
            PreviousMinionSpawnTime_BC = __PreviousMinionSpawnTime_BC;
            PreviousMinionSpawnTime_CB = __PreviousMinionSpawnTime_CB;
            PreviousMinionSpawnTime_CD = __PreviousMinionSpawnTime_CD;
            PreviousMinionSpawnTime_DC = __PreviousMinionSpawnTime_DC;
            PreviousMinionSpawnTime_DE = __PreviousMinionSpawnTime_DE;
            PreviousMinionSpawnTime_EA = __PreviousMinionSpawnTime_EA;
            PreviousMinionSpawnTime_ED = __PreviousMinionSpawnTime_ED;
            //  __CCDelta = _CCDelta;
            OrderRespawnBonus = __OrderRespawnBonus;
            ChaosRespawnBonus = __ChaosRespawnBonus;
            BombQuestID_Order = __BombQuestID_Order;
            BombQuestID_Chaos = __BombQuestID_Chaos;
            // ScoringFloor = _ScoringFloor;
            IsFirstBlood = __IsFirstBlood;

            capturePointManager.__IsFirstRun = false;


            OdinOpeningCeremonybool2 = __OdinOpeningCeremonybool2;
            //  capturePointManagerClass_script.__IsFirstRun = false;

            if (Game.Config.EnableLogBehaviourTree)
            {
                // Console.WriteLine("bt capturepointloaded");
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool Odin_BT_LEVELSCRIPT_ThresholdAnnouncements()
    {

        if (ThresholdAnnouncements.ThresholdAnnouncements(out uint musiccue,
            TeamscoreChaos,
            TeamscoreOrder))
        {

            if (Game.Config.EnableLogBehaviourTree)
            {
                // Console.WriteLine("ThresholdAnnouncements loaded");
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool Odin_BT_LEVELSCRIPT_EndOfGameCeremony()
    {

        if (Teamwinner == TeamId.TEAM_ORDER)
        {
            if (EndOfGameCeremony.EndOfGameCeremony(

               out int __EndGameState,
                Teamwinner,
                OrderNexusBeam_L1, //  DWORD ??? 
                OrderNexusBeam_L2,
                OrderNexusBeam_L3,
                OrderNexusBeam_L4,
                OrderNexusBeam_R1,
                OrderNexusBeam_R2,
                OrderNexusBeam_R3,
                OrderNexusBeam_R4,
                EndGameState,
                TeamscoreOrder,
                TeamscoreChaos,
                OrderNexusGlow_1,
                OrderNexusGlow_2,
                OrderNexusGlow_3,
                OrderNexusGlow_4,
                OrderNexusCrystal,
                OrderNexusBeamRed_L1,
                OrderNexusBeamRed_L2,
                OrderNexusBeamRed_L3,
                OrderNexusBeamRed_L4,
                OrderNexusBeamRed_R1,
                OrderNexusBeamRed_R2,
                OrderNexusBeamRed_R3,
                OrderNexusBeamRed_R4

               ))
            {
                EndGameState = __EndGameState;
                return true;
            }
            else
            {
                return true;
            }
        }
        if (Teamwinner == TeamId.TEAM_CHAOS)
        {
            if (EndOfGameCeremony.EndOfGameCeremony(

               out int __EndGameState,
                Teamwinner,
                ChaosNexusBeam_L1, //  DWORD ??? 
                ChaosNexusBeam_L2,
                ChaosNexusBeam_L3,
                ChaosNexusBeam_L4,
                ChaosNexusBeam_R1,
                ChaosNexusBeam_R2,
                ChaosNexusBeam_R3,
                ChaosNexusBeam_R4,
                EndGameState,
                TeamscoreOrder,
                TeamscoreChaos,
                ChaosNexusGlow_1,
                ChaosNexusGlow_2,
                ChaosNexusGlow_3,
                ChaosNexusGlow_4,
                ChaosNexusCrystal,
                ChaosNexusBeamRed_L1,
                ChaosNexusBeamRed_L2,
                ChaosNexusBeamRed_L3,
                ChaosNexusBeamRed_L4,
                ChaosNexusBeamRed_R1,
                ChaosNexusBeamRed_R2,
                ChaosNexusBeamRed_R3,
                ChaosNexusBeamRed_R4

               ))
            {
                EndGameState = __EndGameState;
                return true;
            }
            else
            {
                return true;
            }
        }

        return true;
    }
}