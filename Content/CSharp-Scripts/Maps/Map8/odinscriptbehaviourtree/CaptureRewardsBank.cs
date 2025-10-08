using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class CaptureRewardsBankClass : OdinLayout
{


    public bool CaptureRewardsBank(
               out float __BankTimer,
     out float __FullCaptureValue,
   TeamId CurrentPointOwner,
   TeamId Attacker,
   TeamId Defender,
   float NeutralizationBank,
   Vector3 CapturePosition,
     float CaptureRadius,
   float BankTimer,
   float FullCaptureValue

         )
    {

        float _BankTimer = BankTimer;
        float _FullCaptureValue = FullCaptureValue;
        bool result =
                          // Sequence name :Selector

                          // Sequence name :Set vars, test if capping point or neutralizing

                          GetTurret(
                                out CentralID,
                                TeamId.TEAM_ORDER,
                                0,
                                1) &&
                          AddFloat(
                                out RewardRadius,
                                CaptureRadius,
                                600) &&
                          DivideFloat(
                                out GoldReward,
                                NeutralizationBank,
                                2) &&
                          GetUnitsInTargetArea(
                                out CollectionChamp,
                                CentralID,
                                CapturePosition,
                                RewardRadius,
                                AffectEnemies | AffectFriends | AffectHeroes | AlwaysSelf) &&
                          SetVarFloat(
                                out TwoCountModifier,
                                0.65f) &&
                          // Sequence name :MaskFailure
                          (
                                      // Sequence name :Selector

                                      // Sequence name :Sequence
                                      (
                                            Attacker == TeamId.TEAM_ORDER &&
                                            GetUnitsInTargetArea(
                                                  out ChampionCount,
                                                  CentralID,
                                                  CapturePosition,
                                                  RewardRadius,
                                                  AffectFriends | AffectHeroes) &&
                                            GetCollectionCount(
                                                  out ChampionCollectionCount,
                                                  ChampionCount)
                                      ) ||
                                      // Sequence name :Sequence
                                      (
                                            Attacker == TeamId.TEAM_CHAOS &&
                                            GetUnitsInTargetArea(
                                                  out ChampionCount,
                                                  CentralID,
                                                  CapturePosition,
                                                  RewardRadius,
                                                  AffectEnemies | AffectHeroes) &&
                                            GetCollectionCount(
                                                  out ChampionCollectionCount,
                                                  ChampionCount)
                                      ) ||
                                      // Sequence name :Sequence
                                      (
                                            Attacker == TeamId.TEAM_NEUTRAL &&
                                            SetVarInt(
                                                  out ChampionCollectionCount,
                                                  1)
                                      )

                                ||
                                 DebugAction("MaskFailure")
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                      // Sequence name :Selector

                                      // Sequence name :Is the count 0?
                                      (
                                            ChampionCollectionCount == 0 &&
                                            SetVarFloat(
                                                  out ChampCountModifier,
                                                  0)
                                      ) ||
                                      // Sequence name :Is the count 1?
                                      (
                                            ChampionCollectionCount == 1 &&
                                            SetVarFloat(
                                                  out ChampCountModifier,
                                                  1)
                                      ) ||
                                      // Sequence name :Is the count 2?
                                      (
                                            ChampionCollectionCount == 2 &&
                                            SetVarFloat(
                                                  out ChampCountModifier,
                                                  0.65f)
                                      ) ||
                                      // Sequence name :Is the count 3?
                                      (
                                            ChampionCollectionCount == 3 &&
                                            SetVarFloat(
                                                  out ChampCountModifier,
                                                  0.55f)
                                      ) ||
                                      // Sequence name :Is the count 4?
                                      (
                                            ChampionCollectionCount == 4 &&
                                            SetVarFloat(
                                                  out ChampCountModifier,
                                                  0.45f)
                                      ) ||
                                      // Sequence name :Is the count 5?
                                      (
                                            GreaterEqualInt(
                                                  ChampionCollectionCount,
                                                  5) &&
                                            SetVarFloat(
                                                  out ChampCountModifier,
                                                  0.35f)
                                      )

                                ||
                                 DebugAction("MaskFailure")
                          ) &&
                          MultiplyFloat(
                                out FullCapGoldToGive,
                                _FullCaptureValue,
                                ChampCountModifier) &&
                          // Sequence name :MaskFailure
                          (
                                ForEach(CollectionChamp, IterateThese =>
                                            // Sequence name :Sequence

                                            GetUnitTeam(
                                                  out RewardsTeam,
                                                  IterateThese) &&
                                            RewardsTeam == CurrentPointOwner &&
                                            // Sequence name :Selector
                                            (
                                                  // Sequence name :Are they the offensive force for this capture?
                                                  (
                                                        GetGameTime(
                                                              out _BankTimer) &&
                                                        RewardsTeam == Attacker &&
                                                        GiveChampionGold(
                                                              IterateThese,
                                                              FullCapGoldToGive)
                                                  ) ||
                                                  // Sequence name :Are they recapping after the point was neutralized?
                                                  (
                                                        GetGameTime(
                                                              out _BankTimer) &&
                                                        RewardsTeam == Defender &&
                                                        GiveChampionGold(
                                                              IterateThese,
                                                              75)
                                                  ) ||
                                                        // Sequence name :Was there no previous attacker or defender?

                                                        GiveChampionGold(
                                                              IterateThese,
                                                              75)

                                            )

                                )
                                ||
                                 DebugAction("MaskFailure")
                          ) &&
                                // Sequence name :neutralization sequence

                                CurrentPointOwner == TeamId.TEAM_NEUTRAL &&
                                GetGameTime(
                                      out GameTime) &&
                                SubtractFloat(
                                      out PreMultiplier,
                                      GameTime,
                                      BankTimer) &&
                                DivideFloat(
                                      out Multiplier,
                                      PreMultiplier,
                                      90) &&
                                MultiplyFloat(
                                      out NeutralGoldReward,
                                      NeutralizationBank,
                                      Multiplier) &&
                                // Sequence name :MaskFailure
                                (
                                      // Sequence name :Sequence
                                      (
                                            GreaterFloat(
                                                  NeutralGoldReward,
                                                  500) &&
                                            SetVarFloat(
                                                  out NeutralGoldReward,
                                                  500)
                                      )
                                      ||
                                 DebugAction("MaskFailure")
                                ) &&
                                DivideFloat(
                                      out _FullCaptureValue,
                                      NeutralGoldReward,
                                      2) &&
                                MultiplyFloat(
                                      out NeutralGoldToGive,
                                      NeutralGoldReward,
                                      ChampCountModifier) &&
                                ForEach(CollectionChamp, IterateThese =>
                                            // Sequence name :Sequence

                                            GetUnitTeam(
                                                  out RewardsTeam,
                                                  IterateThese) &&
                                            RewardsTeam == Attacker &&
                                            GiveChampionGold(
                                                  IterateThese,
                                                  NeutralGoldToGive)


                                )


              ;
        __BankTimer = _BankTimer;
        __FullCaptureValue = _FullCaptureValue;

        return result;
    }
}

