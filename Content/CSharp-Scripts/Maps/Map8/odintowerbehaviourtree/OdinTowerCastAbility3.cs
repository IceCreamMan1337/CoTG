using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees;


class OdinTowerCastAbility3Class : OdinLayout
{


    bool OdinTowerCastAbility3(
            AttackableUnit Self
        )
    {
        return
                    // Sequence name :OdinTowerCastAbility3

                    GetUnitAIAttackTarget(
                          out Target) &&
                          // Sequence name :Get Cast Range and Cast or Move

                          GetUnitSpellCastRange(
                                out CastRange,
                                Self,
                                SPELLBOOK_CHAMPION,
                                3) &&
                          SetVarFloat(
                                out CastRange,
                                600) &&
                                      // Sequence name :Cast or Move

                                      // Sequence name :Cast

                                      GetDistanceBetweenUnits(
                                            out DistanceBetweenSelfAndTarget,
                                            Self,
                                            Target) &&
                                      LessEqualFloat(
                                            DistanceBetweenSelfAndTarget,
                                            CastRange) &&
                                      CastUnitSpell(
                                            Self,
                                            SPELLBOOK_CHAMPION,
                                            3
                                            )




              ;
    }
}

