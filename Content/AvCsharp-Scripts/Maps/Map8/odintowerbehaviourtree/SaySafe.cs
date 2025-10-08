using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class SaySafeClass : OdinLayout 
{

     public bool SaySafe(
              out string __CurrentString,
    AttackableUnit Self,
    string NewString,
    string CurrentString

        
        )
      {

        string _CurrentString = CurrentString;
bool result = 
            // Sequence name :Selector
            (
                  // Sequence name :IfDefault
                  (
                        _CurrentString == default
                  ) ||
                  // Sequence name :IfSame
                  (
                        _CurrentString == NewString
                  ) ||
                  // Sequence name :IfNew
                  (
                        NotEqualString(
                              _CurrentString, 
                              NewString) &&
                        Say(
                              Self, 
                              NewString)

                  )
            );
        __CurrentString = _CurrentString;
        return result;
      }
}

