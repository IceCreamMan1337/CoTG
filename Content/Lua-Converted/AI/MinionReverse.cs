namespace AIScripts;
//Status: Modified script, not like original
public class MinionReverseAI : MinionLibAI
{
    const float MAX_ENGAGE_DISTANCE = 550;
    protected override float FEAR_WANDER_DISTANCE => 500;
    const float DELAY_FIND_ENEMIES = 0.25f;

    // Global variables
    public AttackableUnit tauntTarget;
    AIState currentState;
    float lastAttackScan = 0;

    // Events
    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED) return;

        tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            if (IsObjectUseable(tauntTarget))
            {
                UseTarget(tauntTarget);
            }
            else
            {
                SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
            }
            StopTimer(TimerAntiKite);
        }
    }

    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;

        tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            if (IsObjectUseable(tauntTarget))
            {
                UseTarget(tauntTarget);
            }
            else
            {
                SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, tauntTarget);
            }
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            FindTargetOrMove();
        }
    }

    // Timers
    public override void TimerMoveForward()
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (GetState() == AIState.AI_IDLE)
        {
            FindTargetOrMove();
        }
        else if (GetState() == AIState.AI_ATTACKMOVESTATE)
        {
            SetStateAndMoveToBackwardNav(AIState.AI_ATTACKMOVESTATE);
            lastAttackScan = 0;
        }
    }

    public override void TimerFindEnemies()
    {
        var state = GetState();
        if (state == AIState.AI_HALTED) return;

        if (state == AIState.AI_ATTACKMOVESTATE)
        {
            var useableObject = FindUseableObjectInAcR();
            if (useableObject != null)
            {
                SetState(AIState.AI_IDLE);
                TurnOffAutoAttack(StopReason.MOVING);
                UseTarget(useableObject);
                return;
            }

            var target = FindTargetInAcR();
            if (target == null)
            {
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                return;
            }

            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
            ResetAndStartTimer(TimerAntiKite);
        }
        else if (state == AIState.AI_TAUNTED)
        {
            var target = GetTauntTarget();
            if (target != null)
            {
                if (IsObjectUseable(target))
                {
                    UseTarget(target);
                }
                else
                {
                    SetStateAndCloseToTarget(AIState.AI_TAUNTED, target);
                }
            }
        }

        if (state == AIState.AI_ATTACKMOVE_ATTACKING)
        {
            var target = GetTauntTarget();
            if (target != null && TargetInAttackRange())
            {
                if (state != AIState.AI_TAUNTED)
                {
                    ResetAndStartTimer(TimerAntiKite);
                }
                // TurnOnAutoAttack(target, target);
                TurnOnAutoAttack(target);
            }
            else if (!TargetInAttackRange())
            {
                TurnOffAutoAttack(StopReason.MOVING);
                lastAttackScan = 0;
            }
        }
    }

    // Search for objectives or move
    public override void FindTargetOrMove()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var useableObject = FindUseableObjectInAcR();
        if (useableObject != null)
        {
            SetState(AIState.AI_IDLE);
            TurnOffAutoAttack(StopReason.MOVING);
            UseTarget(useableObject);
            return;
        }

        var target = FindTargetInAcR();
        if (target != null)
        {
            if (!LastAutoAttackFinished())
            {
                ResetAndStartTimer(TimerFindEnemies);
                return;
            }
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            SetStateAndMoveToBackwardNav(AIState.AI_ATTACKMOVESTATE);
            StopTimer(TimerAntiKite);
            lastAttackScan = 0;
        }
    }

}

