namespace BehaviourTrees.all;

/*
class OdinTowerCastAbility0 : AI_Characters 
{
      AttackableUnit Self,

     public bool OdinTowerCastAbility0()
      {
      return
            // Sequence name :OdinTowerCastAbility0
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
                              0) &&
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
                                          0) &&
                                    CastUnitSpell(
                                          Self, 
                                          SPELLBOOK_CHAMPION, 
                                          0, 
                                          1, 
                                          false)

                              )
                        )
                  )
            ),
      }
}

*/