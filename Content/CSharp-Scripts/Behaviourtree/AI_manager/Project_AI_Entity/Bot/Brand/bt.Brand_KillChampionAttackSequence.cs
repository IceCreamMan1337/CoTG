using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Brand_KillChampionAttackSequenceClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private SummonerIgniteClass summonerIgnite = new();
    private AutoAttackClass autoAttack = new();

    public bool Brand_KillChampionAttackSequence(
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
                  // Sequence name :BrandKillChampionSequence

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :BrandKillChampion
                        (
                              // Sequence name :UseUltimate
                              (
                                          // Sequence name :MultipleChampionsNearby

                                          GetUnitPosition(
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
                                     &&
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
                                          CastSpellTimeThreshold,
                                         default,
                                          false)
                              ) ||
                              // Sequence name :UseEIfAblazeAndNearbyEnemies
                              (
                                          // Sequence name :GetRandomChance

                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                1,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                25)
                                     &&
                                    TestUnitHasBuff(
                                          Target,
                                         default,
                                          "BrandAblaze",
                                          true) &&
                                          // Sequence name :MultipleChampionsNearby

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
                                          CastSpellTimeThreshold,
                                          default,
                                          false)
                              ) ||
                              // Sequence name :UseQIfAblaze
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
                                    TestUnitHasBuff(
                                          Target,
                                          default,
                                          "BrandAblaze",
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
                                          Target,
                                          default,
                                          "BrandAblaze",
                                          true) &&
                                    TestUnitHasBuff(
                                          Target,
                                          default,
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
                                     &&
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

                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                0,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                25)
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
                        // Sequence name :BrandKillChampion
                        (
                              // Sequence name :UseUltimate
                              (
                                          // Sequence name :MultipleChampionsNearby

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
                                     &&
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
                              // Sequence name :UseQIfAblaze
                              (
                                    TestUnitHasBuff(
                                          Target, default
                                          ,
                                          "BrandAblaze",
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
                                    PredictLineMissileCastPosition(
                                          out SkillShotCastPosition,
                                          Target,
                                          SelfPosition,
                                          1600,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out NewSkillShotCastPosition,
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
                                          NewSkillShotCastPosition,
                                          true)
                              ) ||
                              // Sequence name :UseEIfAblazeAndNearbyEnemies
                              (
                                    TestUnitHasBuff(
                                          Target,
                                          default,
                                          "BrandAblaze",
                                          true) &&
                                          // Sequence name :MultipleChampionsNearby

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
                                          CastSpellTimeThreshold,
                                          default,
                                          false)
                              ) ||
                              // Sequence name :UseWIfHasStunAndAblaze
                              (
                                    TestUnitHasBuff(
                                          Target,
                                          default,
                                          "BrandAblaze",
                                          true) &&
                                    TestUnitHasBuff(
                                          Target,
                                          default,
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
                                    PredictLineMissileCastPosition(
                                          out SkillShotCastPosition,
                                          Target,
                                          SelfPosition,
                                          1200,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out NewSkillShotCastPosition,
                                          TargetPosition,
                                          SkillShotCastPosition,
                                          0.3f) &&
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
                                          NewSkillShotCastPosition,
                                          true)
                              ) ||
                              // Sequence name :UseUltimate
                              (
                                          // Sequence name :TargetBelow50%Health

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
                                     &&
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
                                          CastSpellTimeThreshold,
                                          default,
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
                                          1200,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out NewSkillShotCastPosition,
                                          TargetPosition,
                                          SkillShotCastPosition,
                                          0.3f) &&
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
                                          NewSkillShotCastPosition,
                                          true)
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
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __IssuedAttack = _IssuedAttack;
        return result;


    }
}

