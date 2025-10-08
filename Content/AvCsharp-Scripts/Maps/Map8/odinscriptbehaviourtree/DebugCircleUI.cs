using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;

/*
class DebugCircleUIClass : OdinLayout 
{


     public bool DebugCircleUI(

                float CaptureProgress,
    float CaptureRadius,
    int ChaosCaptureID,
    TeamId CapPointOwner,
    int OrderDebugID,
    int NeutralDebugID
          )
      {
      return
            // Sequence name :Selector
            (
                  // Sequence name :Order capping
                  (
                        GreaterFloat(
                              CaptureProgress, 
                              0) &&
                        DivideFloat(
                              out DebugMultiplierA, 
                              CaptureProgress, 
                              100) &&
                        MultiplyFloat(
                              out OrderDebugRadiusA, 
                              DebugMultiplierA, 
                              CaptureRadius) &&
                        ModifyDebugCircleRadius(
                              OrderDebugID, 
                              OrderDebugRadiusA) &&
                        ModifyDebugCircleRadius(
                              ChaosCaptureID, 
                              0)
                  ) ||
                  // Sequence name :Chaos capping
                  (
                        LessFloat(
                              CaptureProgress, 
                              0) &&
                        AbsFloat(
                              out AbsRadiusA, 
                              CaptureProgress) &&
                        DivideFloat(
                              out DebugMultiplierA, 
                              AbsRadiusA, 
                              100) &&
                        MultiplyFloat(
                              out ChaosDebugRadiusA, 
                              DebugMultiplierA, 
                              CaptureRadius) &&
                        ModifyDebugCircleRadius(
                              ChaosCaptureID, 
                              ChaosDebugRadiusA) &&
                        ModifyDebugCircleRadius(
                              OrderDebugID, 
                              0)

                  )
            );
      }
}

*/