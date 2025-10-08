using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class MinionAI : MinionLibAI
{
    const float MAX_ENGAGE_DISTANCE = 2500;
    protected override float FEAR_WANDER_DISTANCE => 500;
    const float DELAY_FIND_ENEMIES = 0.25f;
    public int LastAttackScan = 0;

    // Load scripts (dofile equivalent in C# to include if necessary)
    // LoadMinionLibrary(); // Specific implementation according to your environment

    public override void OnTauntBegin()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
            StopTimer(TimerAntiKite);
        }
    }

    public override void OnTauntEnd()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var tauntTarget = GetTauntTarget();
        if (tauntTarget != null)
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, tauntTarget);
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            FindTargetOrMove();
        }
    }

    public override void TimerFindEnemies()
    {
        var state = GetState();

        if (state == AIState.AI_HALTED)
            return;

        if (state == AIState.AI_ATTACKMOVESTATE)
        {
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
            var tauntTarget = GetTauntTarget();

            if (tauntTarget != null)
            {
                SetStateAndCloseToTarget(AIState.AI_TAUNTED, tauntTarget);
            }
        }

        if (state == AIState.AI_ATTACKMOVE_ATTACKING)
        {
            var target = GetTarget();
            if (target == null || Vector2.Distance(Me.Position , target.Position) > MAX_ENGAGE_DISTANCE)
            {
                FindTargetOrMove();
            }
            else if (TargetInAttackRange())
            {
                if (state != AIState.AI_TAUNTED)
                {
                    ResetAndStartTimer(TimerAntiKite);
                }
                TurnOnAutoAttack(target);
            }
            else if (!TargetInCancelAttackRange())
            {
                TurnOffAutoAttack(StopReason.MOVING);
                LastAttackScan = 0;
            }
        }
    }

    public override void FindTargetOrMove()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        var target = FindTargetInAcR();
        if (target != null)
        {
            if (!LastAutoAttackFinished())
            {
                InitTimer(TimerFindEnemies, DELAY_FIND_ENEMIES, true);
                return;
            }

            SetStateAndCloseToTarget(AIState.AI_ATTACKMOVE_ATTACKING, target);
            ResetAndStartTimer(TimerAntiKite);
        }
        else
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
            StopTimer(TimerAntiKite);
            LastAttackScan = 0;
        }
    }

    public override void TimerMoveForward()
    {
        if (GetState() == AIState.AI_HALTED)
            return;

        if (GetState() == AIState.AI_IDLE)
        {
            FindTargetOrMove();
        }
        else if (GetState() == AIState.AI_ATTACKMOVESTATE)
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
            LastAttackScan = 0;
        }
    }

}

