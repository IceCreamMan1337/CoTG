using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class AggroAI : CAIScript
{
    private const float MAX_ENGAGE_DISTANCE = 2500;
    protected override float FEAR_WANDER_DISTANCE => 500;

    public override bool OnInit()
    {
        SetState(AIState.AI_IDLE);
        InitTimer(TimerFindEnemies, 0, true);
        InitTimer(TimerFeared, 1, true);
        StopTimer(TimerFeared);
        return false;
    }

    public void OnTargetLost()
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (GetState() == AIState.AI_ATTACKMOVE_ATTACKING || GetState() == AIState.AI_TAUNTED)
        {
            FindTargetOrMove();
        }
    }

    public override void OnPathToTargetBlocked()
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (GetState() == AIState.AI_ATTACKMOVE_ATTACKING)
        {
            AddToIgnore(0.1f);
            FindTargetOrMove();
        }
    }

    public void OnCallForHelp(AIState state, AttackableUnit target)
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (target != null && (state == AIState.AI_ATTACKMOVESTATE || state == AIState.AI_ATTACKMOVE_ATTACKING || state == AIState.AI_IDLE))
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
        }
    }

    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
        }
    }

    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, tauntTarget);
        }
        else
        {
            FindTargetOrMove();
        }
    }

    public override void OnFearBegin()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFeared);
    }

    public override void OnFearEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;

        StopTimer(TimerFeared);
        FindTargetOrMove();
    }

    public void TimerFeared()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }

    public override void OnCanMove()
    {
        if (GetState() == AIState.AI_HALTED) return;

        SetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }

    public override void OnCanAttack()
    {
        if (GetState() == AIState.AI_HALTED) return;

        SetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }

    public void TimerMoveForward()
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (GetState() == AIState.AI_IDLE)
        {
            FindTargetOrMove();
        }
        else if (GetState() == AIState.AI_ATTACKMOVESTATE)
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
        }
    }

    public void TimerFindEnemies()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var state = GetState();

        if (state == AIState.AI_ATTACKMOVESTATE)
        {
            var target = FindTargetInAcR();
            if (target == null)
            {
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                return;
            }

            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVESTATE, target);
        }
        else if (state == AIState.AI_ATTACKMOVE_ATTACKING || state == AIState.AI_TAUNTED)
        {
            var target = GetTarget();
            if (target != null && DistanceBetweenMeAndObject(target) > MAX_ENGAGE_DISTANCE)
            {
                FindTargetOrMove();
            }

            if (TargetInAttackRange())
            {
                TurnOnAutoAttack(target);
            }
            else if (!TargetInCancelAttackRange())
            {
                TurnOffAutoAttack(StopReason.MOVING);
            }
        }
    }
    public void TimerAntiKite()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }
        if (GetState() == AIState.AI_ATTACKMOVE_ATTACKING && IsMoving())
        {
            AddToIgnore(0.1f);
            FindTargetOrMove();
        }
    }

    public void FindTargetOrMove()
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED)
        {
            return;
        }

        var target = FindTargetInAcR();
        if (target != null)
        {
            if (!LastAutoAttackFinished())
            {
                InitTimer(TimerFindEnemies, 0.1f, true);
                return;
            }
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
        }
        else
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
        }
    }

    public bool OnCollisionEnemy(ObjAIBase enemy)
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return false;
        }
        if (GetState() != AIState.AI_TAUNTED && GetState() != AIState.AI_FEARED)
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, enemy);
            return false;
        }
        return true;
    }

    public bool OnCollisionOther(object other)
    {
        var currentState = GetState();
        if (currentState == AIState.AI_HALTED)
        {
            return true;
        }

        if (currentState != AIState.AI_TAUNTED && currentState != AIState.AI_FEARED)
        {
            var target = FindTargetInAcR();
            if (target != null)
            {
                SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
            }
            return false;
        }

        return true;
    }

    public override void OnReachedDestinationForGoingToLastLocation()
    {
        if (GetState() == AIState.AI_HALTED)
        {
            return;
        }
        NetSetState(AIState.AI_IDLE);
       // TimerDistanceScan();
       // TimerCheckAttack();
    }


    public override void HaltAI()
    {
        StopTimer(TimerFindEnemies);
        StopTimer(TimerFeared);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        SetState(AIState.AI_HALTED);
    }


}

