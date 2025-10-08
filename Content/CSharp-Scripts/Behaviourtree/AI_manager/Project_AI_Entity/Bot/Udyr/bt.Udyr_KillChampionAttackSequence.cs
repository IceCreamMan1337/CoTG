using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Udyr_KillChampionAttackSequenceClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private AutoAttackClass autoAttack = new();
    private SummonerExhaustClass summonerExhaust = new();
    public bool Udyr_KillChampionAttackSequence(
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
                  // Sequence name :UdyrKillChampion

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                              // Sequence name :UdyrKillChampion

                              GetUnitPosition(
                                    out SelfPosition,
                                    Self) &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget,
                                    Target,
                                    Self) &&
                              CountUnitsInTargetArea(
                                    out UnitsNearby,
                                    Self,
                                    SelfPosition,
                                    300,
                                    AffectEnemies | AffectHeroes,
                                    "") &&
                              // Sequence name :UdyrKillChampion
                              (
                                    // Sequence name :Exhaust
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.5f) &&
                                       summonerExhaust.SummonerExhaust(
                                                Self,
                                                Target,
                                                ExhaustSlot)
                                    ) ||
                                    // Sequence name :CastRAoE
                                    (
                                          GreaterEqualInt(
                                                UnitsNearby,
                                                2) &&
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
                                    // Sequence name :CastQ
                                    (
                                          LessFloat(
                                                DistanceToTarget,
                                                300) &&
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
                                    // Sequence name :CastR
                                    (
                                          LessFloat(
                                                DistanceToTarget,
                                                300) &&
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
                                    // Sequence name :CastE
                                    (
                                          UnitsNearby == 0 &&
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
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Self,
                                                2,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
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

                              // Sequence name :UdyrKillChampion

                              (GetUnitPosition(
                                    out SelfPosition,
                                    Self) &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget,
                                    Target,
                                    Self) &&
                              CountUnitsInTargetArea(
                                    out UnitsNearby,
                                    Self,
                                    SelfPosition,
                                    300,
                                    AffectEnemies | AffectHeroes,
                                    "") &&
                              // Sequence name :UdyrKillChampion
                              (
                                    // Sequence name :Exhaust
                                    (
                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.5f) &&
                                       summonerExhaust.SummonerExhaust(
                                                Self,
                                                Target,
                                                ExhaustSlot)
                                    ) ||
                                    // Sequence name :CastRAoE
                                    (
                                          GreaterEqualInt(
                                                UnitsNearby,
                                                2) &&
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
                                    // Sequence name :CastE
                                    (
                                          UnitsNearby == 0 &&
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
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :CastQ
                                    (
                                          LessFloat(
                                                DistanceToTarget,
                                                300) &&
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
                                    // Sequence name :CastR
                                    (
                                          LessFloat(
                                                DistanceToTarget,
                                                300) &&
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
                                           // Sequence name :AutoAttackOnlyOnKill

                                           autoAttack.AutoAttack(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                Target,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)


                              ))


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

