using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;
using System.Collections.Generic;

namespace AIScripts;
//Status: Modified script, not like original
public class TaskRetreatAI : CAIScript
{
    /*
    // Global variable declarations
    float basePriority = 1;
    float factor = 0.25f;
    float distance;
    float maxTravelDistance;
    float currentHP;
    float maxHP;
    float healthFactor;
    float regroupDistance;
    float currentTime;
    float positionX, positionY, positionZ, positionW;
    Dictionary<int, float> priorityHistory = new Dictionary<int, float>();

    // Implementation of the UpdatePriority method
    void UpdatePriority(Creature creature)
    {
        // Get regrouping position and calculate distance
        var regroupPos = GetRegroupPos();
        distance = GetDist(regroupPos);
        regroupDistance = regroupPos;

        // Get current position and extract coordinates
        var pos = GetPos();
        positionX = pos.Item1;
        positionY = pos.Item2;
        positionZ = pos.Item3;
        positionW = pos.Item4;

        // Calculate distance and square root
        distance = CalculateDistance(regroupDistance, positionX, positionY, positionZ, positionW);

        // Calculate maximum travel distance
        maxTravelDistance = GetMaxTravelDistSquared();
        maxTravelDistance = MathF.Sqrt(maxTravelDistance);

        // Calculate health factor (remaining health proportion)
        healthFactor = MathF.Max(distance / maxTravelDistance, 0);
        currentHP = GetHP() / GetMaxHP();
        currentHP = 1 - currentHP;

        // Apply health factor (limit to 0 if too low)
        healthFactor = MathF.Max(currentHP, 0);

        // If conditions are favorable, maximum priority
        if (healthFactor > 0.9f && currentHP > 0)
        {
            currentHP = 1;
        }

        // Calculate priority based on different factors
        float priorityAdjustment = factor * healthFactor;
        float remainingFactor = 1 - factor;
        priorityAdjustment = priorityAdjustment + remainingFactor;
        float finalPriority = basePriority * priorityAdjustment * currentHP;

        // Update creature priority
        creature.Priority = finalPriority;

        // Get current time (in ticks)
        currentTime = DateTime.Now.Ticks;

        // Check and update priority history
        foreach (var entry in priorityHistory)
        {
            int timestamp = entry.Key;
            float previousPriority = entry.Value;
            float elapsedTime = currentTime - 2;

            // If entry is obsolete, reset
            if (timestamp < elapsedTime)
            {
                priorityHistory[timestamp] = 0;
            }
            // If entry matches current time, update if necessary
            else if (timestamp == currentTime)
            {
                float currentHP = GetHP();
                if (previousPriority > currentHP)
                {
                    priorityHistory[timestamp] = previousPriority;
                }
            }
        }

        // If history is empty, add current priority
        if (priorityHistory.Count == 0)
        {
            priorityHistory[currentTime] = currentHP;
        }

        // If too many entries are present, adjust priority
        if (priorityHistory.Count > 110)
        {
            creature.Priority = 1;
        }
    }

    // Helper method to calculate distance
    float CalculateDistance(float startX, float startY, float endX, float endY, float endZ)
    {
        // Replace this line with the actual distance calculation
        return MathF.Sqrt(MathF.Pow(endX - startX, 2) + MathF.Pow(endY - startY, 2) + MathF.Pow(endZ, 2));
    }


    // Implementation of the BeginTask method
    void BeginTask(Creature c)
    {
        var regroupPos = GetRegroupPos(); // Replace GetRegroupPos() with the appropriate method
        SetStateAndMove(0, regroupPos); // Replace SetStateAndMove() with the appropriate method
    }

    // Implementation of the Tick method
    void Tick(Creature c)
    {
        // No specific action here in the original Lua version
    }

    // Implementation of the OnTargetLost method
    void OnTargetLost(Creature c)
    {
        // No specific action here in the original Lua version
    }
    */

}

