using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class FindLastMinionInWaveClass : AI_Characters
{


    public bool FindLastMinionInWave(
         out AttackableUnit _Minion,
     AttackableUnit Self
        )
    {
        AttackableUnit Minion = default;

        bool result =
                    // Sequence name :FindLastMinionInWave

                    GetUnitPosition(
                          out SelfPosition,
                          Self) &&
                    GetUnitsInTargetArea(
                          out MinionsNearby,
                          Self,
                          SelfPosition,
                          800,
                          AffectFriends | AffectMinions) &&
                    SetVarFloat(
                          out NearestDistance,
                          50000) &&
                    GetUnitAIBasePosition(
                          out BasePosition,
                          Self) &&
                    GetCollectionCount(
                          out MinionCount,
                          MinionsNearby) &&
                    GreaterInt(
                          MinionCount,
                          0) &&
                    ForEach(MinionsNearby, TempMinion =>
                                // Sequence name :Sequence

                                DistanceBetweenObjectAndPoint(
                                      out DistanceToBase,
                                      TempMinion,
                                      BasePosition) &&
                                LessFloat(
                                      DistanceToBase,
                                      NearestDistance) &&
                                SetVarFloat(
                                      out NearestDistance,
                                      DistanceToBase) &&
                                SetVarAttackableUnit(
                                      out Minion,
                                      TempMinion)



                 )
              ;
        _Minion = Minion;
        return result;
    }
}

