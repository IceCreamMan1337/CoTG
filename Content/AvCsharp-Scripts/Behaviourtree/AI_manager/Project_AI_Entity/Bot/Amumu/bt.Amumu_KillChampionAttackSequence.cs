using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Amumu_KillChampionAttackSequenceClass : AI_Characters 
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
     public bool Amumu_KillChampionAttackSequence(
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
            // Sequence name :AmumuKillChampionLogic
            (
                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                             EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :AmumuKillChampion
                        (
                              // Sequence name :ToggleW_Off
                              (
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Target,
                                          Self) &&
                                    GreaterFloat(
                                          Distance,
                                          375) &&
                                    // Sequence name :ToggleSpell
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "AuraofDespair",
                                                true) &&
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
                                    // Sequence name :SecondaryChecks
                                    (
                                          // Sequence name :SuperLowHealth
                                          (
                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                LessFloat(
                                                      TargetHealthRatio,
                                                      0.5f)
                                          ) ||
                                          // Sequence name :CatchMultipleEnemies
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
                                                GetUnitPosition(
                                                      out SelfPosition,
                                                      Self) &&
                                                CountUnitsInTargetArea(
                                                      out CollectionCount,
                                                      Self,
                                                      SelfPosition,
                                                      Radius,
                                                      AffectEnemies | AffectHeroes,
                                                      "") &&
                                                GreaterEqualInt(
                                                      CollectionCount,
                                                      2)
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
                              // Sequence name :CastQ
                              (
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
                              // Sequence name :E
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
                              // Sequence name :ToggleW
                              (
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Target,
                                          Self) &&
                                    LessFloat(
                                          Distance,
                                          275) &&
                                    // Sequence name :ToggleSpell
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "AuraofDespair",
                                                false) &&
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
                                    )
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
                        // Sequence name :AmumuKillChampion
                        (
                              // Sequence name :ToggleW_Off
                              (
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Target,
                                          Self) &&
                                    GreaterFloat(
                                          Distance,
                                          375) &&
                                    // Sequence name :ToggleSpell
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "AuraofDespair",
                                                true) &&
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
                                    )
                              ) ||
                              // Sequence name :CastQ
                              (
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
                                    PredictLineMissileCastPosition(
                                          out SkillShotCastPosition,
                                          Target,
                                          SelfPosition,
                                          2000,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out SkillShotCastPosition,
                                          TargetPosition,
                                          SkillShotCastPosition
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
                                    // Sequence name :SecondaryChecks
                                    (
                                          // Sequence name :SuperLowHealth
                                          (
                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                LessFloat(
                                                      TargetHealthRatio,
                                                      0.5f)
                                          ) ||
                                          // Sequence name :CatchMultipleEnemies
                                          (
                                                GetUnitSpellRadius(
                                                      out Radius,
                                                      Self,
                                                      SPELLBOOK_CHAMPION,
                                                      3) &&
                                                GetUnitPosition(
                                                      out SelfPosition,
                                                      Self) &&
                                                CountUnitsInTargetArea(
                                                      out CollectionCount,
                                                      Self,
                                                      SelfPosition,
                                                      Radius,
                                                      AffectEnemies | AffectHeroes,
                                                      "") &&
                                                GreaterEqualInt(
                                                      CollectionCount,
                                                      2)
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
                              // Sequence name :E
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
                                          CastSpellTimeThreshold,
                                          default,
                                          false)
                              ) ||
                              // Sequence name :ToggleW
                              (
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Target,
                                          Self) &&
                                    LessFloat(
                                          Distance,
                                          275) &&
                                    // Sequence name :ToggleSpell
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "AuraofDespair",
                                                false) &&
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
                                    )
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

