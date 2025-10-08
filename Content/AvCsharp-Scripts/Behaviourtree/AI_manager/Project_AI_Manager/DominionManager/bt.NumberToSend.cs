using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class NumberToSendClass : CommonAI 
{


     public bool NumberToSend(
    out int NumberToSend,
    float Need)
      {

        int _NumberToSend = default;
        bool result =
            // Sequence name :Selector
            (
                  // Sequence name :If Need &lt; 1
                  (
                        LessFloat(
                              Need, 
                              1) &&
                        SetVarInt(
                              out _NumberToSend, 
                              0)
                  ) ||
                  // Sequence name :Else If Need &lt; 2
                  (
                        LessFloat(
                              Need, 
                              2) &&
                        SetVarInt(
                              out _NumberToSend, 
                              1)
                  ) ||
                  // Sequence name :Else If Need &lt; 3
                  (
                        LessFloat(
                              Need, 
                              3) &&
                        SetVarInt(
                              out _NumberToSend, 
                              2)
                  ) ||
                  // Sequence name :Else If Need &lt; 4
                  (
                        LessFloat(
                              Need, 
                              4) &&
                        SetVarInt(
                              out _NumberToSend, 
                              3)
                  ) ||
                  // Sequence name :Else If Need &lt; 5
                  (
                        LessFloat(
                              Need, 
                              5) &&
                        SetVarInt(
                              out _NumberToSend, 
                              4)
                  ) ||
                  SetVarInt(
                        out _NumberToSend, 
                        5)

            );
        NumberToSend = _NumberToSend;

        return result; 
    }
}

