using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class FindTargetByDistanceHealthScoreClass : AI_Characters
{
    private DistanceHealthScoreClass distanceHealthScore = new();
    private IsCrowdControlledClass isCrowdControlled = new();
    public bool FindTargetByDistanceHealthScore(
        out AttackableUnit _Target,
     out float _BestScore,
     AttackableUnit ReferenceUnit,
     float TargetAcquisitionRange
        )
    {
        AttackableUnit Target = default;
        float BestScore = default;
        bool result =
                  // Sequence name :AcquireNewTarget

                  SetVarFloat(
                        out BestScore,
                        -99999) &&
                  GetUnitPosition(
                        out ReferencePosition,
                        ReferenceUnit) &&
                  GetUnitsInTargetArea(
                        out TargetCollection,
                        ReferenceUnit,
                        ReferencePosition,
                        TargetAcquisitionRange,
                        AffectEnemies | AffectHeroes) &&
                        DebugAction("TargetCollection count " + TargetCollection.Count()) &&
                        ForEach(TargetCollection, Unit =>
                              // Sequence name :FindBestScoreTarget

                              TestUnitIsVisibleToTeam(
                                    ReferenceUnit,
                                    Unit,
                                    true) &&
                              DistanceBetweenObjectAndPoint(
                                    out Distance,
                                    Unit,
                                    ReferencePosition) &&
                              GetUnitCurrentHealth(
                                    out UnitHealth,
                                    Unit) &&
                                       DebugAction("DistanceHealthScore ") &&
                              distanceHealthScore.DistanceHealthScore(
                                    out UnitScore,
                                    UnitHealth,
                                    Distance) &&
                              isCrowdControlled.IsCrowdControlled(
                                    out _IsCrowdControlled,
                                    ReferenceUnit) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :CC_IncreasedSCore
                                    (
                                          _IsCrowdControlled == true &&
                                          MultiplyFloat(
                                                out UnitScore,
                                                UnitScore,
                                                1.35f)
                                    ) || MaskFailure()
                              ) &&
                              GreaterFloat(
                                    UnitScore,
                                    BestScore) &&
                              SetVarFloat(
                                    out BestScore,
                                    UnitScore) &&
                              SetVarAttackableUnit(
                                    out Target,
                                    Unit)
                            && DebugAction(" findtarget return true and score = " + BestScore)

                  ) &&
                  GreaterEqualFloat(
                        BestScore,
                        0)

            ;
        _Target = Target;
        _BestScore = BestScore;
        return result;
    }
}

