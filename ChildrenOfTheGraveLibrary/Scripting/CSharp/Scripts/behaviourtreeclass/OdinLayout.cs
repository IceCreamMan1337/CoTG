using ChildrenOfTheGraveEnumNetwork.Content;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using System.Collections.Generic;
using System.Numerics;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class OdinLayout : AImission_bt
{

    public bool __IsFirstRun;

    public bool GuardianInit;

    public bool Initialization;

    public int WaveEncounterID;

    public int MutationProgressionIndex;

    public float MinionSpawnRate_Seconds;

    public Vector3 MinionSpawnPoint_A1;

    public Vector3 MinionSpawnPoint_A2;

    public Vector3 MinionSpawnPoint_B1;

    public Vector3 MinionSpawnPoint_B2;

    public Vector3 MinionSpawnPoint_C1;

    public Vector3 MinionSpawnPoint_C2;

    public Vector3 MinionSpawnPoint_D1;

    public Vector3 MinionSpawnPoint_D2;

    public Vector3 MinionSpawnPoint_E1;

    public Vector3 MinionSpawnPoint_E2;

    public int MinionSpawnPortalParticleEncounterID;

    public AttackableUnit OrderShrineTurret;

    public AttackableUnit ChaosShrineTurret;

    public float Scoring_PreviousUpdateTime;

    public float ScoringFloor;

    public bool OverTimeLimitWarning;

    public int RelicSquad_A;
    public int RelicSquad_B;
    public int RelicSquad_C;
    public float RelicSpawnTime_A;
    public float RelicSpawnTime_B;
    public float RelicSpawnTime_C;
    public float RelicSpawnTime_D;
    public float RelicSpawnTime_E;
    public int RelicSquad_D;
    public int RelicSquad_E;
    public int RelicEncounterShield;
    public float RelicSpawnTime_F;
    public float RelicSpawnTime_G;
    public float RelicSpawnTime_H;
    public int RelicSquad_F;
    public int RelicSquad_G;
    public int RelicSquad_H;
    public float RelicSpawnTime_I;
    public float RelicSpawnTime_J;
    public int RelicSquad_I;
    public int RelicSquad_J;
    public Vector3 RelicPositionA;
    public Vector3 RelicPositionB;
    public Vector3 RelicPositionC;
    public Vector3 RelicPositionD;
    public Vector3 RelicPositionE;
    public Vector3 RelicPositionF;
    public Vector3 RelicPositionG;
    public Vector3 RelicPositionI;
    public Vector3 RelicPositionJ;
    public Vector3 RelicPositionH;

    public float CenterRelicSpawnTime_A;
    public float CenterRelicSpawnTime_B;
    public int CenterRelicSquad_A;
    public int CenterRelicSquad_B;
    public int RelicEncounterCenter;
    public int RelicEncounterCenter2;
    public Vector3 CenterRelicPositionB;
    public Vector3 CenterRelicPositionA;
    public bool CenterRelicParticleAttached1;
    public bool CenterRelicParticleAttached2;

    public int ChaosRespawnMutator;
    public int OrderRespawnMutator;
    public int RespawnWindowAdjustment_Order;
    public int RespawnWindowAdjustment_Chaos;

    public Vector3 MinionGraveyardA;
    public Vector3 MinionGraveyardB;
    public Vector3 MinionGraveyardC;
    public Vector3 MinionGraveyardD;
    public Vector3 MinionGraveyardE;
    public int MinionGraveyardPortalEncounter;
    public int MinionGraveyardPortalID_A;
    public int MinionGraveyardPortalID_B;
    public int MinionGraveyardPortalID_C;
    public int MinionGraveyardPortalID_D;
    public int MinionGraveyardPortalID_E;
    public float MinionGraveyardPortalStartTime_A;
    public float MinionGraveyardPortalStartTime_B;
    public float MinionGraveyardPortalStartTime_C;
    public float MinionGraveyardPortalStartTime_D;
    public float MinionGraveyardPortalStartTime_E;

    public float PreviousQuestCompleteTime;
    public int QuestID_Order;
    public int QuestID_Chaos;
    public int QuestObjective_Order;
    public int QuestObjective_Chaos;
    public int QuestGiver_EncounterID;
    public int QuestIconEncounterID;
    public int QuestIconSquadID_Order;
    public int QuestIconSquadID_Chaos;
    public int QuestIconEncounterID2;
    public int QuestRewardsID_Order;
    public int QuestRewardsID_Chaos;

    public float GameEndTime;

    public bool EndCeremonyBool;

    public int EndOfGameState;

    public TeamId FinalWinTeam;

    public bool NexusParticleEnabledOrder;
    public bool NexusParticleEnabledChaos;
    public uint NexusParticleOrder_1;
    public uint NexusParticleOrder_2;
    public uint NexusParticleChaos_1;
    public uint NexusParticleChaos_2;
    public GameObject SoGLevelProp_Order;
    public GameObject SoGLevelProp_Chaos;

    public AttackableUnit Guardian0;

    public AttackableUnit Guardian1;

    public AttackableUnit Guardian2;

    public AttackableUnit Guardian3;

    public AttackableUnit Guardian4;

    public GameObject Bird1;

    public GameObject Bird2;

    public GameObject Bird3;

    public GameObject Saw2;

    public Vector3 ReferencePosition;


    public IEnumerable<AttackableUnit> PossibleAnnouncers;

    public int PossibleAnnouncerCount;

    public bool AnnouncementSaid;

    public float DebugMultiplierA;

    public float OrderDebugRadiusA;

    public float AbsRadiusA;

    public float ChaosDebugRadiusA;

    public IEnumerable<Champion> ChampionCollection;

    public Vector3 NexusDeathPosition;

    public GameObject StairsProp;

    public GameObject CrystalProp;

    public uint OrderPerc;

    public uint ChaosPerc;

    public float EoGStartTime;

    public float TimePassed;

    public bool FoundGuardian;

    public IEnumerable<AttackableUnit> MinionsInArea;

    public string MinionName;

    public int OdinCapturePointNeutralEncounter;

    public int CapturePointA_SquadID;

    public int CapturePointB_SquadID;

    public int CapturePointC_SquadID;

    public int CapturePointD_SquadID;

    public int CapturePointE_SquadID;

    public Vector3 CapturePointAPos;
    public Vector3 CapturePointBPos;
    public Vector3 CapturePointCPos;
    public Vector3 CapturePointDPos;
    public Vector3 CapturePointEPos;

    public bool DidInitParticles;

    public AttackableUnit Guardian;


    public AttackableUnit ShrineTurret;

    public IEnumerable<AttackableUnit> LocalChampionCollection;

    public int LocalChampCount;

    public int ExtraChampCount;

    public int TotalGold;

    public int ExtraGold;

    public float IndividualGold;

    public float Radius;

    public bool AwardCapturePoint;

    public Vector3 GuardianPosition;

    public TeamId ToSet;

    public bool ScoreChanged;


    public float PercentToHealTo;

    public float HalfHealth;

    public float AmountToHeal;

    public int CCDelta;

    public IEnumerable<AttackableUnit> AllMinions;

    public string SquadNameToIgnoreChaos_A;

    public string SquadNameToIgnoreChaos_B;

    public string SquadNameToIgnoreOrder_A;

    public string SquadNameToIgnoreOrder_B;

    public string SquadName;

    public TeamId MinionTeam;

    public float TimeInFuture;

    public int NumChaosPoints;

    public int NumOrderPoints;

    public float NegativeSpawnRate;

    public int PointDiff;

    public int CapturePointMutator;

    public string SquadName_Team;

    public int SquadId3;

    public bool OrderLosing;

    public bool ChaosLosing;

    public TeamId GuardianTeam;

    public IEnumerable<AttackableUnit> Champions;

    public TeamId ObjectiveOwner_Order;

    public AttackableUnit ObjectiveGuardian;

    public int SquadId;

    public TeamId ObjectiveOwner_Chaos;

    public float CalculatedValue_PointA_Order;

    public float CalculatedValue_PointB_Order;

    public float CalculatedValue_PointC_Order;

    public float CalculatedValue_PointD_Order;

    public float CalculatedValue_PointE_Order;


    public float CalculatedValue_PointA_Chaos;

    public float CalculatedValue_PointB_Chaos;

    public float CalculatedValue_PointC_Chaos;

    public float CalculatedValue_PointD_Chaos;

    public float CalculatedValue_PointE_Chaos;

    public float CombinedValueA;

    public float CombinedValueB;

    public float CombinedValueC;

    public float CombinedValueD;

    public float CombinedValueE;

    public float TempOrderScore;

    public float TempChaosScore;

    public float TempObjectiveScore;

    public int TotalOrderPoint;

    public int TotalChaosPoint;

    public int TotalPointDiff;

    public float ScoreChange;

    public string CapturePointName;

    public string QuestText;

    public string CapturePointNameOrder;

    public string CapturePointNameChaos;

    public int NumConnectedPlayers_Order;

    public int NumConnectedPlayers_Chaos;

    public float PointsDiff;

    public float AbsPointDiff;

    public float Modifier;

    public int NewModifier;

    public int NegativeModifier;

    public int ModifierDiff;
    public float ModifierToSet;

    public int Cur_NH_RespawnMod_Order;
    public int Cur_NH_RespawnMod_Chaos;
    public int Cur_NH_WindowMod_x10_Order;
    public int Cur_NH_WindowMod_x10_Chaos;
    public int TotalRespawnModOrder;
    public int TotalRespawnModChaos;

    public int Cur_DC_RespawnMod_Order;
    public int Cur_DC_RespawnMod_Chaos;
    public int Cur_DC_WindowMod_x10_Order;
    public int Cur_DC_WindowMod_x10_Chaos;
    public int TotalWindowModChaos;
    public int TotalWindowModOrder;

    public TeamId KillerTeam;

    public float LaterScoreValue;

    public IEnumerable<Champion> ChampCollection;

    public Vector3 ChaosDeathLoc;

    public Vector3 OrderDeathLoc;

    public uint OrderPerc2;

    public uint ChaosPerc2;

    public uint OrderWinParticle;

    public GameObject OrderStairsProp;

    public uint NexusParticle_Order;

    public int TotalOrderPoints;

    public int TotalChaosPoints;

    public bool GameOver;

    public bool HitOrderThreshold1;

    public bool HitOrderThreshold2;

    public bool HitOrderThreshold3;

    public bool HitOrderThreshold4;

    public bool HitChaosThreshold1;

    public bool HitChaosThreshold2;

    public bool HitChaosThreshold3;

    public bool HitChaosThreshold4;

    public bool HitEndOfGame;

    public IEnumerable<Champion> HeroCollection;

    public TeamId ChampTeam;

    public IEnumerable<AttackableUnit> ToGivePoints;

    public IEnumerable<AttackableUnit> UnitsToGivePoints;

    public float TotalPersonalScore;

    public IEnumerable<Champion> AllChamps;

    public float ChaosValue;

    public float OrderValue;

    public IEnumerable<Champion> Champions_;

    public AttackableUnit ChampWhoInterrupted;

    public TeamId CapturePointOwner;

    public TeamId CapturePoint_PreviousOwner;

    public TeamId InterrupterTeam;

    public int EnemyTeamCount;

    public UnitType KillerType;

    public TeamId DeadChampionTeam;

    public int ClosestCapturePointIndex;

    public AttackableUnit ClosestGuardian;

    public int CashedCapturePointIndex;

    public float TotalScore;

    public int NumberOfKillers;

    public float LastStandScore;

    public float DistanceAssisterGuardian;

    public int KillingSpreeNumber;

    public string ToPrint;

    public AttackableUnit Killers_killer;

    public Vector3 TargetLocation;

    public UnitType DeadUnitType;

    public UnitType KillerUnitType;

    public Vector3 KillerPosition;

    public IEnumerable<AttackableUnit> ChampsToGivePoints;

    public Vector3 Shrine1Position;

    public int SpeedShrineID;

    public Vector3 Shrine2Position;

    public Vector3 Shrine5Position;

    public int Shrine1SquadID;

    public float UpperBound;

    public float NewCenterRelicSpawnTime;

    public IEnumerable<AttackableUnit> UnitsToSearch;

    public string UnitSquadName;

    public float RandomTimeDiff;

    public Color ChaosColor;

    public Color OrderColor;

    public float NewRelicSpawnTime;

    public float SpawnBarrierRadius;

    public Vector3 OrderStartPoint;

    public Vector3 ChaosStartPoint;

    public TeamId LocalHeroTeam;

    public float ThirtySecondsAnnouncementTime;

    public float WelcomeAnnouncementTime;

    public GameObject ChaosStairsProp;

    public bool PlayAnim_1;

    public bool PlayAnim_2;

    public bool PlayAnim_3;

    public bool PlayAnim_4;

    public bool PlayAnim_5;

    public bool PlayAnim_6;

    public bool ParticleEffect1;

    public bool ParticleEffect2;

    public bool ParticleEffect3;

    public bool ParticleEffect4;

    public bool AnnouncementGiven;

    public bool WelcomeAnnouncementGiven;

    public int OdinOpeningBarrierEncounter;

    public uint EffectID;

    public uint OtherTeamEffectID;

    public bool QuestActive;

    public int TipIndex;

    public int TipID;

    public bool IsValid_AB;
    public bool IsValid_BC;
    public bool IsValid_CD;
    public bool IsValid_DE;
    public bool IsValid_EA;
    public bool IsValid_A;
    public bool IsValid_B;
    public bool IsValid_C;
    public bool IsValid_D;
    public bool IsValid_E;
    public float ValueAB;
    public float ValueBC;
    public float ValueCD;
    public float ValueDE;
    public float ValueEA;

    public int NumAllies0;
    public int NumEnemies0;
    public int NumAllies1;
    public int NumEnemies1;
    public int NumAllies2;
    public int NumEnemies2;
    public int NumAllies3;
    public int NumEnemies3;
    public int NumAllies4;
    public int NumEnemies4;

    public float ScoreDifference;

    public bool Function_MostPositive;

    public bool Function_MostNegative;

    public bool Function_LeastPositive;

    public bool Function_LeastNegative;
    public float CurrentBestScore;

    public int CurrentBestObjective_A;

    public int CurrentBestObjective_B;

    public TeamId ObjectiveA_Owner;


    public int TotalAssist;


    /// <summary>
    /// starting of scripting bt , do another class ? 
    /// </summary>



    public IEnumerable<Champion> ChampionsTotal;

    public Vector3 OrderSpawnPoint;

    public Vector3 ChaosSpawnPoint;

    public Vector3 GraveyardA;

    public Vector3 GraveyardB;

    public Vector3 GraveyardC;

    public Vector3 GraveyardD;

    public Vector3 GraveyardE;

    public AttackableUnit OrderTurretA;

    public AttackableUnit ChaosTurretA;

    public AttackableUnit OrderTurretB;


    public AttackableUnit ChaosTurretB;

    public AttackableUnit OrderTurretC;

    public AttackableUnit ChaosTurretC;

    public AttackableUnit OrderTurretD;

    public AttackableUnit ChaosTurretD;

    public AttackableUnit ChaosTurretE;
    public AttackableUnit OrderTurretE;

    public float NextScoreUpdateTime;

    public float ScoreUpdatePeriod;

    public float NextWaveSpawnTime;

    public float WaveSpawnPeriod;

    public float CaptureRadius;

    public bool CapPointRunA;

    public bool CapPointRunB;

    public bool CapPointRunC;

    public bool CapPointRunD;

    public bool CapPointRunE;

    public int PointAGuardId;

    public int PointBGuardId;

    public int PointCGuardId;

    public int PointDGuardId;

    public int PointEGuardId;

    public int RespawnUIA;

    public int RespawnUIB;

    public int RespawnUIC;


    public int RespawnUID;

    public int RespawnUIE;


    public int RespawnUIOrderSpawn;

    public int RespawnUIChaosSpawn;

    public int GraveyardA_;

    public int GraveyardB_;

    public int GraveyardC_;


    public int GraveyardD_;

    public int GraveyardE_;

    public int GraveyardSpawnOrder_;

    public int GraveyardSpawnChaos_;


    public float BankNeutralA;

    public float BankNeutralB;

    public float BankNeutralC;

    public float BankNeutralD;

    public float BankNeutralE;

    public TeamId PointAttackerA;

    public TeamId PointAttackerB;

    public TeamId PointAttackerC;

    public TeamId PointAttackerD;

    public TeamId PointAttackerE;


    public float BankTimerA;

    public float BankTimerB;

    public float BankTimerC;

    public float BankTimerD;


    public float BankTimerE;



    public float FullCaptureValueA;

    public float FullCaptureValueB;

    public float FullCaptureValueC;

    public float FullCaptureValueD;


    public float FullCaptureValueE;


    public Vector3 PerceptionLocation1;
    public Vector3 PerceptionLocation2;
    public Vector3 PerceptionLocation3;
    public Vector3 PerceptionLocation4;
    public Vector3 PerceptionLocation5;
    public Vector3 PerceptionLocation6;
    public Vector3 PerceptionLocation7;
    public Vector3 PerceptionLocation8;
    public Vector3 PerceptionLocation9;
    public Vector3 PerceptionLocation10;
    public Vector3 PerceptionLocation11;
    public Vector3 PerceptionLocation12;
    public Vector3 PerceptionLocation13;
    public Vector3 PerceptionLocation14;
    public Vector3 PerceptionLocation15;
    public Vector3 PerceptionLocation16;
    public Vector3 PerceptionLocation17;
    public Vector3 PerceptionLocation18;
    public Vector3 PerceptionLocation19;
    public Vector3 PerceptionLocation20;
    public Vector3 PerceptionLocation21;
    public Vector3 PerceptionLocation22;
    public Vector3 PerceptionLocation23;
    public Vector3 PerceptionLocation24;
    public Vector3 PerceptionLocation25;
    public Vector3 PerceptionLocation26;
    public Vector3 PerceptionLocation27;
    public Vector3 PerceptionLocation28;
    public Vector3 PerceptionLocation29;
    public Vector3 PerceptionLocation30;
    public Vector3 PerceptionLocation31;
    public Vector3 PerceptionLocation32;
    public Vector3 PerceptionLocation33;
    public Vector3 PerceptionLocation34;
    public Vector3 PerceptionLocation35;
    public Vector3 PerceptionLocation36;
    public Vector3 PerceptionLocation37;
    public Vector3 PerceptionLocation38;
    public Vector3 PerceptionLocation39;
    public Vector3 PerceptionLocation40;
    public Vector3 PerceptionLocation41;

    public float OrderDebugRadiusB;

    public float OrderDebugRadiusC;

    public float OrderDebugRadiusD;

    public float OrderDebugRadiusE;

    public float ChaosDebugRadiusB;

    public float ChaosDebugRadiusC;

    public float ChaosDebugRadiusD;

    public float ChaosDebugRadiusE;

    public Color CaptureRadiusColor;

    public int CapDebugCircleIdA;

    public int CapDebugCircleIdB;

    public int CapDebugCircleIdC;

    public int CapDebugCircleIdD;

    public int CapDebugCircleIdE;

    public int OrderDebugCircleA;

    public int OrderDebugCircleB;

    public int OrderDebugCircleC;

    public int OrderDebugCircleD;

    public int OrderDebugCircleE;

    public int ChaosDebugCircleA;

    public int ChaosDebugCircleB;

    public int ChaosDebugCircleC;

    public int ChaosDebugCircleD;

    public int ChaosDebugCircleE;

    public TeamId Test;

    public int ExtraAttackers;

    public float BaseCaptureContribution;

    public float ExtraCaptureContributionPerPlayer;


    public float ExtraContribution;

    public float NormalizedCaptureRate;

    public string DebugString;

    public int intneverused;

    public AttackableUnit CentralID;

    public float RewardRadius;

    public float GoldReward;

    public IEnumerable<AttackableUnit> CollectionChamp;

    public float TwoCountModifier;

    public IEnumerable<AttackableUnit> ChampionCount;

    public int ChampionCollectionCount;

    public float ChampCountModifier;

    public float FullCapGoldToGive;

    public TeamId RewardsTeam;
    public float PreMultiplier;

    public float Multiplier;

    public float NeutralGoldReward;

    public float NeutralGoldToGive;

    public float OnePointRate;

    public float TwoPointRate;

    public float ThreePointRate;

    public float FourPointRate;

    public float FivePointRate;

    public int ChampionCountOrder;

    public int ChampionCountChaos;

    public float MinionCaptureRadius;

    public float OrderRegenRate;

    public float ChaosRegenRate;

    public float OrderMinionCaptureRate;

    public float ChaosMinionCaptureRate;

    public float CaptureRate;

    public int AttackerCount;

    public TeamId NewPointOwner;

    public TeamId AttackingTeam;

    public float NormalizedRate;

    public float neverusedfloat;

    public IEnumerable<AttackableUnit> LocalMinionCollection;

    public IEnumerable<AttackableUnit> FriendlyMinionCollection;

    public TeamId LocalMinionTeam;

    public int FriendlyMinionCollectionCount;

    public int FriendlyMinionsFromOtherPointsCount;

    public int MinionCollectionCount;

    public IEnumerable<AttackableUnit> ChaosMinionCollection;

    public IEnumerable<AttackableUnit> OrderMinionCollection;

    public bool NonWaveMinionsPresent;

    public int DeadDefendingMinionCount;

    public float TotalGoldToSplit;

    public float IndividualBounty;

    public int AttackingChampionCount;

    public Vector3 vectornotused;

    public TeamId CurrentPointOwner;

    public AttackableUnit OrderTurret;

    public Vector3 CapturePosition;

    public float CaptureProgress;

    public TeamId LocalChampionTeam;

    public uint BubbleID;

    public Color RedColor;

    public int DebugCircleId;

    public int OrderDebugCircleId;

    public int ShrineEncounterID;

    public int Shrine2SquadID;

    public IEnumerable<Champion> ChampionListing;

    public TeamId RespawnTeam;

    public Vector3 SpawnPointAlpha;

    public int waveEncounterId;

    public AttackableUnit Player;

    public int Lane0Quest;
    public int Lane1Quest;
    public int Lane2Quest;
    public int Lane0QuestReverse;
    public int Lane1QuestReverse;
    public int Lane2QuestReverse;

    public int EncounterId;

    public int squadID;

    public IEnumerable<AttackableUnit> FriendlyChamps;
    public IEnumerable<AttackableUnit> EnemyChamps;

    public int EnemyChampCount;

    public TeamId TargetTeam;

    public float AttackRange;


    public AttackableUnit Self;

    public Vector3 AssistPosition;

    public TeamId SelfTeam;

    public float AcquisitionRange;

    public float AttackRadius;

    public float DefenseRadius;

    public Vector3 SelfPosition;

    public int FriendlyChampCount;


    public bool LostAggro;

    public AttackableUnit AggroTarget;

    public UnitType AggroTargetType;

    public Vector3 AggroPosition;

    public IEnumerable<AttackableUnit> EnemyUnitsNearSelf;

    public int EnemyCount;

    public bool TargetFound;

    public AttackableUnit CurrentClosestTarget;

    public float Spell0Cooldown;

    public int Ability1Level;

    public float Spell1Cooldown;

    public PrimaryAbilityResourceType PAR_Type;

    public float Current_PAR;

    public float PAR_Cost;

    public int AbilityLevel2;

    public float Spell2Cooldown;

    public int AbilityLevel3;

    public float Spell3Cooldown;

    public float CastRange;

    public float DistanceBetweenSelfAndTarget;

    public int Skillpoints;

    public int Ability0Level;

    public int Ability2Level;

    public int Ability3Level;

    public AttackableUnit attackneverused;




}

