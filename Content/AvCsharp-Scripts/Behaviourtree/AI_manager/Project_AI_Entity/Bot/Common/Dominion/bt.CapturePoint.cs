using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class CapturePointClass : AI_Characters 
{
     

     public bool CapturePoint(
          out string _ActionPerformed,
      AttackableUnit Self,
      float CurrentGameTime,
      float LastUseSpellTime
         )
      {
        string ActionPerformed = default;
        bool result =
            // Sequence name :Sequence
            (
                  // Sequence name :NotAlreadyCapturingPoint
                  (
                        TestUnitHasBuff(
                              Self,
                              default,
                              "OdinCaptureChannel",
                              false)

                        || TestUnitIsChanneling(
                              Self,
                              false)
                  ) &&
                  SubtractFloat(
                        out TimePassed,
                        CurrentGameTime,
                        LastUseSpellTime) &&
                  GreaterEqualFloat(
                        TimePassed,
                        4) &&
                  GetUnitAITaskPosition(
                        out TaskPosition) &&
                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitsInTargetArea(
                        out CapturePoints,
                        Self,
                        SelfPosition,
                        2500,
                        AffectEnemies| AffectMinions| AffectNeutral| AffectUseable) &&
                  GetCollectionCount(
                        out CapturePointCount,
                        CapturePoints) &&
                  GreaterInt(
                        CapturePointCount,
                        0) &&
                  ForEach(CapturePoints,Point => (           
                  // Sequence name :Sequence
                        (
                              GetUnitBuffCount(
                                    out Count,
                                    Point,
                                    "OdinGuardianStatsByLevel") &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :CapturePoint
                                    (
                                          GreaterInt(
                                                Count,
                                                0) &&
                                          DistanceBetweenObjectAndPoint(
                                                out Distance,
                                                Point,
                                                TaskPosition) &&
                                          LessFloat(
                                                Distance,
                                                500)
                                    ) ||
                                    // Sequence name :CenterRelic
                                    (
                                          TestEntityDifficultyLevel(
                                                false,
                                             EntityDiffcultyType.   DIFFICULTY_NEWBIE) &&
                                          Count == 0 &&
                                          GetUnitHealthRatio(
                                                out HealthRatio,
                                                Self) &&
                                          GreaterFloat(
                                                HealthRatio,
                                                0.4f) &&
                                          GetUnitPosition(
                                                out RelicPosition,
                                                Point) &&
                                          CountUnitsInTargetArea(
                                                out FriendlyChampCount,
                                                Self,
                                                RelicPosition,
                                                300,
                                                AffectFriends | AffectHeroes | NotAffectSelf) &&
                                          FriendlyChampCount == 0 &&
                                          CountUnitsInTargetArea(
                                                out EnemyChampCount,
                                                Self,
                                                SelfPosition,
                                                1000,
                                                AffectEnemies | AffectHeroes) &&
                                          EnemyChampCount == 0
                                    )
                              ) &&
                              TestCanUseObject(
                                    Self,
                                    Point) &&
                              IssueUseObjectOrder(
                                    Point)
                        ) )
                  ) &&
                  SetVarString(
                        out ActionPerformed,
                        "CapturePoint")

            );
        _ActionPerformed = ActionPerformed;
        return result;
      }
}

