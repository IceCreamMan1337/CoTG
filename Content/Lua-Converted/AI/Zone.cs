using CoTG.CoTGServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class ZoneAI : CAIScript
{
    /*
    private const float FEAR_WANDER_DISTANCE = 500f;
    private Timer timerScanDistance;
    private Timer timerFindEnemies;
    private Timer timerFeared;
    private object owner;

    public ZoneAI()
    {
        // Initialize timers and states.
        Init();
    }

    public void Init()
    {
        SetState(AI_PET_IDLE);
        timerScanDistance = InitTimer(0.15, true);
        timerFindEnemies = InitTimer(0.15, true);
        timerFeared = InitTimer(1, true);
        StopTimer(timerFeared);
    }

    public bool OnOrder(int orderType, object target)
    {
        if (GetState() == AI_HALTED)
        {
            return false;
        }

        if (GetState() == AI_TAUNTED || GetState() == AI_FEARED)
        {
            return false;
        }

        // Handle specific orders based on current state
        if ((GetState() == AI_PET_HARDATTACK || GetState() == AI_PET_HARDMOVE || GetState() == AI_PET_HARDIDLE ||
             GetState() == AI_PET_HARDIDLE_ATTACKING || GetState() == AI_PET_HARDRETURN || GetState() == AI_PET_HARDSTOP) &&
            (orderType == ORDER_ATTACKTO || orderType == ORDER_MOVETO || orderType == ORDER_ATTACKMOVE || orderType == ORDER_STOP))
        {
            return true;
        }

        owner = GetOwner();
        if (owner == null)
        {
            Die(DAMAGESOURCE_INTERNALRAW);
            return false;
        }

        switch (orderType)
        {
            case ORDER_ATTACKTO:
            case ORDER_MOVETO:
            case ORDER_ATTACKMOVE:
                return true;
            case ORDER_STOP:
                return true;
            case ORDER_PETHARDSTOP:
                TurnOffAutoAttack(STOPREASON_TARGET_LOST);
                SetStateAndCloseToTarget(AI_PET_HARDSTOP);
                return true;
            case ORDER_PETHARDATTACK:
                if (target == null) return false;
                TurnOffAutoAttack(STOPREASON_TARGET_LOST);
                SetStateAndCloseToTarget(AI_PET_HARDATTACK, target);
                return true;
            case ORDER_PETHARDMOVE:
                SetStateAndMoveInPos(AI_PET_HARDMOVE);
                return true;
            case ORDER_PETHARDRETURN:
                SetStateAndCloseToTarget(AI_PET_HARDRETURN, owner);
                return true;
            default:
                Say("BAD ORDER DUDE!");
                return false;
        }
    }

    public bool OnTargetLost()
    {
        if (GetState() == AI_HALTED)
        {
            return false;
        }

        if (GetState() == AI_PET_HARDMOVE || GetState() == AI_PET_HARDRETURN || GetState() == AI_FEARED || GetState() == AI_PET_HARDSTOP)
        {
            return true;
        }

        object newTarget = FindTargetInAcR();
        if (newTarget == null)
        {
            owner = GetOwner();
            if (owner == null)
            {
                Die(DAMAGESOURCE_INTERNALRAW);
                return false;
            }

            if (GetState() == AI_PET_HARDIDLE_ATTACKING)
            {
                NetSetState(AI_PET_HARDIDLE);
                return true;
            }
            else if (GetState() == AI_PET_RETURN_ATTACKING)
            {
                SetStateAndCloseToTarget(AI_PET_RETURN, owner);
                return true;
            }
            else if (GetState() == AI_PET_ATTACKMOVE_ATTACKING)
            {
                SetStateAndCloseToTarget(AI_PET_ATTACKMOVE, owner);
                return true;
            }
        }
        else if (GetState() == AI_PET_HARDATTACK || GetState() == AI_TAUNTED)
        {
            SetStateAndCloseToTarget(AI_PET_ATTACK, newTarget);
            return true;
        }
        else if (GetState() == AI_PET_HARDIDLE_ATTACKING)
        {
            NetSetState(AI_PET_HARDIDLE_ATTACKING);
            SetTarget(newTarget);
            return true;
        }

        NetSetState(AI_PET_HARDIDLE);
        return true;
    }

    public void OnFearBegin()
    {
        if (GetState() == AI_HALTED)
        {
            return;
        }

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AI_FEARED, wanderPoint);
        TurnOffAutoAttack(STOPREASON_IMMEDIATELY);
        ResetAndStartTimer(timerFeared);
    }

    public void OnFearEnd()
    {
        if (GetState() == AI_HALTED)
        {
            return;
        }

        StopTimer(timerFeared);
        NetSetState(AI_PET_HARDIDLE);
        TimerFindEnemies();
    }

    public void TimerFeared()
    {
        if (GetState() == AI_HALTED)
        {
            return;
        }

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AI_FEARED, wanderPoint);
    }

    public void OnCanMove()
    {
        if (GetState() == AI_HALTED)
        {
            return;
        }

        NetSetState(AI_PET_HARDIDLE);
        TimerFindEnemies();
    }

    public void OnCanAttack()
    {
        if (GetState() == AI_HALTED)
        {
            return;
        }

        NetSetState(AI_PET_HARDIDLE);
        TimerFindEnemies();
    }

    public void TimerScanDistance()
    {
        if (GetState() == AI_HALTED)
        {
            return;
        }

        var tempOwner = GetOwner();
        if (tempOwner == null)
        {
            Die(DAMAGESOURCE_INTERNALRAW);
            return;
        }

        if (!IsMoving() && GetState() == AI_PET_HARDMOVE)
        {
            NetSetState(AI_PET_HARDIDLE);
            return;
        }

        if (GetState() == AI_PET_SPAWNING && IsNetworkLocal())
        {
            NetSetState(AI_PET_HARDIDLE);
        }
    }

    public void TimerFindEnemies()
    {
        if (GetState() == AI_HALTED)
        {
            return;
        }

        if (GetOwner() == null)
        {
            Die(DAMAGESOURCE_INTERNALRAW);
            return;
        }

        var e = GetState();
        if (e == AI_PET_HARDMOVE || e == AI_PET_HARDSTOP)
        {
            return;
        }

        if (e == AI_PET_HARDIDLE)
        {
            var newTarget = FindTargetInAcR();
            if (newTarget == null)
            {
                TurnOffAutoAttack(STOPREASON_TARGET_LOST);
                return;
            }

            NetSetState(AI_PET_HARDIDLE_ATTACKING);
            SetTarget(newTarget);
        }

        if (e == AI_PET_HARDATTACK || e == AI_PET_HARDIDLE_ATTACKING || e == AI_TAUNTED)
        {
            if (TargetInAttackRange())
            {
                TurnOnAutoAttack(GetTarget());
            }
            else if (!TargetInCancelAttackRange())
            {
                TurnOffAutoAttack(STOPREASON_MOVING);
            }
        }
    }

    public void HaltAI()
    {
        StopTimer(timerScanDistance);
        StopTimer(timerFindEnemies);
        StopTimer(timerFeared);
        TurnOffAutoAttack(STOPREASON_IMMEDIATELY);
        NetSetState(AI_HALTED);
    }
    */
}

