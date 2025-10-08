using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class PersonalScore_InitializationClass : OdinLayout 
{


    public bool PersonalScore_Initialization(

                out bool IsFirstBlood)
      {

        bool _IsFirstBlood = default;
bool result = 
            // Sequence name :Sequence
            (
                  SetVarBool(
                        out _IsFirstBlood, 
                        true)

            );
        IsFirstBlood = _IsFirstBlood;
        return result;
      }
}

