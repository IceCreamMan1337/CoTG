namespace BehaviourTrees.all;


class MinValueClass : AI_Characters
{


    public bool MinValue(
         out float _minValue,
     float Value1,
     float Value2,
     float Value3,
     float Value4,
     float Value5
        )
    {

        float minValue = default;
        bool result =
                    // Sequence name :FindTheLowestValue

                    SetVarFloat(
                          out minValue,
                          1E+09f) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value1) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value2) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value3) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value4) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value5)

              ;
        _minValue = minValue;
        return result;
    }
}

