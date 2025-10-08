using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class CountPointsClass : OdinLayout 
{


     public bool CountPoints(
                out int Total,
    TeamId CapturePointOwnerA,
    TeamId CapturePointOwnerB,
    TeamId CapturePointOwnerC,
    TeamId CapturePointOwnerD,
    TeamId CapturePointOwnerE,
    TeamId TeamToCountFor
          
          )
      {

        int _Total = default;

bool result = 
            // Sequence name :Sequence
            (
                  SetVarInt(
                        out _Total, 
                        0) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              CapturePointOwnerA == TeamToCountFor &&
                              AddInt(
                                    out _Total,
                                    _Total, 
                                    1)
                               &&  DebugAction("Capture A = " + CapturePointOwnerA)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              CapturePointOwnerB == TeamToCountFor &&
                              AddInt(
                                    out _Total,
                                    _Total, 
                                    1)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              CapturePointOwnerC == TeamToCountFor &&
                              AddInt(
                                    out _Total,
                                    _Total, 
                                    1)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              CapturePointOwnerD == TeamToCountFor &&
                              AddInt(
                                    out _Total,
                                    _Total, 
                                    1)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              CapturePointOwnerE == TeamToCountFor &&
                              AddInt(
                                    out _Total,
                                    _Total, 
                                    1)

                        )
                        ||
                               DebugAction("MaskFailure")
                  )
            );

        Total = _Total;
        return result;
      }
}

