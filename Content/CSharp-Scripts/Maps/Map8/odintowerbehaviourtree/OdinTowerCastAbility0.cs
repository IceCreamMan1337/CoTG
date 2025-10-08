using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees;


class OdinTowerCastAbility0Class : OdinLayout
{


    public bool OdinTowerCastAbility0(
         AttackableUnit Self
         )
    {
        return
                    // Sequence name :OdinTowerCastAbility0

                    GetUnitAIAttackTarget(
                          out Target) &&
                          // Sequence name :Get Cast Range and Cast or Move

                          GetUnitSpellCastRange(
                                out CastRange,
                                Self,
                                SPELLBOOK_CHAMPION,
                                0) &&
                                      // Sequence name :Cast or Move

                                      // Sequence name :Cast

                                      GetDistanceBetweenUnits(
                                            out DistanceBetweenSelfAndTarget,
                                            Self,
                                            Target) &&
                                      LessEqualFloat(
                                            DistanceBetweenSelfAndTarget,
                                            CastRange) &&
                                      SetUnitAISpellTarget(
                                            Target,
                                            0) &&
                                      CastUnitSpell(
                                            Self,
                                            SPELLBOOK_CHAMPION,
                                            0,
                                            1,
                                            false)




              ;
    }
}

