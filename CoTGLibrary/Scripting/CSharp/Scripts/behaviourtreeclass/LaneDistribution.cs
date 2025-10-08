using CoTG.CoTGServer.GameObjects.AttackableUnits;
using System.Numerics;

namespace CoTG.CoTGServer.Scripting.CSharp;

public class AI_LaneDistribution : CommonAI
{
    public float BestScore;

    public int CurrentEntityIndex;

    public float ScoreTop;

    public float ScoreMid;

    public float ScoreBot;

    public float Temp;

    public float PriorityTop;

    public float PriorityMid;

    public float PriorityBot;

    public float TempFriendlyStrengthTop;

    public float TempFriendlyStrengthMid;

    public float TempFriendlyStrengthBot;

    public bool AssignedEntity0;

    public bool AssignedEntity1;

    public bool AssignedEntity2;

    public bool AssignedEntity3;

    public bool AssignedEntity4;

    public int TotalEntitiesAssigned;

    public int EntitiesAlive;

    public AttackableUnit BestScore_Entity;

    public int BestScore_EntityIndex;

    public int BestScore_Lane;

    public int CurrentLane;

    public int LaneAssigned;

    public float Presence;

    public float temp;

    public Vector3 ClosestPoint;

    public float ClosestDistanceToLane;

    public float NormalizedDistanceValue;

    public int DistributionCount;

    public int LaneID;




}

