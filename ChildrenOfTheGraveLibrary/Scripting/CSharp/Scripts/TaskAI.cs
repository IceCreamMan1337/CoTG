using System;
using System.Collections.Generic;
using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp
{
    public class AITask
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool Done { get; set; }
        public int? LaneID { get; set; }

        // Properties for AIManager
        public AITaskTopicType Topic { get; set; }
        public AttackableUnit TargetUnit { get; set; }  // TARGET of the task
        public Vector3 TargetLocation { get; set; }
        public int Index { get; set; }
        public LogicResultType Status { get; set; }

        // REMOVED: HeroID, StructureID (redundant since the task is assigned to the champion)

        public AITask()
        {
            Status = LogicResultType.RUNNING;
            Done = false;
        }

        public AITask(AITaskTopicType topic, AttackableUnit targetUnit = null, Vector3 targetLocation = default, int laneID = 0)
        {
            Topic = topic;
            TargetUnit = targetUnit;
            TargetLocation = targetLocation;
            LaneID = laneID;
            Status = LogicResultType.RUNNING;
            Done = false;
        }
    }

    public class AITaskManager
    {
        private Dictionary<string, Action> taskActions = new();

        public AITaskManager()
        {
            // Register actions for different tasks
            taskActions["AttackHero"] = () => AttackHero();
            taskActions["DefendStructure"] = () => DefendStructure();
        }

        public void ExecuteTask(string taskName)
        {
            if (taskActions.ContainsKey(taskName))
            {
                taskActions[taskName].Invoke();  // Execute the task action
            }
        }

        private void AttackHero()
        {
        }

        private void DefendStructure()
        {
        }
    }

}
