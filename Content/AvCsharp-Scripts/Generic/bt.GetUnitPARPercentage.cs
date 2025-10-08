using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class GetUnitPARPercentageClass : BehaviourTree 
{


      bool GetUnitPARPercentage(
                out float MaxPAR,
      out float CurrentPAR,
      out float PAR_Percentage,
    PrimaryAbilityResourceType PARType,
    AttackableUnit Unit)
      {
        float _MaxPAR = default;
        float _CurrentPAR = default;
        float _PAR_Percentage = default;


        bool result = 
            // Sequence name :Sequence
            (
                  GetUnitMaxPAR(
                        out _MaxPAR, 
                        Unit, 
                        PARType) &&
                  GetUnitCurrentPAR(
                        out _CurrentPAR, 
                        Unit, 
                        PARType) &&
                  DivideFloat(
                        out _PAR_Percentage, 
                        _CurrentPAR, 
                        _MaxPAR)

            );

        MaxPAR = _MaxPAR;
        CurrentPAR = _CurrentPAR;
        PAR_Percentage = _PAR_Percentage;

        return result;
      }
}

