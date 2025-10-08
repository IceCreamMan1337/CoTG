namespace BehaviourTrees.all;


class Graves_KillChampionAttackSequenceClass : AI_Characters
{
    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private SummonerIgniteClass summonerIgnite = new();
    private SummonerExhaustClass summonerExhaust = new();
    public bool Graves_KillChampionAttackSequence(
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
                  // Sequence name :GravesKillSequence

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                              // Sequence name :Sequence

                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              // Sequence name :GravesKillChampion
                              (
                                    // Sequence name :UseUltimate
                                    (
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.6f) &&
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
                                    // Sequence name :UseIgnite
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.5f) &&
                                    summonerIgnite.SummonerIgnite(
                                                Self,
                                                Target,
                                                0)
                                    ) ||
                                    // Sequence name :UseExhaust
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.5f) &&
                                     summonerExhaust.SummonerExhaust(
                                                Self,
                                                Target,
                                                ExhaustSlot)
                                    ) ||
                                    // Sequence name :UseQ
                                    (
                                                // Sequence name :GetRandomChance

                                                GenerateRandomFloat(
                                                      out RandomFloat,
                                                      0,
                                                      100) &&
                                                LessFloat(
                                                      RandomFloat,
                                                      75)
                                           &&
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
                                    // Sequence name :UseW
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
                                                // Sequence name :GetSkillShotCastPosition(AoE)

                                                GetDistanceBetweenUnits(
                                                      out DistanceToTarget,
                                                      Target,
                                                      Self) &&
                                                DivideFloat(
                                                      out SkillShotAoERadius,
                                                      DistanceToTarget,
                                                      4) &&
                                                GetUnitPosition(
                                                      out TargetPosition,
                                                      Target) &&
                                                GetRandomPositionInCircle(
                                                      out SkillShotCastPosition,
                                                      TargetPosition,
                                                      SkillShotAoERadius)
                                           &&
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
                                                CastSpellTimeThreshold,
                                                SkillShotCastPosition,
                                                true)
                                    ) ||
                                    // Sequence name :UseE
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                2,
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
                                                2,
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
                  (
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                              // Sequence name :Sequence

                              GetUnitHealthRatio(
                                    out HealthRatio,
                                    Self) &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              SubtractFloat(
                                    out HealthRatioDelta,
                                    HealthRatio,
                                    TargetHealthRatio) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget,
                                    Target,
                                    Self) &&
                              // Sequence name :GravesKillChampion
                              (
                                    // Sequence name :UseUltimate
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.6f) &&
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
                                    // Sequence name :UseIgnite
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.5f) &&
                                    summonerIgnite.SummonerIgnite(
                                                Self,
                                                Target,
                                                0)
                                    ) ||
                                    // Sequence name :UseExhaust
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.5f) &&
                                      summonerExhaust.SummonerExhaust(
                                                Self,
                                                Target,
                                                ExhaustSlot)
                                    ) ||
                                    // Sequence name :UseQ
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
                                    // Sequence name :UseW
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
                                          PredictLineMissileCastPosition(
                                                out SkillShotCastPosition,
                                                Target,
                                                SelfPosition,
                                                1000,
                                                0.25f) &&
                                          GetRandomPositionBetweenTwoPoints(
                                                out SkillShotCastPosition,
                                                TargetPosition,
                                                SkillShotCastPosition,
                                                0.3f) &&
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
                                                CastSpellTimeThreshold,
                                                SkillShotCastPosition,
                                                true)
                                    ) ||
                                    // Sequence name :UseE
                                    (
                                          GreaterFloat(
                                                HealthRatioDelta,
                                                0.15f) &&
                                          // Sequence name :DashDistance
                                          (
                                                // Sequence name :DistanceToTarget&lt,600
                                                (
                                                      LessFloat(
                                                            DistanceToTarget,
                                                            600) &&
                                                            // Sequence name :UseE

                                                            canCastChampionAbilityClass.CanCastChampionAbility(
                                                                  Self,
                                                                  2,
                                                                  PreviousSpellCastTime,
                                                                  CastSpellTimeThreshold,
                                                                  PreviousSpellCastTarget,
                                                                  Self,
                                                                  PreviousSpellCast,
                                                                  false,
                                                                  false) &&
                                                            ComputeUnitAISpellPosition(
                                                                  Target,
                                                                  Self,
                                                                  25,
                                                                  false) &&
                                                            GetUnitAISpellPosition(
                                                                  out SpellCastPosition) &&
                                                           castTargetAbility.CastTargetAbility(
                                                                  out CurrentSpellCast,
                                                                  out CurrentSpellCastTarget,
                                                                  out PreviousSpellCastTime,
                                                                  out CastSpellTimeThreshold,
                                                                  Self,
                                                                  Self,
                                                                  2,
                                                                  2,
                                                                  PreviousSpellCast,
                                                                  PreviousSpellCastTarget,
                                                                  PreviousSpellCastTime,
                                                                  CastSpellTimeThreshold,
                                                                  SpellCastPosition,
                                                                  true)

                                                ) ||
                                                // Sequence name :DistanceToTarget&lt,=600
                                                (
                                                      GreaterEqualFloat(
                                                            DistanceToTarget,
                                                            600) &&
                                                      canCastChampionAbilityClass.CanCastChampionAbility(
                                                            Self,
                                                            2,
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
                                                            2,
                                                            1,
                                                            PreviousSpellCast,
                                                            PreviousSpellCastTarget,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold, default
                                                            ,
                                                            false)
                                                )
                                          )
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

