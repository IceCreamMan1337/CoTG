using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class GetSpellCastDelayClass : AI_Characters 
{
     

     public bool GetSpellCastDelay(
          out float CastTimeThreshold
         )
      {
        return
              // Sequence name :CalculateTheTimeBeforeNextSpell
              (
                    SetVarFloat(
                          out CastTimeThreshold,
                          0) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :Easy
                          (
                                TestEntityDifficultyLevel(
                                      true,
                                    EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                GenerateRandomFloat(
                                      out CastTimeThreshold,
                                      2,
                                      3)

                          )
                    )
                    ||
                    MaskFailure()
              );
      }
}

