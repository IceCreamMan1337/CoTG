using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DominionNinjaCapturePointClass : AI_Characters 
{
      

     public bool DominionNinjaCapturePoint(
         out string _ActionPerformed,
      AttackableUnit Self,
      float LastUseSpellTime,
      float CurrentGameTime)
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
                          ||
                          TestUnitIsChanneling(
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
                    CountUnitsInTargetArea(
                          out ChampionCount,
                          Self,
                          TaskPosition,
                          1200,
                          AffectEnemies | AffectHeroes,
                          "") &&
                    LessEqualInt(
                          ChampionCount,
                          0) &&
                    GetUnitsInTargetArea(
                          out CapturePoints,
                          Self,
                          TaskPosition,
                          1200,
                          AffectEnemies | AffectMinions | AffectNeutral | AffectUseable) &&
                    ForEach(CapturePoints, Point => (
                          // Sequence name :Sequence
                          (
                                GetUnitBuffCount(
                                      out Count,
                                      Point,
                                      "OdinGuardianStatsByLevel") &&
                                GreaterInt(
                                      Count,
                                      0)
                          ))
                    ) &&
                    GetDistanceBetweenUnits(
                          out Distance,
                          Point,
                          Self) &&
                    LessFloat(
                          Distance,
                          500) &&
                    TestCanUseObject(
                          Self,
                          Point) &&
                    IssueUseObjectOrder(
                          Point) &&
                    SetVarString(
                          out ActionPerformed,
                          "NinjaCapturePoint")

              );

         _ActionPerformed = ActionPerformed;
        return result;

    }
}

