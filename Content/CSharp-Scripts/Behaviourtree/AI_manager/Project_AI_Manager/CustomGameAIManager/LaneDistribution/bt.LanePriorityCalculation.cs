/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class LanePriorityCalculationClass : BehaviourTree 
{


      bool LanePriorityCalculation(
          out float LanePriority,
    float LanePresenceIndicator)
      {
      return
            // Sequence name :Calculate_Lane_Priority
            (
                  // Sequence name :LanePresencePositive
                  (
                        GreaterEqualFloat(
                              LanePresenceIndicator, 
                              0) &&
                        InterpolateLine(
                              out LanePriority, 
                              0, 
                              1, 
                              0, 
                              0.05, 
                              0, 
                              1, 
                              LanePresenceIndicator) &&
                        MinFloat(
                              out LanePriority, 
                              0.05, 
                              LanePriority)
                  ) ||
                  // Sequence name :LanePresenceNegative
                  (
                        MultiplyFloat(
                              out temp, 
                              LanePresenceIndicator, 
                              -1) &&
                        InterpolateLine(
                              out LanePriority, 
                              0, 
                              1, 
                              0, 
                              1, 
                              0, 
                              1, 
                              temp)

                  )
            );
      }
}

*/