using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using System.Collections.Generic;
using System.Numerics;

namespace CoTG.CoTGServer.Scripting.CSharp;

public class bt_OdinManager : CommonAI

{

    public Vector3 CapturePointAPosition;

    public Vector3 CapturePointBPosition;

    public Vector3 CapturePointCPosition;

    public Vector3 CapturePointDPosition;

    public Vector3 CapturePointEPosition;

    public AISquadClass CapturePointASquad;

    public AISquadClass CapturePointBSquad;

    public AISquadClass CapturePointCSquad;

    public AISquadClass CapturePointDSquad;

    public AISquadClass CapturePointESquad;

    public AISquadClass WaitInBaseSquad;

    public AISquadClass KillSquad;

    /*
   public AISquad CapturePointASquad;

   public AISquad CapturePointBSquad;

   public AISquad CapturePointCSquad;

   public AISquad CapturePointDSquad;

   public AISquad CapturePointESquad;

   public AISquad WaitInBaseSquad;

   public AISquad KillSquad;
   */

    public AIMissionClass CapturePointA_mission;

    public AIMissionClass CapturePointB_mission;

    public AIMissionClass CapturePointC_mission;

    public AIMissionClass CapturePointD_mission;

    public AIMissionClass CapturePointE_mission;

    public AIMissionClass WaitInBaseMission;
    /*
    public AIMission CapturePointA_mission;

    public AIMission CapturePointB_mission;

    public AIMission CapturePointC_mission;

    public AIMission CapturePointD_mission;

    public AIMission CapturePointE_mission;

    public AIMission WaitInBaseMission;
    */
    public float MinionPointValue;

    public float ChampionPointValue;

    public float CPPointValue;

    public bool CapturePointsFound;

    public float StrengthEvaluatorRadius;

    public bool Opened;

    public int UpdateLimit;

    public int PreviousEnemyCPCount;

    public int PreviousNeutralCPCount;

    public int PreviousOwnedCPCount;

    public bool IsBeingCapturedA;

    public bool IsBeingCapturedB;

    public bool IsBeingCapturedC;

    public bool IsBeingCapturedD;

    public bool IsBeingCapturedE;

    public float NextUpdateTime;

    public float LastGoldScalingUpdateTime;

    public bool ManuallyForceUpdate;

    public TeamId ReferenceTeam;

    public IEnumerable<AttackableUnit> CapPointAColl;

    public IEnumerable<AttackableUnit> CapPointBColl;

    public IEnumerable<AttackableUnit> CapPointCColl;

    public IEnumerable<AttackableUnit> CapPointDColl;

    public IEnumerable<AttackableUnit> CapPointEColl;

    public AttackableUnit CapturePointA;

    public AttackableUnit CapturePointB;

    public AttackableUnit CapturePointC;

    public AttackableUnit CapturePointD;

    public AttackableUnit CapturePointE;

    public int EnemyCPCount;

    public int OwnedCPCount;

    public int NeutralCPCount;

    public int BotIndex;

    public float GameTime;

    public int Count;

    public TeamId CapturePointTeam;

    public IEnumerable<AttackableUnit> AIEntities;
    public int NeedOffsetA;
    public int NeedOffsetB;
    public int NeedOffsetC;
    public int NeedOffsetD;
    public int NeedOffsetE;
    public float StrengthOffsetA;
    public float StrengthOffsetB;
    public float StrengthOffsetC;
    public float StrengthOffsetD;
    public float StrengthOffsetE;
    public float TopPointScoreModifier;
    public float MidPointScoreModifier;
    public float BasePointScoreModifier;
    public float MaxDistance;
    public float DistanceNormalizer;
    public float StrengthOffset;
    public int CurrentEntityID;
    public int DisconnectAdjustmentEntityID;
    public bool DisconnectAdjustmentNeeded;


    public TeamId EnemyTeam;

    public int ConnectedPlayersOnEnemyTeam;

    public AITask KillTask;

    public AttackableUnit KillTaskTarget;

    public float DistanceToKillTaskTarget;



    public float DistanceToBase;

    public float EntityCurrentHealth;

    public float EntityMaxHealth;

    public float EntityHealthRatio;

    public float EnemyStrengthPointA;

    public float FriendlyStrengthPointA;

    public float EnemyStrengthPointB;

    public float FriendlyStrengthPointB;

    public float EnemyStrengthPointC;

    public float FriendlyStrengthPointC;

    public float EnemyStrengthPointD;

    public float FriendlyStrengthPointD;

    public float EnemyStrengthPointE;

    public float FriendlyStrengthPointE;

    public float PriorityA;

    public float PriorityB;

    public float PriorityC;

    public float PriorityD;

    public float PriorityE;

    public float DistanceA;

    public float DistanceB;

    public float DistanceC;

    public float DistanceD;

    public float DistanceE;

    public float PairScoreA;

    public float PairScoreB;

    public float PairScoreC;

    public float PairScoreD;

    public float PairScoreE;

    public TeamId EntityTeam;

    public float BestScore;

    public int PointID;

    public float DistanceToPoint;

    public float OrderScore;

    public float ChaosScore;

    public float ScoreDiff;
    public float UpdateModifier;

    public float MinUpdateDelay;

    public float MaxUpdateDelay;

    public float temp;



}
