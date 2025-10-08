using System.Collections.Generic;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Inventory
{
    public class TalentInventory
    {
        public Dictionary<string, Talent> Talents { get; } = new(80);
        private static ILog _logger = LoggerProvider.GetLogger();

        public void Add(TalentData data, byte level)
        {
            if (level > 0 && !Talents.TryAdd(data.Id, new Talent(data, level)))
            {
                _logger.Warn($"Talent {data.Id} cannot not be added to Inventory twice!");
            }
        }

        public void Activate(ObjAIBase owner)
        {
            foreach (var talent in Talents.Values)
            {
                talent.ScriptInternal.OnActivate(owner, talent.Rank);
            }
        }
    }
}