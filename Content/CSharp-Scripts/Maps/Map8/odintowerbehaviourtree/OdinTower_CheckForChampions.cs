using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class OdinTower_CheckForChampionsClass_forscript : OdinLayout
{


    public bool OdinTower_CheckForChampions(
               out int FriendlyChampCount,
   AttackableUnit Self,
   Vector3 SelfPosition,
     float AttackRadius,
   float DefenseRadius
         )
    {

        int _FriendlyChampCount = default;
        bool result =
                                // Sequence name :BaseCheckForChampions

                                // Sequence name :MakeSureFriendsArePresent

                                GetUnitsInTargetArea(
                                      out FriendlyChamps,
                                      Self,
                                      SelfPosition,
                                      DefenseRadius,
                                      AffectFriends | AffectHeroes) &&
                                GetCollectionCount(
                                      out FriendlyChampCount,
                                      FriendlyChamps) &&
                                GreaterInt(
                                      FriendlyChampCount,
                                      0)
                           &&
                                // Sequence name :MakeSureEnemiesArePresent

                                GetUnitsInTargetArea(
                                      out EnemyChamps,
                                      Self,
                                      SelfPosition,
                                      600,
                                      AffectEnemies | AffectHeroes | AffectMinions) &&
                                GetCollectionCount(
                                      out EnemyChampCount,
                                      EnemyChamps) &&
                                GreaterInt(
                                      EnemyChampCount,
                                      0)


                    ;

        FriendlyChampCount = _FriendlyChampCount;
        return result;
    }
}

