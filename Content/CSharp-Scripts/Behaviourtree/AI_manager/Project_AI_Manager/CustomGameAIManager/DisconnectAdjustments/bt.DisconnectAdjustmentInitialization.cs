/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class DisconnectAdjustmentInitialization : BehaviourTree 
{
      out int DisconnectAdjustmentEntityID;
      out bool DisconnectAdjustmentEnabled;

      bool DisconnectAdjustmentInitialization()
      {
      return
            // Sequence name :Sequence
            (
                  SetVarBool(
                        out DisconnectAdjustmentEnabled, 
                        true) &&
                  SetVarInt(
                        out DisconnectAdjustmentEntityID, 
                        -1)

            );
      }
}

*/