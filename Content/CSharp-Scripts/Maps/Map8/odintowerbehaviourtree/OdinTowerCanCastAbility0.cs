using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees;


class OdinTowerCanCastAbility0Class : OdinLayout
{


    public bool OdinTowerCanCastAbility0(
         AttackableUnit Self)
    {
        return
                    // Sequence name :OdinTowerCanCastAbility0

                    GetSpellSlotCooldown(
                          out Spell0Cooldown,
                          Self,
                          SPELLBOOK_CHAMPION,
                          0) &&
                    LessEqualFloat(
                          Spell0Cooldown,
                          0)

              ;
    }
}

