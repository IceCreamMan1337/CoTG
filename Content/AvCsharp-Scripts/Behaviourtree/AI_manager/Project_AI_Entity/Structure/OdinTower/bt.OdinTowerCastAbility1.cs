using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;

/*
class OdinTowerCastAbility1 : AI_Characters 
{
      AttackableUnit Self,

     public bool OdinTowerCastAbility1()
      {
      return
            // Sequence name :OdinTowerCastAbility1
            (
                  GetUnitAIAttackTarget(
                        out Target, 
                        out Target) &&
                  // Sequence name :Get Cast Range and Cast or Move
                  (
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
                        (
                              // Sequence name :Cast
                              (
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
                                          1, 
                                          , 
                                          )

                              )
                        )
                  )
            ),
      }
}

*/