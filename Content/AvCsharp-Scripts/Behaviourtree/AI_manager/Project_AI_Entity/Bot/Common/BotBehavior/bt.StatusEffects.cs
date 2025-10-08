using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class StatusEffectsClass : AI_Characters 
{
     

     public bool StatusEffects(
          AttackableUnit Self
         )
      {
        return
              // Sequence name :Selector
              (
                    TestUnitHasBuff(
                          Self,
                          default,
                          "Move",
                          true) ||
                          TestUnitHasBuff(
                          Self,
                          default,
                          "MoveAway",
                          true) ||
                          TestUnitHasBuff(
                          Self,
                          default,
                          "MoveAwayCollision",
                          true)

              );
      }
}

