namespace BehaviourTrees.all;

/*
class OdinTowerCastAbility3 : AI_Characters 
{
      AttackableUnit Self,

     public bool OdinTowerCastAbility3()
      {
      return
            // Sequence name :OdinTowerCastAbility3
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
                              3) &&
                        SetVarFloat(
                              out CastRange, 
                              600) &&
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
                                    CastUnitSpell(
                                          Self, 
                                          SPELLBOOK_CHAMPION, 
                                          3, 
                                          , 
                                          )

                              )
                        )
                  )
            ),
      }
}

*/