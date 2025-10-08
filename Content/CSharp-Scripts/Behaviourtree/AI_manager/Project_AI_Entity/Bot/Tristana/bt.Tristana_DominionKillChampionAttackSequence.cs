using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Tristana_DominionKillChampionAttackSequenceClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private AutoAttackClass autoAttack = new();
    private SummonerIgniteClass summonerIgnite = new();
    private SummonerExhaustClass summonerExhaust = new();

    public bool Tristana_DominionKillChampionAttackSequence(
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
     bool IsDominionGameMode)
    {
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        bool _IssuedAttack = IssuedAttack;
        AttackableUnit CurrentSpellCastTarget = default;
        int CurrentSpellCast = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;


        bool result =
                  // Sequence name :TristanaDominionKillChampion

                  // Sequence name :BeginnerSpells
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        // Sequence name :TristanaKillChampion
                        (
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
                              // Sequence name :CastQ
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
                                          false) &&
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
                                    GetUnitHealthRatio(
                                          out SelfHealthRatio,
                                          Self) &&
                                    GreaterFloat(
                                          SelfHealthRatio,
                                          0.35f) &&
                                    GetUnitPosition(
                                          out TargetPosition,
                                          Target) &&
                                    CountUnitsInTargetArea(
                                          out TurretCount,
                                          Self,
                                          TargetPosition,
                                          1000,
                                          AffectEnemies | AffectMinions | AffectUseable,
                                          "OdinGuardianStatsByLevel") &&
                                    TurretCount == 0 &&
                                    ClearUnitAISpellPosition() &&
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          150,
                                          false) &&
                                    GetUnitAISpellPosition(
                                          out RocketJumpPosition) &&
                                    ClearUnitAISpellPosition() &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          1,
                                          0.9f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          RocketJumpPosition,
                                          true)
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
                                    GetUnitCurrentHealth(
                                          out TargetHealth,
                                          Target) &&
                                    GetUnitSpellLevel(
                                          out UltLevel,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          3) &&
                                    // Sequence name :Conditions
                                    (
                                          // Sequence name :PushTowardBase
                                          (
                                                GreaterInt(
                                                      KillChampionScore,
                                                      5) &&
                                                LessFloat(
                                                      DistanceTargetToBase,
                                                      DistanceSelfToBase)
                                          ) ||
                                          // Sequence name :Level1UltKill
                                          (
                                                UltLevel == 1 &&
                                                LessFloat(
                                                      TargetHealth,
                                                      225)
                                          ) ||
                                          // Sequence name :Level2UltKill
                                          (
                                                UltLevel == 2 &&
                                                LessFloat(
                                                      TargetHealth,
                                                      300)
                                          ) ||
                                          // Sequence name :Level3UltKill
                                          (
                                                UltLevel == 3 &&
                                                LessFloat(
                                                      TargetHealth,
                                                      375)
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
                              // Sequence name :UseIgnite
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                              summonerIgnite.SummonerIgnite(
                                          Self,
                                          Target,
                                          IgniteSlot)
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
                        TestEntityDifficultyLevel(
                              false,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        // Sequence name :TristanaKillChampion
                        (
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
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
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
                                    GetUnitHealthRatio(
                                          out SelfHealthRatio,
                                          Self) &&
                                    GreaterFloat(
                                          SelfHealthRatio,
                                          0.35f) &&
                                    GetUnitPosition(
                                          out TargetPosition,
                                          Target) &&
                                    CountUnitsInTargetArea(
                                          out TurretCount,
                                          Self,
                                          TargetPosition,
                                          1000,
                                          AffectEnemies | AffectMinions | AffectUseable,
                                          "OdinGuardianStatsByLevel") &&
                                    TurretCount == 0 &&
                                    ClearUnitAISpellPosition() &&
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          150,
                                          false) &&
                                    GetUnitAISpellPosition(
                                          out RocketJumpPosition) &&
                                    ClearUnitAISpellPosition() &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          1,
                                          0.9f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          RocketJumpPosition,
                                          true)
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
                                    GetUnitCurrentHealth(
                                          out TargetHealth,
                                          Target) &&
                                    GetUnitSpellLevel(
                                          out UltLevel,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          3) &&
                                    // Sequence name :Conditions
                                    (
                                          // Sequence name :Level1UltKill
                                          (
                                                UltLevel == 1 &&
                                                LessFloat(
                                                      TargetHealth,
                                                      225)
                                          ) ||
                                          // Sequence name :Level2UltKill
                                          (
                                                UltLevel == 2 &&
                                                LessFloat(
                                                      TargetHealth,
                                                      300)
                                          ) ||
                                          // Sequence name :Level3UltKill
                                          (
                                                UltLevel == 3 &&
                                                LessFloat(
                                                      TargetHealth,
                                                      375)
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
                              // Sequence name :UseIgnite
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                  summonerIgnite.SummonerIgnite(
                                          Self,
                                          Target,
                                          IgniteSlot)
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

