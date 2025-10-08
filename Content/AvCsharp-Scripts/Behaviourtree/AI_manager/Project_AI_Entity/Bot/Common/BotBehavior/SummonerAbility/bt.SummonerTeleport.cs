using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerTeleportClass : AI_Characters 
{
    private FindClosestTargetClass findClosestTarget = new FindClosestTargetClass ();
     public bool SummonerTeleport(
           AttackableUnit Self,
      Vector3 GotoPosition,
      int TeleportSlot

         )
    {
        return
              // Sequence name :Selector
              (
                    TestUnitHasBuff(
                          Self,
                          default,
                          "SummonerTeleport",
                          true) ||
                    // Sequence name :TeleportCheck
                    (
                          // Sequence name :Difficulty
                          (
                                TestEntityDifficultyLevel(
                                      true,
                                 EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) ||
                                      TestEntityDifficultyLevel(
                                      true,
                                 EntityDiffcultyType.DIFFICULTY_ADVANCED)
                          ) &&
                          NotEqualInt(
                                TeleportSlot,
                                -1) &&
                          GetUnitPosition(
                                out SelfPosition,
                                Self) &&
                          CountUnitsInTargetArea(
                                out Count,
                                Self,
                                SelfPosition,
                                1000,
                                AffectEnemies | AffectHeroes | AffectTurrets,
                                "") &&
                          LessEqualInt(
                                Count,
                                0) &&
                          TestCanCastSpell(
                                Self,
                                SPELLBOOK_SUMMONER,
                                TeleportSlot,
                                true) &&
                          GetUnitsInTargetArea(
                                out Allies,
                                Self,
                                GotoPosition,
                                2500,
                                AffectFriends | AffectMinions) &&
                          GetCollectionCount(
                                out Count,
                                Allies) &&
                        findClosestTarget.FindClosestTarget(
                                out ClosestTarget,
                                Self,
                                Allies,
                                Self,
                                false,
                                false,
                                GotoPosition,
                                false,
                                "",
                                false) &&
                          SetAIUnitSpellTarget(
                                ClosestTarget,
                                SPELLBOOK_SUMMONER,
                                TeleportSlot) &&
                          // Sequence name :SetUnitSpellIgnoreVisibility
                          (
                                // Sequence name :TeleportSlot==0
                                (
                                      TeleportSlot == 0 &&
                                      SetUnitSpellIgnoreVisibity(
                                            Self,
                                            SPELLBOOK_SUMMONER,
                                            0,
                                            true)
                                ) ||
                                // Sequence name :TeleportSlot==1
                                (
                                      TeleportSlot == 1 &&
                                      SetUnitSpellIgnoreVisibity(
                                            Self,
                                            SPELLBOOK_SUMMONER,
                                            1,
                                            true)
                                )
                          ) &&
                          CastUnitSpell(
                                Self,
                                SPELLBOOK_SUMMONER,
                                TeleportSlot,
                                default, default
                                )

                    )
              );


      }
}

