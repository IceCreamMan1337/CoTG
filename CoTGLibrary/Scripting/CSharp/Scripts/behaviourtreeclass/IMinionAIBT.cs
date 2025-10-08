using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using System.Collections.Generic;
using System.Numerics;

namespace CoTG.CoTGServer.Scripting.CSharp;

public class IMinionAIBT : CommonAI
{
    //public float SelfPosition;

    public bool LostAggro;

    public AttackableUnit Self;

    public Vector3 AssistPosition;

    public TeamId SelfTeam;

    public bool TargetFound;

    public AttackableUnit CurrentClosestTarget = null;


    public Vector3 SelfPosition;

    public AttackableUnit AggroTarget;

    public Vector3 AggroPosition;

    public float CurrentClosestDistance;

    public float Distance;

    public AttackableUnit CFHTarget;
    public IEnumerable<AttackableUnit> EnemyUnitsNearSelf;
    public AttackableUnit CFHCaller;

    public int EnemyCount;

    public AttackableUnit Target;
    public TeamId TargetTeam;

    public float AttackRange;
    public bool AttackIssued;

    public AttackableUnit AttackIssuedTarget;

    public float AttackIssuedTimestamp;

    public float GameTime;

    public float AcquisitionRange;

    public float PursuitTime;

    public Vector3 TaskPosition;

    bool targetFound = false;

}