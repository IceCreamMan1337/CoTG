using CoTG.CoTGServer.Scripting.CSharp.Converted;
using CoTGEnumNetwork;

namespace AIScripts;
//Status: Modified script, not like original
public class TaskWanderAI : CAIScript
{
    // Variable declarations
    Vector3 position = new(0, 0, 0);
    AIState stateMove;
    float stateStop = 0;
    AIState currentState;
    Vector2 wanderPoint;
    long lastUpdateTime = 0;

    // UpdatePriority method
    void UpdatePriority(AttackableUnit creature)
    {
        //  creature.Priority = 0.001f;
    }

    // BeginTask method
    void BeginTask(AttackableUnit creature)
    {
        TurnOffAutoAttack(StopReason.TARGET_LOST);
    }

    // Main Tick method
    void Tick(AttackableUnit creature)
    {
        // Check time elapsed since last update
        long currentTime = DateTime.Now.Ticks;

        if (lastUpdateTime < currentTime)
        {
            // Update time and generate a wandering point
            lastUpdateTime = currentTime;
            Random rand = new();
            int randomValue = rand.Next(2, 5);
            lastUpdateTime += randomValue;

            // Generate a wandering point and update
            Vector3 currentPos = GetPos(Me);
            wanderPoint = MakeWanderPoint(currentPos.ToVector2(), 250);
        }

        // Check current state and handle movements
        currentState = GetState();

        if (currentState == AIState.AI_MOVE)
        {
            float distance = Vector2.Distance(wanderPoint, GetPos(Me).ToVector2());
            if (distance < 80)
            {
                StopMove(Me);
                SetState(AIState.AI_STOP);
            }
        }

        // If state is stop, check distance to resume movement
        if (currentState == AIState.AI_STOP)
        {
            float distance = Vector2.Distance(wanderPoint, GetPos(Me).ToVector2());
            if (distance >= 80)
            {
                SetStateAndMove(AIState.AI_MOVE, wanderPoint);
            }
        }
    }

    // Function to handle target loss
    void OnTargetLost(AttackableUnit creature)
    {
        // Empty function for target loss handling
    }

}

