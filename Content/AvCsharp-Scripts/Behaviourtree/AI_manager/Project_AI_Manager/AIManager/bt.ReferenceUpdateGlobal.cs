using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class ReferenceUpdateGlobalClass : CommonAI 
{


    public bool ReferenceUpdateGlobal(
      out float PrevTime,
      out float EnemyStrengthTop,
      out float EnemyStrengthBot,
      out float EnemyStrengthMid,
      out float FriendlyStrengthTop,
      out float FriendlyStrengthMid,
      out float FriendlyStrengthBot,
    float __PrevTime,
    AttackableUnit ReferenceUnit,
    float __EnemyStrengthTop,
    float __EnemyStrengthMid,
    float __EnemyStrengthBot,
    float __FriendlyStrengthTop,
    float __FriendlyStrengthMid,
    float __FriendlyStrengthBot,
    float ChampionPointValue,
    float MinionPointValue
          )
      {
        float _PrevTime = __PrevTime;
        float _EnemyStrengthTop = __EnemyStrengthTop;
        float _EnemyStrengthMid = __EnemyStrengthMid;
        float _EnemyStrengthBot = __EnemyStrengthBot;
        float _FriendlyStrengthTop = __FriendlyStrengthTop;
        float _FriendlyStrengthMid = __FriendlyStrengthMid;
        float _FriendlyStrengthBot = __FriendlyStrengthBot;


        var pointEvaluation = new PointEvaluationClass();


        bool result =
            // Sequence name :MaskFailure
            (
                  // Sequence name :PerSecond
                  (
                        GetGameTime(
                              out CurrentTime) &&
                        SubtractFloat(
                              out TimeDiff, 
                              CurrentTime,
                              __PrevTime) &&
                        // Sequence name :TimeGreaterThanOneSecond
                        (
                              GreaterFloat(
                                    TimeDiff, 
                                    3)         
                              || // todo verify 
                              LessFloat(
                                    TimeDiff, 
                                    0)
                        ) &&
                        // Sequence name :DamageAndStrengthDecay
                        (
                              MultiplyFloat(
                                    out _EnemyStrengthTop, 
                                    __EnemyStrengthTop, 
                                    0.729f) &&
                              MultiplyFloat(
                                    out _EnemyStrengthMid, 
                                    __EnemyStrengthMid, 
                                    0.729f) &&
                              MultiplyFloat(
                                    out _EnemyStrengthBot, 
                                    __EnemyStrengthBot, 
                                    0.729f) &&
                              MultiplyFloat(
                                    out _FriendlyStrengthTop, 
                                    __FriendlyStrengthTop, 
                                    0.729f) &&
                              MultiplyFloat(
                                    out _FriendlyStrengthMid, 
                                    __FriendlyStrengthMid, 
                                    0.729f) &&
                              MultiplyFloat(
                                    out _FriendlyStrengthBot, 
                                    __FriendlyStrengthBot, 
                                    0.729f)
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
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Center0, 
                                                ReferenceUnit, 
                                                3000,
                                                ChampionPointValue,
                                                MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthMid, 
                                                _EnemyStrengthMid, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthMid, 
                                                _FriendlyStrengthMid, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Center1, 
                                                ReferenceUnit, 
                                                1660,
                                                ChampionPointValue,
                                                MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthMid, 
                                                _EnemyStrengthMid, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthMid, 
                                                _FriendlyStrengthMid, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Center2, 
                                                ReferenceUnit, 
                                                1740,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthMid, 
                                                _EnemyStrengthMid, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthMid, 
                                                _FriendlyStrengthMid, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Center3, 
                                                ReferenceUnit, 
                                                1050,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthMid, 
                                                _EnemyStrengthMid, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthMid, 
                                                _FriendlyStrengthMid, 
                                                FriendStrength)
                                    ) &&
                                    // Sequence name :Bot_Lane
                                    (
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot0, 
                                                ReferenceUnit, 
                                                3000,
                                               ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthBot, 
                                                _EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthBot, 
                                                _FriendlyStrengthBot, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot1, 
                                                ReferenceUnit, 
                                                2000,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthBot, 
                                                _EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthBot, 
                                                _FriendlyStrengthBot, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot2, 
                                                ReferenceUnit, 
                                                1200,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthBot, 
                                                _EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthBot, 
                                                _FriendlyStrengthBot, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot3, 
                                                ReferenceUnit, 
                                                2000,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthBot, 
                                                _EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthBot, 
                                                _FriendlyStrengthBot, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Bot4, 
                                                ReferenceUnit, 
                                                1200,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthBot, 
                                                _EnemyStrengthBot, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthBot, 
                                                _FriendlyStrengthBot, 
                                                FriendStrength)
                                    ) &&
                                    // Sequence name :Top_Lane
                                    (
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top0, 
                                                ReferenceUnit, 
                                                3000,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthTop, 
                                                _EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthTop, 
                                                _FriendlyStrengthTop, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top1, 
                                                ReferenceUnit, 
                                                1200,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthTop, 
                                                _EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthTop, 
                                                _FriendlyStrengthTop, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top2, 
                                                ReferenceUnit, 
                                                1800,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthTop, 
                                                _EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthTop, 
                                                _FriendlyStrengthTop, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top3, 
                                                ReferenceUnit, 
                                                1200,
                                                ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthTop, 
                                                _EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthTop, 
                                                _FriendlyStrengthTop, 
                                                FriendStrength) &&
                                          pointEvaluation.PointEvaluation(
                                                out EnemyStrength, 
                                                out FriendStrength, 
                                                Top4, 
                                                ReferenceUnit, 
                                                1750,
                                               ChampionPointValue, MinionPointValue) &&
                                          AddFloat(
                                                out _EnemyStrengthTop, 
                                                _EnemyStrengthTop, 
                                                EnemyStrength) &&
                                          AddFloat(
                                                out _FriendlyStrengthTop, 
                                                _FriendlyStrengthTop, 
                                                FriendStrength)
                                    )
                              )
                        ) &&
                        SetVarFloat(
                              out _PrevTime, 
                              CurrentTime)
                   )
                  ||
                               DebugAction("MaskFailure")
            );
        PrevTime = _PrevTime;
        EnemyStrengthTop = _EnemyStrengthTop;
        EnemyStrengthMid = _EnemyStrengthMid;
        EnemyStrengthBot = _EnemyStrengthBot;
        FriendlyStrengthTop = _FriendlyStrengthTop;
        FriendlyStrengthMid = _FriendlyStrengthMid;
        FriendlyStrengthBot = _FriendlyStrengthBot;
        return result;
      }
}

