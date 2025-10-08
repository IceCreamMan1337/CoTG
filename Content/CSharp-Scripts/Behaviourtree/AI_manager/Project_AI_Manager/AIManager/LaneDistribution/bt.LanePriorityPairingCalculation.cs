namespace BehaviourTrees;


class LanePriorityPairingCalculationClass : AI_LaneDistribution
{


    public bool LanePriorityPairingCalculation(

      out float TopPairValue,
      out float MidPairValue,
      out float BotPairValue,
    float TopLanePriority,
    float MidLanePriority,
    float BotLanePriority,
    AttackableUnit ReferenceUnit
        )
    {
        float _TopPairValue = default;
        float _MidPairValue = default;
        float _BotPairValue = default;
        bool result =
                         // Sequence name :CalculatePairingValue

                         // Sequence name :CalculateTopLaneValue

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
                               out _TopPairValue,
                               TopLanePriority,
                               NormalizedDistanceValue)
                    &&
                         // Sequence name :CalculateMidLaneValue

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
                               out _MidPairValue,
                               MidLanePriority,
                               NormalizedDistanceValue)
                    &&
                         // Sequence name :CalculateBotLaneValue

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
                               out _BotPairValue,
                               BotLanePriority,
                               NormalizedDistanceValue)


             ;

        BotPairValue = _BotPairValue;
        MidPairValue = _MidPairValue;
        TopPairValue = _TopPairValue;
        return result;
    }
}

