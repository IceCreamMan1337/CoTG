using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class QuestRewardPointAwardingClass : OdinLayout 
{


     public bool QuestRewardPointAwarding(
                out float __ChaosScore,
      out float __OrderScore,
    float VictoryPointRewardOffense,
    float VictoryPointRewardDefense,
    int CapturePointID,
    TeamId TeamToReward,
    float OrderScore,
    float ChaosScore
          
          )
      {

        float _OrderScore = OrderScore;
        float _ChaosScore = ChaosScore;

        var changeScore = new ChangeScoreClass();

bool result = 
            // Sequence name :MaskFailure
            (
                  // Sequence name :Which_Point?
                  (
                        // Sequence name :Point=A
                        (
                              CapturePointID == 0 &&
                              // Sequence name :Which_Team?
                              (
                                    // Sequence name :Team=Order
                                    (
                                          TeamToReward == TeamId.TEAM_ORDER &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardDefense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_ORDER, 
                                                false)
                                    ) ||
                                    // Sequence name :Team=Chaos
                                    (
                                          TeamToReward == TeamId.TEAM_CHAOS &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardDefense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_CHAOS, 
                                                false)
                                    )
                              )
                        ) ||
                        // Sequence name :Point=B
                        (
                              CapturePointID == 1 &&
                              // Sequence name :Which_Team?
                              (
                                    // Sequence name :Team=Order
                                    (
                                          TeamToReward == TeamId.TEAM_ORDER &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardOffense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_ORDER, 
                                                false)
                                    ) ||
                                    // Sequence name :Team=Chaos
                                    (
                                          TeamToReward == TeamId.TEAM_CHAOS &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardOffense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_CHAOS, 
                                                false)
                                    )
                              )
                        ) ||
                        // Sequence name :Point=C
                        (
                              CapturePointID == 2 &&
                              // Sequence name :Which_Team?
                              (
                                    // Sequence name :Team=Order
                                    (
                                          TeamToReward == TeamId.TEAM_ORDER &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardOffense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_ORDER, 
                                                false)
                                    ) ||
                                    // Sequence name :Team=Chaos
                                    (
                                          TeamToReward == TeamId.TEAM_CHAOS &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardOffense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_CHAOS, 
                                                false)
                                    )
                              )
                        ) ||
                        // Sequence name :Point=D
                        (
                              CapturePointID == 3 &&
                              // Sequence name :Which_Team?
                              (
                                    // Sequence name :Team=Order
                                    (
                                          TeamToReward == TeamId.TEAM_ORDER &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardOffense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_ORDER, 
                                                false)
                                    ) ||
                                    // Sequence name :Team=Chaos
                                    (
                                          TeamToReward == TeamId.TEAM_CHAOS &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardOffense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_CHAOS, 
                                                false)
                                    )
                              )
                        ) ||
                        // Sequence name :Point=E
                        (
                              CapturePointID == 4 &&
                              // Sequence name :Which_Team?
                              (
                                    // Sequence name :Team=Order
                                    (
                                          TeamToReward == TeamId.TEAM_ORDER &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardOffense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_ORDER, 
                                                false)
                                    ) ||
                                    // Sequence name :Team=Chaos
                                    (
                                          TeamToReward == TeamId.TEAM_CHAOS &&
                                          changeScore.ChangeScore(
                                                out _ChaosScore, 
                                                out _OrderScore,
                                                VictoryPointRewardOffense, 
                                                ChaosScore, 
                                                OrderScore, 
                                                TeamId.TEAM_CHAOS, 
                                                false)

                                    )
                              )
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            );
         __OrderScore = _OrderScore;
         __ChaosScore = _ChaosScore;
        return result;


    }
}

