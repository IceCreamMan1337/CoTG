using CoTG.CoTGServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class TaskKillTowerAI : CAIScript
{
    /*
    // Get current attack range
    float attackRange = GetAttackRange();

    // Calculate minimum combat distance (with a 200 buffer)
    float minFightDist = attackRange + 200;

    // Calculate the square of the minimum combat distance
    float minFightDistSq = minFightDist * minFightDist;

    // Calculate engagement distance (with an 800 buffer)
    float minEngageDist = attackRange + 800;

    // Calculate the square of the engagement distance
    float minEngageDistSq = minEngageDist * minEngageDist;

    // Initialize constants for AI
    const int AI_ATTACK = 0;
    const int AI_FOLLOW = 1;


    // FindNearestTower function
    object FindNearestTower(object c)
    {
        var b = GetStructures();
        var d = GetOtherTeam();
        var e = d.Item1;
        var f = d.Item2;
        var g = d.Item3;
        var h = d.Item4;
        var i = d.Item5;
        var j = d.Item6;
        var k = d.Item7;
        var l = d.Item8;
        var m = d.Item9;

        b = b(e, f, g, h, i, j, k, l, m);

        var maxDistSquared = GetMaxTravelDistSquared();

        object nearestTower = null;
        float minDistSquared = float.MaxValue;

        for (int idx = f; idx < g; idx += h)
        {
            var obj = GetObject(b[idx]);
            var distSquared = GetDistSquared(GetPos(obj), GetPos());

            if (!IsDead(obj) && distSquared < MIN_ENGAGE_DIST_SQ && maxDistSquared > distSquared)
            {
                nearestTower = obj;
                minDistSquared = distSquared;
            }
        }

        return nearestTower;
    }

    // UpdatePriority function
    void UpdatePriority(object c)
    {
        var nearestTower = FindNearestTower(c);

        if (nearestTower != null)
        {
            var currentHP = GetHP(nearestTower);
            var maxHP = GetMaxHP(nearestTower);
            var healthPercentage = currentHP / maxHP;

            if (healthPercentage < 0.2)
            {
                c.Priority = 1 - healthPercentage;
            }
            else
            {
                c.Priority = 0.4f;
            }
        }
    }

    // BeginTask function
    void BeginTask(object c)
    {
        TurnOffAutoAttack(STOPREASON_TARGET_LOST);
    }

    // OnTargetLost function
    void OnTargetLost(object c)
    {
        // No action needed
    }

    // Tick function
    void Tick(object c)
    {
        var currentState = GetState();

        var nearestTower = FindNearestTower(c);

        if (nearestTower != null)
        {
            var targetPos = GetPos(nearestTower);
            var distSquared = GetDistSquared(GetPos(), targetPos);

            if (currentState == AI_FOLLOW)
            {
                if (distSquared < MIN_FIGHT_DIST_SQ)
                {
                    SetStateAndCloseToTarget(AI_ATTACK, nearestTower);
                }
                else
                {
                    SetStateAndMove(AI_FOLLOW, targetPos);
                }
            }
            else if (currentState == AI_ATTACK)
            {
                if (TargetInAttackRange())
                {
                    TurnOnAutoAttack(nearestTower);
                }
                else
                {
                    TurnOffAutoAttack(STOPREASON_MOVING);
                    SetStateAndMove(AI_FOLLOW, targetPos);
                }
            }
        }
    }*/

}

