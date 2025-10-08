/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class LanePriorityPairingCalculation : BehaviourTree 
{
      out float TopPairValue;
      out float MidPairValue;
      out float BotPairValue;
      float TopLanePriority;
      float MidLanePriority;
      float BotLanePriority;
      AttackableUnit ReferenceUnit;

      bool LanePriorityPairingCalculation()
      {
      return
            // Sequence name :CalculatePairingValue
            (
                  // Sequence name :CalculateTopLaneValue
                  (
                        GetUnitAIClosestLanePoint(
                              out ClosestPoint, 
                              ReferenceUnit, 
                              2) &&
                        DistanceBetweenObjectAndPoint(
                              out ClosestDistanceToLane, 
                              ReferenceUnit, 
                              ClosestPoint) &&
                        SubtractFloat(
                              out Temp, 
                              10000, 
                              ClosestDistanceToLane) &&
                        DivideFloat(
                              out NormalizedDistanceValue, 
                              Temp, 
                              10000) &&
                        MinFloat(
                              out NormalizedDistanceValue, 
                              NormalizedDistanceValue, 
                              1) &&
                        MultiplyFloat(
                              out TopPairValue, 
                              TopLanePriority, 
                              NormalizedDistanceValue)
                  ) &&
                  // Sequence name :CalculateMidLaneValue
                  (
                        GetUnitAIClosestLanePoint(
                              out ClosestPoint, 
                              ReferenceUnit, 
                              1) &&
                        DistanceBetweenObjectAndPoint(
                              out ClosestDistanceToLane, 
                              ReferenceUnit, 
                              ClosestPoint) &&
                        SubtractFloat(
                              out Temp, 
                              10000, 
                              ClosestDistanceToLane) &&
                        DivideFloat(
                              out NormalizedDistanceValue, 
                              Temp, 
                              10000) &&
                        MinFloat(
                              out NormalizedDistanceValue, 
                              NormalizedDistanceValue, 
                              1) &&
                        MultiplyFloat(
                              out MidPairValue, 
                              MidLanePriority, 
                              NormalizedDistanceValue)
                  ) &&
                  // Sequence name :CalculateBotLaneValue
                  (
                        GetUnitAIClosestLanePoint(
                              out ClosestPoint, 
                              ReferenceUnit, 
                              0) &&
                        DistanceBetweenObjectAndPoint(
                              out ClosestDistanceToLane, 
                              ReferenceUnit, 
                              ClosestPoint) &&
                        SubtractFloat(
                              out Temp, 
                              10000, 
                              ClosestDistanceToLane) &&
                        DivideFloat(
                              out NormalizedDistanceValue, 
                              Temp, 
                              10000) &&
                        MinFloat(
                              out NormalizedDistanceValue, 
                              NormalizedDistanceValue, 
                              1) &&
                        MultiplyFloat(
                              out BotPairValue, 
                              BotLanePriority, 
                              NormalizedDistanceValue)

                  )
            );
      }
}

*/