namespace BehaviourTrees;


class DualUpdateScoreMathClass : OdinLayout
{


    public bool DualUpdateScoreMath(
               out float __TotalOrderScore,
     out float __TotalChaosScore,
   float TotalOrderScore,
   float TotalChaosScore,
   int OrderControlCount,
   int ChaosControlCount
         )
    {

        float _TotalOrderScore = TotalOrderScore;
        float _TotalChaosScore = TotalChaosScore;

        bool result =
                          // Sequence name :Sequence

                          NormalizeFloatToTickRate(
                                out OnePointRate,
                                1f) &&
                          NormalizeFloatToTickRate(
                                out TwoPointRate,
                                1.5f) &&
                          NormalizeFloatToTickRate(
                                out ThreePointRate,
                                2f) &&
                          NormalizeFloatToTickRate(
                                out FourPointRate,
                                2.5f) &&
                          NormalizeFloatToTickRate(
                                out FivePointRate,
                                10f) &&
                          // Sequence name :Selector
                          (
                                OrderControlCount == 0 ||
                                // Sequence name :Sequence
                                (
                                      OrderControlCount == 1 &&
                                      AddFloat(
                                            out _TotalOrderScore,
                                            _TotalOrderScore,
                                            OnePointRate)
                                ) ||
                                // Sequence name :Sequence
                                (
                                      OrderControlCount == 2 &&
                                      AddFloat(
                                            out _TotalOrderScore,
                                            _TotalOrderScore,
                                            TwoPointRate)
                                ) ||
                                // Sequence name :Sequence
                                (
                                      OrderControlCount == 3 &&
                                      AddFloat(
                                            out _TotalOrderScore,
                                            _TotalOrderScore,
                                            ThreePointRate)
                                ) ||
                                // Sequence name :Sequence
                                (
                                      OrderControlCount == 4 &&
                                      AddFloat(
                                            out _TotalOrderScore,
                                            _TotalOrderScore,
                                            FourPointRate)
                                ) ||
                                      // Sequence name :Sequence

                                      AddFloat(
                                            out _TotalOrderScore,
                                            _TotalOrderScore,
                                            FivePointRate)

                          ) &&
                          // Sequence name :Selector
                          (
                                ChaosControlCount == 0 ||                      // Sequence name :Sequence
                                (
                                      ChaosControlCount == 1 &&
                                      AddFloat(
                                            out _TotalChaosScore,
                                            _TotalChaosScore,
                                            OnePointRate)
                                ) ||
                                // Sequence name :Sequence
                                (
                                      ChaosControlCount == 2 &&
                                      AddFloat(
                                            out _TotalChaosScore,
                                            _TotalChaosScore,
                                            TwoPointRate)
                                ) ||
                                // Sequence name :Sequence
                                (
                                      ChaosControlCount == 3 &&
                                      AddFloat(
                                            out _TotalChaosScore,
                                                _TotalChaosScore,
                                            ThreePointRate)
                                ) ||
                                // Sequence name :Sequence
                                (
                                      ChaosControlCount == 4 &&
                                      AddFloat(
                                            out _TotalChaosScore,
                                            _TotalChaosScore,
                                            FourPointRate)
                                ) ||
                                      // Sequence name :Sequence

                                      AddFloat(
                                            out _TotalChaosScore,
                                            _TotalChaosScore,
                                            FivePointRate)

                          ) &&
                          SetGameScore(
                                TeamId.TEAM_ORDER,
                                _TotalOrderScore) &&
                          SetGameScore(
                                TeamId.TEAM_CHAOS,
                                _TotalChaosScore)

                    ;
        __TotalOrderScore = _TotalOrderScore;
        __TotalChaosScore = _TotalChaosScore;

        return result;
    }
}

