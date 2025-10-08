using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class MinionOdin : MinionLibAI
{
    private const float MaxEngageDistance = 550f;
    private const float FearWanderDistance = 500f;
    private const float DelayFindEnemies = 0.25f;

    private AIState currentState;
    private AttackableUnit tauntTarget;
    private float lastAttackScan = 0f;

    public override void OnTauntBegin()
    {
        if (currentState == AIState.AI_HALTED)
            return;

        tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            if (IsObjectUsable(tauntTarget))
            {
                UseTarget(tauntTarget);
            }
            else
            {
                SetStateAndCloseToTarget(AIState.AI_TAUNTED, (tauntTarget as AttackableUnit));
            }
            StopTimer(TimerAntiKite);
        }
    }

    public override void OnTauntEnd()
    {
        if (currentState == AIState.AI_HALTED)
            return;

        tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            if (IsObjectUsable(tauntTarget))
            {
                UseTarget(tauntTarget);
            }
            else
            {
                SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, (tauntTarget as AttackableUnit));
            }
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            FindTargetOrMove();
        }
    }

    public override void TimerFindEnemies()
    {
        if (currentState == AIState.AI_HALTED)
            return;

        if (currentState == AIState.AI_ATTACKMOVESTATE)
        {
            var usableObject = FindUsableObjectInAcR(); 
            if (usableObject != null)
            {
                SetState(AIState.AI_IDLE);
                TurnOffAutoAttack(StopReason.MOVING);
                UseTarget(usableObject);
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
        else if (currentState == AIState.AI_TAUNTED)
        {
            tauntTarget = GetTauntTarget();
            if (tauntTarget != null)
            {
                if (IsObjectUsable(tauntTarget))
                {
                    UseTarget(tauntTarget);
                }
                else
                {
                    SetStateAndCloseToTarget(AIState.AI_TAUNTED, (tauntTarget as AttackableUnit));
                }
            }
        }

        if (currentState == AIState.AI_ATTACKMOVE_ATTACKING)
        {
            var target = GetTarget();
            if (target != null && TargetInAttackRange())
            {
                if (currentState != AIState.AI_TAUNTED)
                {
                    ResetAndStartTimer(TimerAntiKite);
                }
                TurnOnAutoAttack(target);
            }
            else if (!TargetInCancelAttackRange())
            {
                TurnOffAutoAttack(StopReason.MOVING);
                lastAttackScan = 0f;
            }
        }
    }

    public override void FindTargetOrMove()
    {
        if (currentState == AIState.AI_HALTED)
            return;

        var usableObject = FindUsableObjectInAcR();
        if (usableObject != null)
        {
            SetState(AIState.AI_IDLE);
            TurnOffAutoAttack(StopReason.MOVING);
            UseTarget(usableObject);
            return;
        }

        var target = FindTargetInAcR();
        if (target != null)
        {
            if (!LastAutoAttackFinished())
            {
                InitTimer(TimerFindEnemies, DelayFindEnemies, true);
                return;
            }

            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
            StopTimer(TimerAntiKite);
            lastAttackScan = 0f;
        }
    }

    public override void TimerMoveForward()
    {
        if (currentState == AIState.AI_HALTED)
            return;

        if (currentState == AIState.AI_IDLE)
        {
            FindTargetOrMove();
        }
        else if (currentState == AIState.AI_ATTACKMOVESTATE)
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
            lastAttackScan = 0f;
        }
    }

    // Placeholder methods for functionality from Lua.


}

