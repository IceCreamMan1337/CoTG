using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class ScoreboardPrototypeManagerClass : OdinLayout 
{


     public bool ScoreboardPrototypeManager(
                float CapturePointEProgress
          )
      {
      return
            // Sequence name :Selector
            (
                  // Sequence name :Initialization
                  (
                        __IsFirstRun == true
                  ) ||
                  // Sequence name :Sequence
                  (
                        AddString(
                              out DebugString,
                              "Prototype: ", 
                              $"{CapturePointEProgress}")

                  )
            );
      }
}

