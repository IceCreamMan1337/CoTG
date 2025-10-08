using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Karthus_KillChampionAttackSequenceClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private IsCrowdControlledClass isCrowdControlled = new();
    private AutoAttackClass autoAttack = new();
    public bool Karthus_KillChampionAttackSequence(
         out AttackableUnit __IssuedAttackTarget,
      out bool __IssuedAttack,
      out AttackableUnit _CurrentSpellCastTarget,
      out int _CurrentSpellCast,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
       out bool __SpellStall,
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
      bool IsDominionGameMode,
          bool SpellStall
         )
    {
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        bool _IssuedAttack = IssuedAttack;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        bool _SpellStall = SpellStall;
        bool result =
                  // Sequence name :KarthusKillChampionSequence

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                           EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        GetDistanceBetweenUnits(
                              out DistanceToTarget,
                              Target,
                              Self) &&
                        // Sequence name :KarthusKillChampion
                        (
                              // Sequence name :ToggleEOn
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Defile",
                                          false) &&
                                    GetUnitSpellRadius(
                                          out DefileRadius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    LessFloat(
                                          DistanceToTarget,
                                          DefileRadius) &&
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
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Self,
                                          2,
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
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          Target) &&
                                    // Sequence name :DefileOnOrTargetLowHealth
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "Defile",
                                                true)
                                          ||
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.3f)
                                    ) &&
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
                                    GetUnitSpellRadius(
                                          out DefileRadius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    SubtractFloat(
                                          out DefileRadius,
                                          DefileRadius,
                                          300) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          DefileRadius) &&
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          50,
                                          false) &&
                                    GetUnitAISpellPosition(
                                          out WallOfPainCastPosition) &&
                                    ClearUnitAISpellPosition() &&
                                    ClearUnitAISpellTarget(
                                          1) &&
                                    SetUnitAISpellTargetLocation(
                                          WallOfPainCastPosition,
                                          1) &&
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
                                          WallOfPainCastPosition,
                                          true)
                              ) ||
                              // Sequence name :ToggleEOff
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Defile",
                                          true) &&
                                    GetUnitSpellRadius(
                                          out DefileRadius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          DefileRadius) &&
                                    GetSpellSlotCooldown(
                                          out WCooldown,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          1) &&
                                    GreaterFloat(
                                          WCooldown,
                                          0) &&
                                   isCrowdControlled.IsCrowdControlled(
                                          out IsCCd,
                                          Target) &&
                                    IsCCd == false &&
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
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Self,
                                          2,
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
                                          CastSpellTimeThreshold,
                                          default,
                                          false)
                              ) ||
                              // Sequence name :MoveTowardEnemyIfDefileOn
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Defile",
                                          true) &&
                                    GetUnitSpellRadius(
                                          out DefileRadius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    SubtractFloat(
                                          out DefileRadius,
                                          DefileRadius,
                                          50) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          DefileRadius) &&
                                    IssueMoveToUnitOrder(
                                          Target)
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
                        GetDistanceBetweenUnits(
                              out DistanceToTarget,
                              Target,
                              Self) &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        // Sequence name :KarthusKillChampion
                        (
                              // Sequence name :ToggleEOn
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Defile",
                                          false) &&
                                    GetUnitSpellRadius(
                                          out DefileRadius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    SubtractFloat(
                                          out DefileRadius,
                                          DefileRadius,
                                          100) &&
                                    LessFloat(
                                          DistanceToTarget,
                                          DefileRadius) &&
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
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Self,
                                          2,
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
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          Target) &&
                                    // Sequence name :DefileOnOrTargetLowHealth
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "Defile",
                                                true)

                                          ||

                                          LessFloat(
                                                TargetHealthRatio,
                                                0.3f)
                                    ) &&
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
                                    GetUnitSpellRadius(
                                          out DefileRadius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    SubtractFloat(
                                          out DefileRadius,
                                          DefileRadius,
                                          300) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          DefileRadius) &&
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          50,
                                          false) &&
                                    GetUnitAISpellPosition(
                                          out WallOfPainCastPosition) &&
                                    ClearUnitAISpellPosition() &&
                                    ClearUnitAISpellTarget(
                                          1) &&
                                    SetUnitAISpellTargetLocation(
                                          WallOfPainCastPosition,
                                          1) &&
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
                                          WallOfPainCastPosition,
                                          true)
                              ) ||
                              // Sequence name :ToggleEOff
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Defile",
                                          true) &&
                                    GetUnitSpellRadius(
                                          out DefileRadius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          DefileRadius) &&
                                    GetSpellSlotCooldown(
                                          out WCooldown,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          1) &&
                                    GreaterFloat(
                                          WCooldown,
                                          0) &&
                                      isCrowdControlled.IsCrowdControlled(
                                          out IsCCd,
                                          Target) &&
                                    IsCCd == false &&
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
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Self,
                                          2,
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
                                          1000,
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
                                          SkillShotCastPosition,
                                          true)
                              ) ||
                              // Sequence name :MoveTowardEnemyIfDefileOn
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    TestUnitHasBuff(
                                          Self,
                                         default,
                                          "Defile",
                                          true) &&
                                    GetUnitSpellRadius(
                                          out DefileRadius,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    SubtractFloat(
                                          out DefileRadius,
                                          DefileRadius,
                                          100) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          DefileRadius) &&
                                    IssueMoveToUnitOrder(
                                          Target)
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

        __IssuedAttackTarget = _IssuedAttackTarget;
        __IssuedAttack = _IssuedAttack;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __SpellStall = _SpellStall;

        return result;
    }
}

