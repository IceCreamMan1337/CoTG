using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Jax_HighThreatManagementClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private SummonerGhostClass summonerGhost = new();

    public bool Jax_HighThreatManagement(
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
                  // Sequence name :RetreatHighThreat

                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self, default
                        ,
                        true,
                        SelfPosition,
                        600,
                        AffectEnemies | AffectHeroes) &&
                  // Sequence name :ManageHighThreat
                  (
                                    // Sequence name :UseE

                                    // Sequence name :Use E

                                    // Sequence name :MultipleEnemies
                                    (
                                          LessFloat(
                                                HealthRatio,
                                                0.75f) &&
                                          CountUnitsInTargetArea(
                                                out UnitsNearby,
                                                Self,
                                                SelfPosition,
                                                550,
                                                AffectEnemies | AffectHeroes,
                                                "") &&
                                          GreaterEqualInt(
                                                UnitsNearby,
                                                2) &&
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
                                    // Sequence name :LowHealth
                                    (
                                          LessFloat(
                                                HealthRatio,
                                                0.3f) &&
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
                                    )

                         ||
                        // Sequence name :UseQ
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.3f) &&
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    800,
                                    AffectEnemies | AffectHeroes) &&
                              GetUnitAIClosestTargetInArea(
                                    out ClosestEnemyMinion,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    800,
                                    AffectEnemies | AffectFriends | AffectMinions) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToClosestEnemyHero,
                                    Self,
                                    CurrentClosestTarget) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToClosestEnemyMinion,
                                    Self,
                                    ClosestEnemyMinion) &&
                              GetDistanceBetweenUnits(
                                    out DistanceFromMinionToEnemyHero,
                                    ClosestEnemyMinion,
                                    CurrentClosestTarget) &&
                              GreaterFloat(
                                    DistanceFromMinionToEnemyHero,
                                    DistanceToClosestEnemyMinion) &&
                              SubtractFloat(
                                    out DistanceDifference,
                                    DistanceToClosestEnemyMinion,
                                    DistanceToClosestEnemyHero) &&
                              GreaterEqualFloat(
                                    DistanceDifference,
                                    200) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    ClosestEnemyMinion,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    ClosestEnemyMinion,
                                    2,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                        // Sequence name :UseGhost
                        (
                              GetUnitHealthRatio(
                                    out HealthRatio,
                                    Self) &&
                              LessFloat(
                                    HealthRatio,
                                    0.2f) &&
                         summonerGhost.SummonerGhost(
                                    Self,
                                    GhostSlot)

                        )
                  )
            ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;


    }
}

