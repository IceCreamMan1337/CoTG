using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class TaskPushLaneAI : AITask
{
    /*
    // Distance initialization
    float MIN_FOLLOW_DIST_SQ = 5625f;
    float MAX_DIST_TO_LANE = 500f;
    float MIN_FIGHT_DIST_SQ = 22500f;
    float MAX_FIGHT_DIST_SQ = 90000f;
    bool allBarracksStarted = false;

    // Reference to AI (assuming this class exists in your project)
    private CAIScript aiScript;

    // Action to execute when the task is activated
    public override Action TaskAction { get; set; }

    public TaskPushLaneAI(CAIScript script)
    {
        aiScript = script;
        TaskAction = () => ExecutePushLaneTask();
    }

    // Execute the task to push the lane
    private void ExecutePushLaneTask()
    {
        // You can adapt the execution of different functions here
        var nearestMinion = FindNearestFriendlyMinion();
        if (nearestMinion != null)
        {
            UpdatePriority(nearestMinion);
        }
    }



    // Function to find the nearest friendly minion
    object FindNearestFriendlyMinion(object c)
    {
        var minions = GetMinions();
        var team = GetTeam();
        var laneID = c.LaneID;

        minions = minions(team, laneID); // Get team minions in the given lane

        float maxTravelDistSquared = GetMaxTravelDistSquared();
        object nearestMinion = null;
        float closestDistSquared = float.MaxValue;

        for (int i = team.StartIndex; i < team.EndIndex; i += team.Step)
        {
            var minion = minions[i];
            var distToLane = GetMinionDistanceToLane(minion);

            if (distToLane < MAX_DIST_TO_LANE)
            {
                var minionPos = GetPos(minion);
                var currentPos = GetPos();
                var distSquared = GetDistSquared(minionPos, currentPos);

                if (distSquared > MIN_FOLLOW_DIST_SQ && distSquared < maxTravelDistSquared)
                {
                    nearestMinion = minion;
                    closestDistSquared = distSquared;
                }
            }
        }

        return nearestMinion;
    }

    // Function to update minion priority
    void UpdatePriority(object c)
    {
        var nearestMinion = FindNearestFriendlyMinion(c);

        if (nearestMinion != null)
        {
            var position = GetPos(nearestMinion);
            SetStateAndMove(AI_SOFTATTACK, position);
        }
    }

    // Function to follow the closest minion
    void FollowClosestMinion(object c)
    {
        TurnOffAutoAttack(STOPREASON_TARGET_LOST);
        FollowClosestMinion(c);
    }

    // Function to check if a minion is close to the lane
    bool IsMinionCloseToLane(object c, object n)
    {
        var distToLane = GetMinionDistanceToLane(n);
        return distToLane < MAX_DIST_TO_LANE;
    }

    // Function to handle movement based on distance
    void Tick(object c)
    {
        var nearestMinion = FindNearestFriendlyMinion(c);

        if (nearestMinion != null)
        {
            var distToMinion = GetDistSquared(GetPos(c), GetPos(nearestMinion));

            if (!IsMoving())
            {
                if (distToMinion > MAX_FIGHT_DIST_SQ)
                {
                    FollowClosestMinion(c);
                }
            }
            else
            {
                if (distToMinion < MIN_FIGHT_DIST_SQ)
                {
                    StopMove();
                }
            }
        }
    }

    // Function executed when target is lost
    void OnTargetLost(object c)
    {
        TurnOffAutoAttack(STOPREASON_TARGET_LOST);
    }
    */
}

