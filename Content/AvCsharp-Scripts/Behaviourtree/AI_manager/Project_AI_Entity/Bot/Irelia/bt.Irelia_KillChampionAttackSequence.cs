using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Irelia_KillChampionAttackSequenceClass : AI_Characters 
{
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttack_MeleeClass autoAttack_Melee = new AutoAttack_MeleeClass();
    private LastHitClass lastHit = new LastHitClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();

    public bool Irelia_KillChampionAttackSequence(
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
            // Sequence name :IreliaKillSequence
            (
                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                        EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :IreliaKillChampion
                        (
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              GetUnitHealthRatio(
                                    out SelfHealthRatio,
                                    Self) &&
                              GetUnitAIBasePosition(
                                    out SelfPosition,
                                    Self) &&
                              // Sequence name :IreliaKillChampion
                              (
                                    // Sequence name :UseUltimate
                                    (
                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "IreliaTranscendentBladesSpell",
                                                true) &&
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
                                    // Sequence name :UseE
                                    (
                                          GreaterFloat(
                                                TargetHealthRatio,
                                                SelfHealthRatio) &&
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
                                    // Sequence name :UseW
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                1,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Self,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                          // Sequence name :GetRandomChance
                                          (
                                                GenerateRandomFloat(
                                                      out RandomFloat,
                                                      0,
                                                      100) &&
                                                LessFloat(
                                                      RandomFloat,
                                                      75)
                                          ) &&
                                          GetDistanceBetweenUnits(
                                                out DistanceToTarget,
                                                Target,
                                                Self) &&
                                          LessFloat(
                                                DistanceToTarget,
                                                400) &&
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
                                    // Sequence name :UseQ
                                    (
                                          // Sequence name :GetRandomChance
                                          (
                                                GenerateRandomFloat(
                                                      out RandomFloat,
                                                      0,
                                                      100) &&
                                                LessFloat(
                                                      RandomFloat,
                                                      75)
                                          ) &&
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
                                                Self,
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
                                                3,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
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
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        // Sequence name :IreliaKillChampion
                        (
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              GetUnitHealthRatio(
                                    out SelfHealthRatio,
                                    Self) &&
                              GetUnitAIBasePosition(
                                    out SelfPosition,
                                    Self) &&
                              // Sequence name :IreliaKillChampion
                              (
                                    // Sequence name :UseE
                                    (
                                          GreaterFloat(
                                                TargetHealthRatio,
                                                SelfHealthRatio) &&
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
                                    // Sequence name :UseUltimate
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "IreliaTranscendentBladesSpell",
                                                true) &&
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
                                          PredictLineMissileCastPosition(
                                                out SkillShotCastPosition,
                                                Target,
                                                SelfPosition,
                                                1600,
                                                0.25f) &&
                                          GetRandomPositionBetweenTwoPoints(
                                                out SkillShotCastPosition,
                                                TargetPosition,
                                                SkillShotCastPosition, default
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
                                                CastSpellTimeThreshold,
                                                SkillShotCastPosition,
                                                true)
                                    ) ||
                                    // Sequence name :UseW
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                1,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Self,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                          GetDistanceBetweenUnits(
                                                out DistanceToTarget,
                                                Target,
                                                Self) &&
                                          LessFloat(
                                                DistanceToTarget,
                                                400) &&
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
                                    // Sequence name :UseQOnMinion
                                    (
                                          GetUnitAIClosestTargetInArea(
                                                out ClosestEnemyMinion,
                                                Self, default
                                                ,
                                                true,
                                                SelfPosition,
                                                600,
                                                AffectEnemies | AffectMinions) &&
                                          GetDistanceBetweenUnits(
                                                out DistanceToClosestEnemyHero,
                                                Self,
                                                Target) &&
                                          GetDistanceBetweenUnits(
                                                out DistanceToClosestEnemyMinion,
                                                Self,
                                                ClosestEnemyMinion) &&
                                          GetDistanceBetweenUnits(
                                                out DistanceToClosestEnemyMinionFromTarget,
                                                ClosestEnemyMinion,
                                                Target) &&
                                          LessFloat(
                                                DistanceToClosestEnemyMinionFromTarget,
                                                DistanceToClosestEnemyHero) &&
                                          SubtractFloat(
                                                out DistanceDifference,
                                                DistanceToClosestEnemyMinion,
                                                DistanceToClosestEnemyHero) &&
                                          GreaterEqualFloat(
                                                DistanceDifference,
                                                200) &&
                                          GetUnitHealthRatio(
                                                out MinionHealthRatio,
                                                ClosestEnemyMinion) &&
                                          LessFloat(
                                                MinionHealthRatio,
                                                0.2f) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                0,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                ClosestEnemyMinion,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                          GetUnitPosition(
                                                out TargetPosition,
                                                ClosestEnemyMinion) &&
                                          PredictLineMissileCastPosition(
                                                out SkillShotCastPosition,
                                                ClosestEnemyMinion,
                                                SelfPosition,
                                                1600,
                                                0.25f) &&
                                          GetRandomPositionBetweenTwoPoints(
                                                out SkillShotCastPosition,
                                                TargetPosition,
                                                SkillShotCastPosition, default
                                                ) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                ClosestEnemyMinion,
                                                0,
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
                                                Self,
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
                                                3,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
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

