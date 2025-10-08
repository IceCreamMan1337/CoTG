namespace BehaviourTrees;


class QuestObjectiveSelector_HelperClass : OdinLayout
{


    public bool QuestObjectiveSelector_Helper(
               out float __BestCurrentValue,
     out int __BestObjectiveIndex_0,
     out int __BestObjectiveIndex_1,
   float BestCurrentValue,
   float CurrentValue,
   bool CurrentValue_IsValid,
   bool Function_MostNegative,
   bool Function_LeastNegative,
   bool Function_MostPositive,
   bool Function_LeastPositive,
   int BestObjectiveIndex_0,
   int BestObjectiveIndex_1,
   int CurrentObjective_0,
   int CurrentObjective_1

         )
    {
        float _BestCurrentValue = BestCurrentValue;
        int _BestObjectiveIndex_0 = BestObjectiveIndex_0;
        int _BestObjectiveIndex_1 = BestObjectiveIndex_1;
        bool result =
                                // Sequence name :MaskFailure

                                // Sequence name :Selector

                                // Sequence name :MostPositive
                                (
                                      Function_MostPositive == true &&
                                      CurrentValue_IsValid == true &&
                                      GreaterEqualFloat(
                                            CurrentValue,
                                            -0.1f) &&
                                      GreaterFloat(
                                            CurrentValue,
                                            BestCurrentValue) &&
                                      SetVarFloat(
                                            out _BestCurrentValue,
                                            CurrentValue) &&
                                      SetVarInt(
                                            out _BestObjectiveIndex_0,
                                            CurrentObjective_0) &&
                                      SetVarInt(
                                            out _BestObjectiveIndex_1,
                                            CurrentObjective_1)
                                ) ||
                                // Sequence name :LeastPositive
                                (
                                      Function_LeastPositive == true &&
                                      CurrentValue_IsValid == true &&
                                      GreaterEqualFloat(
                                            CurrentValue,
                                            -0.1f) &&
                                      LessFloat(
                                            CurrentValue,
                                            BestCurrentValue) &&
                                      SetVarFloat(
                                            out _BestCurrentValue,
                                            CurrentValue) &&
                                      SetVarInt(
                                            out _BestObjectiveIndex_0,
                                            CurrentObjective_0) &&
                                      SetVarInt(
                                            out _BestObjectiveIndex_1,
                                            CurrentObjective_1)
                                ) ||
                                // Sequence name :MostNegative
                                (
                                      Function_MostNegative == true &&
                                      CurrentValue_IsValid == true &&
                                      LessEqualFloat(
                                            CurrentValue,
                                            0.1f) &&
                                      LessFloat(
                                            CurrentValue,
                                            BestCurrentValue) &&
                                      SetVarFloat(
                                            out _BestCurrentValue,
                                            CurrentValue) &&
                                      SetVarInt(
                                            out _BestObjectiveIndex_0,
                                            CurrentObjective_0) &&
                                      SetVarInt(
                                            out _BestObjectiveIndex_1,
                                            CurrentObjective_1)
                                ) ||
                                // Sequence name :LeastNegative
                                (
                                      Function_LeastNegative == true &&
                                      CurrentValue_IsValid == true &&
                                      LessEqualFloat(
                                            CurrentValue,
                                            0.1f) &&
                                      GreaterFloat(
                                            CurrentValue,
                                            BestCurrentValue) &&
                                      SetVarFloat(
                                            out _BestCurrentValue,
                                            CurrentValue) &&
                                      SetVarInt(
                                            out _BestObjectiveIndex_0,
                                            CurrentObjective_0) &&
                                      SetVarInt(
                                            out _BestObjectiveIndex_1,
                                            CurrentObjective_1)

                                )

                          ||
                                       DebugAction("MaskFailure")
                    ;
        __BestCurrentValue = _BestCurrentValue;
        __BestObjectiveIndex_0 = _BestObjectiveIndex_0;
        __BestObjectiveIndex_1 = _BestObjectiveIndex_1;
        return result;
    }
}

