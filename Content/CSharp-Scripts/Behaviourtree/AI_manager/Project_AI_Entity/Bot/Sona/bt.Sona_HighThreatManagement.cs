using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Sona_HighThreatManagementClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private SummonerGhostClass summonerGhost = new();

    public bool Sona_HighThreatManagement(
        out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out float __CastSpellTimeThreshold,
     out float __PreviousSpellCastTime,
     AttackableUnit Self,
     int PreviousSpellCast,
     AttackableUnit PreviousSpellCastTarget,
     Vector3 SelfPosition,
     float CastSpellTimeThreshold,
     float PreviousSpellCastTime,
     int ExhaustSlot,
     int GhostSlot
        )
    {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
                  // Sequence name :HighThreatManagement

                  // Sequence name :CastR
                  (
                        canCastChampionAbilityClass.CanCastChampionAbility(
                              Self,
                              3,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold,
                              PreviousSpellCastTarget,
                              NearestEnemyToAttack,
                              PreviousSpellCast,
                              false,
                              false) &&
                        GetUnitAIClosestTargetInArea(
                              out NearestEnemyToAttack,
                              Self, default
                              ,
                              true,
                              SelfPosition,
                              600,
                              AffectEnemies | AffectHeroes) &&
                        GetUnitHealthRatio(
                              out HealthRatio,
                              Self) &&
                        LessFloat(
                              HealthRatio,
                              0.3f) &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              NearestEnemyToAttack) &&
                        PredictLineMissileCastPosition(
                              out SkillShotCastPosition,
                              NearestEnemyToAttack,
                              SelfPosition,
                              1800,
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
                              3,
                              1,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold,
                              SkillShotCastPosition,
                              true)
                  ) ||
                  // Sequence name :PowerChordDebuff
                  (
                        TestUnitHasBuff(
                              Self, default
                              ,
                              "SonaAriaOfPerseveranceCheck",
                              true) &&
                        GetUnitAIClosestTargetInArea(
                              out NearestEnemyToAttack,
                              Self, default
                              ,
                              true,
                              SelfPosition,
                              600,
                              AffectEnemies | AffectHeroes) &&
                        SetUnitAIAttackTarget(
                              NearestEnemyToAttack) &&
                        IssueChaseOrder()
                  ) ||
                  // Sequence name :E
                  (
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
                  // Sequence name :W
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
                              CastSpellTimeThreshold, default
                              ,
                              false)
                  ) ||
                  // Sequence name :UseGhost
                  (
                        GetUnitHealthRatio(
                              out HealthRatio,
                              Self) &&
                        LessFloat(
                              HealthRatio,
                              0.35f) &&
                      summonerGhost.SummonerGhost(
                              Self,
                              GhostSlot)

                  )
            ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;


    }
}

