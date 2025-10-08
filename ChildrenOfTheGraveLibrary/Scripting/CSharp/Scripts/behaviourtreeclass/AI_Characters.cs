using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using System.Collections.Generic;
using System.Numerics;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using System;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class AI_Characters : CommonAI
{
    //Bot side 

    public TeamId TargetTeam;

    public TeamId SelfTeam;

    public UnitType TargetType;

    public float TargetHealthRatio;

    public float SelfPAR_Ratio;

    public int UnitLevel;

    public float AttackRange;

    public bool IsTargetInFront;

    public float ThresholdRange;

    public Vector3 SafePosition;




    public AttackableUnit Self;

    public float DeaggroDistance;

    public float Damage;

    public float PrevHealth;

    public float PrevTime;

    public bool LostAggro;

    public float StrengthRatioOverTime;

    public bool LowThreatMode;

    public int PotionsToBuy;

    public bool TeleportHome;


    public Vector3 SelfPosition;

    public AttackableUnit Target;

    public Vector3 TargetAcquiredPosition;

    public bool TargetValid;

    public bool TargetDeAggro;

    public float TargetDeAggroTime;

    public float LastKnownTime;

    public Vector3 LastKnownPosition;

    public int PreviousSpellCastNumber;

    public float PrevRetreatTime;




    public int KillChampionScore;

    public bool IssuedAttack;

    public AttackableUnit IssuedAttackTarget;

    public AttackableUnit PreviousSpellCastTarget;

    public float LastKnownHealthRatio;

    public float CastTimeThreshold;

    public float PreviousSpellCastTime;

    public AttackableUnit PrevKillChampTarget;

    public float PrevKillChampTargetHealth;

    public float PrevKillChampDamageTime;

    public int ClaritySlot;
    public int ExhaustSlot;
    public int GhostSlot;
    public int HealSlot;
    public int IgniteSlot;
    public int TeleportSlot;

    public bool SpellStall;

    public int PurchaseItemIndex;
    public bool IsDominionGameMode = false;

    public object _AttackTarget;

    public object _HealAbilities;

    public object HighThreatManagementSpells;

    public object _ItemBuildPurchase;

    public object _KillChampionAttackSequence;

    public object _KillChampionScoreModifier;

    public object _LevelUpAbilities;

    public object _PostActionBehavior;

    public object _PushLaneAbilities;


    public int PromoteSlot;
    public int CleanseSlot;
    public int FlashSlot;
    public int SurgeSlot;

    public object _GlobalAbilities;

    public string PreviousActionPerformed;

    public float LastIssuedEventTime;

    public bool FinishedItemBuild;

    public int ActionDebugText;

    public int TaskDebugText;

    public string ExtraItem;

    public bool ExtraItemPurchased;


    public bool BeginnerScaling;

    public Vector3 OrderBaseTopEntrance;

    public Vector3 OrderBaseBottomEntrance;

    public Vector3 ChaosBaseTopEntrance;

    public Vector3 ChaosBaseBottomEntrance;

    public float LastUseSpellTime;

    public string PreviousTask;

    public float RetreatPositionStartTime;

    public Vector3 RetreatSafePosition;

    public float RetreatFromCP_RetreatUntilTime;

    public float WanderUntilTime;

    public float BeginnerWaitInBaseTime;

    public float NextActionTime;

    public object _DominionAttackMinion;

    public object _MoveToCapturePointAbilities;

    public int GarrisonSlot;

    public string _Champion;

    public float ValueOfAbility0;

    public float ValueOfAbility1;

    public float ValueOfAbility2;

    public float ValueOfAbility3;

    public int CC_AbilityNumber;

    public float AbilityNotReadyMaxScore;

    public float AbilityNotReadyMinScore;

    public float AbilityReadinessMaxScore;

    public float AbilityReadinessMinScore;

    public float DamageRatioThreshold;

    public float LowHealthPercentThreshold;

    public string CurrentTask;

    public object _HighThreatManagementSpells;

    public bool RunBT_SuppressCapturePoint;

    public bool RunBT_KillChampion;

    public bool RunBT_HighThreat;

    public bool RunBT_LowThreat;
    public bool RunBT_CapturePoint;

    public bool RunBT_ReturnToBase;
    public bool RunBT_Attack;

    public bool RunBT_Move;

    public bool RunBT_Wander;

    public bool RunBT_InterruptCapture;

    public bool KillChampionAggressiveState;
    public bool RunBT_RetreatFromEnemyCapturePoint;

    public bool RunBT_MidThreat;
    public bool RunBT_NinjaCapturePoint;

    public float StrRatio;

    public int Count;

    public float NewDamage;

    public Vector3 BaseLocation;

    public Vector3 ReferencePoint;

    public float DistanceToReferencePoint;

    public int ReferenceUnitLevel;

    public int FriendlyChamps;

    public int EnemyChamps;

    public float StrengthToAdd;

    public float StrengthModifier;

    public int SelfLevel;

    public int ChampionLevel;

    public int LevelDiff;

    public float Modifier;

    public float ChampionMaxHealth;

    public float ChampionCurrentHealth;

    public float ChampionHealthRatio;

    public float HealthModifier;

    public TeamId ChampionTeam;

    public float ChampRatioModifier;

    public float UnitDistanceToRefPoint;

    public UnitType TargetUnitType;

    public string ToPrint;

    public AttackableUnit Taunter;

    public Vector3 FleePoint;

    public float Health_Ratio;

    public int SkillPoints;

    public float GameTime;

    public ChildrenOfTheGraveEnumNetwork.Content.Color ActionDebugTextColor;

    public ChildrenOfTheGraveEnumNetwork.Content.Color TaskDebugTextColor;

    public Vector3 TextOffset;

    public int Kills;

    public int Deaths;

    public int KillDiff;

    public int IntModifier;

    public int CaptureChannelCount;

    public float DistanceToBase;

    public float SelfHealthRatio;

    public float PAR_Ratio;

    public float HP_Ratio;

    public float DetectionRange;

    public float DetectionRangeModifier;

    public int HealthPackEncounterId;

    public int SquadId;

    public Vector3 NearestHealthPackPosition;

    public float DistanceToHealthPack;

    public int EnemyHeroesInArea;

    public AttackableUnit ClosestEnemyChamp;

    public float DistanceToClosestEnemyChamp;

    public IEnumerable<AttackableUnit> NonOwnedCPsNearSelf;

    public int NonOwnedCPsNearSelfCount;

    public AttackableUnit Point;

    public float DistanceToPoint;

    public float HealthRatio;

    public bool IsBlitzMicroStun;


    public AttackableUnit AreYouBlitzcrank;

    public string SkinName;

    public float RocketGrabCooldown;

    public float TimePassed;

    public Vector3 TaskPosition;

    public int ChampionCount;

    public IEnumerable<AttackableUnit> CapturePoints;

    public int CapturePointCount;

    public int NumNearbyAllies;

    public int NumSuppressingAllies;

    public bool CPBeingTaken;

    public AttackableUnit ToInterrupt;

    public AITask DefendTask;

    public Vector3 DefendPosition;

    public IEnumerable<AttackableUnit> EnemyChampsByDefendPoint;

    public float DistanceToInterrupt;

    public IEnumerable<AttackableUnit> Minions;

    public AttackableUnit TargetCapturePoint;

    public TeamId CapturePointTeam;

    public float MaxPAR;

    public float CurrentPAR;

    public float CaptureRate;

    public int EnemyCount;

    public int AllyCount;

    public int GarrisonBuffCount;

    public int GarrisonDebuffCount;

    public int TotalGarrisonBuffCount;

    public float DistanceToCapturePoint;

    public float RetreatTimeDiff;

    public float Ability0;
    public float Ability1;
    public float Ability2;
    public float Ability3;

    public float DamageRatioThresholdAdjustment;

    public int TurretCount;

    public float BestScore;

    public float PreviousTargetHealth;
    public float PreviousTargetScore;

    public Vector3 ReferencePosition;

    public bool _IsCrowdControlled;

    public float UnitHealth;

    public float DistanceScore;

    public float HealthScore;

    public int PrevKillChampionScore;

    public float MyHealthRatio;

    public FuzzyFunctor FuzzyFunctor_NoTowerNoCC;

    public FuzzyFunctor FuzzyFunctor_NoTowerHasCC;

    public FuzzyFunctor Fuzzy_AggressiveState;

    public bool HasStun;

    public bool InEnemyTowerRange;

    public PrimaryAbilityResourceType PAR_Type;

    public PrimaryAbilityResourceType PARType;

    public float MAX_PAR;

    public int TurretsCount;

    public IEnumerable<float> FuzzyInputs;

    public float IsHealthyScore;

    public float IsUnhealthyScore;

    public float IsTargetHealthyScore;

    public float IsTargetUnhealthyScore;

    public float SafetyScore;

    public float UnsafetyScore;

    public float TotalMembershipValue;

    public int AbilityLevel;

    public float temp;

    public float HasMana;

    public float NoMana;

    public float IsHighBurst;

    public float IsLowBurst;

    public float DamageRatio;

    public float MyHealth;

    public float PokeScore;

    public float RetreatScore;

    public float KillScore;

    public float tempScore;

    public float tempScore1;

    public float tempScore2;

    public IEnumerable<float> FuzzyOutputs;

    public int NearbyTurretCount;

    public int EnemyChampCount;

    public int FriendlyChampCount;

    public TeamId Team;

    public Vector3 UpperBaseEntrance;

    public Vector3 LowerBaseEntrance;

    public float TargetDistanceToUpperBaseEntrance;

    public float TargetDistanceToLowerBaseEntrance;

    public float DistanceToTaskPosition;

    public float KillChampTargetHealth;

    public float DamageTimeDiff;

    public float AntiLeashThreshold;

    public float DisableAntiLeashTime;

    public int InverseKillChampionScore;

    public float Damage_Ratio;

    public bool boolIsHighBurst;

    public bool IsLowHP;

    public int NearbyEnemies;

    public AttackableUnit CurrentClosestTarget;

    public float _Range;

    public float DistanceToTarget;

    public Vector3 FlashPosition;

    public int EnemyHeroesAtDestination;

    public float ClosestDistance;

    public float CurrentDistance;

    public AttackableUnit ClosestCapturePoint;

    public int EnemyChampionCount;

    public float BaseDistance;

    public float EnemyCheckRange;

    public AttackableUnit ClosestEnemy;

    public AITask KillTask;

    public float DistanceToEventTarget;

    public Vector3 EventTargetPosition;

    public int NumEnemyChamps;

    public int NumFriendlyChamps;

    public int NumChampDifferential;

    public bool EnemyCPInTheWay;

    public AITask Task;

    public IEnumerable<AttackableUnit> CPCollection;

    public AttackableUnit _CapturePoint;

    public float DistanceToCP;

    public IEnumerable<AttackableUnit> EnemyChampsByPoint;

    public float RetreatTimeRemaining;

    public int BuffCount;

    public float TimeToAdd;

    public float EnemyDistanceToCP;

    public Vector3 RelicPosition;

    public IEnumerable<AttackableUnit> AllyMinions;

    public int TotalSuperMinions;

    public float TimeToWander;

    public int UnitsNearby;

    public float QCost;

    public float WCost;

    public float ECost;

    public float RCost;

    public float ComboCost;

    public float CurrentMana;

    public int QLevel;

    public int WLevel;

    public int ELevel;

    public int RLevel;


    public float WKillThreshold;

    public float QKillThreshold;

    public float EKillThreshold;

    public float RKillThreshold;

    public float TargetCurrentHealth;

    public float Health;

    public Vector3 AlistarPosition;

    public AttackableUnit SelectedFriendlyTarget;

    public float SelfDistanceToBase;

    public float TargetDistanceToBase;

    public Vector3 TargetPosition;

    public Vector3 MoveToPosition;

    public float Radius;

    public int CollectionCount;

    public float SkillShotAoERadius;

    public Vector3 SkillShotCastPosition;

    public float RandomFloat;

    public AITask _CurrentTask;

    public int PlayerKills;

    public int BotKills;

    public TeamId BotTeam;

    public IEnumerable<Champion> ChampionCollection;

    public TeamId ChampTeam;

    public float ScaleFactor;

    public float UnitAttackSpeed;

    public PrimaryAbilityResourceType SelfPARType;

    public float CurrentGold;

    public bool AggressiveState;

    public int Level;

    public bool _IsMelee;

    public Vector3 EnemyBase;

    public int AbilityLevel0;

    public int AbilityLevel1;

    public int AbilityLevel2;

    public int AbilityLevel3;

    public Vector3 AshePosition;

    public IEnumerable<AttackableUnit> EnemyChampionsInArea;

    public float GlobalTargetHealthRatio;

    public Vector3 GlobalTargetPosition;

    public float SelfAttackRange;

    public float TargetAcquisitionRange;

    public IEnumerable<AttackableUnit> Attackers;

    public AttackableUnit NearestEnemy;

    public IEnumerable<AttackableUnit> EnemyStructures;

    public int NumEnemyStructures;

    public AttackableUnit FirstAlliedMinion;

    public float DistanceFirstMinionToBase;

    public float DistanceTargetToBase;

    public AttackableUnit FirstMinion;

    public AttackableUnit LastMinion;

    public float DistanceBetweenMinions;

    public Vector3 MinionPosition;

    public int FriendlyMinionsByFirstMinion;

    public AttackableUnit _Minion;

    public AttackableUnit _Nexus;

    public Vector3 BotLaneCorner;

    public float CornerToNexus;

    public float MinionToNexus;

    public Vector3 TopLaneCorner;

    public int NearbyEnemyTurrets;

    public bool IsHighBurstbool;

    public IEnumerable<AttackableUnit> EnemyChampionsInRange;

    public IEnumerable<AttackableUnit> MinionsNearby;

    public float FurthestDistance;

    public int MinionCount;

    public float SelfAttackDamage;

    public float TargetHealth;

    public float WaitThreshold;

    public int FriendlyMinionCount;

    public int EnemyMinionCount;

    public int MinionDiff;

    public int FriendlyStructureCount;

    public float MinionModifier;

    public float DistanceModifier;

    public float NearestDistance;

    public AttackableUnit ClosestTurret;
    public Vector3 TurretPosition;

    public int DangerousEnemyCount;

    public int DangerousEnemyNearbCount;

    public Vector3 TeleportPosition;

    public float DistanceToTeleportPosition;

    public IEnumerable<LaneTurret> AllTurrets;

    public TeamId TurretTeam;

    public int FriendsCount;

    public float TurretHealthRatio;

    public IEnumerable<AttackableUnit> Allies;

    public AttackableUnit ClosestTarget;

    public Vector3 CaitlynPosition;

    public float UltRange;

    public int UltLevel;

    public float UltCooldown;

    public float UltThreshold;

    public float UltThresholdOffset;

    public Vector3 EzrealPosition;

    public float HealthRatioDelta;

    public float IteratorUnitHealthRatio;

    public IEnumerable<AttackableUnit> EnemyChampCollection;

    public Vector3 ZiggsPosition;

    public float HPThreshold;

    public float GlobalTargetHealth;

    public float DistanceBetweenUnits;

    public float DefileRadius;

    public Vector3 WallOfPainCastPosition;

    public float WCooldown;

    public bool IsCCd;

    public Vector3 LuxPosition;

    public Vector3 NewCastPosition;

    public Vector3 PowerballPosition;

    public float FlashRange;

    public TeamId IterateTeam;

    public float IterateHealthRatio;
    public Vector3 SorakaPosition;

    public float DistanceToMinion;

    public float SpellRangeQ;

    public AttackableUnit ClosestEnemyUnit;

    public AttackableUnit ClosestEnemyMinion;

    public float DistanceToClosestEnemyHero;

    public float DistanceToClosestEnemyMinion;

    public float DistanceFromMinionToEnemyHero;

    public float DistanceDifference;

    public AttackableUnit NearestEnemyToAttack;

    public AttackableUnit CFHTarget;

    public AttackableUnit CFHCaller;

    public AttackableUnit AggroTarget;

    public Vector3 AggroPosition;

    public IEnumerable<AttackableUnit> EnemyUnitsNearSelf;

    public bool TargetFound;

    public bool AttackIssued;

    public AttackableUnit AttackIssuedTarget;

    public float AttackIssuedTimestamp;

    public float AcquisitionRange;

    public float PursuitTime;

    public float Cost;

    public float Cooldown;

    public int Spell0Level;

    public int Spell1Level;

    public int Spell2Level;

    public int EnemiesInDefileRange;

    public Vector3 NewSkillShotCastPosition;

    public int Ability0Level;

    public int Ability1Level;

    public int Ability2Level;

    public float QCastRange;

    public float WCastRange;

    public float EnemyAttackRange;

    public float DistanceSelfToBase;

    public AttackableUnit TargetToKite;

    public AITask AssistTask;

    public AttackableUnit EventTarget;

    public Vector3 RocketJumpPosition;

    public Vector3 SpellPosition;

    public int RisingSpellForceCount;

    public Vector3 DrainTowerCheckPosition;

    public int DrainTowerCheckCount;

    public bool IsCC;

    public Vector3 GalioPosition;

    public Vector3 SelectedFriendlyTargetPosition;

    public Vector3 TauntPosition;

    public Vector3 SpellCastPosition;

    public float DistanceToNearestMinion;

    public float DistanceToClosestEnemyMinionFromTarget;

    public float MinionHealthRatio;

    public float EnemyHealthRatio;

    public Vector3 KaylePosition;

    public Vector3 LucentSingularityPosition;

    public Vector3 ShieldCastPosition;

    public float ESpellRadius;

    public float ECastRange;

    public AttackableUnit NearestTarget;

    public float MaokaiPAR_Ratio;

    public int FriendlyCount;

    public float SelfPARRatio;

    public PrimaryAbilityResourceType NasusPARType;

    public Vector3 NidaleePosition;


    public AttackableUnit ConsumeTarget;

    public int FriendliesCount;

    public int EnemiesNearby;

    public PrimaryAbilityResourceType RenektonPARType;

    public float CurrentFury;

    public float HealthRatioDiff;

    public float EnemyHealth;

    public Vector3 SonaPosition;

    public int SpellLevel;

    public Vector3 TaricPosition;

    public AttackableUnit ContaminateRangeCheck;

    public Vector3 UdyrPosition;

    public int SpellLevelQ;

    public float SelfAP;

    public float SpellRadiusE;

    public float SpellRadiusR;

    public Vector3 YorickPosition;

    public float ThrowDistance;

    public Vector3 ZileanPosition;

    public bool _BeginnerScaling; //rito ? 

    public float Threshold;

    /// <summary>
    /// Override of the Update method to call the champion-specific behavior method
    /// </summary>
    public override void Update()
    {
        base.Update();

        // Determine the champion name from the class name if _Champion is not defined
        string championName = _Champion;
        if (string.IsNullOrEmpty(championName) && Owner is Champion)
        {
            var className = GetType().Name;
            if (className.EndsWith("BehaviorClass"))
            {
                championName = className.Replace("BehaviorClass", "");
            }
            else if (className.EndsWith("Class"))
            {
                championName = className.Replace("Class", "");
            }

            // Define _Champion for future uses
            _Champion = championName;
        }

        // Call the champion-specific behavior method via reflection
        var behaviorMethod = GetType().GetMethod($"{championName}Behavior",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (behaviorMethod != null)
        {
            try
            {
                var result = behaviorMethod.Invoke(this, null);
            }
            catch (Exception)
            {
            }
        }
    }

    /// <summary>
    /// Propagates the owner to child nodes of this class
    /// </summary>
    protected override void PropagateOwnerToChildren(ObjAIBase owner)
    {
        base.PropagateOwnerToChildren(owner);

        // AI_Characters classes often contain references to other classes
        // that inherit from BehaviourTree. We need to identify them and propagate the owner.

        // Use reflection to find all fields that inherit from BehaviourTree
        var fields = GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (field.FieldType.IsSubclassOf(typeof(BehaviourTree)) || field.FieldType == typeof(BehaviourTree))
            {
                var value = field.GetValue(this);
                if (value is BehaviourTree bt)
                {
                    bt.SetOwner(owner);
                }
            }
        }
    }

    /// <summary>
    /// AI_Characters classes are never map behavior trees
    /// </summary>
    protected override bool IsMapBehaviourTree()
    {
        return false; // AI_Characters are always for champions
    }
}
