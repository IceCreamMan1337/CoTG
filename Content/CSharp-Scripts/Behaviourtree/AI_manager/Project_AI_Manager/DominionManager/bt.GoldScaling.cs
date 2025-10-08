using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class GoldScalingClass : CommonAI
{


    public bool GoldScaling(
               out float LastGoldScalingUpdateTime,
   int DifficultyIndex,
   float __LastGoldScalingUpdateTime,
   AttackableUnit ReferenceUnit
         )
    {

        float _LastGoldScalingUpdateTime = __LastGoldScalingUpdateTime;

        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :Sequence
                  (
                        GreaterEqualInt(
                              DifficultyIndex,
                              1) &&
                        GetGameTime(
                              out CurrentTime) &&
                        SubtractFloat(
                              out TimeDiff,
                              CurrentTime,
                              __LastGoldScalingUpdateTime) &&
                        GreaterFloat(
                              TimeDiff,
                              5) &&
                        SetVarFloat(
                              out _LastGoldScalingUpdateTime,
                              CurrentTime) &&
                        SetVarInt(
                              out BotTeamAverageLevel,
                              0) &&
                        SetVarInt(
                              out BotTeamTotalLevel,
                              0) &&
                        SetVarInt(
                              out HumanTeamAverageLevel,
                              0) &&
                        SetVarInt(
                              out HumanTeamTotalLevel,
                              0) &&
                              // Sequence name :BotTeamAverageLevel

                              GetAIManagerEntities(
                                    out BotTeam) &&
                              GetCollectionCount(
                                    out BotTeamCount,
                                    BotTeam) &&
                              GreaterInt(
                                    BotTeamCount,
                                    0) &&
                              ForEach(BotTeam, Bot =>
                                          // Sequence name :IncrementTotalByBotLevel

                                          GetUnitLevel(
                                                out BotLevel,
                                                Bot) &&
                                          AddInt(
                                                out BotTeamTotalLevel,
                                                BotTeamTotalLevel,
                                                BotLevel)

                              ) &&
                              DivideInt(
                                    out BotTeamAverageLevel,
                                    BotTeamTotalLevel,
                                    BotTeamCount)
                         &&
                              // Sequence name :HumanTeamAverageLevel

                              GetUnitPosition(
                                    out SampleBotPosition,
                                    ReferenceUnit) &&
                              GetUnitsInTargetArea(
                                    out HumanTeam,
                                    ReferenceUnit,
                                    SampleBotPosition,
                                    20000,
                                    AffectEnemies | AffectHeroes) &&
                              GetCollectionCount(
                                    out HumanTeamCount,
                                    HumanTeam) &&
                              GreaterInt(
                                    HumanTeamCount,
                                    0) &&
                              ForEach(HumanTeam, Human =>
                                          // Sequence name :IncrementTotalByHumanLevel

                                          GetUnitLevel(
                                                out HumanLevel,
                                                Human) &&
                                          AddInt(
                                                out HumanTeamTotalLevel,
                                                HumanTeamTotalLevel,
                                                HumanLevel)

                              ) &&
                              DivideInt(
                                    out HumanTeamAverageLevel,
                                    HumanTeamTotalLevel,
                                    HumanTeamCount)
                         &&
                              // Sequence name :RubberBandAdjustment

                              SubtractInt(
                                    out LevelDisparity,
                                    HumanTeamAverageLevel,
                                    BotTeamAverageLevel) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :NoDisparity
                                    (
                                          LevelDisparity == 0 &&
                                          SetVarFloat(
                                                out RubberBandTotalAdjustment,
                                                1)
                                    ) ||
                                    // Sequence name :HumansAreAhead
                                    (
                                          GreaterInt(
                                                LevelDisparity,
                                                0) &&
                                          SetVarInt(
                                                out RubberBandModifier,
                                                0) &&
                                          MultiplyFloat(
                                                out ModiferAdditive,
                                                LevelDisparity,
                                                0.5f) &&
                                          AddFloat(
                                                out RubberBandTotalAdjustment,
                                                ModiferAdditive,
                                                RubberBandModifier)
                                    ) ||
                                    // Sequence name :BotAreAhead
                                    (
                                          LessInt(
                                                LevelDisparity,
                                                0) &&
                                          SetVarFloat(
                                                out RubberBandTotalAdjustment,
                                                1)
                                    )
                              )
                         &&
                                    // Sequence name :GoldScaling

                                    // Sequence name :BonusGold

                                    SetVarFloat(
                                          out BaseAmbientGold,
                                          1) &&
                                    AddFloat(
                                          out Temp,
                                          BotTeamAverageLevel,
                                          1) &&
                                    MultiplyFloat(
                                          out AmbientGold,
                                          BaseAmbientGold,
                                          Temp) &&
                                    AddFloat(
                                          out AmbientGold,
                                          AmbientGold,
                                          1.5f) &&
                                    ForEach(BotTeam, Bot =>
                                                // Sequence name :Sequence

                                                GiveChampionGold(
                                                      Bot,
                                                      AmbientGold) &&
                                                // Sequence name :MaskFailure
                                                (
                                                      // Sequence name :Advanced
                                                      (
                                                            DifficultyIndex == 2 &&
                                                            GiveChampionGold(
                                                                  Bot,
                                                                  AmbientGold)
                                                      )
                                                      ||
                               DebugAction("MaskFailure")
                                                )

                                    )

                         &&
                              // Sequence name :AmbientXP

                              MaxInt(
                                    out HumanTeamAverageLevel,
                                    HumanTeamAverageLevel,
                                    1) &&
                              GreaterInt(
                                    HumanTeamAverageLevel,
                                    1) &&
                              SetVarFloat(
                                    out BaseAmbientXP,
                                    0.4f) &&
                              AddFloat(
                                    out Temp,
                                    BotTeamAverageLevel,
                                    1) &&
                              MultiplyFloat(
                                    out AmbientXP,
                                    BaseAmbientXP,
                                    Temp) &&
                              AddFloat(
                                    out AmbientXP,
                                    AmbientXP,
                                    1.5f) &&
                              MultiplyFloat(
                                    out AmbientXP,
                                    AmbientXP,
                                    RubberBandTotalAdjustment) &&
                              ForEach(BotTeam, Bot =>
                                          // Sequence name :Sequence

                                          GiveChampionExp(
                                                Bot,
                                                AmbientXP) &&
                                          // Sequence name :MaskFailure
                                          (
                                                // Sequence name :Advanced
                                                (
                                                      DifficultyIndex == 2 &&
                                                      GiveChampionExp(
                                                            Bot,
                                                            AmbientXP)
                                                )
                                                ||
                               DebugAction("MaskFailure")
                                          ) &&
                                          // Sequence name :MaskFailure
                                          (
                                                // Sequence name :BigLevelGap
                                                (
                                                      GetUnitLevel(
                                                            out BotLevel,
                                                            Bot) &&
                                                      AddInt(
                                                            out BotLevel,
                                                            BotLevel,
                                                            2) &&
                                                      LessEqualInt(
                                                            BotLevel,
                                                            HumanTeamAverageLevel) &&
                                                      GiveChampionExp(
                                                            Bot,
                                                            AmbientXP)

                                                )
                                                ||
                               DebugAction("MaskFailure")
                                          )

                              )

                  )
                  ||
                               DebugAction("MaskFailure")
            ;
        LastGoldScalingUpdateTime = _LastGoldScalingUpdateTime;
        return result;
    }
}

