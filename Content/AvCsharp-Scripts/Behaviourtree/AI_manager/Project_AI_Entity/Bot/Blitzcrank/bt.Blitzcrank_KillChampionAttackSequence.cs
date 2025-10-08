using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Blitzcrank_KillChampionAttackSequenceClass : AI_Characters 
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
     public bool Blitzcrank_KillChampionAttackSequence(
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
            // Sequence name :BlitzcrankKillChampionLogic
            (
                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                            EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        GetDistanceBetweenUnits(
                              out DistanceToTarget,
                              Target,
                              Self) &&
                        // Sequence name :BlitzcrankKillChampion
                        (
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
                                    // Sequence name :EnemyInRange
                                    (
                                          GetUnitSpellRadius(
                                                out Radius,
                                                Self,
                                                SPELLBOOK_CHAMPION,
                                                3) &&
                                          SubtractFloat(
                                                out Radius,
                                                Radius,
                                                25) &&
                                          LessFloat(
                                                DistanceToTarget,
                                                Radius)
                                    ) &&
                                    // Sequence name :LowHealth
                                    (
                                          GetUnitHealthRatio(
                                                out TargetHealthRatio,
                                                Target) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.6f)
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
                                          default,
                                          false)
                              ) ||
                              // Sequence name :CastQ
                              (
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    LessFloat(
                                          DistanceToTarget,
                                          900) &&
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
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
                                    TestLineMissilePathIsClear(
                                          Self,
                                          Target,
                                          80,
                                          AffectEnemies | AffectHeroes | AffectMinions) &&
                                    // Sequence name :GetSkillShotCastPosition
                                    (
                                          DivideFloat(
                                                out SkillShotAoERadius,
                                                DistanceToTarget,
                                                4) &&
                                          GetRandomPositionInCircle(
                                                out SkillShotCastPosition,
                                                TargetPosition,
                                                SkillShotAoERadius)
                                    ) &&
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
                                          CastSpellTimeThreshold,
                                          SkillShotCastPosition,
                                          true)
                              ) ||
                              // Sequence name :CastE
                              (
                                    // Sequence name :GetRandomChance
                                    (
                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                1,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                50)
                                    ) &&
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
                                          CastSpellTimeThreshold,
                                          default,
                                          false)
                              ) ||
                              // Sequence name :AutoAttackOnlyOnKill
                              (
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
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
                        GetDistanceBetweenUnits(
                              out DistanceToTarget,
                              Target,
                              Self) &&
                        // Sequence name :BlitzcrankKillChampion
                        (
                              // Sequence name :UseOdyn'sVeilAfterUlt
                              (
                                    PreviousSpellCast == 3 &&
                                    TestUnitAICanUseItem(
                                          3180) &&
                                    IssueUseItemOrder(
                                          3180,
                                          Self)
                              ) ||
                              // Sequence name :CastQ
                              (
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    LessFloat(
                                          DistanceToTarget,
                                          825) &&
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
                                    GreaterFloat(
                                          DistanceToTarget,
                                          300) &&
                                    TestLineMissilePathIsClear(
                                          Self,
                                          Target,
                                          80,
                                          AffectEnemies | AffectHeroes | AffectMinions) &&
                                    PredictLineMissileCastPosition(
                                          out SkillShotCastPosition,
                                          Target,
                                          SelfPosition,
                                          2000,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out SkillShotCastPosition,
                                          TargetPosition,
                                          SkillShotCastPosition,
                                          default
                                          ) &&
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
                                          CastSpellTimeThreshold,
                                          default,
                                          false)
                              ) ||
                              // Sequence name :CastE
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
                                    LessFloat(
                                          DistanceToTarget,
                                          300) &&
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
                                          CastSpellTimeThreshold,
                                          default,
                                          false)
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
                                    GetUnitSpellRadius(
                                          out Radius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          3) &&
                                    SubtractFloat(
                                          out Radius,
                                          Radius,
                                          25) &&
                                    // Sequence name :SingleOrMultiTarget
                                    (
                                          // Sequence name :SingleLowHealthTarget
                                          (
                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                LessFloat(
                                                      TargetHealthRatio,
                                                      0.25f) &&
                                                LessFloat(
                                                      DistanceToTarget,
                                                      Radius)
                                          ) ||
                                          // Sequence name :MultipleTargets
                                          (
                                                GetUnitsInTargetArea(
                                                      out EnemyChampionsInRange,
                                                      Self,
                                                      SelfPosition,
                                                      Radius,
                                                      AffectEnemies | AffectHeroes) &&
                                                GetCollectionCount(
                                                      out EnemyChampionCount,
                                                      EnemyChampionsInRange) &&
                                                // Sequence name :ScalingCondition
                                                (
                                                      GreaterInt(
                                                            EnemyChampionCount,
                                                            2)
                                                      &&
                                                      // Sequence name :TwoTargets+OneLowHealth
                                                      (
                                                            GreaterInt(
                                                                  EnemyChampionCount,
                                                                  1) &&
                                                            ForEach(EnemyChampionsInRange, EnemyChampion => (
                                                                  // Sequence name :FindLowHealthTarget
                                                                  (
                                                                        GetUnitHealthRatio(
                                                                              out ChampionHealthRatio,
                                                                              EnemyChampion) &&
                                                                        LessFloat(
                                                                              ChampionHealthRatio,
                                                                              0.4f)
                                                                  ))
                                                            )
                                                      )
                                                )
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
                                          CastSpellTimeThreshold,
                                          default,
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
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          default,
                                          false)
                              ) ||
                              // Sequence name :AutoAttackOnlyOnKill
                              (
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)

                              )
                        )
                  )
            );
         __IssuedAttackTarget = _IssuedAttackTarget;
        __IssuedAttack = _IssuedAttack;
        _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        return result;
    }
}

