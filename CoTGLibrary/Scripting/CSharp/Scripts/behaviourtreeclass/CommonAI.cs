using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using System.Collections.Generic;
using System.Numerics;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

namespace CoTG.CoTGServer.Scripting.CSharp;

public class CommonAI : BehaviourTree
{
    public bool ValueChanged;

    public float CurrentClosestDistance;

    public Vector3 UnitPosition;

    public float Distance;

    public Vector3 BasePosition;


    public IEnumerable<AttackableUnit> UnitsToEval;

    public TeamId MyTeam;

    public UnitType UnitType;

    public float UnitScore;

    public TeamId UnitTeam;

    public float CurrentHealth;

    public float MaxHealth;

    public float HealthPercent;


    /// <summary>
    /// seem that used in initialisation 
    /// </summary>
    public float StartGameTime;
    public float LaneUpdateTime;
    public float EnemyStrengthTop;
    public float EnemyStrengthMid;
    public float EnemyStrengthBot;
    public float FriendlyStrengthTop;
    public float FriendlyStrengthBot;
    public float FriendlyStrengthMid;
    public AISquadClass Squad_PushBot;
    public AISquadClass Squad_PushMid;
    public AISquadClass Squad_PushTop;
    public AISquadClass Squad_WaitAtBase;
    public AIMissionClass Mission_PushBot;
    public AIMissionClass Mission_PushMid;
    public AIMissionClass Mission_PushTop;
    public AIMissionClass Mission_WaitAtBase;
    public float PrevLaneDistributionTime;
    public float PointValue_Champion;
    public float PointValue_Minion;
    public int DistributionCount;
    public float DynamicDistributionStartTime;
    public float DynamicDistributionUpdateTime;
    public float UpdateGoldXP;
    public bool DisconnectAdjustmentEnabled;
    public int DisconnectAdjustmentEntityID;
    public float TotalDeadTurrets;
    public bool DifficultyScaling_IsWinState;
    public bool IsDifficultySet;
    public bool OverrideDifficulty;

    public AISquadClass KillSquad;

    public IEnumerable<AttackableUnit> AllEntities;

    public AttackableUnit ReferenceUnit;

    public TeamId ReferenceUnitTeam;


    public int DifficultyIndex;

    public float CurrentTime;

    public float TimeDiff;

    public int BotTeamAverageLevel;

    public int BotTeamTotalLevel;

    public int HumanTeamAverageLevel;

    public int HumanTeamTotalLevel;

    public IEnumerable<AttackableUnit> BotTeam;


    public int BotTeamCount;

    public int BotLevel;

    public Vector3 SampleBotPosition;

    public IEnumerable<AttackableUnit> HumanTeam;

    public int HumanTeamCount;

    public int HumanLevel;

    public int LevelDisparity;


    public float RubberBandTotalAdjustment;

    public int RubberBandModifier;

    public float ModiferAdditive;

    public float BaseAmbientXP;

    public float Temp;

    public float AmbientXP;

    public float BaseAmbientGold;

    public float AmbientGold;

    public IEnumerable<AttackableUnit> TargetCollection;

    public float Strength;

    public Vector3 Center0;

    public Vector3 Center1;

    public Vector3 Center2;

    public Vector3 Center3;

    public Vector3 Bot0;

    public Vector3 Bot1;

    public Vector3 Bot2;

    public Vector3 Bot3;

    public Vector3 Bot4;

    public Vector3 Top0;

    public Vector3 Top1;

    public Vector3 Top2;

    public Vector3 Top3;

    public Vector3 Top4;

    public float EnemyStrength;

    public float FriendStrength;

    public AttackableUnit SourceUnit;

    public AttackableUnit TargetUnit;

    public string AITaskTopic;

    public string EventString;

    public Vector3 SourceUnitPosition;

    public float DistanceBetweenEventUnits;

    public IEnumerable<AttackableUnit> FriendlyChampsNearby;

    public int NumFriendlyChampsNearby;

    public AIMissionClass KillMission;
    public AITask DefendTask;
    public AITask IndividualDefendTask;
    //public AIMission KillMission;
    public int LaneID;

    public float CurrentGameTime;

    public float LaneUpdateTimeDiff;

    public Vector3 DragonPosition;

    public AISquad Squad_Dragon;

    public AISquad Squad_Jungling;

    public bool Run;

    public int IsCP;

    public TeamId CPTeam;

    public float Temp1;

    public float Temp2;

    public float ThreatScore;

    public float TempNeed;

    public bool PlayersOnTeam;

    public int Bot1Lane;

    public int Bot2Lane;

    public int Bot3Lane;

    public int Bot4Lane;

    public bool TeamInit;

    public IEnumerable<Champion> ChampCollection;

    public int ChampCount;

    public int BotCount;

    public TeamId ChampTeam;

    public TeamId EntityTeam;


    public int Lane0Count;

    public int Lane1Count;

    public int Lane2Count;

    public int BotIndex;

    public int ClosestLaneID;

    public Vector3 ClosestLanePoint;

    public float DistanceToClosestLanePoint;

    public int LaneToAssign;

    /// <summary>
    /// Les classes CommonAI ne sont jamais des behaviour trees de map
    /// </summary>
    protected override bool IsMapBehaviourTree()
    {
        return false; // Les CommonAI sont toujours pour les champions
    }
}
