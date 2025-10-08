namespace BehaviourTrees;


class RespawnFloorClass : OdinLayout
{


    public bool RespawnFloor(

   out int FlooredInt,
   float ToFloor)
    {

        int _FlooredInt = default;
        bool result =
                          // Sequence name :Selector

                          // Sequence name :&gt;10
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      10) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      10)
                          ) ||
                          // Sequence name :&gt;=9
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      9) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      9)
                          ) ||
                          // Sequence name :&gt;=8
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      8) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      8)
                          ) ||
                          // Sequence name :&gt;=7
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      7) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      7)
                          ) ||
                          // Sequence name :&gt;=6
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      6) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      6)
                          ) ||
                          // Sequence name :&gt;=5
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      5) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      5)
                          ) ||
                          // Sequence name :&gt;=4
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      4) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      4)
                          ) ||
                          // Sequence name :&gt;=3
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      3) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      3)
                          ) ||
                          // Sequence name :&gt;=2
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      2) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      2)
                          ) ||
                          // Sequence name :&gt;=1
                          (
                                GreaterEqualFloat(
                                      ToFloor,
                                      1) &&
                                SetVarInt(
                                      out _FlooredInt,
                                      1)
                          ) ||
                          SetVarInt(
                                out _FlooredInt,
                                0)

                    ;
        FlooredInt = _FlooredInt;
        return result;
    }
}

