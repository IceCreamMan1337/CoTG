using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Fiddlesticks_KillChampionAttackSequenceClass : AI_Characters
{
    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private IsCrowdControlledClass isCrowdControlled = new();
    private SummonerIgniteClass summonerIgnite = new();
    private SummonerFlashClass summonerFlash = new();


    public bool Fiddlesticks_KillChampionAttackSequence(
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
                  // Sequence name :FiddlesticksKIllSequence

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :FiddlesticksKillChampion
                        (
                              // Sequence name :UseUltimate
                              (
                                          // Sequence name :NoTowersNearby

                                          GetUnitPosition(
                                                out TargetPosition,
                                                Target) &&
                                          CountUnitsInTargetArea(
                                                out DrainTowerCheckCount,
                                                Self,
                                                TargetPosition,
                                                900,
                                                AffectEnemies | AffectTurrets,
                                                "") &&
                                          DrainTowerCheckCount == 0
                                     &&
                                    // Sequence name :Conditions
                                    (
                                          // Sequence name :TargetIsLowHealth
                                          (
                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                LessFloat(
                                                      TargetHealthRatio,
                                                      0.5f)
                                          ) ||
                                          // Sequence name :MultipleChampionsNearby
                                          (
                                                CountUnitsInTargetArea(
                                                      out EnemyChampCount,
                                                      Self,
                                                      TargetPosition,
                                                      1000,
                                                      AffectEnemies | AffectHeroes,
                                                      "") &&
                                                GreaterInt(
                                                      EnemyChampCount,
                                                      1)
                                          ) ||
                                          // Sequence name :TargetIsCC'd
                                          (
                                                SetVarBool(
                                                      out IsCC,
                                                      false) &&
                                            isCrowdControlled.IsCrowdControlled(
                                                      out IsCC,
                                                      Target) &&
                                                IsCC == true
                                          ) ||
                                                // Sequence name :TargetCannotSeeFiddlesticks

                                                TestUnitIsVisible(
                                                      Target,
                                                      Self,
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
                              // Sequence name :CastE
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
                              // Sequence name :CastZhonyas
                              (
                                    TestUnitAICanUseItem(
                                          3157) &&
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Crowstorm",
                                          true) &&
                                    GetUnitPosition(
                                          out SelfPosition,
                                          Self) &&
                                    CountUnitsInTargetArea(
                                          out EnemyChampCount,
                                          Self,
                                          SelfPosition,
                                          400,
                                          AffectEnemies | AffectHeroes,
                                          "") &&
                                    GreaterEqualInt(
                                          EnemyChampCount,
                                          3) &&
                                    IssueUseItemOrder(
                                          3157, default
                                          )
                              ) ||
                              // Sequence name :CastQ
                              (
                                          // Sequence name :GetRandomChance

                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                0,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                50)
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
                                          // Sequence name :CheckForEnemyTower

                                          GetUnitPosition(
                                                out DrainTowerCheckPosition,
                                                Self) &&
                                          CountUnitsInTargetArea(
                                                out DrainTowerCheckCount,
                                                Self,
                                                DrainTowerCheckPosition,
                                                900,
                                                AffectEnemies | AffectTurrets,
                                                "") &&
                                          DrainTowerCheckCount == 0
                                     &&
                                    // Sequence name :Conditions
                                    (
                                          // Sequence name :HasCrowstormTargetNear
                                          (
                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
                                                      "Crowstorm",
                                                      true) &&
                                                      // Sequence name :TargetIsNear

                                                      GetDistanceBetweenUnits(
                                                            out DistanceToTarget,
                                                            Self,
                                                            Target) &&
                                                      LessFloat(
                                                            DistanceToTarget,
                                                            200)

                                          ) ||
                                                // Sequence name :NoCrowstorm

                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
                                                      "Crowstorm",
                                                      false)

                                    ) &&
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
                                          false) &&
                                    SetVarBool(
                                          out SpellStall,
                                          true)
                              ) ||
                              // Sequence name :ChaseWithCrowstorm
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Crowstorm",
                                          true) &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          275) &&
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

                              // Sequence name :FiddlesticksKillChampion

                              // Sequence name :UseUltimate
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                          // Sequence name :NoTowersNearby

                                          GetUnitPosition(
                                                out TargetPosition,
                                                Target) &&
                                          CountUnitsInTargetArea(
                                                out DrainTowerCheckCount,
                                                Self,
                                                TargetPosition,
                                                900,
                                                AffectEnemies | AffectTurrets,
                                                "") &&
                                          DrainTowerCheckCount == 0
                                     &&
                                    // Sequence name :Conditions
                                    (
                                          // Sequence name :MultipleChampionsNearby
                                          (
                                                CountUnitsInTargetArea(
                                                      out EnemyChampCount,
                                                      Self,
                                                      TargetPosition,
                                                      1000,
                                                      AffectEnemies | AffectHeroes,
                                                      "") &&
                                                GreaterInt(
                                                      EnemyChampCount,
                                                      1)
                                          ) ||
                                          // Sequence name :TargetIsCC'd
                                          (
                                                SetVarBool(
                                                      out IsCC,
                                                      false) &&
                                             isCrowdControlled.IsCrowdControlled(
                                                      out IsCC,
                                                      Target) &&
                                                IsCC == true
                                          ) ||
                                                // Sequence name :TargetCannotSeeFiddlesticks

                                                TestUnitIsVisible(
                                                      Target,
                                                      Self,
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
                              // Sequence name :CastZhonyas
                              (
                                    TestUnitAICanUseItem(
                                          3157) &&
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Crowstorm",
                                          true) &&
                                    GetUnitPosition(
                                          out SelfPosition,
                                          Self) &&
                                    CountUnitsInTargetArea(
                                          out EnemyChampCount,
                                          Self,
                                          SelfPosition,
                                          400,
                                          AffectEnemies | AffectHeroes,
                                          "") &&
                                    GreaterEqualInt(
                                          EnemyChampCount,
                                          3) &&
                                    IssueUseItemOrder(
                                          3157, default
                                          )
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
                                          // Sequence name :CheckForEnemyTower

                                          GetUnitPosition(
                                                out DrainTowerCheckPosition,
                                                Self) &&
                                          CountUnitsInTargetArea(
                                                out DrainTowerCheckCount,
                                                Self,
                                                DrainTowerCheckPosition,
                                                900,
                                                AffectEnemies | AffectTurrets,
                                                "") &&
                                          DrainTowerCheckCount == 0
                                     &&
                                    // Sequence name :Conditions
                                    (
                                          // Sequence name :HasCrowstormTargetNear
                                          (
                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
                                                      "Crowstorm",
                                                      true) &&
                                                      // Sequence name :TargetIsNear

                                                      GetDistanceBetweenUnits(
                                                            out DistanceToTarget,
                                                            Self,
                                                            Target) &&
                                                      LessFloat(
                                                            DistanceToTarget,
                                                            200)

                                          ) ||
                                                // Sequence name :NoCrowstorm

                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
                                                      "Crowstorm",
                                                      false)

                                    ) &&
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
                                          false) &&
                                    SetVarBool(
                                          out SpellStall,
                                          true)
                              ) ||
                              // Sequence name :ChaseWithCrowstorm
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "Crowstorm",
                                          true) &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          275) &&
                                    // Sequence name :MoveOrFlash
                                    (
                                          // Sequence name :Flash
                                          (
                                                GreaterFloat(
                                                      DistanceToTarget,
                                                      600) &&
                                                GreaterInt(
                                                      FlashSlot,
                                                      -1) &&
                                             summonerFlash.SummonerFlash(
                                                      Self,
                                                      FlashSlot,
                                                      Target,
                                                      true)
                                          ) ||
                                          IssueMoveToUnitOrder(
                                                Target)
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

