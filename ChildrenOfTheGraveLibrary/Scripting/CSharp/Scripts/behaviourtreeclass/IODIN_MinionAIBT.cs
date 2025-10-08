using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using System.Collections.Generic;
using System.Numerics;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class IODIN_MinionAIBT : IMinionAIBT
{


    public Vector3 GuardianPosition;

    public AttackableUnit ClosestEnemyGuardian;

    public IEnumerable<AttackableUnit> UnitsToSearch;

    public int total;

    public AttackableUnit Guardian;

    public TeamId MyTeam;

    public TeamId CapturePointOwner_A;

    public TeamId CapturePointOwner_B;

    public TeamId CapturePointOwner_C;

    public TeamId CapturePointOwner_D;

    public TeamId CapturePointOwner_E;

    public int TargetPoint;

    public float PreviousCastTime;
    /// <summary>
    /// possibly that are in other class 
    /// </summary>
    public AttackableUnit Taunter;

    public TeamId GuardianTeam;

    public float SpellRange;

    public float CurrentGameTime;

    public float TimePassed;

    public Vector3 TargetPosition;

    public bool TargetValid;

    public bool FirstTarget;

    public Vector3 SelfPosition;

    public string ToPrint;
}

