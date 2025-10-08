using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class PetAIdeprecated : CAIScript
{
    protected override float FAR_MOVEMENT_DISTANCE => 1000;
    protected override float TELEPORT_DISTANCE => 2000;
    protected override float FEAR_WANDER_DISTANCE => 500;

    public override bool OnInit()
    {
        SetState(AIState.AI_PET_IDLE);
        InitTimer(TimerScanDistance, 0.15f, true);
        InitTimer(TimerFindEnemies, 0.15f, true);
        InitTimer(TimerFeared, 1f, true);
        StopTimer(TimerFeared);
        return false;
    }

    public bool OnOrder(OrderType command, AttackableUnit target)
    {
        if (GetState() == AIState.AI_HALTED)
            return false;

        if (GetState() == AIState.AI_TAUNTED || GetState() == AIState.AI_FEARED)
            return false;

        if (IsHardState() && IsOrderType(command))
            return true;

        var owner = GetOwner();
        if (owner == null)
        {
            Die(Me,DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return false;
        }

        switch (command)
        {
            case OrderType.AttackTo:
                if (target == null) return false;
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, target);
                return true;

            case OrderType.MoveTo:
                if (IsFarFromOwner() || IsHoldPositionState())
                    SetStateAndCloseToTarget(AIState.AI_PET_MOVE, owner);
                return true;

            case OrderType.AttackMove:
                SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE, owner);
                return true;

            case OrderType.Stop:
                return true;

            case OrderType.PetHardStop:
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                SetStateAndCloseToTarget(AIState.AI_PET_HARDSTOP, Me);
                return true;

            case OrderType.PetHardAttack:
                if (target == null) return false;
                TurnOffAutoAttack(StopReason.TARGET_LOST);
                SetStateAndCloseToTarget(AIState.AI_PET_HARDATTACK, target);
                return true;

            case OrderType.PetHardMove:
                SetStateAndMoveInPos(AIState.AI_PET_HARDMOVE);
                return true;

            case OrderType.PetHardReturn:
                SetStateAndCloseToTarget(AIState.AI_PET_HARDRETURN, owner);
                return true;

            case OrderType.Hold:
                SetStateAndCloseToTarget(AIState.AI_PET_HOLDPOSITION, Me);
                return true;

            default:
               // Say("BAD ORDER DUDE!");
                return false;
        }
    }

    public bool OnTargetLost()
    {
        if (GetState() == AIState.AI_HALTED)
            return false;

        if (IsTransitionState())
            return true;

        var newTarget = FindTargetInAcR();
        if (newTarget == null)
        {
            var owner = GetOwner();
            if (owner == null)
            {
                Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
                return false;
            }

            if (HandleIdleAttackStates(owner))
                return true;
        }
        else if (IsAttackState())
        {
            SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, newTarget);
            return true;
        }
        else if (HandleTargetAttackStates(newTarget))
        {
            return true;
        }

        NetSetState(AIState.AI_PET_IDLE);
        return true;
    }

    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
        }
    }

    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, tauntTarget);
        }
        else
        {
            NetSetState(AIState.AI_PET_IDLE);
            TimerFindEnemies();
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
        NetSetState(AIState.AI_PET_IDLE);
        TimerFindEnemies();
    }

    public void TimerFeared()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }

    public void TimerScanDistance()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var owner = GetOwner();
        if (owner == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }

        float distanceToOwner = DistanceBetweenObjects(Me, owner);
        if (distanceToOwner > TELEPORT_DISTANCE)
        {
            SetActorPositionFromObject(Me, owner);
            NetSetState(AIState.AI_PET_IDLE);
            return;
        }

        HandleStateTransitions(distanceToOwner);
    }

    public void TimerFindEnemies()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        if (GetOwner() == null)
        {
            Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
            return;
        }

        HandleEnemyFinding();
    }

    public override void HaltAI()
    {
        StopTimer(TimerScanDistance);
        StopTimer(TimerFindEnemies);
        StopTimer(TimerFeared);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        NetSetState(AIState.AI_HALTED);
    }
}

