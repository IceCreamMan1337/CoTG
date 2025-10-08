using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_DuelistClass : OdinLayout 
{
      

     public bool PersonalScore_Duelist(
          AttackableUnit Killer,
    IEnumerable<AttackableUnit> Assists,
    float Score_Duelist
          )
      {
      return
            // Sequence name :Sequence
            (
                  GetCollectionCount(
                        out TotalAssist, 
                        Assists) &&
                  LessEqualInt(
                        TotalAssist, 
                        0) &&
                  IncrementPlayerScore(
                        Killer, 
                       ScoreCategory.Combat,
                       ScoreEvent.Duelist,
                        Score_Duelist
                        )

            );
      }
}

