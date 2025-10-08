using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;
using System.Threading.Tasks;

namespace AIScripts;
//Status: Modified script, not like original
public class TaskKillMinionAI : CAIScript
{
    /*
    private const float HERO_FIGHT_DIST = GetAttackRange();
    private const float HERO_ENGAGE_DIST = HERO_FIGHT_DIST + 500;
    private const int AI_FOLLOW_HERO = 0;
    private const int AI_ATTACK_HERO = 1;
    private int TargetHealth = 0;
    private bool ReducePriority = false;

    public void FindNearLowestHPMinion(Task c)
    {
        c.Priority = 0;
        float dist = GetDist();
        var pos1 = GetPos();
        var pos2 = GetPos(c.HeroID);
        dist = GetDist(pos1, pos2);

        dist = Math.Max(dist, 1);

        if (ReducePriority)
        {
            c.Priority = 0;
            return;
        }

        if (!IsDead(c.HeroID))
        {
            if (dist < HERO_ENGAGE_DIST)
            {
                c.Priority = 0.5f;
            }
            else
            {
                float engageDist = HERO_ENGAGE_DIST * 2;
                if (dist < engageDist)
                {
                    float factor = (dist - HERO_ENGAGE_DIST) / HERO_ENGAGE_DIST;
                    c.Priority = 0.5f * (1 - factor);
                }
            }
        }
    }
    void UpdatePriority(object c)
    {
        c.Priority = 0;

        var lowestHPMinion = c.FindNearLowestHPMinion();
        if (lowestHPMinion != null)
        {
            c.Priority = 0.45f;
        }
    }


    void BeginTask(object c)
    {
        // Turn off auto attack
        TurnOffAutoAttack(STOPREASON_TARGET_LOST);

        // Find the minion with the lowest HP
        var lowestHPMinion = c.FindNearLowestHPMinion();
        if (lowestHPMinion != null)
        {
            // Set state and close to target for attack
            SetStateAndCloseToTarget(AI_ATTACK, lowestHPMinion);
        }
    }

    void Tick(object c)
    {
        // Get the current state
        var currentState = GetState();

        // Find the minion with the lowest HP
        var lowestHPMinion = c.FindNearLowestHPMinion();
        if (lowestHPMinion != null)
        {
            // Get the position of the minion
            var minionPos = GetPos(lowestHPMinion);

            // Calculate distance squared from the current position
            var distanceSquared = GetDistSquared(GetPos(), minionPos);

            // Check if the current state is AI_FOLLOW
            if (currentState == AI_FOLLOW)
            {
                // Compare distance with the minimum fight distance squared
                if (distanceSquared < MIN_FIGHT_DIST_SQ)
                {
                    // Set state to AI_ATTACK and move towards the minion
                    SetStateAndCloseToTarget(AI_ATTACK, lowestHPMinion);
                }
                else
                {
                    // Set state to AI_FOLLOW and move towards the minion
                    SetStateAndMove(AI_FOLLOW, minionPos);
                }
            }
            else if (currentState == AI_ATTACK)
            {
                // Check if the target is in attack range
                if (TargetInAttackRange())
                {
                    // Turn on auto attack
                    TurnOnAutoAttack(lowestHPMinion);
                }
                else
                {
                    // Turn off auto attack and move
                    TurnOffAutoAttack(STOPREASON_MOVING);
                    SetStateAndMove(AI_FOLLOW, minionPos);
                }
            }
        }
    }


    public void AntiKiteTimer(Task c)
    {
        // Implement logic for AntiKiteTimer here
    }

    public void OnTargetLost(Task c)
    {
        // Implement logic for handling lost target here
    }
    */
}

