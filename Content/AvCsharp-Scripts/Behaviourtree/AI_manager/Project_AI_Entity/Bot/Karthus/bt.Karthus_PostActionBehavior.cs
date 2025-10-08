using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Karthus_PostActionBehaviorClass : AI_Characters 
{
      

     public bool Karthus_PostActionBehavior(
         AttackableUnit Self,
      string ActionPerformed
         )
      {
        return
              // Sequence name :Sequence
              (
                    NotEqualString(
                          ActionPerformed,
                          "KillChampion") &&
                    TestUnitHasBuff(
                          Self,
                          default,
                          "Defile",
                          true) &&
                    GetUnitPosition(
                          out SelfPosition,
                          Self) &&
                    GetUnitSpellRadius(
                          out DefileRadius,
                          Self,
                          SPELLBOOK_CHAMPION,
                          2) &&
                    CountUnitsInTargetArea(
                          out EnemiesInDefileRange,
                          Self,
                          SelfPosition,
                          DefileRadius,
                          AffectEnemies | AffectHeroes,
                          "") &&
                    EnemiesInDefileRange == 0 &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_CHAMPION,
                          2,
                          true) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_CHAMPION,
                          2,
                          default,
                          default
                          )

              );
      }
}

