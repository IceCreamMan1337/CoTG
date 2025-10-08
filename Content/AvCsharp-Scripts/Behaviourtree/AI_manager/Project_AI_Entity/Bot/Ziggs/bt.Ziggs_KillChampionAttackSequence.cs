using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Ziggs_KillChampionAttackSequenceClass : AI_Characters 
{
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();
    private SummonerIgniteClass summonerIgnite = new SummonerIgniteClass();

    public bool Ziggs_KillChampionAttackSequence(
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
            // Sequence name :ZiggsKillChampionSequence
            (
                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        GetUnitHealthRatio(
                              out HealthRatio,
                              Self) &&
                        GetUnitHealthRatio(
                              out TargetHealthRatio,
                              Target) &&
                        GetDistanceBetweenUnits(
                              out DistanceToTarget,
                              Self,
                              Target) &&
                        // Sequence name :ZiggsKillChampion
                        (
                              // Sequence name :DetonateW
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ZiggsWToggle",
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
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
                                    // Sequence name :GetRandomChance
                                    (
                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                0,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                50)
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
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          25,
                                          true) &&
                                    GetUnitAISpellPosition(
                                          out SpellPosition) &&
                                    ClearUnitAISpellPosition() &&
                                    ClearUnitAISpellTarget(
                                          0) &&
                                    SetUnitAISpellTargetLocation(
                                          SpellPosition,
                                          0) &&
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
                                          SpellPosition,
                                          true)
                              ) ||
                              // Sequence name :UseW
                              (
                                    // Sequence name :DistanceCheck
                                    (
                                          // Sequence name :HRDelta&gt,0.15
                                          (
                                                SubtractFloat(
                                                      out HealthRatioDelta,
                                                      HealthRatio,
                                                      TargetHealthRatio) &&
                                                GreaterFloat(
                                                      HealthRatioDelta,
                                                      0.15f) &&
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      75,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellPosition() &&
                                                ClearUnitAISpellTarget(
                                                      1) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      1) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      1,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :Else
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      75,
                                                      true) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellPosition() &&
                                                ClearUnitAISpellTarget(
                                                      1) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      1) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      1,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          )
                                    )
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
                        ) &&
                        // Sequence name :ZiggsKillChampion
                        (
                              // Sequence name :UseUltimate
                              (
                                    // Sequence name :MultipleChampionsNearby
                                    (
                                          GetUnitAIBasePosition(
                                                out TargetPosition,
                                                Target) &&
                                          CountUnitsInTargetArea(
                                                out EnemyChampCount,
                                                Self,
                                                TargetPosition,
                                                350,
                                                AffectEnemies | AffectHeroes,
                                                "") &&
                                          GreaterInt(
                                                EnemyChampCount,
                                                1)
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
                              // Sequence name :UseEIfAblazeAndNearbyEnemies
                              (
                                    // Sequence name :GetRandomChance
                                    (
                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                0,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                25)
                                    ) &&
                                    TestUnitHasBuff(
                                          Target, default
                                          ,
                                          "ZiggsAblaze",
                                          true) &&
                                    // Sequence name :MultipleChampionsNearby
                                    (
                                          GetUnitAIBasePosition(
                                                out TargetPosition,
                                                Target) &&
                                          CountUnitsInTargetArea(
                                                out EnemyChampCount,
                                                Self,
                                                TargetPosition,
                                                350,
                                                AffectEnemies | AffectHeroes,
                                                "") &&
                                          GreaterInt(
                                                EnemyChampCount,
                                                1)
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :UseQIfAblaze
                              (
                                    // Sequence name :GetRandomChance
                                    (
                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                0,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                50)
                                    ) &&
                                    TestUnitHasBuff(
                                          Target, default
                                          ,
                                          "ZiggsAblaze",
                                          true) &&
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
                                          60,
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
                              // Sequence name :UseWIfHasStunAndAblaze
                              (
                                    TestUnitHasBuff(
                                          Target, default
                                          ,
                                          "ZiggsAblaze",
                                          true) &&
                                    TestUnitHasBuff(
                                          Target, default
                                          ,
                                          "Stun",
                                          true) &&
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
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          1,
                                          0.8f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :UseUltimate
                              (
                                    // Sequence name :TargetBelow50%Health
                                    (
                                          GetUnitCurrentHealth(
                                                out Health,
                                                Target) &&
                                          GetUnitMaxHealth(
                                                out MaxHealth,
                                                Target) &&
                                          DivideFloat(
                                                out HP_Ratio,
                                                Health,
                                                MaxHealth) &&
                                          LessFloat(
                                                HP_Ratio,
                                                0.5f)
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
                              // Sequence name :UseE
                              (
                                    // Sequence name :GetRandomChance
                                    (
                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                0,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                25)
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
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          1,
                                          0.8f,
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
                                    TestLineMissilePathIsClear(
                                          Self,
                                          Target,
                                          60,
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
                        GetUnitHealthRatio(
                              out HealthRatio,
                              Self) &&
                        GetUnitHealthRatio(
                              out TargetHealthRatio,
                              Target) &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        GetDistanceBetweenUnits(
                              out DistanceToTarget,
                              Self,
                              Target) &&
                        // Sequence name :ZiggsKillChampion
                        (
                              // Sequence name :DetonateW
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ZiggsWToggle",
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
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
                                    PredictLineMissileCastPosition(
                                          out SkillShotCastPosition,
                                          Target,
                                          SelfPosition,
                                          5000,
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
                                    PredictLineMissileCastPosition(
                                          out SkillShotCastPosition,
                                          Target,
                                          SelfPosition,
                                          1500,
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
                                          2,
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
                                    // Sequence name :DistanceCheck
                                    (
                                          // Sequence name :HRDelta&gt,0.15
                                          (
                                                SubtractFloat(
                                                      out HealthRatioDelta,
                                                      HealthRatio,
                                                      TargetHealthRatio) &&
                                                GreaterFloat(
                                                      HealthRatioDelta,
                                                      0.15f) &&
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      75,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellPosition() &&
                                                ClearUnitAISpellTarget(
                                                      1) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      1) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      1,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :Else
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      75,
                                                      true) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellPosition() &&
                                                ClearUnitAISpellTarget(
                                                      1) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      1) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      1,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          )
                                    )
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
                                    PredictLineMissileCastPosition(
                                          out SkillShotCastPosition,
                                          Target,
                                          SelfPosition,
                                          1700,
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
                                          0,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          SkillShotCastPosition,
                                          true)
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

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __IssuedAttackTarget = _IssuedAttackTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttack = _IssuedAttack;
        return result;


    }
}

