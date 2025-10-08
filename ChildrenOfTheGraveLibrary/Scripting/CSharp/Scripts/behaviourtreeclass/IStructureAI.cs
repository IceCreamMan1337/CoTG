using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using System.Collections.Generic;
using System.Numerics;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class IStructureAI : CommonAI
{

    //public float SelfPosition;

    public bool PreviousTargetValid;

    public AttackableUnit Self;

    public AttackableUnit PreviousTarget;


    public float TargetAcquisitionRange;

    public float CurrentTime;

    public float TargetAcquisitionTime;


    public float TimeToFireFirstShot;

    public float AttackInterval;

    public float PreviousAttackTime;

    public TeamId MyTeam;


    public float Distance;

    public UnitType TargetUnitTeam;


    public float TimePassed;


    public bool Run;


    public IEnumerable<AttackableUnit> Friends;

    public float ClosestEnemyDistance;


    public bool TargetFound;


    public AttackableUnit ClosestAttacker;

    public AttackableUnit Attacker;


    public float CurrentDistance;

    public AttackableUnit CFHTarget;

    public AttackableUnit CFHCaller;

    public AttackableUnit ClosestMinion;


    public IEnumerable<AttackableUnit> Enemies;


    public AttackableUnit ClosestChampion;

    public bool DoNothing;


    public Vector3 SelfPosition;

    public TeamId TargetTeam;


}
