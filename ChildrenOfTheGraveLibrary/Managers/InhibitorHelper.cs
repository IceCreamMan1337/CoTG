using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using log4net;

namespace ChildrenOfTheGraveLibrary.Managers
{
    internal static class InhibitorHelper
    {
        static ILog _logger = LoggerProvider.GetLogger();
        internal static Lane FindLaneOfInhibitor(Inhibitor inhibitor)
        {
            if (inhibitor.Name.Contains("_L"))
            {
                return Lane.LANE_L;
            }
            if (inhibitor.Name.Contains("_C"))
            {
                return Lane.LANE_C;
            }
            if (inhibitor.Name.Contains("_R"))
            {
                return Lane.LANE_R;
            }

            _logger.Warn($"Inhibitor is badly named! {inhibitor.Name}");
            return Lane.LANE_Unknown;
        }

        internal static Barrack GetLinkedEnemyBarrack(Inhibitor inhibitor)
        {
            if (inhibitor.Team is not TeamId.TEAM_CHAOS or TeamId.TEAM_ORDER)
            {
                _logger.Error("Inhibitor must be on Team Chaos or Team Order!");
                return null;
            }

            return Barrack.GetBarrack(inhibitor.Team is not TeamId.TEAM_ORDER ? TeamId.TEAM_ORDER : TeamId.TEAM_CHAOS, inhibitor.Lane);
        }

        internal static Barrack GetLinkedBarrack(Inhibitor inhibitor)
        {
            return Barrack.GetBarrack(inhibitor.Team, inhibitor.Lane);
        }
    }
}
