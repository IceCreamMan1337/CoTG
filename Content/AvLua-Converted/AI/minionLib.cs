using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class MinionLibAI : CAIScript
{
    const float DELAY_FIND_ENEMIES = 0.25f;
    public override bool OnInit()
    {
        SetState(AIState.AI_IDLE);
        InitTimer(TimerFindEnemies, DELAY_FIND_ENEMIES, true);
        InitTimer(TimerMoveForward, 0, true);
        InitTimer(TimerAntiKite, 4, false);
        InitTimer(TimerFeared, 1, true);
        StopTimer(TimerAntiKite);
        StopTimer(TimerFeared);
        return false;
    }

    public void OnTargetLost()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        if (GetState() == AIState.AI_ATTACKMOVE_ATTACKING || GetState() == AIState.AI_TAUNTED)
            FindTargetOrMove();
    }

    public override void OnPathToTargetBlocked()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        if (GetState() == AIState.AI_ATTACKMOVE_ATTACKING)
        {
            AddToIgnore(0.1f);
            FindTargetOrMove();
        }
    }

    public void OnCallForHelp(object c, ObjAIBase d)
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        if (d != null && (GetState() == AIState.AI_ATTACKMOVESTATE || GetState() == AIState.AI_ATTACKMOVE_ATTACKING))
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, d);
            ResetAndStartTimer(TimerAntiKite);
        }
    }

    public override void OnFearBegin()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFeared);
    }

    public override void OnFearEnd()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        StopTimer(TimerFeared);
        FindTargetOrMove();
    }

    public void TimerFeared()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }

    public override void OnCanMove()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        NetSetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }

    public void OnCanAttack()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        NetSetState(AIState.AI_IDLE);
        FindTargetOrMove();
    }

    public void TimerAntiKite()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        if (GetState() == AIState.AI_ATTACKMOVE_ATTACKING && IsMoving())
        {
            AddToIgnore(0.1f);
            FindTargetOrMove();
        }
    }

    public bool OnCollisionEnemy(ObjAIBase e)
    {
        if (GetState() == AIState.AI_HALTED)
            return true;

        if (GetState() != AIState.AI_TAUNTED && GetState() != AIState.AI_FEARED)
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, e);
            return false;
        }
        return true;
    }

    public bool OnCollisionOther(object f)
    {
        var state = GetState();
        if (state == AIState.AI_HALTED)
            return true;

        if (state != AIState.AI_TAUNTED && state != AIState.AI_FEARED)
        {
            var target = FindTargetInAcR();
            if (target != null)
                SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);

            return false;
        }
        return true;
    }

    public override void OnReachedDestinationForGoingToLastLocation()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        NetSetState(AIState.AI_IDLE);
       // TimerDistanceScan();
       // TimerCheckAttack();
    }

    public override void HaltAI()
    {
        StopTimer(TimerFindEnemies);
        StopTimer(TimerMoveForward);
        StopTimer(TimerAntiKite);
        StopTimer(TimerFeared);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
    public virtual void TimerFindEnemies()
    {
      //to overrdie
    }
    public virtual void TimerMoveForward()
    {
        //to overrdie
    }
    public virtual void FindTargetOrMove()
    {
        //to overrdie
    }
}

