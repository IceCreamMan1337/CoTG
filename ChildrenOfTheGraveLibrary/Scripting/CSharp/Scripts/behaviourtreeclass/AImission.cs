using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using System.Collections.Generic;
using System.Numerics;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class AImission_bt : bt_OdinManager
{
    public float EntityDistance;

    public int EntityLaneID;

    public float LastUpdateTime;

    public AIMissionClass ThisMission;

    public int MyLane;

    public IEnumerable<AttackableUnit> SquadMembers;

    public Vector3 ReferencePosition;

    public AITask PushLaneTask;

    public TeamId ReferenceTeam;

    public int ChampsNearRendezvousPoint;

    public float AttackersStrengthAtRendezvous;

    public float ModifiedChampionValue;

    public int SquadCount;

    public float ModifiedDefStrength;

    public Vector3 CapturePointPosition;

    public AttackableUnit AdjacentCapturePoint0;

    public AttackableUnit AdjacentCapturePoint1;

    public int AdjacentCapturePointIndex1;

    public int AdjacentCapturePointIndex0;



    public AttackableUnit MinionFromPoint0;

    public AttackableUnit MinionFromPoint1;

    public float ClosestDistance;

    public int ActionToPick;

    public Vector3 TaskPosition;

    public TeamId ToCaptureTeam;

    public TeamId TeamCapturePoint0;

    public TeamId TeamCapturePoint1;

    public string FromSquadName0;

    public string FromSquadName1;

    public IEnumerable<AttackableUnit> Minions;

    public float ClosestMinionDistance0;

    public float ClosestMinionDistance1;

    public string MinionSquadName;

    public float CurrentDistance;

    public Vector3 ThisMissionPosition;

    public bool InitCapturePoint;

    public int SubMission_AssaultModeState;

    public IEnumerable<AttackableUnit> CapturePoints;

    public int CapturePointIndex;

    public Vector3 ClosestCapturePointPosition;

    public TeamId CapturePointPreviousState;

    public bool ResetParameters;

    public float RetreatHealthRatioModifier;


    public TeamId CurrentCapturePointState;


    public int NumDefenders;

    public float StrOfDef;

    public float DefenderStrength_Normalized;

    public int NumAttackers;

    public float StrOfAttackers;

    public float HealthRatio;

    public float DefendersValue;

    public float AttackersValue;

    public Vector3 RendezvousPosition;

    public string TargetStringName;

    public AITask GotoTask;

    public IEnumerable<AttackableUnit> UseableUnits;

    public Vector3 CapturePointPositionA;

    public Vector3 CapturePointPositionB;

    public Vector3 CapturePointPositionC;

    public Vector3 CapturePointPositionD;

    public Vector3 CapturePointPositionE;

    public TeamId AttackerTeam;

    public IEnumerable<Champion> AllChampions;

    public int Lane0;

    public int Lane1;

    public int Lane2;

    public int Lane3;

    public TeamId ChampionTeam;

    public int ClosestCapturePoint;

    public float ChampionHealth;

    public float ChampionMaxHealth;

    public float ChampionHealthRatio;

    public Vector3 ClosestLanePoint;

    public float DistanceToLane;

    public float HealthModifier;

    public AITask ToAssign;

    public AttackableUnit Target;

    public AIMissionClass MissionSelf;

    public Vector3 MissionPosition;

    public int TotalBotsNearPosition;

    public int UnitLevel;

    public IEnumerable<AttackableUnit> JungleTarget;

    public int JungleTargetCount;

    public float DistanceFromPointToTarget;

    public AITask WaitTask;

    public AISquadClass CurrentMissionSquad;

    public Vector3 PositionWolf;

    public Vector3 PositionGolem;

    public Vector3 PositionWraith;

    public Vector3 PositionAncientGolem;

    public Vector3 PositionLizardElder;

    public IEnumerable<AttackableUnit> NeutralWolves;

    public AttackableUnit ReferenceNeutral;

    public IEnumerable<AttackableUnit> Neutrals;

    public bool UnitsCollectionInitialized;

    public IEnumerable<AttackableUnit> FriendlyUnitsTurretsInhibs;

    public AttackableUnit FurthestUnit;

    public float FurthestDistance;

    public Vector3 FurthestPosition;

    public AITask EntityTask;

    public float DistFurthestUnitToTaskPos;

    public bool IsValid_AdjCapturePoint1;
    public bool IsValid_AdjCapturePoint0;
    public bool IsValid_ClosestMinionLane0;
    public bool IsValid_ClosestMinionLane1;

    public AIMissionClass missionowner = null;

    public virtual void InitOwner(AIMissionClass owner)
    {
        this.missionowner = owner;
    }

}

