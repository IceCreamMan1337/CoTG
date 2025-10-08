using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AIScripts;
//Status: Modified script, not like original
public class BotAI : CAIScript
{
 /*   object activeTask;
    List<Task> tasks;

    void OnLostTarget()
    {
        if (activeTask != null && GetMetaTable(activeTask).OnTargetLost != null)
        {
            GetMetaTable(activeTask).OnTargetLost(activeTask);
        }
    }

    AITask LoadTask(AITask path)
    {

        var task = LoadScript(path);
        task.__index = task;
        return task;
    }

   public void OnLevelUp()
    {
        UpgradeSpellsInOrder(4, 2, 3, 1);
    }

    public void OnInit()
    {
        InitTimer(TimerHackDelayedInit, 1, false);
    }

    public void TimerHackDelayedInit()
    {
        List<int> laneIDs = new List<int> { 0, 1, 2 };
        tasks = new List<Task>();

        // Define and initialize specific tasks
        tasks.Add(CreateTask(CastSpell()));
        tasks.Add(CreateTask(Retreat()));
        tasks.Add(CreateTask(KillLowHPMinion()));
        tasks.Add(CreateTask(KillNearbyStruct()));

        foreach (var laneID in laneIDs)
        {
            var pushLaneTask = new AITask
            {
                LaneID = laneID,
                Name = "Push Lane " + laneID
            };
            SetMetaTable(pushLaneTask, LoadTask(TaskPushLaneAI(this)));
            tasks.Add(pushLaneTask);
        }

        List<int> structureIDs = GetStructureIDs();
        foreach (var structureID in structureIDs)
        {
            var defendTask = new Task
            {
                StructureID = structureID,
                Name = "Defend " + GetObjectName(GetObject(structureID))
            };
            SetMetaTable(defendTask, LoadTask(TaskDefendStructure));
            tasks.Add(defendTask);
        }

        var buyItemTask = CreateTask("Buy Item");
        tasks.Add(buyItemTask);

        List<int> heroIDs = GetHeroIDs();
        foreach (var heroID in heroIDs)
        {
            var killHeroTask = new Task
            {
                HeroID = heroID,
                Name = "Kill Hero " + GetObjectName(heroID)
            };
            SetMetaTable(killHeroTask, LoadTask("TaskKillHero.lua"));
            tasks.Add(killHeroTask);
        }
    }

    public void AntiKiteTimer()
    {
        if (activeTask is AITask task && task.HeroID != null && GetMetaTable(activeTask).AntiKiteTimer != null)
        {
            GetMetaTable(activeTask).AntiKiteTimer(activeTask);
        }
    }

    public void TimerUpdate()
    {
        Task highestPriorityTask = tasks[0];

        for (int i = tasks.Count - 1; i >= 0; i--)
        {
            tasks[i].UpdatePriority();

            if (tasks[i].Done)
            {
                if (activeTask == tasks[i])
                {
                    activeTask = null;
                }

                SetDebugSlotText(tasks.Count, "", 0);
                tasks.RemoveAt(i);
            }
            else if (tasks[i].Priority > highestPriorityTask.Priority)
            {
                highestPriorityTask = tasks[i];
            }
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            int colorIndex = tasks[i] == highestPriorityTask ? 1 : 0;
            SetDebugSlotText(i + 1, tasks[i].Name + " : " + tasks[i].Priority, colorIndex);
        }

        if (activeTask != highestPriorityTask)
        {
            activeTask = highestPriorityTask;
        }
    }

    */
}

