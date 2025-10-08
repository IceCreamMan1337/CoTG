using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class LevelNormalizerClass : CommonAI
{


    public bool LevelNormalizer(
      out float PrevTime,
      float __PrevTime,
      AttackableUnit ReferenceUnit,
      int DifficultyIndex)
    {
        float _PrevTime = default;



        bool result =
                          // Sequence name :MaskFailure

                          // Sequence name :Sequence
                          (
                                GreaterInt(
                                      DifficultyIndex,
                                      0) &&
                                GetGameTime(
                                      out CurrentTime) &&
                                SubtractFloat(
                                      out TimeDiff,
                                      CurrentTime,
                                      __PrevTime) &&
                                GreaterFloat(
                                      TimeDiff,
                                      5) &&
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
                                SetVarFloat(
                                      out _PrevTime,
                                      CurrentTime) &&
                                      // Sequence name :BotTeamAverageLevel

                                      GetAIManagerEntities(
                                            out BotTeam) &&
                                      GetCollectionCount(
                                            out BotTeamCount,
                                            BotTeam) &&
                                      GreaterInt(
                                            BotTeamCount,
                                            0) &&
                                      ForEach(BotTeam, Bot =>                                     // Sequence name :IncrementTotalByBotLevel

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
                                      ForEach(HumanTeam, Human =>                                     // Sequence name :IncrementTotalByHumanLevel

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
                                      // Sequence name :Sequence

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
                                                        0.75f) &&
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
                                // Sequence name :MaskFailure
                                (
                                      // Sequence name :AmbientXP
                                      (
                                            MaxInt(
                                                  out HumanTeamAverageLevel,
                                                  HumanTeamAverageLevel,
                                                  1) &&
                                            GreaterInt(
                                                  HumanTeamAverageLevel,
                                                  1) &&
                                            SetVarFloat(
                                                  out BaseAmbientXP,
                                                  1) &&
                                            SubtractFloat(
                                                  out Temp,
                                                  HumanTeamAverageLevel,
                                                  1) &&
                                            MultiplyFloat(
                                                  out AmbientXP,
                                                  BaseAmbientXP,
                                                  Temp) &&
                                            AddFloat(
                                                  out AmbientXP,
                                                  AmbientXP,
                                                  1.2f) &&
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
                                ) &&
                                            // Sequence name :MaskFailure

                                            // Sequence name :AmbientGold

                                            MaxInt(
                                                  out HumanTeamAverageLevel,
                                                  HumanTeamAverageLevel,
                                                  1) &&
                                            GreaterInt(
                                                  HumanTeamAverageLevel,
                                                  1) &&
                                            SetVarFloat(
                                                  out BaseAmbientGold,
                                                  2) &&
                                            SubtractFloat(
                                                  out Temp,
                                                  HumanTeamAverageLevel,
                                                  1) &&
                                            MultiplyFloat(
                                                  out Temp,
                                                  0.4f,
                                                  Temp) &&
                                            MultiplyFloat(
                                                  out AmbientGold,
                                                  BaseAmbientGold,
                                                  Temp) &&
                                            AddFloat(
                                                  out AmbientGold,
                                                  AmbientGold,
                                                  8) &&
                                            MinFloat(
                                                  out AmbientGold,
                                                  20,
                                                  AmbientGold) &&
                                            ForEach(BotTeam, Bot =>
                                                        // Sequence name :Sequence

                                                        GiveChampionGold(
                                                              Bot,
                                                              AmbientGold)


                                            )


                          )
                          ||
                                       DebugAction("MaskFailure")
                    ;
        PrevTime = _PrevTime;
        return result;
    }
}

