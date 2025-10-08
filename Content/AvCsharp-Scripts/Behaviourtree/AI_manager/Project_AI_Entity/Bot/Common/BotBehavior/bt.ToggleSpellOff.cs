using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class ToggleSpellOffClass : AI_Characters 
{
    

     public bool ToggleSpellOff(
           AttackableUnit Self,
      int SpellToToggleOff
         )
      {
        return
              // Sequence name :Sequence
              (
                    GreaterEqualInt(
                          SpellToToggleOff,
                          0) &&
                    TestUnitSpellToggledOn(
                          Self,
                          SpellToToggleOff) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_CHAMPION,
                          SpellToToggleOff,
                            default
                          , default
  
                          )

              );
      }
}

