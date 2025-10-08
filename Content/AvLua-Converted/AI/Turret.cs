using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class TurretAI : CAIScript
{
    object a, b;
    long lastTargetLostTime;

    // Initialisation
    public override bool OnInit()
    {
        SetState(AIState.AI_HARDIDLE);
        InitTimer(TimerFindEnemies, 0.15f, true);
        return false;
    }

    // Handle target loss
    public void OnTargetLost()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        var newTarget = FindTargetInAcR();

        if (newTarget == null)
        {
            if (GetState() == AIState.AI_HARDIDLE_ATTACKING || GetState() == AIState.AI_TAUNTED)
            {
                NetSetState(AIState.AI_HARDIDLE);
                return;
            }
        }
        else if (GetState() == AIState.AI_HARDIDLE_ATTACKING || GetState() == AIState.AI_TAUNTED)
        {
            NetSetState(AIState.AI_HARDIDLE_ATTACKING);
            SetTarget(newTarget);
            return;
        }

        NetSetState(AIState.AI_HARDIDLE);
    }

    // Handle help call
    public void OnCallForHelp(AttackableUnit c, AttackableUnit d)
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        if (d != null && (GetState() == AIState.AI_HARDIDLE || GetState() == AIState.AI_HARDIDLE_ATTACKING))
        {
            NetSetState(AIState.AI_HARDIDLE_ATTACKING);
            SetTarget(d);
        }
    }

    // Start of taunt
    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            NetSetState(AIState.AI_TAUNTED);
            SetTarget(tauntTarget);
        }
    }

    // End of taunt
    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            NetSetState(AIState.AI_HARDIDLE_ATTACKING);
            SetTarget(tauntTarget);
        }
        else
        {
            NetSetState(AIState.AI_HARDIDLE);
            TimerFindEnemies();
        }
    }

    // Allow movement
    public override void OnCanMove()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        NetSetState(AIState.AI_HARDIDLE);
        TimerFindEnemies();
    }

    // Allow attack
    public void OnCanAttack()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        NetSetState(AIState.AI_HARDIDLE);
        TimerFindEnemies();
    }

    // Enemy search function
    public void TimerFindEnemies()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }

        if (GetState() == AIState.AI_HARDIDLE)
        {
            var newTarget = FindTargetInAcR();
            if (newTarget == null)
            {
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                return;
            }

            if (GetState() == AIState.AI_HARDIDLE)
            {
                NetSetState(AIState.AI_HARDIDLE_ATTACKING);
                SetTarget(newTarget);
            }
        }

        if (GetState() == AIState.AI_HARDIDLE_ATTACKING || GetState() == AIState.AI_TAUNTED)
        {
            if (TargetInAttackRange())
            {
                TurnOnAutoAttack(GetTarget());
            }
            else
            {
                NetSetState(AIState.AI_HARDIDLE);
                TurnOffAutoAttack(StopReason.MOVING);
            }
        }
    }

    // Stop AI
    public override void HaltAI()
    {
        StopTimer(TimerFindEnemies);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}

