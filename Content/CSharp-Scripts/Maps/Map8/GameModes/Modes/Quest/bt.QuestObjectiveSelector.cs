namespace BehaviourTrees;


class QuestObjectiveSelectorClass : OdinLayout
{

    private CapturePointOwnerByIndexClass capturePointOwnerByIndex = new();
    private QuestCapturePointChampionCountClass questCapturePointChampionCount = new();
    private QuestObjectiveSelector_HelperClass questObjectiveSelector_Helper = new();
    public bool QuestObjectiveSelector(
           out int ObjectiveIndex_Order,
      out int ObjectiveIndex_Chaos,
    TeamId CapturePointOwnerA,
    TeamId CapturePointOwnerB,
    TeamId CapturePointOwnerC,
    TeamId CapturePointOwnerD,
    TeamId CapturePointOwnerE,
    float CombinedValue_A,
    float CombinedValue_B,
    float CombinedValue_C,
    float CombinedValue_D,
    float CombinedValue_E,
    float OrderScore,
    float ChaosScore,
    AttackableUnit CapturePointGuardian0,
    AttackableUnit CapturePointGuardian1,
    AttackableUnit CapturePointGuardian2,
    AttackableUnit CapturePointGuardian3,
    AttackableUnit CapturePointGuardian4
          )
    {

        int _ObjectiveIndex_Order = default;
        int _ObjectiveIndex_Chaos = default;
        bool result =
                  // Sequence name :Sequence

                  SetVarBool(
                        out IsValid_AB,
                        false) &&
                  SetVarBool(
                        out IsValid_BC,
                        false) &&
                  SetVarBool(
                        out IsValid_CD,
                        false) &&
                  SetVarBool(
                        out IsValid_DE,
                        false) &&
                  SetVarBool(
                        out IsValid_EA,
                        false) &&
                  SetVarBool(
                        out IsValid_A,
                        false) &&
                  SetVarBool(
                        out IsValid_B,
                        false) &&
                  SetVarBool(
                        out IsValid_C,
                        false) &&
                  SetVarBool(
                        out IsValid_D,
                        false) &&
                  SetVarBool(
                        out IsValid_E,
                        false) &&
                  SetVarFloat(
                        out ValueAB,
                        25000) &&
                  SetVarFloat(
                        out ValueBC,
                        25000) &&
                  SetVarFloat(
                        out ValueCD,
                        25000) &&
                  SetVarFloat(
                        out ValueDE,
                        25000) &&
                  SetVarFloat(
                        out ValueEA,
                        25000) &&
                  questCapturePointChampionCount.QuestCapturePointChampionCount(
                        out NumAllies0,
                        out NumEnemies0,
                        CapturePointGuardian0) &&
                  questCapturePointChampionCount.QuestCapturePointChampionCount(
                        out NumAllies1,
                        out NumEnemies1,
                        CapturePointGuardian1) &&
                  questCapturePointChampionCount.QuestCapturePointChampionCount(
                        out NumAllies2,
                        out NumEnemies2,
                        CapturePointGuardian2) &&
                 questCapturePointChampionCount.QuestCapturePointChampionCount(
                        out NumAllies3,
                        out NumEnemies3,
                        CapturePointGuardian3) &&
                  questCapturePointChampionCount.QuestCapturePointChampionCount(
                        out NumAllies4,
                        out NumEnemies4,
                        CapturePointGuardian4) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :CalculateBC
                        (
                              NotEqualUnitTeam(
                                    CapturePointOwnerB,
                                    TeamId.TEAM_NEUTRAL) &&
                              NotEqualUnitTeam(
                                    CapturePointOwnerC,
                                    TeamId.TEAM_NEUTRAL) &&
                              NotEqualUnitTeam(
                                    CapturePointOwnerB,
                                    CapturePointOwnerC) &&
                              LessEqualInt(
                                    NumEnemies1,
                                    0) &&
                              LessEqualInt(
                                    NumEnemies2,
                                    0) &&
                              AddFloat(
                                    out ValueBC,
                                    CombinedValue_B,
                                    CombinedValue_C) &&
                              SetVarBool(
                                    out IsValid_BC,
                                    true)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :CalculateCD
                        (
                              NotEqualUnitTeam(
                                    CapturePointOwnerC,
                                    TeamId.TEAM_NEUTRAL) &&
                              NotEqualUnitTeam(
                                    CapturePointOwnerD,
                                    TeamId.TEAM_NEUTRAL) &&
                              NotEqualUnitTeam(
                                    CapturePointOwnerC,
                                    CapturePointOwnerD) &&
                              LessEqualInt(
                                    NumEnemies2,
                                    0) &&
                              LessEqualInt(
                                    NumEnemies3,
                                    0) &&
                              AddFloat(
                                    out ValueCD,
                                    CombinedValue_C,
                                    CombinedValue_D) &&
                              SetVarBool(
                                    out IsValid_CD,
                                    true)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :CalculateEA
                        (
                              NotEqualUnitTeam(
                                    CapturePointOwnerE,
                                    TeamId.TEAM_NEUTRAL) &&
                              NotEqualUnitTeam(
                                    CapturePointOwnerA,
                                    TeamId.TEAM_NEUTRAL) &&
                              NotEqualUnitTeam(
                                    CapturePointOwnerE,
                                    CapturePointOwnerA) &&
                              LessEqualInt(
                                    NumEnemies4,
                                    0) &&
                              LessEqualInt(
                                    NumEnemies0,
                                    0) &&
                              AddFloat(
                                    out ValueEA,
                                    CombinedValue_E,
                                    CombinedValue_A) &&
                              SetVarBool(
                                    out IsValid_EA,
                                    true)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  GetTurret(
                        out OrderShrineTurret,
                        TeamId.TEAM_ORDER,
                        0,
                        1) &&
                  SubtractFloat(
                        out ScoreDifference,
                        OrderScore,
                        ChaosScore) &&
                  SetVarBool(
                        out Function_MostPositive,
                        false) &&
                  SetVarBool(
                        out Function_MostNegative,
                        false) &&
                  SetVarBool(
                        out Function_LeastPositive,
                        false) &&
                  SetVarBool(
                        out Function_LeastNegative,
                        false) &&
                  SetVarFloat(
                        out CurrentBestScore,
                        0) &&
                  // Sequence name :Selector
                  (
                        // Sequence name :OrderWin&gt;50_BigWin
                        (
                              GreaterEqualFloat(
                                    ScoreDifference,
                                    50) &&
                              SetVarBool(
                                    out Function_MostPositive,
                                    true) &&
                              SetVarFloat(
                                    out CurrentBestScore,
                                    -1000)
                        ) ||
                        // Sequence name :ChaosWin&gt;50_BigWin
                        (
                              LessEqualFloat(
                                    ScoreDifference,
                                    -50) &&
                              SetVarBool(
                                    out Function_MostNegative,
                                    true) &&
                              SetVarFloat(
                                    out CurrentBestScore,
                                    1000)
                        ) ||
                        // Sequence name :OrderWin&lt;50_SmallWin
                        (
                              LessEqualFloat(
                                    ScoreDifference,
                                    50) &&
                              GreaterEqualFloat(
                                    ScoreDifference,
                                    0) &&
                              SetVarBool(
                                    out Function_LeastPositive,
                                    true) &&
                              SetVarFloat(
                                    out CurrentBestScore,
                                    99999)
                        ) ||
                        // Sequence name :ChaosWin&lt;=50
                        (
                              GreaterEqualFloat(
                                    ScoreDifference,
                                    -50) &&
                              LessEqualFloat(
                                    ScoreDifference,
                                    0) &&
                              SetVarBool(
                                    out Function_LeastNegative,
                                    true) &&
                              SetVarFloat(
                                    out CurrentBestScore,
                                    -99999)
                        )
                  ) &&
                  SetVarInt(
                        out CurrentBestObjective_A,
                        -1) &&
                  SetVarInt(
                        out CurrentBestObjective_B,
                        -1) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        ValueAB,
                        IsValid_AB,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        0,
                        1) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        ValueBC,
                        IsValid_BC,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        1,
                        2) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        ValueCD,
                       IsValid_CD,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        2,
                        3) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        ValueDE,
                        IsValid_DE,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        3,
                        4) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        ValueEA,
                        IsValid_EA,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        4,
                        0) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        CombinedValue_A,
                        IsValid_A,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        0,
                        0) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        CombinedValue_B,
                        IsValid_B,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        1,
                        1) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        CombinedValue_C,
                        IsValid_C,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        2,
                        2) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        CombinedValue_D,
                        IsValid_D,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        3,
                        3) &&
                  questObjectiveSelector_Helper.QuestObjectiveSelector_Helper(
                        out CurrentBestScore,
                        out CurrentBestObjective_A,
                        out CurrentBestObjective_B,
                        CurrentBestScore,
                        CombinedValue_E,
                        IsValid_E,
                        Function_MostNegative,
                        Function_LeastNegative,
                        Function_MostPositive,
                        Function_LeastPositive,
                        CurrentBestObjective_A,
                        CurrentBestObjective_B,
                        4,
                        4) &&
                  GreaterEqualInt(
                        CurrentBestObjective_A,
                        0) &&
                  GreaterEqualInt(
                        CurrentBestObjective_B,
                        0) &&
                  // Sequence name :Selector
                  (
                        // Sequence name :NeutralPoint
                        (
                              CurrentBestObjective_A == CurrentBestObjective_B &&
                              SetVarInt(
                                    out _ObjectiveIndex_Order,
                                    CurrentBestObjective_A) &&
                              SetVarInt(
                                    out _ObjectiveIndex_Chaos,
                                    CurrentBestObjective_A)
                        ) ||
                        // Sequence name :Chaos/Order
                        (
                              capturePointOwnerByIndex.CapturePointOwnerByIndex(
                                    out ObjectiveA_Owner,
                                    out ObjectiveA_Owner,
                                    CurrentBestObjective_A,
                                    CapturePointOwnerA,
                                    CapturePointOwnerB,
                                    CapturePointOwnerC,
                                    CapturePointOwnerD,
                                    CapturePointOwnerE,
                                    CapturePointOwnerA,
                                    CapturePointOwnerB,
                                    CapturePointOwnerC,
                                    CapturePointOwnerD,
                                    CapturePointOwnerE) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :OwnerA==Order
                                    (
                                          ObjectiveA_Owner == TeamId.TEAM_ORDER &&
                                          SetVarInt(
                                                out _ObjectiveIndex_Chaos,
                                                CurrentBestObjective_A) &&
                                          SetVarInt(
                                                out _ObjectiveIndex_Order,
                                                CurrentBestObjective_B)
                                    ) ||
                                    // Sequence name :OwnerA==Chaos
                                    (
                                          ObjectiveA_Owner == TeamId.TEAM_CHAOS &&
                                          SetVarInt(
                                                out _ObjectiveIndex_Order,
                                                CurrentBestObjective_A) &&
                                          SetVarInt(
                                                out _ObjectiveIndex_Chaos,
                                                CurrentBestObjective_B)

                                    )
                              )
                        )
                  )
            ;
        ObjectiveIndex_Chaos = _ObjectiveIndex_Chaos;
        ObjectiveIndex_Order = _ObjectiveIndex_Order;
        return result;
    }
}

