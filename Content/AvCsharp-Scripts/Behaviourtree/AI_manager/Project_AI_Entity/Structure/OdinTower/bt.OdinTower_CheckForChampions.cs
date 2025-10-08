using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;

/*
class OdinTower_CheckForChampions : AI_Characters 
{
      out int FriendlyChampCount,
      AttackableUnit Self,
      Vector SelfPosition,
      float AttackRadius,
      float DefenseRadius,

     public bool OdinTower_CheckForChampions()
      {
      return
            // Sequence name :BaseCheckForChampions
            (
                  // Sequence name :MakeSureFriendsArePresent
                  (
                        GetUnitsInTargetArea(
                              out FriendlyChamps, 
                              Self, 
                              SelfPosition, 
                              DefenseRadius, 
                              AffectFriends,AffectHeroes, 
                              "") &&
                        GetCollectionCount(
                              out FriendlyChampCount, 
                              FriendlyChamps) &&
                        GreaterInt(
                              FriendlyChampCount, 
                              0)
                  ) &&
                  // Sequence name :MakeSureEnemiesArePresent
                  (
                        GetUnitsInTargetArea(
                              out EnemyChamps, 
                              Self, 
                              SelfPosition, 
                              600, 
                              AffectEnemies | AffectHeroes | AffectMinions, 
                              "") &&
                        GetCollectionCount(
                              out EnemyChampCount, 
                              EnemyChamps) &&
                        GreaterInt(
                              EnemyChampCount, 
                              0)

                  )
            ),
      }
}

*/