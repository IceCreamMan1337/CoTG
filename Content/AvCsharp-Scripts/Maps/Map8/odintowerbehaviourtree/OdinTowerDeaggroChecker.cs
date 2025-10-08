using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class OdinTowerDeaggroCheckerClass : OdinLayout 
{
      

     public bool OdinTowerDeaggroChecker(
          out bool LostAggro,
    Vector3 AggroPosition,
      AttackableUnit AggroTarget
    )
      {

       bool _LostAggro = default;
      bool result = 
            // Sequence name :OdinTowerDeaggroChecker
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
                                    out _LostAggro, 
                                    true)
                        ) ||
                        SetVarBool(
                              out _LostAggro, 
                              false)

                  )
            );
        LostAggro = _LostAggro;
        return result;
      }
}

