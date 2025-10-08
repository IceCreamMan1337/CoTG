using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class OdinTowerCanCastAbility0Class : OdinLayout 
{
     

      public bool OdinTowerCanCastAbility0(
           AttackableUnit Self)
      {
      return
            // Sequence name :OdinTowerCanCastAbility0
            (
                  GetSpellSlotCooldown(
                        out Spell0Cooldown, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        0) &&
                  LessEqualFloat(
                        Spell0Cooldown, 
                        0)

            );
      }
}

