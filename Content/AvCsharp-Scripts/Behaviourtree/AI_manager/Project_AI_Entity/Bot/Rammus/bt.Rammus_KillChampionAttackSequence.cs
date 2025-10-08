using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Rammus_KillChampionAttackSequenceClass : AI_Characters 
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();
    private SummonerFlashClass summonerFlash = new SummonerFlashClass();
     public bool Rammus_KillChampionAttackSequence(
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
            // Sequence name :RammusKillSequence
            (
                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                           EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :RammusKillChampion
                        (
                              // Sequence name :PowerballIntoEnemy
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Powerball",
                                          true) &&
                                    ClearUnitAISpellPosition() &&
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          200,
                                          false) &&
                                    GetUnitAISpellPosition(
                                          out PowerballPosition) &&
                                    ClearUnitAISpellPosition() &&
                                    IssueMoveToPositionOrder(
                                          PowerballPosition)
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
                                    // Sequence name :GetRandomChance
                                    (
                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                1,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                75)
                                    ) &&
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
                                          CastSpellTimeThreshold,default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastUlt
                              (
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
                                    CastUnitSpell(
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          3,
                                          default, default
                                          )
                              ) ||
                              // Sequence name :CastW
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
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Powerball",
                                          false) &&
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "DefensiveBallCurl",
                                          false) &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    // Sequence name :Conditions
                                    (
                                          TestUnitHasBuff(
                                                Target,
                                                Self,
                                                "Taunt",
                                                true) ||
                                                LessEqualFloat(
                                                DistanceToTarget,
                                                200)
                                    ) &&
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
                                    CastUnitSpell(
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          1,
                                          default, default
                                          )
                              ) ||
                              // Sequence name :CastQ
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "DefensiveBallCurl",
                                          false) &&
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Powerball",
                                          false) &&
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
                                    CastUnitSpell(
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          0,
                                          default, default
                                          )
                              ) ||
                              // Sequence name :Ghost
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          Target) &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.3f) &&
                                    LessFloat(
                                          DistanceToTarget,
                                          300) &&
                                summonerGhost.SummonerGhost(
                                          Self,
                                          GhostSlot)
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
                        // Sequence name :RammusKillChampion
                        (
                              // Sequence name :UseFlash
                              (
                                    GreaterInt(
                                          FlashSlot,
                                          -1) &&
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    // Sequence name :InPowerballOrHasTaunt
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "Powerball",
                                                true)
                                          ||
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                2,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Target,
                                                PreviousSpellCast,
                                                false,
                                                false)
                                    ) &&
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
                                    GetUnitSpellCastRange(
                                          out FlashRange,
                                          Self,
                                          SPELLBOOK_SUMMONER,
                                          FlashSlot) &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    LessFloat(
                                          DistanceToTarget,
                                          FlashRange) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          250) &&
                                  summonerFlash.SummonerFlash(
                                          Self,
                                          FlashSlot,
                                          Target,
                                          true)
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
                              // Sequence name :CastW
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Powerball",
                                          false) &&
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "DefensiveBallCurl",
                                          false) &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    // Sequence name :Conditions
                                    (
                                          TestUnitHasBuff(
                                                Target,
                                                Self,
                                                "Taunt",
                                                true) ||
                                                LessEqualFloat(
                                                DistanceToTarget,
                                                200)
                                    ) &&
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
                                    CastUnitSpell(
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          1,
                                          default, default
                                          )
                              ) ||
                              // Sequence name :CastUlt
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
                                    CastUnitSpell(
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          3,
                                          default, default
                                          )
                              ) ||
                              // Sequence name :CastQ
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "DefensiveBallCurl",
                                          false) &&
                                    TestUnitHasBuff(
                                          Self,
                                         default,
                                          "Powerball",
                                          false) &&
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
                                    CastUnitSpell(
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          0,
                                          default, default
                                          )
                              ) ||
                              // Sequence name :PowerballIntoEnemy
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Powerball",
                                          true) &&
                                    ClearUnitAISpellPosition() &&
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          300,
                                          false) &&
                                    GetUnitAISpellPosition(
                                          out PowerballPosition) &&
                                    ClearUnitAISpellPosition() &&
                                    IssueMoveToPositionOrder(
                                          PowerballPosition)
                              ) ||
                              // Sequence name :Ghost
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          Target) &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.3f) &&
                                    LessFloat(
                                          DistanceToTarget,
                                          300) &&
                                  summonerGhost.SummonerGhost(
                                          Self,
                                          GhostSlot)
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

