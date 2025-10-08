using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees;


class OdinTowerCastAbility1Class : OdinLayout
{


    bool OdinTowerCastAbility1(
        AttackableUnit Self)
    {
        return
                    // Sequence name :OdinTowerCastAbility1

                    GetUnitAIAttackTarget(
                          out Target) &&
                          // Sequence name :Get Cast Range and Cast or Move

                          GetUnitSpellCastRange(
                                out CastRange,
                                Self,
                                SPELLBOOK_CHAMPION,
                                1) &&
                          GreaterEqualFloat(
                                CastRange,
                                300) &&
                          SetVarFloat(
                                out CastRange,
                                300) &&
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
                                            1) &&
                                      CastUnitSpell(
                                            Self,
                                            SPELLBOOK_CHAMPION,
                                            1
                                            )




              ;
    }
}

