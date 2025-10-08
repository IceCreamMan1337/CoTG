namespace BehaviourTrees;


class CapturePointManagerClass_script : OdinLayout
{
    //at first view is with help of this one we get json  

    //edited because is full of shit 
    public bool CapturePointManager_script(
         /*            out float CapturePointEProgress,
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
         bool __IsFirstRun2
         )
    {
        float _CapturePointEProgress = default;
        float _CapturePointAProgress = default;
        float _CapturePointBProgress = default;
        float _CapturePointCProgress = default;
        float _CapturePointDProgress = default;
        TeamId _CapturePointEOwner = default;
        TeamId _CapturePointAOwner = default;
        TeamId _CapturePointBOwner = default;
        TeamId _CapturePointCOwner = default;
        TeamId _CapturePointDOwner = default;
        float _TeamOrderScore = default;
        float _TeamChaosScore = default;

        var locationIllumination = new LocationIlluminationClass();
        bool result =
                  // Sequence name :Selector

                  // Sequence name :Initialization
                  (
                        __IsFirstRun2 == true &&
// Sequence name :Variable Initialization

/*   GetChampionCollection(
         out ChampionsTotal) &&
   GetWorldLocationByName(
         out OrderSpawnPoint,
         "OrderStartPoint") &&
   GetWorldLocationByName(
         out ChaosSpawnPoint,
         "ChaosStartPoint") &&*/
/* GetWorldLocationByName(
       out GraveyardA,
       "GraveyardA") &&
 GetWorldLocationByName(
       out GraveyardB,
       "GraveyardB") &&
 GetWorldLocationByName(
       out GraveyardC,
       "GraveyardC") &&
 GetWorldLocationByName(
       out GraveyardD,
       "GraveyardD") &&
 GetWorldLocationByName(
       out GraveyardE,
       "GraveyardE") &&
 GetTurret(
       out OrderTurretA, 
       TeamId.TEAM_ORDER, 
       0, 
       2) &&
 GetTurret(
       out ChaosTurretA, 
       TeamId.TEAM_CHAOS, 
       0, 
       2) &&
 GetTurret(
       out OrderTurretB, 
       TeamId.TEAM_ORDER, 
       0, 
       3) &&
 GetTurret(
       out ChaosTurretB, 
       TeamId.TEAM_CHAOS, 
       0, 
       3) &&
 GetTurret(
       out OrderTurretC, 
       TeamId.TEAM_ORDER, 
       0, 
       4) &&
 GetTurret(
       out ChaosTurretC, 
       TeamId.TEAM_CHAOS, 
       0, 
       4) &&
 GetTurret(
       out OrderTurretD, 
       TeamId.TEAM_ORDER, 
       0, 
       5) &&
 GetTurret(
       out ChaosTurretD, 
       TeamId.TEAM_CHAOS, 
       0, 
       5) &&
 GetTurret(
       out ChaosTurretE, 
       TeamId.TEAM_CHAOS, 
       0, 
       6) &&
 GetTurret(
       out OrderTurretE, 
       TeamId.TEAM_ORDER, 
       0, 
       6) &&*/

//todo get this functionnal with debugcircleradius 
/*     SetVarFloat(
           out _CapturePointAProgress, 
           0) &&
     SetVarFloat(
           out _CapturePointBProgress, 
           0) &&
     SetVarFloat(
           out _CapturePointCProgress, 
           0) &&
     SetVarFloat(
           out _CapturePointDProgress, 
           0) &&
     SetVarFloat(
           out _CapturePointEProgress, 
           0) &&
*/
//todo get this functionnal with debugcircleradius 

/*   SetVarUnitTeam(
         out _CapturePointAOwner, 
         TeamId.TEAM_NEUTRAL) &&
   SetVarUnitTeam(
         out _CapturePointBOwner, 
         TeamId.TEAM_NEUTRAL) &&
   SetVarUnitTeam(
         out _CapturePointCOwner, 
         TeamId.TEAM_NEUTRAL) &&
   SetVarUnitTeam(
         out _CapturePointDOwner, 
         TeamId.TEAM_NEUTRAL) &&
   SetVarUnitTeam(
         out _CapturePointEOwner, 
         TeamId.TEAM_NEUTRAL) &&*/
/*  SetVarFloat(
        out _TeamOrderScore, 
        0) &&
  SetVarFloat(
        out _TeamChaosScore, 
        0) &&  /*




/*   GetUnitPosition(
        out CapturePointAPos, 
        OrderTurretA) &&
  GetUnitPosition(
        out CapturePointBPos, 
        OrderTurretB) &&
  GetUnitPosition(
        out CapturePointCPos, 
        OrderTurretC) &&
  GetUnitPosition(
        out CapturePointDPos, 
        OrderTurretD) &&
  GetUnitPosition(
        out CapturePointEPos, 
        OrderTurretE) &&*/
/*     GetGameTime(
           out CurrentGameTime) &&
     SetVarFloat(
           out NextScoreUpdateTime, 
           CurrentGameTime) &&
     SetVarFloat(
           out ScoreUpdatePeriod, 
           1) &&
*/
/* CreateEncounterFromDefinition(
       out WaveEncounterID,
       "SmallWave"

       ) &&  */

//same here 
/*         SetVarFloat(
               out NextWaveSpawnTime, 
               CurrentGameTime) &&
         SetVarFloat(
               out WaveSpawnPeriod, 
               25) &&
         SetVarFloat(
               out CaptureRadius, 
               600) &&
*/



/*  AddMutatorToEncounter(
        WaveEncounterID,
        "MeleeWaveBase",
        "Melee", 
        0) &&
  AddMutatorToEncounter(
        WaveEncounterID,
        "RangedWaveBase",
        "Ranged", 
        0) &&
  AddMutatorToEncounter(
        WaveEncounterID,
        "MeleeWaveProgression",
        "Melee", 
        0) &&
  AddMutatorToEncounter(
        WaveEncounterID,
        "RangedWaveProgression",
        "Ranged", 
        0) &&
  SetVarInt(
        out MutationProgressionIndex, 
        0) &&
  AddMutatorToEncounter(
        WaveEncounterID,
        "CapturePointMutator",
        "Melee,Ranged",
        MutationProgressionIndex) &&*/

//same here 

/*SetVarInt(
out NumOrderPoints, 
0) &&
SetVarInt(
out NumChaosPoints, 
0) &&
SetVarBool(
out CapPointRunA, 
true) &&
SetVarBool(
out CapPointRunB, 
true) &&
SetVarBool(
out CapPointRunC, 
true) &&
SetVarBool(
out CapPointRunD, 
true) &&
SetVarBool(
out CapPointRunE, 
true) &&
SetVarInt(
out PointAGuardId, 
-1) &&
SetVarInt(
out PointBGuardId, 
-1) &&
SetVarInt(
out PointCGuardId, 
-1) &&
SetVarInt(
out PointDGuardId, 
-1) &&
SetVarInt(
out PointEGuardId, 
-1) && */





/*     SetVarInt(
           out RespawnUIA, 
           0) &&
     SetVarInt(
           out RespawnUIB, 
           1) &&
     SetVarInt(
           out RespawnUIC, 
           2) &&
     SetVarInt(
           out RespawnUID, 
           3) &&
     SetVarInt(
           out RespawnUIE, 
           4) &&
     SetVarInt(
           out RespawnUIOrderSpawn, 
           5) &&
     SetVarInt(
           out RespawnUIChaosSpawn, 
           5) &&
      CreateRespawnPoint(
            out GraveyardA_, 
            GraveyardA, 
            "", 
            TeamId.TEAM_NEUTRAL, 
            0) &&
      CreateRespawnPoint(
            out GraveyardB_, 
            GraveyardB, 
            "", 
            TeamId.TEAM_NEUTRAL, 
            1) &&
      CreateRespawnPoint(
            out GraveyardC_, 
            GraveyardC, 
            "", 
            TeamId.TEAM_NEUTRAL, 
            2) &&
      CreateRespawnPoint(
            out GraveyardD_, 
            GraveyardD, 
            "", 
            TeamId.TEAM_NEUTRAL, 
            3) &&
      CreateRespawnPoint(
            out GraveyardE_, 
            GraveyardE, 
            "", 
            TeamId.TEAM_NEUTRAL, 
            4) &&
      CreateRespawnPoint(
            out GraveyardSpawnOrder_, 
            OrderSpawnPoint, 
            "", 
            TeamId.TEAM_ORDER, 
            5) &&
      CreateRespawnPoint(
            out GraveyardSpawnChaos_, 
            ChaosSpawnPoint, 
            "", 
            TeamId.TEAM_CHAOS, 
            6) &&
      SetVarFloat(
            out BankNeutralA, 
            500) &&
      SetVarFloat(
            out BankNeutralB, 
            500) &&
      SetVarFloat(
            out BankNeutralC, 
            500) &&
      SetVarFloat(
            out BankNeutralD, 
            500) &&
      SetVarFloat(
            out BankNeutralE, 
            500) &&
      SetVarUnitTeam(
            out PointAttackerA, 
            TeamId.TEAM_NEUTRAL) &&
      SetVarUnitTeam(
            out PointAttackerB, 
            TeamId.TEAM_NEUTRAL) &&
      SetVarUnitTeam(
            out PointAttackerC, 
            TeamId.TEAM_NEUTRAL) &&
      SetVarUnitTeam(
            out PointAttackerD, 
            TeamId.TEAM_NEUTRAL) &&
      SetVarUnitTeam(
            out PointAttackerE, 
            TeamId.TEAM_NEUTRAL) &&
      SetVarFloat(
            out BankTimerA, 
            CurrentGameTime) &&
      SetVarFloat(
            out BankTimerB, 
            CurrentGameTime) &&
      SetVarFloat(
            out BankTimerC, 
            CurrentGameTime) &&
      SetVarFloat(
            out BankTimerD, 
            CurrentGameTime) &&
      SetVarFloat(
            out BankTimerE, 
            CurrentGameTime) && */

//same here 
/*   SetVarFloat(
         out FullCaptureValueA, 
         0) &&
   SetVarFloat(
         out FullCaptureValueB, 
         0) &&
   SetVarFloat(
         out FullCaptureValueC, 
         0) &&
   SetVarFloat(
         out FullCaptureValueD, 
         0) &&
   SetVarFloat(
         out FullCaptureValueE, 
         0) &&
   // Sequence name :Sequence
   (   */
//edited the location.dat because doesn't exist __NAV_R01 to __NAV_R041
GetWorldLocationByName(out PerceptionLocation1, "__NAV_R01") &&
GetWorldLocationByName(out PerceptionLocation2, "__NAV_R02") &&
GetWorldLocationByName(out PerceptionLocation3, "__NAV_R03") &&
GetWorldLocationByName(out PerceptionLocation4, "__NAV_R04") &&
GetWorldLocationByName(out PerceptionLocation5, "__NAV_R05") &&
GetWorldLocationByName(out PerceptionLocation6, "__NAV_R06") &&
GetWorldLocationByName(out PerceptionLocation7, "__NAV_R07") &&
GetWorldLocationByName(out PerceptionLocation8, "__NAV_R08") &&
GetWorldLocationByName(out PerceptionLocation9, "__NAV_R09") &&
GetWorldLocationByName(out PerceptionLocation10, "__NAV_R010") &&
GetWorldLocationByName(out PerceptionLocation11, "__NAV_R011") &&
GetWorldLocationByName(out PerceptionLocation12, "__NAV_R012") &&
GetWorldLocationByName(out PerceptionLocation13, "__NAV_R013") &&
GetWorldLocationByName(out PerceptionLocation14, "__NAV_R014") &&
GetWorldLocationByName(out PerceptionLocation15, "__NAV_R015") &&
GetWorldLocationByName(out PerceptionLocation16, "__NAV_R016") &&
GetWorldLocationByName(out PerceptionLocation17, "__NAV_R017") &&
GetWorldLocationByName(out PerceptionLocation18, "__NAV_R018") &&
GetWorldLocationByName(out PerceptionLocation19, "__NAV_R019") &&
GetWorldLocationByName(out PerceptionLocation20, "__NAV_R020") &&
GetWorldLocationByName(out PerceptionLocation21, "__NAV_R021") &&
GetWorldLocationByName(out PerceptionLocation22, "__NAV_R022") &&
GetWorldLocationByName(out PerceptionLocation23, "__NAV_R023") &&
GetWorldLocationByName(out PerceptionLocation24, "__NAV_R024") &&
GetWorldLocationByName(out PerceptionLocation25, "__NAV_R025") &&
GetWorldLocationByName(out PerceptionLocation26, "__NAV_R026") &&
GetWorldLocationByName(out PerceptionLocation27, "__NAV_R027") &&
GetWorldLocationByName(out PerceptionLocation28, "__NAV_R028") &&
GetWorldLocationByName(out PerceptionLocation29, "__NAV_R029") &&
GetWorldLocationByName(out PerceptionLocation30, "__NAV_R030") &&
GetWorldLocationByName(out PerceptionLocation31, "__NAV_R031") &&
GetWorldLocationByName(out PerceptionLocation32, "__NAV_R032") &&
GetWorldLocationByName(out PerceptionLocation33, "__NAV_R033") &&
GetWorldLocationByName(out PerceptionLocation34, "__NAV_R034") &&
GetWorldLocationByName(out PerceptionLocation35, "__NAV_R035") &&
GetWorldLocationByName(out PerceptionLocation36, "__NAV_R036") &&
GetWorldLocationByName(out PerceptionLocation37, "__NAV_R037") &&
GetWorldLocationByName(out PerceptionLocation38, "__NAV_R038") &&
GetWorldLocationByName(out PerceptionLocation39, "__NAV_R039") &&
GetWorldLocationByName(out PerceptionLocation40, "__NAV_R040") &&
GetWorldLocationByName(out PerceptionLocation41, "__NAV_R041")

                               &&
                                  // Sequence name :Sequence

                                  locationIllumination.LocationIllumination(PerceptionLocation1) &&
locationIllumination.LocationIllumination(PerceptionLocation2) &&
locationIllumination.LocationIllumination(PerceptionLocation3) &&
locationIllumination.LocationIllumination(PerceptionLocation4) &&
locationIllumination.LocationIllumination(PerceptionLocation5) &&
locationIllumination.LocationIllumination(PerceptionLocation6) &&
locationIllumination.LocationIllumination(PerceptionLocation7) &&
locationIllumination.LocationIllumination(PerceptionLocation8) &&
locationIllumination.LocationIllumination(PerceptionLocation9) &&
locationIllumination.LocationIllumination(PerceptionLocation10) &&
locationIllumination.LocationIllumination(PerceptionLocation11) &&
locationIllumination.LocationIllumination(PerceptionLocation12) &&
locationIllumination.LocationIllumination(PerceptionLocation13) &&
locationIllumination.LocationIllumination(PerceptionLocation14) &&
locationIllumination.LocationIllumination(PerceptionLocation15) &&
locationIllumination.LocationIllumination(PerceptionLocation16) &&
locationIllumination.LocationIllumination(PerceptionLocation17) &&
locationIllumination.LocationIllumination(PerceptionLocation18) &&
locationIllumination.LocationIllumination(PerceptionLocation19) &&
locationIllumination.LocationIllumination(PerceptionLocation20) &&
locationIllumination.LocationIllumination(PerceptionLocation21) &&
locationIllumination.LocationIllumination(PerceptionLocation22) &&
locationIllumination.LocationIllumination(PerceptionLocation23) &&
locationIllumination.LocationIllumination(PerceptionLocation24) &&
locationIllumination.LocationIllumination(PerceptionLocation25) &&
locationIllumination.LocationIllumination(PerceptionLocation26) &&
locationIllumination.LocationIllumination(PerceptionLocation27) &&
locationIllumination.LocationIllumination(PerceptionLocation28) &&
locationIllumination.LocationIllumination(PerceptionLocation29) &&
locationIllumination.LocationIllumination(PerceptionLocation30) &&
locationIllumination.LocationIllumination(PerceptionLocation31) &&
locationIllumination.LocationIllumination(PerceptionLocation32) &&
locationIllumination.LocationIllumination(PerceptionLocation33) &&
locationIllumination.LocationIllumination(PerceptionLocation34) &&
locationIllumination.LocationIllumination(PerceptionLocation35) &&
locationIllumination.LocationIllumination(PerceptionLocation36) &&
locationIllumination.LocationIllumination(PerceptionLocation37) &&
locationIllumination.LocationIllumination(PerceptionLocation38) &&
locationIllumination.LocationIllumination(PerceptionLocation39) &&
locationIllumination.LocationIllumination(PerceptionLocation40) &&
locationIllumination.LocationIllumination(PerceptionLocation41)


                  /*   &&

                   SpawnAttackableLevelProp(
                        CapturePointAPos,
                        "GolemODIN")  */
                  // ) &&
                  // Sequence name :Logical Initialization
                  //(
                  //personnaly i doesn't call that "logical" 
                  /*   SetUnitRendered(
                           OrderTurretA, 
                           false) &&
                     SetUnitRendered(
                           ChaosTurretA, 
                           false) &&
                     SetUnitRendered(
                           OrderTurretB, 
                           false) &&
                     SetUnitRendered(
                           ChaosTurretB, 
                           false) &&
                     SetUnitRendered(
                           OrderTurretC, 
                           false) &&
                     SetUnitRendered(
                           ChaosTurretC, 
                           false) &&
                     SetUnitRendered(
                           OrderTurretD, 
                           false) &&
                     SetUnitRendered(
                           ChaosTurretD, 
                           false) &&
                     SetUnitRendered(
                           OrderTurretE, 
                           false) &&
                     SetUnitRendered(
                           ChaosTurretE, 
                           false) &&
                     IncPermanentPercentBubbleRadiusMod(
                           OrderTurretE, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           ChaosTurretE, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           OrderTurretA, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           ChaosTurretA, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           OrderTurretB, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           ChaosTurretB, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           OrderTurretC, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           ChaosTurretC, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           OrderTurretD, 
                           -1) &&
                     IncPermanentPercentBubbleRadiusMod(
                           ChaosTurretD, 
                           -1) &&
                     SetVarFloat(
                           out OrderDebugRadiusA, 
                           0) &&
                     SetVarFloat(
                           out OrderDebugRadiusB, 
                           0) &&
                     SetVarFloat(
                           out OrderDebugRadiusC, 
                           0) &&
                     SetVarFloat(
                           out OrderDebugRadiusD, 
                           0) &&
                     SetVarFloat(
                           out OrderDebugRadiusE, 
                           0) &&
                     SetVarFloat(
                           out ChaosDebugRadiusA, 
                           0) &&
                     SetVarFloat(
                           out ChaosDebugRadiusB, 
                           0) &&
                     SetVarFloat(
                           out ChaosDebugRadiusC, 
                           0) &&
                     SetVarFloat(
                           out ChaosDebugRadiusD, 
                           0) &&
                     SetVarFloat(
                           out ChaosDebugRadiusE, 
                           0) && */

                  //same here 
                  /*MakeColor(
                              out CaptureRadiusColor, 
                              180, 
                              180, 
                              180, 
                              60) &&
                        AddDebugCircle(
                              out CapDebugCircleIdA, 
                              null, 
                              CapturePointAPos, 
                              CaptureRadius, 
                              CaptureRadiusColor) &&
                        AddDebugCircle(
                              out CapDebugCircleIdB,
                              null,
                              CapturePointBPos, 
                              CaptureRadius, 
                              CaptureRadiusColor) &&
                        AddDebugCircle(
                              out CapDebugCircleIdC,
                              null,
                              CapturePointCPos, 
                              CaptureRadius, 
                              CaptureRadiusColor) &&
                        AddDebugCircle(
                              out CapDebugCircleIdD,
                              null,
                              CapturePointDPos, 
                              CaptureRadius, 
                              CaptureRadiusColor) &&
                        AddDebugCircle(
                              out CapDebugCircleIdE,
                              null,
                              CapturePointEPos, 
                              CaptureRadius, 
                              CaptureRadiusColor) &&
                        MakeColor(
                              out OrderColor, 
                              75, 
                              75, 
                              225, 
                              60) &&
                        AddDebugCircle(
                              out OrderDebugCircleA,
                              null,
                              CapturePointAPos, 
                              OrderDebugRadiusA, 
                              OrderColor) &&
                        AddDebugCircle(
                              out OrderDebugCircleB,
                              null,
                              CapturePointBPos, 
                              OrderDebugRadiusB, 
                              OrderColor) &&
                        AddDebugCircle(
                              out OrderDebugCircleC,
                              null,
                              CapturePointCPos, 
                              OrderDebugRadiusC, 
                              OrderColor) &&
                        AddDebugCircle(
                              out OrderDebugCircleD,
                              null,
                              CapturePointDPos, 
                              OrderDebugRadiusD, 
                              OrderColor) &&
                        AddDebugCircle(
                              out OrderDebugCircleE,
                              null,
                              CapturePointEPos, 
                              OrderDebugRadiusE, 
                              OrderColor) &&
                        MakeColor(
                              out ChaosColor, 
                              200, 
                              30, 
                              230, 
                              60) &&
                  */
                  /* Announcement_OnStartGame(
                         ) && */


                  /*
                        AddDebugCircle(
                              out ChaosDebugCircleA,
                              null,
                              CapturePointAPos, 
                              ChaosDebugRadiusA, 
                              ChaosColor) &&
                        AddDebugCircle(
                              out ChaosDebugCircleB,
                              null,
                              CapturePointBPos, 
                              ChaosDebugRadiusB, 
                              ChaosColor) &&
                        AddDebugCircle(
                              out ChaosDebugCircleC,
                              null,
                              CapturePointCPos, 
                              ChaosDebugRadiusC, 
                              ChaosColor) &&
                        AddDebugCircle(
                              out ChaosDebugCircleD,
                              null,
                              CapturePointDPos, 
                              ChaosDebugRadiusD, 
                              ChaosColor) &&
                        AddDebugCircle(
                              out ChaosDebugCircleE,
                              null,
                              CapturePointEPos, 
                              ChaosDebugRadiusE, 
                              ChaosColor)
                   */
                  //)

                  )
                  || __IsFirstRun2 == false

            ;

        /*  CapturePointEProgress = _CapturePointEProgress;
          CapturePointAProgress = _CapturePointAProgress;
          CapturePointBProgress = _CapturePointBProgress;
          CapturePointCProgress = _CapturePointCProgress;
          CapturePointDProgress = _CapturePointDProgress;
          CapturePointEOwner = _CapturePointEOwner;
          CapturePointAOwner = _CapturePointAOwner;
          CapturePointBOwner = _CapturePointBOwner;
          CapturePointCOwner = _CapturePointCOwner;
          CapturePointDOwner = _CapturePointDOwner;
          TeamOrderScore = _TeamOrderScore;
          TeamChaosScore = _TeamChaosScore;*/

        return result;
    }
}

