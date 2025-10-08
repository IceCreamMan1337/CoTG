namespace BehaviourTrees.all;


class Chogath_KillChampionAttackSequenceClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private AutoAttackClass autoAttack = new();
    private SummonerExhaustClass summonerExhaust = new();
    public bool Chogath_KillChampionAttackSequence(
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
                  // Sequence name :ChogathKillSequence

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                              // Sequence name :Cho'GathKillChampion

                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              // Sequence name :ChogathKillChampion
                              (
                                    // Sequence name :UseDFGIfUltIsReady
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
                                          TestUnitAICanUseItem(
                                                3128) &&
                                          IssueUseItemOrder(
                                                3128,
                                                Target)
                                    ) ||
                                    // Sequence name :UseUltimate
                                    (
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.3f) &&
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
                                    // Sequence name :Rupture
                                    (
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
                                    // Sequence name :CastW
                                    (
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
                                                Target,
                                                1,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :UseDFGIfTargetIsLow
                                    (
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.1f) &&
                                          TestUnitAICanUseItem(
                                                3128) &&
                                          IssueUseItemOrder(
                                                3128,
                                                Target)
                                    ) ||
                                    // Sequence name :UseExhaust
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                  summonerExhaust.SummonerExhaust(
                                                Self,
                                                Target,
                                                ExhaustSlot)
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
                  (
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                              // Sequence name :Cho'GathKillChampion

                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              // Sequence name :ChogathKillChampion
                              (
                                    // Sequence name :UseDFGIfUltIsReady
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
                                          TestUnitAICanUseItem(
                                                3128) &&
                                          IssueUseItemOrder(
                                                3128,
                                                Target)
                                    ) ||
                                    // Sequence name :UseUltimate
                                    (
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.3f) &&
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
                                    // Sequence name :Rupture
                                    (
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
                                          PredictLineMissileCastPosition(
                                                out SkillShotCastPosition,
                                                Target,
                                                SelfPosition,
                                                100000,
                                                1) &&
                                          GetRandomPositionBetweenTwoPoints(
                                                out NewSkillShotCastPosition,
                                                TargetPosition,
                                                SkillShotCastPosition,
                                                0.3f) &&
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
                                                CastSpellTimeThreshold,
                                                NewSkillShotCastPosition,
                                                true)
                                    ) ||
                                    // Sequence name :CastW
                                    (
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
                                                Target,
                                                1,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :UseDFGIfTargetIsLow
                                    (
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.1f) &&
                                          TestUnitAICanUseItem(
                                                3128) &&
                                          IssueUseItemOrder(
                                                3128,
                                                Target)
                                    ) ||
                                    // Sequence name :UseExhaust
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                      summonerExhaust.SummonerExhaust(
                                                Self,
                                                Target,
                                                ExhaustSlot)
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

                  )
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

