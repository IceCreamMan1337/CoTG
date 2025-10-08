using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SwainPostActionBehaviorClass : AI_Characters 
{
     private ToggleSpellOffClass toggleSpellOff = new ToggleSpellOffClass();

     public bool SwainPostActionBehavior(AttackableUnit Self,
      string ActionPerformed)
      {
        return
              // Sequence name :RavenousFlockToggleOff
              (
                    NotEqualString(
                          ActionPerformed,
                          "KillChampion") &&
                    GetUnitSpellLevel(
                          out SpellLevel,
                          Self,
                          SPELLBOOK_CHAMPION,
                          3) &&
                    GreaterInt(
                          SpellLevel,
                          0) &&
                    // Sequence name :ToggleOffConditions
                    (
                          // Sequence name :NoValidTarget
                          (
                                GetUnitPosition(
                                      out SelfPosition,
                                      Self) &&
                                CountUnitsInTargetArea(
                                      out UnitsNearby,
                                      Self,
                                      SelfPosition,
                                      700,
                                      AffectEnemies | AffectHeroes | AffectMinions | AffectNeutral,
                                      "") &&
                                UnitsNearby == 0 &&
                                TestUnitSpellToggledOn(
                                      Self,
                                      3) &&
                               toggleSpellOff.ToggleSpellOff(
                                      Self,
                                      3)
                          ) ||
                          // Sequence name :Health&gt,60%Mana&lt,20%
                          (
                                GetUnitPosition(
                                      out SelfPosition,
                                      Self) &&
                                GetUnitHealthRatio(
                                      out HP_Ratio,
                                      Self) &&
                                GreaterFloat(
                                      HP_Ratio,
                                      0.6f) &&
                                GetUnitPARRatio(
                                      out SelfPAR_Ratio,
                                      Self,
                                       PrimaryAbilityResourceType.MANA) &&
                                LessFloat(
                                      SelfPAR_Ratio,
                                      0.2f) &&
                                TestUnitSpellToggledOn(
                                      Self,
                                      3) &&
                                  toggleSpellOff.ToggleSpellOff(
                                      Self,
                                      3)
                          ) ||
                          // Sequence name :Health&gt,80%
                          (
                                GetUnitHealthRatio(
                                      out HP_Ratio,
                                      Self) &&
                                GreaterFloat(
                                      HP_Ratio,
                                      0.8f) &&
                                TestUnitSpellToggledOn(
                                      Self,
                                      3) &&
                                toggleSpellOff.ToggleSpellOff(
                                      Self,
                                      3)

                          )
                    )
              );
      }
}

