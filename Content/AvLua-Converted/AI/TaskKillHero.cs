using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;
using System.Threading.Tasks;

namespace AIScripts;
//Status: Modified script, not like original
public class TaskKillHeroAI : CAIScript
{
    /*
    private const float HERO_FIGHT_DIST = GetAttackRange();
    private const float HERO_ENGAGE_DIST = HERO_FIGHT_DIST + 500;
    private const int AI_FOLLOW_HERO = 0;
    private const int AI_ATTACK_HERO = 1;
    private int TargetHealth = 0;
    private bool ReducePriority = false;

    public void UpdatePriority(Task c)
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

    public void BeginTask(Task c)
    {
        TurnOffAutoAttack(STOPREASON_MOVING);
        SetStateAndCloseToTarget(AI_FOLLOW_HERO, c.HeroID);
        TargetHealth = GetTargetHealth();
        InitTimer("AntiKiteTimer", 2, true);
    }

    public void Tick(Task c)
    {
        var state = GetState();
        float dist = GetDist();
        var pos1 = GetPos();
        var pos2 = GetPos(c.HeroID);
        dist = GetDist(pos1, pos2);

        if (state == AI_FOLLOW_HERO)
        {
            if (dist < HERO_FIGHT_DIST)
            {
                SetStateAndCloseToTarget(AI_ATTACK_HERO, c.HeroID);
            }
            else
            {
                SetStateAndCloseToTarget(AI_FOLLOW_HERO, c.HeroID);
            }
        }
        else if (state == AI_ATTACK_HERO)
        {
            if (dist < HERO_FIGHT_DIST)
            {
                TurnOnAutoAttack(c.HeroID);
            }
            else
            {
                TurnOffAutoAttack(STOPREASON_MOVING);
                SetStateAndCloseToTarget(AI_FOLLOW_HERO, c.HeroID);
            }
        }
    }

    public void AntiKiteTimer(Task c)
    {
        int currentTargetHealth = GetTargetHealth();
        if (currentTargetHealth >= TargetHealth - 10)
        {
            if (IsMoving())
            {
                ReducePriority = true;
                TargetHealth = currentTargetHealth;
                return;
            }
        }

        if (ReducePriority)
        {
            ReducePriority = false;
            TargetHealth = currentTargetHealth;
            return;
        }

        TargetHealth = currentTargetHealth;
    }

    public void OnTargetLost(Task c)
    {
        // Implement logic for AntiKiteTimer here
    }

    */
}

