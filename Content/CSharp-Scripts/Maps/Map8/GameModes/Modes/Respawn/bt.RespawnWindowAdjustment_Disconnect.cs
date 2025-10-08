namespace BehaviourTrees;


class RespawnWindowAdjustment_DisconnectClass : OdinLayout
{


    public bool RespawnWindowAdjustment_Disconnect(
               out int __WindowAdjustmentMutator,
   int WindowAdjustmentMutator,
   int NewWindowAdjustmentMutator,
   TeamId Team
   )
    {

        int _WindowAdjustmentMutator = WindowAdjustmentMutator;

        bool result =
                    // Sequence name :MaskFailure

                    // Sequence name :Sequence
                    (
                          NotEqualInt(
                                NewWindowAdjustmentMutator,
                                WindowAdjustmentMutator) &&
                          SubtractInt(
                                out ModifierDiff,
                                NewWindowAdjustmentMutator,
                                WindowAdjustmentMutator) &&
                          MultiplyFloat(
                                out ModifierToSet,
                                ModifierDiff,
                                1) &&
                          DivideFloat(
                                out ModifierToSet,
                                ModifierToSet,
                                10) &&
                          AdjustWaveSpawnTime(
                                ModifierToSet,
                                Team) &&
                          SetVarInt(
                                out _WindowAdjustmentMutator,
                                NewWindowAdjustmentMutator)

                    )
                    ||
                                 DebugAction("MaskFailure")
              ;
        __WindowAdjustmentMutator = _WindowAdjustmentMutator;
        return result;
    }
}

