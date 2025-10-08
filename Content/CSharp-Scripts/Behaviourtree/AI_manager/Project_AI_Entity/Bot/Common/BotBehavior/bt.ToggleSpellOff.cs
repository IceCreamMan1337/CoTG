using static CoTGEnumNetwork.Enums.SpellbookType;

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

              ;
    }
}

