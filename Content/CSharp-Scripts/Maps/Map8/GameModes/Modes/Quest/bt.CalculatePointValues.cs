namespace BehaviourTrees;


class CalculatePointValuesClass : OdinLayout
{

    private CalculatePointValues_HelperClass calculatePointValues_Helper = new();
    public bool CalculatePointValues(
                out float CalculatedValue_PointA,
      out float CalculatedValue_PointB,
      out float CalculatedValue_PointC,
      out float CalculatedValue_PointD,
      out float CalculatedValue_PointE,
    TeamId CapturePointOwnerA,
    TeamId CapturePointOwnerB,
    TeamId CapturePointOwnerC,
    TeamId CapturePointOwnerD,
    TeamId CapturePointOwnerE,
    TeamId ReferenceTeam,
    float PointA_Value,
    float PointB_Value,
    float PointC_Value,
    float PointD_Value,
    float PointE_Value

          )
    {

        float _CalculatedValue_PointA = default;
        float _CalculatedValue_PointB = default;
        float _CalculatedValue_PointC = default;
        float _CalculatedValue_PointD = default;
        float _CalculatedValue_PointE = default;



        bool result =
                          // Sequence name :Sequence

                          calculatePointValues_Helper.CalculatePointValues_Helper(
                                out _CalculatedValue_PointA,
                                PointA_Value,
                                CapturePointOwnerA,
                                ReferenceTeam) &&
                   calculatePointValues_Helper.CalculatePointValues_Helper(
                                out _CalculatedValue_PointB,
                                PointB_Value,
                                CapturePointOwnerB,
                                ReferenceTeam) &&
                        calculatePointValues_Helper.CalculatePointValues_Helper(
                                out _CalculatedValue_PointC,
                                PointC_Value,
                                CapturePointOwnerC,
                                ReferenceTeam) &&
                       calculatePointValues_Helper.CalculatePointValues_Helper(
                                out _CalculatedValue_PointD,
                                PointD_Value,
                                CapturePointOwnerD,
                                ReferenceTeam) &&
                       calculatePointValues_Helper.CalculatePointValues_Helper(
                                out _CalculatedValue_PointE,
                                PointE_Value,
                                CapturePointOwnerE,
                                ReferenceTeam)

                    ;

        CalculatedValue_PointA = _CalculatedValue_PointA;
        CalculatedValue_PointB = _CalculatedValue_PointB;
        CalculatedValue_PointC = _CalculatedValue_PointC;
        CalculatedValue_PointD = _CalculatedValue_PointD;
        CalculatedValue_PointE = _CalculatedValue_PointE;
        return result;
    }
}

