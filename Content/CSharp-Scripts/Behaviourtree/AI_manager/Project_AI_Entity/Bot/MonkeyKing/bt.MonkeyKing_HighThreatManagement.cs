using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class MonkeyKing_HighThreatManagementClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private SummonerGhostClass summonerGhost = new();

    public bool MonkeyKing_HighThreatManagement(
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
                        800,
                        AffectEnemies | AffectHeroes) &&
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :UseW
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.7f) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
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
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                              // Sequence name :CastQ

                              // Sequence name :Attack
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "MonkeyKingDoubleAttack",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          NearestEnemyToAttack) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.3f) &&
                                    SetUnitAIAttackTarget(
                                          NearestEnemyToAttack) &&
                                    IssueChaseOrder()
                              ) ||
                              // Sequence name :Cast
                              (
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          NearestEnemyToAttack) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.3f) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          true) &&
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
                              )
                         ||
                        // Sequence name :UseE
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.4f) &&
                              GetUnitAIClosestTargetInArea(
                                    out ClosestEnemyMinion,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    800,
                                    AffectEnemies | AffectMinions) &&
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

