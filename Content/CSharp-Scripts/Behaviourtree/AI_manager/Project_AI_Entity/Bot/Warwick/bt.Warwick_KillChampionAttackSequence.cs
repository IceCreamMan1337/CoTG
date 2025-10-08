using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Warwick_KillChampionAttackSequenceClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private SummonerGhostClass summonerGhost = new();
    public bool Warwick_KillChampionAttackSequence(
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
                  // Sequence name :WarwickKillChampion

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                              // Sequence name :WarwickKillChampion

                              GetUnitPosition(
                                    out SelfPosition,
                                    Self) &&
                              GetUnitHealthRatio(
                                    out HealthRatio,
                                    Target) &&
                              GetDistanceBetweenUnits(
                                    out Distance,
                                    Target,
                                    Self) &&
                              // Sequence name :WarwickKillChampion
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          Self,
                                          "InfiniteDuress",
                                          true)
                                    ||
                                    // Sequence name :UseEntropy
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                          TestUnitAICanUseItem(
                                                3184) &&
                                          // Sequence name :UseConditions
                                          (
                                                      // Sequence name :TargetIsSuperLowHealth

                                                      LessFloat(
                                                            HealthRatio,
                                                            0.3f)
                                                 ||
                                                // Sequence name :TargetIsLowAndThereAreAlliesAround
                                                (
                                                      LessFloat(
                                                            HealthRatio,
                                                            0.7f) &&
                                                      CountUnitsInTargetArea(
                                                            out FriendsCount,
                                                            Self,
                                                            SelfPosition,
                                                            700,
                                                            AffectFriends | AffectHeroes | NotAffectSelf,
                                                            "") &&
                                                      GreaterEqualInt(
                                                            FriendsCount,
                                                            1)
                                                )
                                          ) &&
                                          IssueUseItemOrder(
                                                3184, default
                                                )
                                    ) ||
                                    // Sequence name :UseUltimate
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                          // Sequence name :UseConditions
                                          (
                                                      // Sequence name :TargetIsSuperLowHealth

                                                      LessFloat(
                                                            HealthRatio,
                                                            0.3f)
                                                 ||
                                                // Sequence name :TargetIsLowAndThereAreAlliesAround
                                                (
                                                      LessFloat(
                                                            HealthRatio,
                                                            0.7f) &&
                                                      CountUnitsInTargetArea(
                                                            out FriendsCount,
                                                            Self,
                                                            SelfPosition,
                                                            700,
                                                            AffectFriends | AffectHeroes | NotAffectSelf,
                                                            "") &&
                                                      GreaterEqualInt(
                                                            FriendsCount,
                                                            1)
                                                )
                                          ) &&
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
                                    // Sequence name :Ghost
                                    (
                                          LessFloat(
                                                HealthRatio,
                                                0.5f) &&
                                          LessFloat(
                                                Distance,
                                                300) &&
                                   summonerGhost.SummonerGhost(
                                                Self,
                                                GhostSlot)
                                    ) ||
                                    // Sequence name :CastW
                                    (
                                          LessFloat(
                                                Distance,
                                                200) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                1,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Self,
                                                1,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :CastQ
                                    (
                                          GetUnitHealthRatio(
                                                out MyHealthRatio,
                                                Self) &&
                                          LessFloat(
                                                MyHealthRatio,
                                                0.7f) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                0,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Target,
                                                0,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                           // Sequence name :AutoAttackOnlyOnKill

                                           autoAttack.AutoAttack(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                Target,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)

                              )

                  ) ||
                              // Sequence name :OtherSpells

                              // Sequence name :WarwickKillChampion

                              (GetUnitPosition(
                                    out SelfPosition,
                                    Self) &&
                              GetUnitHealthRatio(
                                    out HealthRatio,
                                    Target) &&
                              GetDistanceBetweenUnits(
                                    out Distance,
                                    Target,
                                    Self) &&
                              // Sequence name :WarwickKillChampion
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          Self,
                                          "InfiniteDuress",
                                          true)
                                    ||
                                    // Sequence name :UseEntropy
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                          TestUnitAICanUseItem(
                                                3184) &&
                                          // Sequence name :UseConditions
                                          (
                                                      // Sequence name :TargetIsSuperLowHealth

                                                      LessFloat(
                                                            HealthRatio,
                                                            0.3f)
                                                 ||
                                                // Sequence name :TargetIsLowAndThereAreAlliesAround
                                                (
                                                      LessFloat(
                                                            HealthRatio,
                                                            0.7f) &&
                                                      CountUnitsInTargetArea(
                                                            out FriendsCount,
                                                            Self,
                                                            SelfPosition,
                                                            700,
                                                            AffectFriends | AffectHeroes | NotAffectSelf,
                                                            "") &&
                                                      GreaterEqualInt(
                                                            FriendsCount,
                                                            1)
                                                )
                                          ) &&
                                          IssueUseItemOrder(
                                                3184, default
                                                )
                                    ) ||
                                    // Sequence name :UseUltimate
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                          // Sequence name :UseConditions
                                          (
                                                      // Sequence name :TargetIsSuperLowHealth

                                                      LessFloat(
                                                            HealthRatio,
                                                            0.3f)
                                                 ||
                                                // Sequence name :TargetIsLowAndThereAreAlliesAround
                                                (
                                                      LessFloat(
                                                            HealthRatio,
                                                            0.7f) &&
                                                      CountUnitsInTargetArea(
                                                            out FriendsCount,
                                                            Self,
                                                            SelfPosition,
                                                            700,
                                                            AffectFriends | AffectHeroes | NotAffectSelf,
                                                            "") &&
                                                      GreaterEqualInt(
                                                            FriendsCount,
                                                            1)
                                                )
                                          ) &&
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
                                    // Sequence name :Ghost
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                HealthRatio,
                                                0.5f) &&
                                          LessFloat(
                                                Distance,
                                                300) &&
                                     summonerGhost.SummonerGhost(
                                                Self,
                                                GhostSlot)
                                    ) ||
                                    // Sequence name :CastW
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                Distance,
                                                200) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                1,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Self,
                                                1,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :CastQ
                                    (
                                          GetUnitHealthRatio(
                                                out MyHealthRatio,
                                                Self) &&
                                          LessFloat(
                                                MyHealthRatio,
                                                0.7f) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                0,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Target,
                                                0,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                           // Sequence name :AutoAttackOnlyOnKill

                                           autoAttack.AutoAttack(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                Target,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)


                              ))


            ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __IssuedAttackTarget = _IssuedAttackTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttack = _IssuedAttack;
        return result;


    }
}

