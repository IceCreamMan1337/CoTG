using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class FindFirstMinionInWaveClass : AI_Characters 
{
     

     public bool FindFirstMinionInWave(
          out AttackableUnit _Minion,
      AttackableUnit Self
         )
      {

        AttackableUnit Minion = default;
        bool result =
              // Sequence name :FindLastMinionInWave
              (
                    GetUnitPosition(
                          out SelfPosition,
                          Self) &&
                    GetUnitsInTargetArea(
                          out MinionsNearby,
                          Self,
                          SelfPosition,
                          800,
                          AffectFriends | AffectMinions)
                    &&
                    SetVarFloat(
                          out FurthestDistance,
                          -1) &&
                    GetUnitAIBasePosition(
                          out BasePosition,
                          Self) &&
                    GetCollectionCount(
                          out MinionCount,
                          MinionsNearby) &&
                    GreaterInt(
                          MinionCount,
                          0) &&
                    ForEach(MinionsNearby, TempMinion => (
                          // Sequence name :Sequence
                          (
                                DistanceBetweenObjectAndPoint(
                                      out DistanceToBase,
                                      TempMinion,
                                      BasePosition) &&
                                GreaterFloat(
                                      DistanceToBase,
                                      FurthestDistance) &&
                                SetVarFloat(
                                      out FurthestDistance,
                                      DistanceToBase) &&
                                SetVarAttackableUnit(
                                      out Minion,
                                      TempMinion)

                          )
                    )

                )
              );


        _Minion = Minion;

        return result;
    }
}

