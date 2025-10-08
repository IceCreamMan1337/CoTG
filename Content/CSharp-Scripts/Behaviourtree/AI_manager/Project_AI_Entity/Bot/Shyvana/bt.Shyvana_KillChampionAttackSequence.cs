using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Shyvana_KillChampionAttackSequenceClass : AI_Characters
{
    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttack_MeleeClass autoAttack_Melee = new();
    private LastHitClass lastHit = new();
    private SummonerGhostClass summonerGhost = new();

    public bool Shyvana_KillChampionAttackSequence(
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
                  // Sequence name :Selector

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :ShyvanaKillChampion
                        (
                              // Sequence name :IsDragon
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ShyvanaTransform",
                                          true) &&
                                    // Sequence name :DragonAction
                                    (
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

                                                      GenerateRandomFloat(
                                                            out RandomFloat,
                                                            0,
                                                            100) &&
                                                      LessFloat(
                                                            RandomFloat,
                                                            50)
                                                 &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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

                                                      GenerateRandomFloat(
                                                            out RandomFloat,
                                                            0,
                                                            100) &&
                                                      LessFloat(
                                                            RandomFloat,
                                                            50)
                                                 &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                                GetDistanceBetweenUnits(
                                                      out DistanceToTarget,
                                                      Self,
                                                      Target) &&
                                                LessFloat(
                                                      DistanceToTarget,
                                                      250) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                          // Sequence name :UseGhost
                                          (
                                                GreaterInt(
                                                      KillChampionScore,
                                                      5) &&
                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                LessFloat(
                                                      TargetHealthRatio,
                                                      0.2f) &&
                                               summonerGhost.SummonerGhost(
                                                      Self,
                                                      GhostSlot)
                                          ) ||
                                               // Sequence name :AutoAttackOnlyOnKill

                                               autoAttack_Melee.AutoAttack_Melee(
                                                      out IssuedAttack,
                                                      out IssuedAttackTarget,
                                                      Target,
                                                      Self,
                                                      IssuedAttack,
                                                      IssuedAttackTarget)

                                    )
                              ) ||
                              // Sequence name :IsNotDragon
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ShyvanaTransform",
                                          false) &&
                                    // Sequence name :HumanAction
                                    (
                                          // Sequence name :CastRIfMultipleEnemyChampions
                                          (
                                                GetUnitPosition(
                                                      out SelfPosition,
                                                      Self) &&
                                                CountUnitsInTargetArea(
                                                      out UnitsNearby,
                                                      Self,
                                                      SelfPosition,
                                                      700,
                                                      AffectEnemies | AffectHeroes | AffectMinions | AffectNeutral,
                                                      "") &&
                                                GreaterInt(
                                                      UnitsNearby,
                                                      1) &&
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      200,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellPosition() &&
                                                ClearUnitAISpellTarget(
                                                      3) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      3) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      3,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :CastRIfHealth&gt,60%
                                          (
                                                GetUnitHealthRatio(
                                                      out HP_Ratio,
                                                      Self) &&
                                                GreaterFloat(
                                                      HP_Ratio,
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      200,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellTarget(
                                                      3) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      3) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      3,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :CastRIfSelfHealth&gt,TargetHealth
                                          (
                                                GetUnitCurrentHealth(
                                                      out Health,
                                                      Self) &&
                                                GetUnitCurrentHealth(
                                                      out EnemyHealth,
                                                      Target) &&
                                                GreaterFloat(
                                                      Health,
                                                      EnemyHealth) &&
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      200,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellTarget(
                                                      3) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      3) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      3,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :UseQ
                                          (
                                                GetDistanceBetweenUnits(
                                                      out DistanceToTarget,
                                                      Self,
                                                      Target) &&
                                                LessFloat(
                                                      DistanceToTarget,
                                                      250) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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

                                                      GenerateRandomFloat(
                                                            out RandomFloat,
                                                            0,
                                                            100) &&
                                                      LessFloat(
                                                            RandomFloat,
                                                            50)
                                                 &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                                TestLineMissilePathIsClear(
                                                      Self,
                                                      Target,
                                                      60,
                                                      AffectEnemies | AffectHeroes | AffectMinions) &&
                                                      // Sequence name :GetSkillShotCastPosition

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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                          // Sequence name :UseGhost
                                          (
                                                GreaterInt(
                                                      KillChampionScore,
                                                      5) &&
                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                LessFloat(
                                                      TargetHealthRatio,
                                                      0.2f) &&
                                           summonerGhost.SummonerGhost(
                                                      Self,
                                                      GhostSlot)
                                          ) ||
                                          // Sequence name :CastR
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
                                                      Target,
                                                      PreviousSpellCast,
                                                      false,
                                                      false) &&
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      200,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellTarget(
                                                      3) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      3) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      3,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                            // Sequence name :AutoAttackOnlyOnKill

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
                        // Sequence name :ShyvanaKillChampion
                        (
                              // Sequence name :IsDragon
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ShyvanaTransform",
                                          true) &&
                                    // Sequence name :DragonAction
                                    (
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                                GetDistanceBetweenUnits(
                                                      out DistanceToTarget,
                                                      Self,
                                                      Target) &&
                                                LessFloat(
                                                      DistanceToTarget,
                                                      250) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                          // Sequence name :UseGhost
                                          (
                                                GreaterInt(
                                                      KillChampionScore,
                                                      5) &&
                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                LessFloat(
                                                      TargetHealthRatio,
                                                      0.2f) &&
                                             summonerGhost.SummonerGhost(
                                                      Self,
                                                      GhostSlot)
                                          ) ||
                                              // Sequence name :AutoAttackOnlyOnKill

                                              autoAttack_Melee.AutoAttack_Melee(
                                                      out IssuedAttack,
                                                      out IssuedAttackTarget,
                                                      Target,
                                                      Self,
                                                      IssuedAttack,
                                                      IssuedAttackTarget)

                                    )
                              ) ||
                              // Sequence name :IsNotDragon
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ShyvanaTransform",
                                          false) &&
                                    // Sequence name :HumanAction
                                    (
                                          // Sequence name :CastRIfMultipleEnemyChampions
                                          (
                                                GetUnitPosition(
                                                      out SelfPosition,
                                                      Self) &&
                                                CountUnitsInTargetArea(
                                                      out UnitsNearby,
                                                      Self,
                                                      SelfPosition,
                                                      700,
                                                      AffectEnemies | AffectHeroes | AffectMinions | AffectNeutral,
                                                      "") &&
                                                GreaterInt(
                                                      UnitsNearby,
                                                      1) &&
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      200,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellPosition() &&
                                                ClearUnitAISpellTarget(
                                                      3) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      3) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      3,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :CastRIfHealth&gt,60%
                                          (
                                                GetUnitHealthRatio(
                                                      out HP_Ratio,
                                                      Self) &&
                                                GreaterFloat(
                                                      HP_Ratio,
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      200,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellTarget(
                                                      3) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      3) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      3,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :CastRIfSelfHealth&gt,TargetHealth
                                          (
                                                GetUnitCurrentHealth(
                                                      out Health,
                                                      Self) &&
                                                GetUnitCurrentHealth(
                                                      out EnemyHealth,
                                                      Target) &&
                                                GreaterFloat(
                                                      Health,
                                                      EnemyHealth) &&
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
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      200,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellTarget(
                                                      3) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      3) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      3,
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                                GetDistanceBetweenUnits(
                                                      out DistanceToTarget,
                                                      Self,
                                                      Target) &&
                                                LessFloat(
                                                      DistanceToTarget,
                                                      250) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                          // Sequence name :UseE
                                          (
                                                GetDistanceBetweenUnits(
                                                      out DistanceToTarget,
                                                      Self,
                                                      Target) &&
                                                LessFloat(
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
                                                TestLineMissilePathIsClear(
                                                      Self,
                                                      Target,
                                                      60,
                                                      AffectEnemies | AffectHeroes | AffectMinions) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                          // Sequence name :UseGhost
                                          (
                                                GreaterInt(
                                                      KillChampionScore,
                                                      5) &&
                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                LessFloat(
                                                      TargetHealthRatio,
                                                      0.2f) &&
                                              summonerGhost.SummonerGhost(
                                                      Self,
                                                      GhostSlot)
                                          ) ||
                                          // Sequence name :CastR
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
                                                      Target,
                                                      PreviousSpellCast,
                                                      false,
                                                      false) &&
                                                ComputeUnitAISpellPosition(
                                                      Target,
                                                      Self,
                                                      200,
                                                      false) &&
                                                GetUnitAISpellPosition(
                                                      out SpellPosition) &&
                                                ClearUnitAISpellTarget(
                                                      3) &&
                                                SetUnitAISpellTargetLocation(
                                                      SpellPosition,
                                                      3) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      3,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      SpellPosition,
                                                      true)
                                          ) ||
                                             // Sequence name :AutoAttackOnlyOnKill

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

