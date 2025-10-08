using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerCleanseClass : AI_Characters 
{
      

     public bool SummonerCleanse(
         AttackableUnit Self,
      int CleanseSlot
         )
      {
        return
              // Sequence name :CleanseCheck
              (
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
                          CleanseSlot,
                          -1) &&
                    // Sequence name :Conditions
                    (
                          TestUnitHasBuff(
                                Self,
                                default,
                                "SummonerDot",
                                true) ||
                          TestUnitHasBuff(
                                Self,
                                default,
                                "SummonerExhaust",
                                true) ||
                          TestUnitHasAnyBuffOfType(
                                Self,
                               BuffType.STUN,
                                true) ||
                          TestUnitHasAnyBuffOfType(
                                Self,
                              BuffType.SNARE,
                                true) ||
                          TestUnitHasAnyBuffOfType(
                                Self,
                               BuffType.CHARM,
                                true) ||
                          TestUnitHasAnyBuffOfType(
                                Self,
                              BuffType.SUPPRESSION,
                                true)
                    ) &&
                    // Sequence name :ExclusionList
                    (
                          TestUnitHasBuff(
                                Self,
                                default,
                                "Pulverize",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "RocketGrab2",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "PowerfistSlow",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "RuptureLaunch",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "FizzMoveback",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "MoveAwayCollision",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "Move",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "MoveAway",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "JarvanIVDragonStrikePH2",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "BlindMonkRDamage",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "UnstoppableForceStun",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "MaokaiTrunkLineStun",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "OrianaStun",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "PoppyHeroicChargePart2",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "PowerballStun",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "RivenKnockback",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "ShyvanaTransformDamage",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "Fling",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "VolibearQExtra",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "MonkeyKingSpinKnockup",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "XenZhaoKnockup",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "VayneCondemnMissile",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "BusterShot",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "LeonaZenithBladeRoot",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "BlindMonkRKick",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "LuluRBoom",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "NautilusKnockUp",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "NautilusPassiveRoot",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "NautilusAnchorDragGlobalRoot",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "NautilusAnchorDragRoot",
                                false) &&
                          TestUnitHasBuff(
                                Self,
                                default,
                                "HecarimRampStunCheck",
                                false)
                    ) &&
                    // Sequence name :MicroStunNextToBlitzcrank
                    (
                          GetUnitPosition(
                                out SelfPosition,
                                Self) &&
                          SetVarBool(
                                out IsBlitzMicroStun,
                                false) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Sequence
                                (
                                      TestUnitHasAnyBuffOfType(
                                            Self,
                                            BuffType.STUN,
                                            true) &&
                                      GetUnitAIClosestTargetInArea(
                                            out AreYouBlitzcrank,
                                            Self,
                                            default,
                                            true,
                                            SelfPosition,
                                            300,
                                            AffectEnemies | AffectHeroes) &&
                                      GetUnitSkinName(
                                            out SkinName,
                                            AreYouBlitzcrank) &&
                                      SkinName == "Blitzcrank" &&
                                      GetSpellSlotCooldown(
                                            out RocketGrabCooldown,
                                            AreYouBlitzcrank,
                                            SPELLBOOK_CHAMPION,
                                            0) &&
                                      GreaterFloat(
                                            RocketGrabCooldown,
                                            8) &&
                                      SetVarBool(
                                            out IsBlitzMicroStun,
                                            true)
                                )
                          ) &&
                          IsBlitzMicroStun == false
                    ) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          CleanseSlot,
                          true) &&
                    SetAIUnitSpellTarget(
                          Self,
                          SPELLBOOK_SUMMONER,
                          CleanseSlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          CleanseSlot,
                          default,
                          default
                          )

              );
      }
}

