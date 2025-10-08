using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;

/*
class OdinTowerDeaggroChecker : AI_Characters 
{
      out bool LostAggro,
      Vector AggroPosition,
      AttackableUnit AggroTarget,

     public bool OdinTowerDeaggroChecker()
      {
      return
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
                                    out LostAggro, 
                                    true)
                        ) ||
                        SetVarBool(
                              out LostAggro, 
                              false)

                  )
            ),
      }
}

*/