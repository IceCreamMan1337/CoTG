/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class ReferenceUpdateGlobal : BehaviourTree 
{
      out float PrevTime;
      out float EnemyStrengthTop;
      out float EnemyStrengthBot;
      out float EnemyStrengthMid;
      out float FriendlyStrengthTop;
      out float FriendlyStrengthMid;
      out float FriendlyStrengthBot;
      float PrevTime;
      AttackableUnit ReferenceUnit;
      float EnemyStrengthTop;
      float EnemyStrengthMid;
      float EnemyStrengthBot;
      float FriendlyStrengthTop;
      float FriendlyStrengthMid;
      float FriendlyStrengthBot;
      float ChampionPointValue;
      float MinionPointValue;

      bool ReferenceUpdateGlobal()
      {
      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :PerSecond
                  (
                        GetGameTime(
                              out CurrentTime, 
                              out CurrentTime) &&
                        SubtractFloat(
                              out TimeDiff, 
                              CurrentTime, 
                              PrevTime) &&
                        // Sequence name :TimeGreaterThanOneSecond
                        (
                              GreaterFloat(
                                    TimeDiff, 
                                    3)                              LessFloat(
                                    TimeDiff, 
                                    0)
                        ) &&
                        // Sequence name :DamageAndStrengthDecay
                        (
                              MultiplyFloat(
                                    out EnemyStrengthTop, 
                                    EnemyStrengthTop, 
                                    0.729) &&
                              MultiplyFloat(
                                    out EnemyStrengthMid, 
                                    EnemyStrengthMid, 
                                    0.729) &&
                              MultiplyFloat(
                                    out EnemyStrengthBot, 
                                    EnemyStrengthBot, 
                                    0.729) &&
                              MultiplyFloat(
                                    out FriendlyStrengthTop, 
                                    FriendlyStrengthTop, 
                                    0.729) &&
                              MultiplyFloat(
                                    out FriendlyStrengthMid, 
                                    FriendlyStrengthMid, 
                                    0.729) &&
                              MultiplyFloat(
                                    out FriendlyStrengthBot, 
                                    FriendlyStrengthBot, 
                                    0.729)
                        ) &&
                        // Sequence name :UpdateStrengthEverySecond
                        (
                              // Sequence name :InitializePoints
                              (
                                    // Sequence name :Mid_Lane
                                    (
                                          MakeVector(
                                                out Center0, 
                                                6961, 
                                                46, 
                                                7027) &&
                                          MakeVector(
                                                out Center1, 
                                                3575, 
                                                101, 
                                                3790) &&
                                          MakeVector(
                                                out Center2, 
                                                10175, 
                                                104, 
                                                10493) &&
                                          MakeVector(
                                                out Center3, 
                                                3855, 
                                                -69, 
                                                9626)
                                    ) &&
                                    // Sequence name :Bot_Lane
                                    (
                                          MakeVector(
                                                out Bot0, 
                                                11448, 
                                                -63, 
                                                3010) &&
                                          MakeVector(
                                                out Bot1, 
                                                7034, 
                                                43, 
                                                618) &&
                                          MakeVector(
                                                out Bot2, 
                                                3909, 
                                                100, 
                                                940) &&
                                          MakeVector(
                                                out Bot3, 
                                                13919, 
                                                41, 
                                                7248) &&
                                          MakeVector(
                                                out Bot4, 
                                                13093, 
                                                95, 
                                                10303)
                                    ) &&
                                    // Sequence name :Top_Lane
                                    (
                                          MakeVector(
                                                out Top0, 
                                                2529, 
                                                -30, 
                                                11605) &&
                                          MakeVector(
                                                out Top1, 
                                                814, 
                                                99, 
                                                4216) &&
                                          MakeVector(
                                                out Top2, 
                                                444, 
                                                43, 
                                                7275) &&
                                          MakeVector(
                                                out Top3, 
                                                10081, 
                                                97, 
                                                13448) &&
                                          MakeVector(
                                                out Top4, 
                                                7025, 
                                                35, 
                                                13201)
                                    )
                              ) &&
                              // Sequence name :UpdatePoints
                              (
                                    // Sequence name :Mid_Lane
                                    (
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Center0, 
                                                ReferenceUnit, 
                                                3000, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthMid, 
                                                EnemyStrengthMid, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthMid, 
                                                FriendlyStrengthMid, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Center1, 
                                                ReferenceUnit, 
                                                1660, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthMid, 
                                                EnemyStrengthMid, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthMid, 
                                                FriendlyStrengthMid, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Center2, 
                                                ReferenceUnit, 
                                                1740, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthMid, 
                                                EnemyStrengthMid, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthMid, 
                                                FriendlyStrengthMid, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Center3, 
                                                ReferenceUnit, 
                                                1050, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthMid, 
                                                EnemyStrengthMid, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthMid, 
                                                FriendlyStrengthMid, 
                                                FriendStrength)
                                    ) &&
                                    // Sequence name :Bot_Lane
                                    (
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot0, 
                                                ReferenceUnit, 
                                                3000, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthBot, 
                                                EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthBot, 
                                                FriendlyStrengthBot, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot1, 
                                                ReferenceUnit, 
                                                2000, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthBot, 
                                                EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthBot, 
                                                FriendlyStrengthBot, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot2, 
                                                ReferenceUnit, 
                                                1200, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthBot, 
                                                EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthBot, 
                                                FriendlyStrengthBot, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot3, 
                                                ReferenceUnit, 
                                                2000, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthBot, 
                                                EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthBot, 
                                                FriendlyStrengthBot, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot4, 
                                                ReferenceUnit, 
                                                1200, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthBot, 
                                                EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthBot, 
                                                FriendlyStrengthBot, 
                                                FriendStrength)
                                    ) &&
                                    // Sequence name :Top_Lane
                                    (
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top0, 
                                                ReferenceUnit, 
                                                3000, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthTop, 
                                                EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthTop, 
                                                FriendlyStrengthTop, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top1, 
                                                ReferenceUnit, 
                                                1200, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthTop, 
                                                EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthTop, 
                                                FriendlyStrengthTop, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top2, 
                                                ReferenceUnit, 
                                                1800, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthTop, 
                                                EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthTop, 
                                                FriendlyStrengthTop, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top3, 
                                                ReferenceUnit, 
                                                1200, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthTop, 
                                                EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthTop, 
                                                FriendlyStrengthTop, 
                                                FriendStrength) &&
                                          PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top4, 
                                                ReferenceUnit, 
                                                1750, 
                                                Value=, 
                                                Value=) &&
                                          AddFloat(
                                                out EnemyStrengthTop, 
                                                EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out FriendlyStrengthTop, 
                                                FriendlyStrengthTop, 
                                                FriendStrength)
                                    )
                              )
                        ) &&
                        SetVarFloat(
                              out PrevTime, 
                              CurrentTime)

                  )
            );
      }
}

*/