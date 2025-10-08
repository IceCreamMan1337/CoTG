using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class TaskDefendStructureAI : CAIScript
{
    /*
    // Constant declarations
    const float MIN_FIGHT_DIST_SQ = 22500f;
    const float MAX_FIGHT_DIST_SQ = 562500f;
    const int AI_MOVE = 0;
    const int AI_ATTACK = 1;

    void UpdatePriority(object c)
    {
        var b = GetObject(c.StructureID);
        if (b == null)
        {
            c.Done = true;
            c.Priority = 0;
            return;
        }

        float d = 0.3f;
        float e = 0.25f;
        float f = 0.5f;
        float g = 20f;
        float h = GetTime();
        float lastTookDamageTime = GetLastTookDamageTime(b);
        float timeDiff = h - lastTookDamageTime;

        if (timeDiff >= 0 && timeDiff <= g)
        {
            var targetPos = GetPos();
            var bPos = b.GetPosition();

            float dist = GetDist(targetPos, bPos);
            float maxTravelDistSquared = GetMaxTravelDistSquared();
            float maxDist = MathF.Sqrt(maxTravelDistSquared);

            float ratio = (maxDist - dist) / maxDist;
            float timeFactor = 1 - (timeDiff / g);

            float distSquared = dist * dist;
            if (distSquared < MIN_FIGHT_DIST_SQ)
            {
                d = 0;
            }
            else if (distSquared < MAX_FIGHT_DIST_SQ)
            {
                d *= 0.5f;
            }

            float priority = d * ratio * timeFactor;
            c.Priority = priority;
        }
        else
        {
            c.Priority = 0;
        }
    }

    void BeginTask(object c)
    {
        var b = GetObject(c.StructureID);
        Say("BeginTask: Defend " + b.GetName());
        TurnOffAutoAttack(STOPREASON_TARGET_LOST);
        SetStateAndMove(AI_MOVE, b.GetPosition());
    }

    void Tick(object c)
    {
        var b = GetObject(c.StructureID);
        var distSquared = GetDistSquared(b.GetPosition(), GetPos());
        bool isMoving = IsMoving();

        if (!isMoving)
        {
            if (distSquared > MAX_FIGHT_DIST_SQ)
            {
                SetStateAndMove(AI_MOVE, b.GetPosition());
            }
        }
        else
        {
            if (distSquared < MIN_FIGHT_DIST_SQ)
            {
                StopMove();
            }
        }
    }

    void OnTargetLost(object c)
    {
        // Code to execute when a target is lost
    }

   */

}

