using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Garen_KillChampionAttackSequenceClass : AI_Characters 
{
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttack_MeleeClass autoAttack_Melee = new AutoAttack_MeleeClass();
    private LastHitClass lastHit = new LastHitClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();

    public bool Garen_KillChampionAttackSequence(
         out AttackableUnit __IssuedAttackTarget,
      out bool __IssuedAttack,
      out AttackableUnit _CurrentSpellCastTarget,
      out int _CurrentSpellCast,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      AttackableUnit Target,
      int KillChampionScore,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      int ExhaustSlot,
      int FlashSlot,
      int GhostSlot,
      int IgniteSlot,
      bool IsDominionGameMode
         )
      {
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        bool _IssuedAttack = IssuedAttack;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
            // Sequence name :GarenKillSequence
            (
                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :GarenKillChampion
                        (
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              // Sequence name :WhichAbility
                              (
                                    // Sequence name :SpinToWin
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                               default,
                                                "GarenBladestorm",
                                                true) &&
                                      autoAttack_Melee.AutoAttack_Melee(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                Target,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)
                                    ) ||
                                    // Sequence name :UseUltimate
                                    (
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.33f) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                true) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Target,
                                                3,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :UseQ
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                0,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Self,
                                                PreviousSpellCast,
                                                false,
                                                true) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Self,
                                                0,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :UseE
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "GarenSlash3",
                                                false) &&
                                          GetDistanceBetweenUnits(
                                                out DistanceToTarget,
                                                Self,
                                                Target) &&
                                          LessFloat(
                                                DistanceToTarget,
                                                350) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                2,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                true) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Target,
                                                2,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :UseYoumuus
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          TestUnitAICanUseItem(
                                                3142) &&
                                          IssueUseItemOrder(
                                                3142, default
                                                )
                                    ) ||
                                    // Sequence name :UseGhost
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.2f) &&
                                       summonerGhost.SummonerGhost(
                                                Self,
                                                GhostSlot)
                                    ) ||
                                    // Sequence name :AutoAttackOnlyOnKill
                                    (
                                            autoAttack_Melee.AutoAttack_Melee(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                Target,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)
                                    )
                              )
                        )
                  ) ||
                  // Sequence name :OtherSpells
                  (
                        // Sequence name :GarenKillChampion
                        (
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              // Sequence name :WhichAbility
                              (
                                    // Sequence name :SpinToWin
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "GarenBladestorm",
                                                true) &&
                                            autoAttack_Melee.AutoAttack_Melee(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                Target,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)
                                    ) ||
                                    // Sequence name :UseUltimate
                                    (
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.33f) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                true) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Target,
                                                3,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :UseQ
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                0,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Self,
                                                PreviousSpellCast,
                                                false,
                                                true) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Self,
                                                0,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :UseE
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "GarenSlash3",
                                                false) &&
                                          GetDistanceBetweenUnits(
                                                out DistanceToTarget,
                                                Self,
                                                Target) &&
                                          LessFloat(
                                                DistanceToTarget,
                                                350) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                2,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                true) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Target,
                                                2,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :UseYoumuus
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          TestUnitAICanUseItem(
                                                3142) &&
                                          IssueUseItemOrder(
                                                3142, default
                                                )
                                    ) ||
                                    // Sequence name :UseGhost
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.2f) &&
                                     summonerGhost.SummonerGhost(
                                                Self,
                                                GhostSlot)
                                    ) ||
                                    // Sequence name :AutoAttackOnlyOnKill
                                    (
                                             autoAttack_Melee.AutoAttack_Melee(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                Target,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)

                                    )
                              )
                        )
                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __IssuedAttackTarget = _IssuedAttackTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttack = _IssuedAttack;
        return result;


    }
}

