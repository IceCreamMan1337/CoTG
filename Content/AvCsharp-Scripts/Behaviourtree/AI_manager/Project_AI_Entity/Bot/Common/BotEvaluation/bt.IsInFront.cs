using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class IsInFrontClass : AI_Characters 
{
      

     public bool IsInFront(
         out bool IsTargetInFront,
      AttackableUnit ReferenceUnit,
      AttackableUnit TargetUnit
         )
      {
        return
              // Sequence name :Sequence
              (
                    SetVarBool(
                          out IsTargetInFront,
                          false) &&
                    GetUnitAIBasePosition(
                          out ReferencePoint,
                          ReferenceUnit) &&
                    DistanceBetweenObjectAndPoint(
                          out DistanceToReferencePoint,
                          ReferenceUnit,
                          ReferencePoint) &&
                    DistanceBetweenObjectAndPoint(
                          out UnitDistanceToRefPoint,
                          TargetUnit,
                          ReferencePoint) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :Sequence
                          (
                                GreaterFloat(
                                      UnitDistanceToRefPoint,
                                      DistanceToReferencePoint) &&
                                SetVarBool(
                                      out IsTargetInFront,
                                      true)

                          )
                    )
              );


      }
}

