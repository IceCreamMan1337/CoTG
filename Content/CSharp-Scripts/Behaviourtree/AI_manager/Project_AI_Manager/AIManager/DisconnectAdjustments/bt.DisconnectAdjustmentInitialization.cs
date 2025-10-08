namespace BehaviourTrees;


class DisconnectAdjustmentInitializationClass : CommonAI
{


    public bool DisconnectAdjustmentInitialization(
     out int DisconnectAdjustmentEntityID,
     out bool DisconnectAdjustmentEnabled)
    {

        int _DisconnectAdjustmentEntityID = default;
        bool _DisconnectAdjustmentEnabled = default;

        bool result =
                    // Sequence name :Sequence

                    SetVarBool(
                          out _DisconnectAdjustmentEnabled,
                          true) &&
                    SetVarInt(
                          out _DisconnectAdjustmentEntityID,
                          -1)

              ;
        DisconnectAdjustmentEntityID = _DisconnectAdjustmentEntityID;
        DisconnectAdjustmentEnabled = _DisconnectAdjustmentEnabled;
        return result;

    }
}

