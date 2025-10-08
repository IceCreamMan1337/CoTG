using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
namespace BehaviourTrees;


class GetDefendersAtPointClass : AImission_bt
{


     public bool GetDefendersAtPoint(

                out int NumDefenders,
      out float StrOfDef,
      out float DefenderStrength_Normalized,
    int CapturePointIndex,
    AttackableUnit ReferenceAttacker,
    AttackableUnit CapturePoint)
    {
        int _NumDefenders = 0;
      float _StrOfDef = 0;
        float _DefenderStrength_Normalized = 0;

        var getNearestCapturePoint = new GetNearestCapturePointClass();
        var strengthEvaluation_ChampionHealth = new StrengthEvaluation_ChampionHealthClass();

        bool result = // Sequence name :Sequence
        (
                  GetUnitTeam(
                        out AttackerTeam, 
                        ReferenceAttacker) &&
                  SetVarInt(
                        out NumDefenders, 
                        0) &&
                  SetVarFloat(
                        out StrOfDef, 
                        0) &&
                  SetVarFloat(
                        out DefenderStrength_Normalized, 
                        0) &&
                  GetChampionCollection(
                        out AllChampions) &&
                  AddInt(
                        out Lane0, 
                        CapturePointIndex, 
                        2) &&
                  AddInt(
                        out Lane1, 
                        CapturePointIndex, 
                        3) &&
                  AddInt(
                        out Lane2, 
                        CapturePointIndex, 
                        7) &&
                  AddInt(
                        out Lane3, 
                        CapturePointIndex, 
                        8) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              GreaterInt(
                                    Lane2, 
                                    10) &&
                              SubtractInt(
                                    out Lane2, 
                                    Lane2, 
                                    10)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              GreaterInt(
                                    Lane3, 
                                    10) &&
                              SubtractInt(
                                    out Lane3, 
                                    Lane3, 
                                    10)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  ForEach(AllChampions,Champion => (
                        // Sequence name :Sequence
                        (
                              GetUnitTeam(
                                    out ChampionTeam, 
                                    Champion) &&
                              NotEqualUnitTeam(
                                    ChampionTeam, 
                                    AttackerTeam) &&
                              TestUnitCondition(
                                    Champion) &&
                              strengthEvaluation_ChampionHealth.StrengthEvaluation_ChampionHealth(
                                    out ModifiedChampionValue, 
                                    Champion, 
                                    25) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :IsItNearCapturePoint
                                    (
                                          getNearestCapturePoint.GetNearestCapturePoint(
                                                out ClosestCapturePoint, 
                                                out ClosestCapturePointPosition, 
                                                out ClosestDistance, 
                                                Champion) &&
                                          LessFloat(
                                                ClosestDistance, 
                                                1000) &&
                                          CapturePointIndex == ClosestCapturePoint &&
                                          AddInt(
                                                out _NumDefenders, 
                                                _NumDefenders, 
                                                1) &&
                                          GetUnitCurrentHealth(
                                                out ChampionHealth, 
                                                Champion) &&
                                          GetUnitMaxHealth(
                                                out ChampionMaxHealth, 
                                                Champion) &&
                                          DivideFloat(
                                                out ChampionHealthRatio, 
                                                ChampionHealth, 
                                                ChampionMaxHealth) &&
                                          AddFloat(
                                                out _StrOfDef, 
                                                _StrOfDef, 
                                                ChampionHealthRatio) &&
                                          AddFloat(
                                                out _DefenderStrength_Normalized, 
                                                _DefenderStrength_Normalized, 
                                                ModifiedChampionValue)
                                    ) ||
                                    // Sequence name :IsItNearLane
                                    (
                                          GetDistanceBetweenUnits(
                                                out DistanceToPoint, 
                                                CapturePoint, 
                                                Champion) &&
                                          LessEqualFloat(
                                                DistanceToPoint, 
                                                3250) &&
                                          GetUnitAIClosestLaneID(
                                                out LaneID, 
                                                Champion) &&
                                          // Sequence name :=AdjacentLane
                                          (
                                                Lane0 == LaneID                    
                                                ||Lane1 == LaneID
                                                ||Lane2 == LaneID
                                                ||Lane3 == LaneID
                                          ) &&
                                          GetUnitAIClosestLanePoint(
                                                out ClosestLanePoint, 
                                                Champion, 
                                                LaneID) &&
                                          DistanceBetweenObjectAndPoint(
                                                out DistanceToLane, 
                                                Champion, 
                                                ClosestLanePoint) &&
                                          LessEqualFloat(
                                                DistanceToLane, 
                                                350) &&
                                          AddInt(
                                                out _NumDefenders, 
                                                _NumDefenders, 
                                                1) &&
                                          GetUnitCurrentHealth(
                                                out ChampionHealth, 
                                                Champion) &&
                                          GetUnitMaxHealth(
                                                out ChampionMaxHealth, 
                                                Champion) &&
                                          DivideFloat(
                                                out ChampionHealthRatio, 
                                                ChampionHealth, 
                                                ChampionMaxHealth) &&
                                          AddFloat(
                                                out _StrOfDef, 
                                                _StrOfDef, 
                                                ChampionHealthRatio) &&
                                          AddFloat(
                                                out _DefenderStrength_Normalized, 
                                                _DefenderStrength_Normalized, 
                                                ModifiedChampionValue)

                                    )
                              )
                        ))
                  )
            );

        DefenderStrength_Normalized = _DefenderStrength_Normalized;
        StrOfDef = _StrOfDef;
        NumDefenders = _NumDefenders;
            return result;

      }
}

