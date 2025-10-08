using CoTG.CoTGServer.API;
using CoTG.CoTGServer.Scripting.CSharp.Converted;
using CoTG.CoTGServer.Scripting.Lua;

namespace AIScripts;
//Status: converted from .131 script 
internal class LeashedAI : CAIScript
{
    protected const int HOSTILE = 1;
    protected const int INACTIVE = 0;
    protected const float LEASH_RADIUS = 850f;
    protected const float LEASH_PROTECTION_RADIUS = 750f;
    protected override float FEAR_WANDER_DISTANCE => 500f;
    protected const float REGEN_PERCENT_PER_SECOND = 0.1f;


    private Vector2 _leashedPos;
    private Vector3 _leashedDirection;
    private MinionRoamState _originalState;

    private int _leashCounter;

    public override bool OnInit()
    {
        base.OnInit();
        SetState(AIState.AI_ATTACK);
        //SetMyLeashedPosition(); = 
        _originalState = GetRoamState();
        _leashedPos = GetPosition();
        _leashedDirection = Me.Direction;

        InitTimers();

        return false;
    }

    public void InitTimers()
    {
        InitTimer(TimerRetreat, 0.5f, true);
        InitTimer(TimerAttack, 0f, true);
        InitTimer(TimerFeared, 0.5f, true);
        InitTimer(TimerRegen, 1f, true);
        // InitTimer(TimerTaunt, 0.5f, true);
        //StopTimers("TimerFeared", "TimerRegen", "TimerTaunt");

        StopTimer(TimerRegen);
        StopTimer(TimerFeared);
        /* 
         StopTimer(TimerTaunt);*/
    }



    public void Retreat()
    {
        SetStateAndMoveToLeashedPos(AIState.AI_RETREAT);
        ClearValidTargets();
    }

    public void SetStateAndMoveToLeashedPos(AIState state)
    {
        SetState(state);
        SetTarget(null);
        Me.CancelAutoAttack(false, true);
        Me.IssueOrDelayOrder(OrderType.MoveTo, null, _leashedPos);
    }


    public void TimerRegen()
    {
        if (GetState() == AIState.AI_HALTED) return;

        float maxHP = Me.Stats.HealthPoints.Total;
        float regenAmount = maxHP * REGEN_PERCENT_PER_SECOND;
        float currentHP = Me.Stats.CurrentHealth;

        if (currentHP > 0 && currentHP < Me.Stats.HealthPoints.Total)
        {
            ApiFunctionManager.Heal(Me, regenAmount, Me, AddHealthType.RGEN);
        }
    }

    public bool OnOrder(OrderType orderType, AttackableUnit target)
    {

        if (GetState() == AIState.AI_HALTED) return false;

        if (orderType == OrderType.AttackTo)
        {
            StopTimer(TimerRegen);
            SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
            SetRoamState(MinionRoamState.Hostile);
            return true;
        }

        //Error("Unsupported Order");
        return false;
    }

    public override void OnTakeDamage(AttackableUnit attacker)
    {
        if (GetState() == AIState.AI_HALTED) return;

        //AddValidTarget(attacker);

        Vector2 currentPosition = GetPosition();
        var target = FindTargetNearPosition(currentPosition, LEASH_RADIUS);

        if (target == null && attacker != null)
        {
            return;
        }

        if (GetRoamState() == MinionRoamState.Inactive && GetState() != AIState.AI_RETREAT && !IsInTauntOrFearState())
        {
            StopTimer(TimerRegen);
            SetStateAndCloseToTarget(AIState.AI_ATTACK, attacker);
            SetRoamState(MinionRoamState.Hostile);
        }


    }

    public bool IsInTauntOrFearState()
    {
        var state = GetState();
        return state == AIState.AI_TAUNTED || state == AIState.AI_FEARED;
    }

    public override void OnLeashedCallForHelp(ObjAIBase attacker, ObjAIBase helpTarget)
    {
        if (GetState() == AIState.AI_HALTED) return;

        //  AddValidTarget(helpTarget);

        Vector2 currentPosition = GetPosition();
        AttackableUnit target = FindTargetNearPosition(currentPosition, LEASH_RADIUS);

        if (target == null)
        {
            target = attacker;
            if (attacker == null) return;
        }


        /**/
        var currentState = GetState();

        var roamState = GetRoamState();
        if (roamState == MinionRoamState.Inactive)
        {
            if (currentState != AIState.AI_RETREAT && currentState != AIState.AI_TAUNTED &&
                currentState != AIState.AI_FEARED && currentState != AIState.AI_FLEEING)
            {
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);
            }
        }
        else if (roamState == MinionRoamState.Hostile && currentState == AIState.AI_ATTACK)
        {
            var myTarget = GetTarget();

            if (myTarget != null)
            {
                float distToAttacker = DistanceBetweenObjectCenterAndPoint(myTarget, currentPosition);
                float distToNewTarget = DistanceBetweenObjectCenterAndPoint(target, currentPosition);

                if (distToAttacker > distToNewTarget + 25)
                {
                    SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                    SetRoamState(MinionRoamState.Hostile);

                    _leashCounter++;
                    if (_leashCounter > 10) Retreat();
                }
            }
        }

