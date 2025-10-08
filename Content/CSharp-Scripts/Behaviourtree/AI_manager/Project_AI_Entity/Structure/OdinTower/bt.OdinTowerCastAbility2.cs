namespace BehaviourTrees.all;

/*
class OdinTowerCastAbility2 : AI_Characters 
{
      AttackableUnit Self,

     public bool OdinTowerCastAbility2()
      {
      return
            // Sequence name :OdinTowerCastAbility2
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
                              2) &&
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
                                          2) &&
                                    CastUnitSpell(
                                          Self, 
                                          SPELLBOOK_CHAMPION, 
                                          2, 
                                          , 
                                          )

                              )
                        )
                  )
            ),
      }
}

*/