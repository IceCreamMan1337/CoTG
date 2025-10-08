using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees;


class PersonalScore_MinionKillClass : OdinLayout
{


    public bool PersonalScore_MinionKill(
         AttackableUnit DeadUnit,
   AttackableUnit Killer
         )
    {
        return
                    // Sequence name :Sequence

                    GetUnitType(
                          out DeadUnitType,
                          DeadUnit) &&
                    DeadUnitType == MINION_UNIT &&
                    GetUnitType(
                          out KillerUnitType,
                          Killer) &&
                    KillerUnitType == HERO_UNIT &&
                    GetSquadNameOfUnit(
                          out SquadName,
                          DeadUnit) &&
                    NotEqualString(
                          SquadName,
                          "") &&
                    IncrementPlayerScore(
                          Killer,
                         ScoreCategory.Combat,
                         ScoreEvent.MinionKill,
                          2,
                          false)

              ;
    }
}