        if (currentState == AIState.AI_RETREAT)
        {
            var distToAttackerFromLeashPoint = DistanceBetweenObjectCenterAndPoint(target, _leashedPos);

            if (distToAttackerFromLeashPoint <= LEASH_RADIUS && _leashCounter < 10)
            {
                _leashCounter++;
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);

            }
            else if (GetDistToLeashedPos() <= 750 && distToAttackerFromLeashPoint <= 1150 && _leashCounter < 10)
            {
                _leashCounter++;
                StopTimer(TimerRegen);
                SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
                SetRoamState(MinionRoamState.Hostile);
            }
        }


        /*  if (ShouldChase(attacker, helpTarget))
          {
              ChaseTarget(attacker, helpTarget);
          }  */
    }

    public bool ShouldChase(AttackableUnit attacker, AttackableUnit helpTarget)
    {
        return GetRoamState() == MinionRoamState.Inactive && !IsInTauntOrFearState() && IsTargetWithinLeashRadius(helpTarget);
    }

    public bool IsTargetWithinLeashRadius(AttackableUnit target)
    {
        var leashPos = _leashedPos;
        return DistanceBetweenObjectCenterAndPoint(target, leashPos) <= LEASH_RADIUS;
    }

    public void ChaseTarget(AttackableUnit attacker, AttackableUnit helpTarget)
    {
        StopTimer(TimerRegen);
        SetStateAndCloseToTarget(AIState.AI_ATTACK, helpTarget);
        SetRoamState(MinionRoamState.Hostile);
    }

    public void OnTargetLost(AttackableUnit target)
    {
        if (GetState() == AIState.AI_HALTED) return;

        AttackableUnit newTarget = Functions.GetOwner(target) ?? GetGoldRedirectTarget(target);

        if (newTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACK, newTarget);
        }
        else
        {
            FindNewTarget();
        }
    }

    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null && GetState() != AIState.AI_FEARED)
        {
            StopTimer(TimerRegen);
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
            SetRoamState(MinionRoamState.Hostile);
        }
    }

    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;

        //StopTimer(TimerTaunt);
        var tauntTarget = GetTauntTarget();

        if (tauntTarget != null && GetState() != AIState.AI_FEARED)
        {
            StopTimer(TimerRegen);
            SetStateAndCloseToTarget(AIState.AI_ATTACK, tauntTarget);
            SetRoamState(MinionRoamState.Hostile);
        }
        else
        {
            ResetToDefaultState();
        }
    }

    public void ResetToDefaultState()
    {
        SetState(AIState.AI_ATTACK);
        TimerRetreat();
        TimerAttack();
    }

    public override void OnFearBegin()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
        SetRoamState(MinionRoamState.Inactive);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        ResetAndStartTimer(TimerFeared);
    }

    public override void OnFearEnd()
    {
        if (GetState() == AIState.AI_HALTED) return;

        StopTimer(TimerFeared);
        SetRoamState(MinionRoamState.Hostile);
        ResetToDefaultState();
    }

    public void TimerFeared()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
        SetRoamState(MinionRoamState.Inactive);
        SetStateAndMove(AIState.AI_FEARED, wanderPoint);
    }

    public float GetDistToLeashedPos()
    {
        return Vector2.Distance(_leashedPos, Me.Position);
    }

    public void TimerRetreat()
    {
        if (GetState() == AIState.AI_HALTED) return;

        float distanceToLeashedPosition = GetDistToLeashedPos();

        if (ShouldRetreat(distanceToLeashedPosition))
        {
            Retreat();
        }
        else if (IsMovementStopped())
        {
            OnStoppedMoving();
        }
    }

    public bool IsMovementStopped()
    {
        return GetState() == AIState.AI_RETREAT && !IsMoving();
    }


    public bool ShouldRetreat(float distanceToLeashedPosition)
    {
        return distanceToLeashedPosition > LEASH_PROTECTION_RADIUS && distanceToLeashedPosition < LEASH_RADIUS;
    }

    public override void OnStoppedMoving()
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (GetState() == AIState.AI_RETREAT)
        {
            _leashCounter = 0;
            SetState(AIState.AI_ATTACK);
            SetRoamState(_originalState);
        }
    }

    public void TimerAttack()
    {
        if (GetState() == AIState.AI_HALTED) return;

        if (ShouldAttack())
        {
            var target = GetTarget();
            HandleAttackTarget(target);
        }
        else
        {
            FindNewTarget();
        }
    }

    public bool ShouldAttack()
    {
        var state = GetState();
        return state == AIState.AI_ATTACK || state == AIState.AI_TAUNTED;
    }

    public void HandleAttackTarget(AttackableUnit target)
    {
        if (target != null)
        {
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

    public void FindNewTarget()
    {
        if (GetState() == AIState.AI_HALTED) return;

        var newTarget = FindTargetNearPosition(GetMyLeashedPos(), LEASH_RADIUS);

        if (newTarget != null && IsWithinLeashRadius(newTarget))
        {


            StopTimer(TimerRegen);
            SetStateAndCloseToTarget(AIState.AI_ATTACK, newTarget);
        }
        else
        {
            ResetAndStartTimer(TimerRegen);
            Retreat();
        }
    }

    public Vector2 GetMyLeashedPos()
    {
        return _leashedPos;
    }

    public bool IsWithinLeashRadius(AttackableUnit target)
    {
        return DistanceBetweenObjectCenterAndPoint(target, GetMyLeashedPos()) <= LEASH_RADIUS;
    }

    public override void HaltAI()
    {
        StopTimer(TimerRetreat);
        StopTimer(TimerAttack);
        StopTimer(TimerFeared);
        StopTimer(TimerRegen);
        TurnOffAutoAttack(StopReason.IMMEDIATELY);
        SetState(AIState.AI_HALTED);
    }
}