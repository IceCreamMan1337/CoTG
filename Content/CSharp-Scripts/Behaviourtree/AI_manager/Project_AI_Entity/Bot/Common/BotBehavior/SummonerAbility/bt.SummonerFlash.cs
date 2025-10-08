using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class SummonerFlashClass : AI_Characters
{


    public bool SummonerFlash(
         AttackableUnit Self,
     int FlashSlot,
     AttackableUnit Target,
     bool FlashTowardTarget
        )
    {
        return
                    // Sequence name :FlashCheck

                    // Sequence name :Difficulty
                    (
                          TestEntityDifficultyLevel(
                                true,
                              EntityDiffcultyType.DIFFICULTY_INTERMEDIATE)
                          ||
                          TestEntityDifficultyLevel(
                                true,
                             EntityDiffcultyType.DIFFICULTY_ADVANCED)
                    ) &&
                    NotEqualInt(
                          FlashSlot,
                          -1) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          FlashSlot,
                          true) &&
                    ClearUnitAISpellPosition() &&
                    GetUnitSpellCastRange(
                          out _Range,
                          Self,
                          SPELLBOOK_SUMMONER,
                          FlashSlot) &&
                    // Sequence name :ComputeFlashPosition
                    (
                          // Sequence name :Offensive
                          (
                                FlashTowardTarget == true &&
                                GetDistanceBetweenUnits(
                                      out DistanceToTarget,
                                      Self,
                                      Target) &&
                                // Sequence name :DetermineFlashPosition
                                (
                                      // Sequence name :TargetOutsideOfFlashRange
                                      (
                                            GreaterFloat(
                                                  DistanceToTarget,
                                                  _Range) &&
                                            ComputeUnitAISpellPosition(
                                                  Self,
                                                  Target,
                                                  _Range,
                                                  true) &&
                                            GetUnitAISpellPosition(
                                                  out FlashPosition)
                                      ) ||
                                      GetUnitPosition(
                                            out FlashPosition,
                                            Target)
                                )
                          ) ||
                          // Sequence name :Defensive
                          (
                                FlashTowardTarget == false &&
                                ComputeUnitAISpellPosition(
                                      Self,
                                      Target,
                                      _Range,
                                      false) &&
                                GetUnitAISpellPosition(
                                      out FlashPosition)
                          )
                    ) &&
                    // Sequence name :SetSpellTargetLocation
                    (
                          // Sequence name :FlashSlot==0
                          (
                                FlashSlot == 0 &&
                                SetAIUnitSpellTargetLocation(
                                      FlashPosition,
                                      SPELLBOOK_SUMMONER,
                                      0)
                          ) ||
                          // Sequence name :FlashSlot==1
                          (
                                FlashSlot == 1 &&
                                SetAIUnitSpellTargetLocation(
                                      FlashPosition,
                                      SPELLBOOK_SUMMONER,
                                      1)
                          )
                    ) &&
                    // Sequence name :CastFlash
                    (
                          // Sequence name :OffensiveFlash
                          (
                                FlashTowardTarget == true &&
                                CastUnitSpell(
                                      Self,
                                      SPELLBOOK_SUMMONER,
                                      FlashSlot,
                                      default, default
                                      )
                          ) ||
                          // Sequence name :DefensiveFlash
                          (
                                FlashTowardTarget == false &&
                                CountUnitsInTargetArea(
                                      out EnemyHeroesAtDestination,
                                      Self,
                                      FlashPosition,
                                      _Range,
                                      AffectEnemies | AffectHeroes,
                                      "") &&
                                EnemyHeroesAtDestination == 0 &&
                                CastUnitSpell(
                                      Self,
                                      SPELLBOOK_SUMMONER,
                                      FlashSlot,
                                      default, default
                                      )

                          )
                    )
              ;



    }
}

