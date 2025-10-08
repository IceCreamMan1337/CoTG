using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class MinionDeaggroCheckerClass : AI_Characters 
{
     

     public bool MinionDeaggroChecker(
          out bool _LostAggro,
      Vector3 AggroPosition,
      AttackableUnit AggroTarget
         )
      {

        bool LostAggro = default;
        bool result =
              // Sequence name :MinionDeaggroChecker
              (
                    DistanceBetweenObjectAndPoint(
                          out Distance,
                          AggroTarget,
                          AggroPosition) &&
                    // Sequence name :Test For Aggro Loss
                    (
                          // Sequence name :Test Target Distance
                          (
                                GreaterFloat(
                                      Distance,
                                      800) &&
                                SetVarBool(
                                      out LostAggro,
                                      true)
                          ) ||
                          SetVarBool(
                                out LostAggro,
                                false)

                    )
              );

        _LostAggro = LostAggro;
        return result;
      }
}

