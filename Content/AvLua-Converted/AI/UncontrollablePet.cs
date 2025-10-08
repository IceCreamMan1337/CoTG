using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class UncontrollablePetAI : CAIScript
{
    protected override float FAR_MOVEMENT_DISTANCE => 800f;
    protected override float MINION_MAX_VISION_DISTANCE => 1200f;
    protected override float TELEPORT_DISTANCE => 2000f;
    protected override float FEAR_WANDER_DISTANCE => 500f;



        public override bool OnInit()
        {
            SetState(AIState.AI_PET_IDLE);
            InitTimer(TimerScanDistanceCallback, 0.15f, true);
            InitTimer(TimerFindEnemiesCallback, 0.15f, true);
            InitTimer(TimerFearedCallback, 1f, true);
            StopTimer(TimerFearedCallback);
            return false;
        }

        private void TimerDebug()
        {
          //  Say("[TimerDebug()] State: " + GetState());
        }

        private bool OnOrder(object c, object d)
        {
            // Handle order logic here.
            return true;
        }

        private bool OnTargetLost()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return false;
            }

            if (GetState() == AIState.AI_PET_MOVE || GetState() == AIState.AI_PET_HARDMOVE || GetState() == AIState.AI_PET_HARDRETURN ||
                GetState() == AIState.AI_FEARED || GetState() == AIState.AI_PET_HARDSTOP)
            {
                return true;
            }

            var newTarget = FindTargetInAcR();
            if (newTarget == null)
            {
                var owner = GetGoldRedirectTarget();
                if (owner == null)
                {
                    Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
                    return false;
                }

                if (GetState() == AIState.AI_PET_HARDIDLE_ATTACKING)
                {
                    NetSetState(AIState.AI_PET_HARDIDLE);
                    return true;
                }
                else if (GetState() == AIState.AI_PET_RETURN_ATTACKING)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_RETURN, owner);
                    return true;
                }
                else if (GetState() == AIState.AI_PET_ATTACKMOVE_ATTACKING)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE, owner);
                    return true;
                }
            }
            else
            {
                if (GetState() == AIState.AI_PET_HARDATTACK || GetState() == AIState.AI_PET_ATTACK || GetState() == AIState.AI_TAUNTED)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, newTarget);
                    return true;
                }
                else if (GetState() == AIState.AI_PET_HARDIDLE_ATTACKING)
                {
                    NetSetState(AIState.AI_PET_HARDIDLE_ATTACKING);
                    SetTarget(newTarget);
                    return true;
                }
                else if (GetState() == AIState.AI_PET_RETURN_ATTACKING)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_RETURN_ATTACKING, newTarget);
                    return true;
                }
                else if (GetState() == AIState.AI_PET_ATTACKMOVE_ATTACKING)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE_ATTACKING, newTarget);
                    return true;
                }
            }

            NetSetState(AIState.AI_PET_IDLE);
            return true;
        }

        public override void OnTauntBegin()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            var tauntTarget = GetTauntTarget();
            if (tauntTarget != null)
            {
                SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
            }
        }

    public override void OnTauntEnd()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            var tauntTarget = GetTauntTarget();
            if (tauntTarget != null)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, tauntTarget);
            }
            else
            {
                NetSetState(AIState.AI_PET_IDLE);
                TimerFindEnemiesCallback();
            }
        }

    public override void OnFearBegin()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
            SetStateAndMove(AIState.AI_FEARED, wanderPoint);
            TurnOffAutoAttack(StopReason.IMMEDIATELY);
            ResetAndStartTimer(TimerFearedCallback);
        }

    public override void OnFearEnd()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            StopTimer(TimerFearedCallback);
            NetSetState(AIState.AI_PET_IDLE);
            TimerFindEnemiesCallback();
        }

        private void TimerFearedCallback()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            var wanderPoint = MakeWanderPoint(GetFearLeashPoint(), FEAR_WANDER_DISTANCE);
            SetStateAndMove(AIState.AI_FEARED, wanderPoint);
        }

    public override void OnCanMove()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            NetSetState(AIState.AI_PET_IDLE);
            TimerFindEnemiesCallback();
        }

    public override void OnCanAttack()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            NetSetState(AIState.AI_PET_IDLE);
            TimerFindEnemiesCallback();
        }

        private void TimerScanDistanceCallback()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            var tempOwner = GetGoldRedirectTarget();
            if (tempOwner == null)
            {
                Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
                return;
            }

            var canMove = GetCanMove(Me);
            var distanceToOwner = DistanceBetweenObjects(Me, tempOwner);
            if (canMove && distanceToOwner > TELEPORT_DISTANCE)
            {
                SetActorPositionFromObject(Me, tempOwner);
                NetSetState(AIState.AI_PET_IDLE);
                return;
            }

            if (canMove && distanceToOwner > FAR_MOVEMENT_DISTANCE)
            {
                if (distanceToOwner > MINION_MAX_VISION_DISTANCE)
                {
                    SetLastPosPointWithObj(tempOwner);
                    SetStateAndMoveInPos(AIState.AI_PET_MOVE);
                }
                else
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_MOVE, tempOwner);
                }
                return;
            }

            var f = GetState();
            var bNoEnemiesNear = FindTargetInAcR() == null;
            if (f == AIState.AI_PET_IDLE && distanceToOwner > GetPetReturnRadius(Me) && bNoEnemiesNear)
            {
                SetStateAndCloseToTarget(AIState.AI_PET_RETURN, tempOwner);
                return;
            }

            if ((f == AIState.AI_PET_MOVE || f == AIState.AI_PET_RETURN || f == AIState.AI_PET_HARDRETURN) && distanceToOwner <= GetPetReturnRadius(Me))
            {
                NetSetState(AIState.AI_PET_IDLE);
                return;
            }

            if (!IsMoving() && f == AIState.AI_PET_HARDMOVE)
            {
                NetSetState(AIState.AI_PET_HARDIDLE);
                return;
            }

            if (f == AIState.AI_PET_SPAWNING && IsNetworkLocal())
            {
                NetSetState(AIState.AI_PET_IDLE);
            }
        }

        private void TimerFindEnemiesCallback()
        {
            if (GetState() == AIState.AI_HALTED)
            {
                return;
            }

            if (GetGoldRedirectTarget() == null)
            {
                Die(Me, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
                return;
            }

            var g = GetState();
            if (g == AIState.AI_PET_MOVE || g == AIState.AI_PET_HARDMOVE || g == AIState.AI_PET_HARDSTOP)
            {
                return;
            }

            if (g == AIState.AI_PET_IDLE || g == AIState.AI_PET_RETURN || g == AIState.AI_PET_ATTACKMOVE || g == AIState.AI_PET_HARDIDLE)
            {
                var newTarget = FindTargetInAcRUsingGoldRedirectTarget();
                if (newTarget == null)
                {
                    TurnOffAutoAttack(StopReason.TARGET_LOST);
                    return;
                }

                if (g == AIState.AI_PET_IDLE)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_ATTACK, newTarget);
                }
                else if (g == AIState.AI_PET_RETURN)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_RETURN_ATTACKING, newTarget);
                }
                else if (g == AIState.AI_PET_ATTACKMOVE)
                {
                    SetStateAndCloseToTarget(AIState.AI_PET_ATTACKMOVE_ATTACKING, newTarget);
                }
                else if (g == AIState.AI_PET_HARDIDLE)
                {
                    NetSetState(AIState.AI_PET_HARDIDLE_ATTACKING);
                }
            }

            if (g == AIState.AI_PET_ATTACK || g == AIState.AI_PET_HARDATTACK || g == AIState.AI_PET_RETURN_ATTACKING || g == AIState.AI_PET_ATTACKMOVE_ATTACKING ||
                g == AIState.AI_PET_HARDIDLE_ATTACKING || g == AIState.AI_TAUNTED)
            {
                if (TargetInAttackRange())
                {
                    TurnOnAutoAttack(GetTarget());
                }
                else if (!TargetInCancelAttackRange())
                {
                    TurnOffAutoAttack(StopReason.MOVING);
                }
            }
        }

    public override void HaltAI()
        {
            StopTimer(TimerScanDistanceCallback);
            StopTimer(TimerFindEnemiesCallback);
            StopTimer(TimerFearedCallback);
        }

        // Additional helper methods (e.g., DistanceBetweenObjects, GetState, SetState, etc.) would go here.



}

