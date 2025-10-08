using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class ChampKillPointsClass : OdinLayout 
{


     public bool ChampKillPoints(
                out float __ChaosScore,
      out float __OrderScore,
    AttackableUnit Attacker,
    float ChaosScore,
    float OrderScore,
    float ScoringFloor,
    AttackableUnit Victim
          )
      {
        float _ChaosScore = ChaosScore;
        float _OrderScore = OrderScore;
        var changeScoreFlatCap = new ChangeScoreFlatCapClass();
bool result = 
            // Sequence name :Sequence
            (
                  GetUnitTeam(
                        out KillerTeam, 
                        Attacker) &&
                  changeScoreFlatCap.ChangeScoreFlatCap(
                        out _OrderScore, 
                        out _ChaosScore, 
                        out ScoreChanged, 
                        OrderScore, 
                        ChaosScore, 
                        false,
                        ScoringFloor,
                        2, 
                        KillerTeam) &&
                  ScoreChanged == true &&
                  // Sequence name :Selector
                  (
                        // Sequence name :Sequence
                        (
                              KillerTeam == TeamId.TEAM_ORDER &&
                              PlayFloatingTextOnUnitForTeam(
                                    Victim,
                                    "game_floating_friendly_kill", 
                                    TeamId.TEAM_ORDER, 
                                    -2) &&
                              PlayFloatingTextOnUnitForTeam(
                                    Victim,
                                    "game_floating_enemy_kill", 
                                    TeamId.TEAM_CHAOS, 
                                    -2)
                        ) ||
                        // Sequence name :Sequence
                        (
                              KillerTeam == TeamId.TEAM_CHAOS &&
                              PlayFloatingTextOnUnitForTeam(
                                    Victim,
                                    "game_floating_friendly_kill", 
                                    TeamId.TEAM_CHAOS, 
                                    -2) &&
                              PlayFloatingTextOnUnitForTeam(
                                    Victim,
                                    "game_floating_enemy_kill", 
                                    TeamId.TEAM_ORDER, 
                                    -2)

                        )
                  )
            );
        __ChaosScore = _ChaosScore;
        __OrderScore = _OrderScore;
        return result;

      }
}

