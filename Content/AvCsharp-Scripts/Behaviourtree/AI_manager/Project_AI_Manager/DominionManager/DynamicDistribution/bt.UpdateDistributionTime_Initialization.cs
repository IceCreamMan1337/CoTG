using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class UpdateDistributionTime_InitializationClass : bt_OdinManager
{

    public  bool UpdateDistributionTime_Initialization(
                out bool IsBeingCapturedA,
      out bool IsBeingCapturedB,
      out bool IsBeingCapturedC,
      out bool IsBeingCapturedD,
      out bool IsBeingCapturedE
          
          )
    {
        bool _IsBeingCapturedA = default;
      bool _IsBeingCapturedB = default;
        bool _IsBeingCapturedC = default;
        bool _IsBeingCapturedD = default;
        bool _IsBeingCapturedE = default;

bool result =
            // Sequence name :Sequence
            (
                  SetVarBool(
                        out _IsBeingCapturedA, 
                        false) &&
                  SetVarBool(
                        out _IsBeingCapturedB, 
                        false) &&
                  SetVarBool(
                        out _IsBeingCapturedC, 
                        false) &&
                  SetVarBool(
                        out _IsBeingCapturedD, 
                        false) &&
                  SetVarBool(
                        out _IsBeingCapturedE, 
                        false)

            );
        IsBeingCapturedA = _IsBeingCapturedA;
        IsBeingCapturedB = _IsBeingCapturedB;
        IsBeingCapturedC = _IsBeingCapturedC;
        IsBeingCapturedD = _IsBeingCapturedD;
        IsBeingCapturedE = _IsBeingCapturedE;
        return result;
      }
}

